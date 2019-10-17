Option Strict On
'***********************************************************************
'* Omron Host Link (RS232) Com
'*
'* Copyright 2012 Archie Jacobs
'*
'* Reference : Omron W342-E1-15 (W342-E1-15+CS-CJ-CP-NSJ+RefManual.pdf)
'* Revision February 2010
'*
'* 29-DEC-12 Created based on FINSHostLinkCom
'***********************************************************************
'Imports OmronDriver.Common
Namespace Omron
    Public Class OmronSerialHostLinkCom
        Inherits Omron.HostLinkBaseCom
        Implements System.ComponentModel.IComponent
        Implements System.ComponentModel.ISupportInitialize
        Implements System.Windows.Forms.IBindableComponent
        Implements MfgControl.AdvancedHMI.Drivers.IComComponent

        Public Event SendProgress As EventHandler
        Public Event ReceiveProgress As EventHandler
        Public Event Disposed As EventHandler Implements System.ComponentModel.IComponent.Disposed

        Private Shared DLL As List(Of MfgControl.AdvancedHMI.Drivers.Omron.HostLinkDataLinkLayer)
        Protected Shared InstanceCount As Integer

        Protected m_synchronizationContext As System.Threading.SynchronizationContext

#Region "Constructor"
        Public Sub New()
            MyBase.New()

            If DLL Is Nothing Then
                DLL = New List(Of MfgControl.AdvancedHMI.Drivers.Omron.HostLinkDataLinkLayer)
            End If

            m_synchronizationContext = System.Windows.Forms.WindowsFormsSynchronizationContext.Current


            TargetAddress = New MfgControl.AdvancedHMI.Drivers.Omron.DeviceAddress
            '* default port 1 (&HFC)
            SourceAddress = New MfgControl.AdvancedHMI.Drivers.Omron.DeviceAddress(0, 0, &HFC)

            InstanceCount += 1
        End Sub

        Public Sub New(ByVal container As System.ComponentModel.IContainer)
            Me.New()

            'Required for Windows.Forms Class Composition Designer support
            container.Add(Me)
        End Sub


        Private IsDisposed As Boolean '* Without this, it can dispose the DLL completely
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If Not IsDisposed Then
                MyBase.Dispose(disposing)

                '* The handle linked to the DataLink Layer has to be removed, otherwise it causes a problem when a form is closed
                If DLL.Count > 0 AndAlso DLL(MyDLLInstance) IsNot Nothing Then
                    RemoveHandler DLL(MyDLLInstance).DataReceived, AddressOf DataLinkLayerDataReceived
                    RemoveHandler DLL(MyDLLInstance).ComError, AddressOf DataLinkLayerComError

                    InstanceCount -= 1

                    '* 14-DEC-11 - Added the Remove from collection to fix problem where new DLL was not created
                    '* if it the port were previously closed
                    If InstanceCount <= 0 Then
                        DLL(MyDLLInstance).dispose(True)
                        DLL(MyDLLInstance) = Nothing
                        'DLL.Remove(DLL(MyDLLInstance))
                    End If
                End If
            End If

            IsDisposed = True
        End Sub
#End Region


#Region "Properties"
        Private m_PortName As String = "COM1"
        <System.ComponentModel.Category("Communication Settings")> _
        Public Property PortName() As String
            Get
                Return m_PortName
            End Get
            Set(ByVal value As String)
                m_PortName = value

                CreateDLLInstance()

                If DLL.Count > 0 AndAlso DLL(MyDLLInstance) IsNot Nothing Then
                    DLL(MyDLLInstance).PortName = value
                End If
            End Set
        End Property

        Private m_BaudRate As Integer = 115200
        <System.ComponentModel.Category("Communication Settings")> _
        Public Property BaudRate() As Integer
            Get
                Return m_BaudRate
            End Get
            Set(ByVal value As Integer)
                m_BaudRate = value

                CreateDLLInstance()

                If DLL.Count > 0 AndAlso DLL(MyDLLInstance) IsNot Nothing Then
                    DLL(MyDLLInstance).BaudRate = value
                End If
            End Set
        End Property

        Private m_Parity As System.IO.Ports.Parity = IO.Ports.Parity.Even
        <System.ComponentModel.Category("Communication Settings")> _
        Public Property Parity() As System.IO.Ports.Parity
            Get
                Return m_Parity
            End Get
            Set(ByVal value As System.IO.Ports.Parity)
                m_Parity = value
                CreateDLLInstance()

                If DLL.Count > 0 AndAlso DLL(MyDLLInstance) IsNot Nothing Then
                    DLL(MyDLLInstance).Parity = value
                End If
            End Set
        End Property

        Private m_DataBits As Integer = 7
        <System.ComponentModel.Category("Communication Settings")> _
        Public Property DataBits() As Integer
            Get
                Return m_DataBits
            End Get
            Set(ByVal value As Integer)
                m_DataBits = value
                CreateDLLInstance()

                If DLL.Count > 0 AndAlso DLL(MyDLLInstance) IsNot Nothing Then
                    DLL(MyDLLInstance).DataBits = value
                End If
            End Set
        End Property

        Private m_StopBits As IO.Ports.StopBits = IO.Ports.StopBits.Two
        <System.ComponentModel.Category("Communication Settings")> _
        Public Property StopBits() As IO.Ports.StopBits
            Get
                Return m_StopBits
            End Get
            Set(ByVal value As IO.Ports.StopBits)
                m_StopBits = value

                CreateDLLInstance()

                If DLL.Count > 0 AndAlso DLL(MyDLLInstance) IsNot Nothing Then
                    DLL(MyDLLInstance).StopBits = value
                End If
            End Set
        End Property


        <System.ComponentModel.Category("Communication Settings")>
        Public Property TargetUnitAddress() As Byte
            Get
                Return TargetAddress.UnitAddress
            End Get
            Set(ByVal value As Byte)
                TargetAddress.UnitAddress = value

                If DLL.Count > 0 AndAlso DLL(MyDLLInstance) IsNot Nothing Then
                    DLL(MyDLLInstance).UnitNumber = value
                End If
            End Set
        End Property


        Private m_EnableLogging As Boolean
        Public Property EnableLogging As Boolean
            Get
                Return m_EnableLogging
            End Get
            Set(value As Boolean)
                m_EnableLogging = value

                If DLL.Count > 0 AndAlso DLL(MyDLLInstance) IsNot Nothing Then
                    DLL(MyDLLInstance).EnableLogging = value
                End If
            End Set
        End Property

        Private m_site As System.ComponentModel.ISite
        <System.ComponentModel.Browsable(False), System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
        Public Overridable Property Site() As System.ComponentModel.ISite Implements System.ComponentModel.IComponent.Site
            Get
                Return m_site
            End Get
            Set(value As System.ComponentModel.ISite)
                m_site = value
            End Set
        End Property

        <System.ComponentModel.Browsable(False), System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
        Protected ReadOnly Property DesignMode() As Boolean
            Get
                Dim s As System.ComponentModel.ISite = m_site
                If s Is Nothing Then
                    Return False
                Else
                    Return s.DesignMode
                End If
                ' Return If((s Is Nothing), False, s.DesignMode)
            End Get
        End Property


        Private m_IniFileName As String = ""
        Public Property IniFileName As String
            Get
                Return m_IniFileName
            End Get
            Set(value As String)
                m_IniFileName = value
            End Set
        End Property

        Private m_IniFileSection As String
        Public Property IniFileSection As String
            Get
                Return m_IniFileSection
            End Get
            Set(value As String)
                m_IniFileSection = value
            End Set
        End Property
#End Region


#Region "Binding Properties"
        Private m_bindingContext As System.Windows.Forms.BindingContext
        Private m_dataBindings As System.Windows.Forms.ControlBindingsCollection
        <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content)>
        Public ReadOnly Property DataBindings As System.Windows.Forms.ControlBindingsCollection Implements System.Windows.Forms.IBindableComponent.DataBindings
            Get
                If (m_dataBindings Is Nothing) Then
                    m_dataBindings = New System.Windows.Forms.ControlBindingsCollection(Me)
                End If
                Return m_dataBindings
            End Get
        End Property

        <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content)>
        Public Property BindingContext As System.Windows.Forms.BindingContext Implements System.Windows.Forms.IBindableComponent.BindingContext
            Get
                If m_bindingContext Is Nothing Then
                    m_bindingContext = New System.Windows.Forms.BindingContext()
                End If
                Return m_bindingContext
            End Get
            Set(value As System.Windows.Forms.BindingContext)
                m_bindingContext = value
            End Set
        End Property
#End Region

#Region "Private Methods"
        '***************************************************************
        '* Create the Data Link Layer Instances
        '* if the IP Address is the same, then resuse a common instance
        '***************************************************************
        Protected Overrides Sub CreateDLLInstance()
            'If Me.DesignMode Then Exit Sub
            '*** For Windows CE port, this checks designmode and works in full .NET also***
            If AppDomain.CurrentDomain.FriendlyName.IndexOf("DefaultDomain", System.StringComparison.CurrentCultureIgnoreCase) >= 0 Then
                Exit Sub
            End If

            If DLL.Count > 0 Then
                '* At least one DLL instance already exists,
                '* so check to see if it has the same IP address
                '* if so, reuse the instance, otherwise create a new one
                Dim i As Integer
                While i < DLL.Count AndAlso ((DLL(i) Is Nothing) Or (DLL(i) IsNot Nothing AndAlso DLL(i).PortName <> m_PortName))
                    i += 1
                End While
                MyDLLInstance = i
            End If

            If MyDLLInstance >= DLL.Count Then
                '* See if there are any unused items in collection
                Dim i As Integer
                While i < DLL.Count AndAlso DLL(i) IsNot Nothing
                    i += 1
                End While
                MyDLLInstance = i

                Dim NewDLL As New MfgControl.AdvancedHMI.Drivers.Omron.HostLinkDataLinkLayer(m_PortName)
                NewDLL.BaudRate = m_BaudRate
                NewDLL.DataBits = m_DataBits
                NewDLL.Parity = m_Parity
                NewDLL.StopBits = m_StopBits
                NewDLL.EnableLogging = m_EnableLogging
                If i >= DLL.Count Then
                    DLL.Add(NewDLL)
                Else
                    DLL(i) = NewDLL
                End If
            End If

            '* Have we already attached event handler to this data link layer?
            If EventHandlerDLLInstance <> (MyDLLInstance + 1) Then
                '* If event handler to another layer has been created, remove them
                If EventHandlerDLLInstance > 0 Then
                    RemoveHandler DLL(EventHandlerDLLInstance).DataReceived, AddressOf DataLinkLayerDataReceived
                    RemoveHandler DLL(EventHandlerDLLInstance).ComError, AddressOf DataLinkLayerComError
                    RemoveHandler DLL(EventHandlerDLLInstance).PartialPacketReceived, AddressOf DataLinkLayerPartialPacketReceived
                    RemoveHandler DLL(EventHandlerDLLInstance).PartialPacketSent, AddressOf DataLinkLayerPartialPacketSent
                End If

                AddHandler DLL(MyDLLInstance).DataReceived, AddressOf DataLinkLayerDataReceived
                AddHandler DLL(MyDLLInstance).ComError, AddressOf DataLinkLayerComError
                AddHandler DLL(EventHandlerDLLInstance).PartialPacketReceived, AddressOf DataLinkLayerPartialPacketReceived
                AddHandler DLL(EventHandlerDLLInstance).PartialPacketSent, AddressOf DataLinkLayerPartialPacketSent
                EventHandlerDLLInstance = MyDLLInstance + 1
            End If
        End Sub


        Friend Overrides Function SendData(ByVal HostLinkF As MfgControl.AdvancedHMI.Drivers.Omron.HostLinkFrame) As Boolean
            If IsDisposed Then
                Throw New Exception("HostLinkCom. Object is disposed")
            End If
            '* If a Subscription (Internal Request) begin to overflow the que, ignore some
            '* This can occur from too fast polling
            If DLL.Count <= 0 Then
                CreateDLLInstance()
            End If

            '****************************************************
            '* Do not send an internal request (subscription),
            '*  if the send que has 10 or more requests pending
            '****************************************************
            If (DLL(MyDLLInstance).SendQueDepth < 20) Then
                '* if reuqested by user code, do not let buffer exceed 30 deep
                If (DLL(MyDLLInstance).SendQueDepth < 30) Then
                    Try
                        DLL(MyDLLInstance).SendData(HostLinkF)
                    Catch ex As Exception
                        '* 15-MAR-12
                        Throw New MfgControl.AdvancedHMI.Drivers.Common.PLCDriverException("1,HostLink,SendData-" & ex.Message)
                    End Try
                    Return True
                Else
                    '* Buffer is full from client requests
                    Return False
                End If
            Else
                Return False
            End If
        End Function
#End Region

#Region "Events"
        Private Sub DataLinkLayerPartialPacketReceived(ByVal sender As Object, ByVal e As EventArgs)
            OnReceiveProgress(e)
        End Sub

        Private Sub DataLinkLayerPartialPacketSent(ByVal sender As Object, ByVal e As EventArgs)
            OnSendProgress(e)
        End Sub

        '***********************************************************************************************************
        '***********************************************************************************************************
        Protected Overrides Sub OnSubscriptionDataReceived(e As MfgControl.AdvancedHMI.Drivers.Common.SubscriptionEventArgs)
            If m_synchronizationContext IsNot Nothing Then
                Dim Parameters() As Object = {Me, e}
                'm_SynchronizingObject.BeginInvoke(e.dlgCallBack, Parameters)
                m_synchronizationContext.Post(AddressOf SubscriptionDataSync, e)
            Else
                MyBase.OnSubscriptionDataReceived(e)
            End If
        End Sub

        Private Sub SubscriptionDataSync(ByVal e As Object)
            Dim e1 As MfgControl.AdvancedHMI.Drivers.Common.SubscriptionEventArgs = DirectCast(e, MfgControl.AdvancedHMI.Drivers.Common.SubscriptionEventArgs)
            e1.dlgCallBack(Me, e1)
        End Sub




        Protected Overridable Sub OnReceiveProgress(ByVal e As EventArgs)
            If m_synchronizationContext Is Nothing Then
                ReceiveProgressAsync(e)
            Else
                m_synchronizationContext.Post(AddressOf ReceiveProgressAsync, New Object() {e})
            End If
        End Sub

        Private Sub ReceiveProgressAsync(ByVal e As Object)
            RaiseEvent ReceiveProgress(Me, DirectCast(e, EventArgs))
        End Sub

        Protected Overridable Sub OnSendProgress(ByVal e As EventArgs)
            If m_synchronizationContext Is Nothing Then
                RaiseEvent SendProgress(Me, e)
            Else
                m_synchronizationContext.Post(AddressOf SendProgressAsync, New Object() {e})
            End If
        End Sub

        ' Delegate Sub SendProgressAsyncDelegate(ByVal e As EventArgs)
        Private Sub SendProgressAsync(ByVal e As Object)
            RaiseEvent SendProgress(Me, DirectCast(e, EventArgs))
        End Sub


        Protected Overrides Sub OnDataReceived(ByVal e As MfgControl.AdvancedHMI.Drivers.Common.PlcComEventArgs)
            If m_synchronizationContext IsNot Nothing Then
                m_synchronizationContext.Post(AddressOf DataReceivedSync, e)
            Else
                MyBase.OnDataReceived(e)
            End If
        End Sub

        Private Sub DataReceivedSync(ByVal e As Object)
            Try
                Dim e1 As MfgControl.AdvancedHMI.Drivers.Common.PlcComEventArgs = DirectCast(e, MfgControl.AdvancedHMI.Drivers.Common.PlcComEventArgs)
                MyBase.OnDataReceived(e1)
            Catch ex As Exception
                Dim dbg = 0
            End Try
        End Sub

        Protected Overrides Sub OnComError(ByVal e As MfgControl.AdvancedHMI.Drivers.Common.PlcComEventArgs)
            If m_synchronizationContext IsNot Nothing Then
                m_synchronizationContext.Post(AddressOf ComErrorSync, e)
            Else
                MyBase.OnDataReceived(e)
            End If
        End Sub

        Private Sub ComErrorSync(ByVal e As Object)
            Try
                Dim e1 As MfgControl.AdvancedHMI.Drivers.Common.PlcComEventArgs = DirectCast(e, MfgControl.AdvancedHMI.Drivers.Common.PlcComEventArgs)
                MyBase.OnComError(e1)
            Catch ex As Exception
                Dim dbg = 0
            End Try
        End Sub

#End Region

#Region "ISupportInitialize"
        Private Initializing As Boolean
        Public Sub BeginInit() Implements System.ComponentModel.ISupportInitialize.BeginInit
            Initializing = True
        End Sub

        Public Sub EndInit() Implements System.ComponentModel.ISupportInitialize.EndInit
            If Not Me.DesignMode Then
                If Not String.IsNullOrEmpty(m_IniFileName) Then
                    Try
                        Utilities.SetPropertiesByIniFile(Me, m_IniFileName, m_IniFileSection)
                    Catch ex As Exception
                        System.Windows.Forms.MessageBox.Show("INI File - " & ex.Message)
                    End Try
                End If
            End If
            Initializing = False
        End Sub
#End Region

        Protected Overrides Function GetNextTransactionID(ByVal maxValue As Integer) As Integer
            If DLL.Count > MyDLLInstance AndAlso DLL(MyDLLInstance) IsNot Nothing Then
                Return DLL(MyDLLInstance).GetNextTransactionNumber(maxValue)
            Else
                Return 0
            End If
        End Function


        Public Sub closeComm()
            DLL(MyDLLInstance).CloseCom()
        End Sub
    End Class
End Namespace
