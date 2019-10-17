﻿Option Strict On
'*****************************************************************************
'* Data Subscriber
'*
'* Archie Jacobs
'* Manufacturing Automation, LLC
'* 03-MAR-13
'* http://www.advancedhmi.com
'*
'* This component is used to simplify the creation of subscriptions
'*
'* 03-MAR-13 Created
'*****************************************************************************
Imports System.ComponentModel

<DefaultEvent("DataChanged")>
Public Class ChartWithLogging
    Inherits MfgControl.AdvancedHMI.Controls.ChartPanZoomLog
    Implements ISupportInitialize

    Public Event DataReturned As EventHandler(Of MfgControl.AdvancedHMI.Drivers.Common.PlcComEventArgs)
    Public Event DataChanged As EventHandler(Of MfgControl.AdvancedHMI.Drivers.Common.PlcComEventArgs)
    Public Event ComError As EventHandler(Of MfgControl.AdvancedHMI.Drivers.Common.PlcComEventArgs)
    Public Event SuccessfulSubscription As EventHandler

    Private m_synchronizationContext As System.Threading.SynchronizationContext

#Region "Constructor/Destructor"
    Public Sub New(ByVal container As System.ComponentModel.IContainer)
        Me.New()

        'Required for Windows.Forms Class Composition Designer support
        If (container IsNot Nothing) Then
            container.Add(Me)
        End If
    End Sub

    Public Sub New()
        MyBase.New()

        m_synchronizationContext = System.Windows.Forms.WindowsFormsSynchronizationContext.Current
    End Sub

    '****************************************************************
    '* Component overrides dispose to clean up the component list.
    '****************************************************************
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If LogTimer IsNot Nothing Then
                    LogTimer.Enabled = False
                    LogTimer.Dispose()
                End If
                'If SubScriptions IsNot Nothing Then
                While SubScriptions.Count > 0
                        SubScriptions(0).UnsubscribeAll()
                        SubScriptions(0).Dispose()
                        SubScriptions.RemoveAt(0)
                    End While
                    'End If
                    RemoveHandler m_PLCAddressValueItems.CollectionChanged, AddressOf SubscribedItemsChanged
                End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub
#End Region

#Region "Basic Properties"
    Public Overrides Property Site() As ISite
        Get
            Return MyBase.Site
        End Get
        Set(value As ISite)
            MyBase.Site = value

            'If (value IsNot Nothing) And m_ComComponent Is Nothing Then
            '    If MyBase.Site.DesignMode Then
            '        If m_ComComponent Is Nothing Then
            '            '********************************************************
            '            '* Search for AdvancedHMIDrivers.IComComponent component in parent form
            '            '* If one exists, set the client of this component to it
            '            '********************************************************
            '            m_ComComponent = AdvancedHMIDrivers.Utilities.GetComComponent(Me.Site.Container)
            '        End If
            '    End If
            'End If
        End Set
    End Property

    Private LogTimer As Timer
    Private m_LogInterval As Integer = 1000
    Public Property LogInterval As Integer
        Get
            Return m_LogInterval
        End Get
        Set(value As Integer)
            m_LogInterval = value
        End Set
    End Property

    <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>
    <System.ComponentModel.Browsable(False)>
    Public Shadows ReadOnly Property DataPoints As System.Collections.ObjectModel.Collection(Of MfgControl.AdvancedHMI.Controls.DataPoint)
        Get
            Return MyBase.DataPoints
        End Get
    End Property


    Private m_EnableLogging As Boolean = True
    Public Property EnableLogging As Boolean
        Get
            Return m_EnableLogging
        End Get
        Set(value As Boolean)
            m_EnableLogging = value
        End Set
    End Property

    Private m_PLCAddressEnableLogging As String
    Public Property PLCAddressEnableLogging As String
        Get
            Return m_PLCAddressEnableLogging
        End Get
        Set(value As String)
            m_PLCAddressEnableLogging = value
        End Set
    End Property
#End Region

#Region "PLC Related Properties"
    ''*****************************************************
    ''* Property - Component to communicate to PLC through
    ''*****************************************************
    'Private m_ComComponent As MfgControl.AdvancedHMI.Drivers.IComComponent
    '<System.ComponentModel.Category("PLC Properties")>
    'Public Property ComComponent() As MfgControl.AdvancedHMI.Drivers.IComComponent
    '    Get
    '        Return m_ComComponent
    '    End Get
    '    Set(ByVal value As MfgControl.AdvancedHMI.Drivers.IComComponent)
    '        If m_ComComponent IsNot value Then
    '            If SubScriptions IsNot Nothing Then
    '                SubScriptions.UnsubscribeAll()
    '                SubScriptions.ComComponent = m_ComComponent
    '            End If

    '            m_ComComponent = value

    '            SubscribeToComDriver()
    '        End If
    '    End Set
    'End Property


    '*****************************************
    '* Property - Address in PLC to Link to
    '*****************************************
    Private m_PLCAddressValueItems As New System.Collections.ObjectModel.ObservableCollection(Of MfgControl.AdvancedHMI.Drivers.PLCAddressItemEx)
    <System.ComponentModel.BrowsableAttribute(True), System.ComponentModel.EditorAttribute(GetType(PLCAddressItemEditor), GetType(System.Drawing.Design.UITypeEditor))>
    <System.ComponentModel.Category("PLC Properties")>
    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content)>
    Public ReadOnly Property PLCAddressValueItems() As System.Collections.ObjectModel.ObservableCollection(Of MfgControl.AdvancedHMI.Drivers.PLCAddressItemEx)
        Get
            Return m_PLCAddressValueItems
        End Get
    End Property
#End Region

#Region "Events"
    Private Initializing As Boolean
    Public Sub BeginInit() Implements ISupportInitialize.BeginInit
        Initializing = True
    End Sub

    Public Sub EndInit() Implements ISupportInitialize.EndInit
        Initializing = False

        'If m_ComComponent IsNot Nothing Then
        SubscribeToComDriver()
        'End If

        AddHandler m_PLCAddressValueItems.CollectionChanged, AddressOf SubscribedItemsChanged
    End Sub


    Protected Overridable Sub OnDataReturned(ByVal e As MfgControl.AdvancedHMI.Drivers.Common.PlcComEventArgs)
        If m_synchronizationContext IsNot Nothing Then
            m_synchronizationContext.Post(AddressOf DataReturnedSync, e)
        Else
            RaiseEvent DataReturned(Me, e)
        End If
    End Sub

    '****************************************************************************
    '* This is required to sync the event back to the parent form's main thread
    '****************************************************************************
    'Dim drsd As EventHandler(Of MfgControl.AdvancedHMI.Drivers.Common.PlcComEventArgs) = AddressOf DataReturnedSync
    Private Sub DataReturnedSync(ByVal e As Object)
        RaiseEvent DataReturned(Me, DirectCast(e, MfgControl.AdvancedHMI.Drivers.Common.PlcComEventArgs))
    End Sub


    Protected Overridable Sub OnDataChanged(ByVal e As MfgControl.AdvancedHMI.Drivers.Common.PlcComEventArgs)
        If m_synchronizationContext IsNot Nothing Then
            m_synchronizationContext.Post(AddressOf DataChangedSync, e)
        Else
            RaiseEvent DataChanged(Me, e)
        End If
    End Sub

    '****************************************************************************
    '* This is required to sync the event back to the parent form's main thread
    '****************************************************************************
    Private Sub DataChangedSync(ByVal e As Object)
        RaiseEvent DataChanged(Me, DirectCast(e, MfgControl.AdvancedHMI.Drivers.Common.PlcComEventArgs))
    End Sub


    Protected Overridable Sub OnSuccessfulSubscription(ByVal e As MfgControl.AdvancedHMI.Drivers.Common.PlcComEventArgs)
        RaiseEvent SuccessfulSubscription(Me, e)

        For i = 0 To PLCAddressValueItems.Count
            If i >= m_ChartSeriesNames.Count Then
                m_ChartSeriesNames.Add("")
            End If
        Next

        For i = 0 To PLCAddressValueItems.Count - 1
            If Not String.IsNullOrEmpty(m_PLCAddressValueItems(i).Name) Then
                'If ChartSeriesNames.Count < i Then
                'If ChartSeriesNames.Count > i Then
                m_ChartSeriesNames(i) = m_PLCAddressValueItems(i).Name
                'Else
                '    ChartSeriesNames.Add(m_PLCAddressValueItems(i).Name & "x")
                'End If
                'End If
            End If
        Next

        '* create the timer to log the data
        If LogTimer Is Nothing Then
            LogTimer = New Timer
            If m_LogInterval > 0 Then
                LogTimer.Interval = m_LogInterval
            Else
                LogTimer.Interval = 1000
            End If
            AddHandler LogTimer.Tick, AddressOf LogInterval_Tick

            LogTimer.Enabled = True
        End If
    End Sub

    Protected Overridable Sub OnComError(ByVal e As MfgControl.AdvancedHMI.Drivers.Common.PlcComEventArgs)
        RaiseEvent ComError(Me, e)
    End Sub

    Protected Overridable Sub SubscribedItemsChanged(ByVal sender As Object, ByVal e As System.Collections.Specialized.NotifyCollectionChangedEventArgs)
        '* Ver 3.99b - Added Not Initializing
        If Not Me.DesignMode And Not Initializing Then
            For index = 0 To SubScriptions.Count - 1
                SubScriptions(index).UnsubscribeAll()
            Next
            SubscribeToComDriver()

        End If
    End Sub
#End Region

#Region "Subscribing and PLC data receiving"

    Private SubScriptions As New List(Of SubscriptionHandler)
    '**************************************************
    '* Subscribe to addresses in the Comm(PLC) Driver
    '**************************************************
    Protected Sub SubscribeToComDriver()
        If Not DesignMode Then
            Dim SubscriptionIndex As Integer
            '* Create a subscription handler object
            'If SubScriptions Is Nothing Then
            '    SubScriptions = New SubscriptionHandler
            '    SubScriptions.Parent = Me
            '    AddHandler SubScriptions.DisplayError, AddressOf DisplaySubscribeError
            'End If
            'SubScriptions.ComComponent = m_ComComponent

            Dim FileHeader As String = ""

            For index2 = 0 To m_PLCAddressValueItems.Count - 1
                If Not String.IsNullOrEmpty(m_PLCAddressValueItems(index2).PLCAddress) AndAlso (m_PLCAddressValueItems(index2).ComComponent IsNot Nothing) Then
                    Dim sh As SubscriptionHandler
                    SubscriptionIndex = 0
                    While (SubscriptionIndex < SubScriptions.Count) AndAlso (m_PLCAddressValueItems(index2).ComComponent IsNot SubScriptions(SubscriptionIndex).ComComponent)
                        SubscriptionIndex += 1
                    End While

                    '* Do we need to create an new Subscriptionhandler?
                    If SubscriptionIndex >= SubScriptions.Count Then
                        sh = New SubscriptionHandler
                        sh.Parent = Me
                        sh.ComComponent = m_PLCAddressValueItems(index2).ComComponent
                        AddHandler sh.DisplayError, AddressOf DisplaySubscribeError
                        SubScriptions.Add(sh)
                    Else
                        sh = SubScriptions(SubscriptionIndex)
                    End If

                    '* We must pass the address as a property name so the subscriptionHandler doesn't confuse the next address as a change for the same property
                    sh.SubscribeTo(m_PLCAddressValueItems(index2).PLCAddress, m_PLCAddressValueItems(index2).NumberOfElements, AddressOf PolledDataReturned, m_PLCAddressValueItems(index2).PLCAddress, 1, 0)

                    Dim x As New MfgControl.AdvancedHMI.Drivers.Common.PlcComEventArgs(0, "")
                    x.PlcAddress = m_PLCAddressValueItems(index2).PLCAddress
                    OnSuccessfulSubscription(x)

                    '* Set the file header to the list of PLC addresses
                    If String.IsNullOrEmpty(FileHeader) Then
                        FileHeader &= "TimeStamp,"
                    End If
                    FileHeader &= m_PLCAddressValueItems(index2).PLCAddress

                ElseIf Not String.IsNullOrEmpty(m_PLCAddressValueItems(index2).PLCAddress) AndAlso (m_PLCAddressValueItems(index2).ComComponent Is Nothing) Then
                    Text = "ComComponent not set for " & m_PLCAddressValueItems(index2).PLCAddress
                End If
            Next

            MyBase.FileHeader = FileHeader
            'SubScriptions.SubscribeAutoProperties()
        End If
    End Sub

    '***************************************
    '* Call backs for returned data
    '***************************************
    Private Sub PolledDataReturned(ByVal sender As Object, ByVal e As SubscriptionHandlerEventArgs)
        If e.PLCComEventArgs.ErrorId = 0 Then
            Try
                If String.IsNullOrEmpty(e.SubscriptionDetail.PropertyNameToSet) Or String.Compare(e.SubscriptionDetail.PropertyNameToSet, e.PLCComEventArgs.PlcAddress, True) = 0 Then
                    PolledDataReturnedValue(sender, e.PLCComEventArgs)
                ElseIf e.SubscriptionDetail.PropertyNameToSet = "Value" Then
                    PolledDataReturnedValue(sender, e.PLCComEventArgs)
                Else
                    Try
                        '* Write the value to the property that came from the end of the PLCAddress... property name
                        Me.GetType().GetProperty(e.SubscriptionDetail.PropertyNameToSet).
                                    SetValue(Me, Utilities.DynamicConverter(e.PLCComEventArgs.Values(0),
                                                Me.GetType().GetProperty(e.SubscriptionDetail.PropertyNameToSet).PropertyType), Nothing)
                    Catch ex As Exception
                        'OnDisplayError("INVALID VALUE RETURNED!" & a.PLCComEventArgs.Values(0))
                    End Try
                End If
            Catch ex As Exception
                DisplayError("INVALID VALUE!" & ex.Message)
            End Try
        Else
            DisplayError("Com Error " & e.PLCComEventArgs.ErrorId & "." & e.PLCComEventArgs.ErrorMessage)
        End If
    End Sub


    '***************************************
    '* Call backs for returned data
    '***************************************
    Private Sub PolledDataReturnedValue(ByVal sender As Object, ByVal e As MfgControl.AdvancedHMI.Drivers.Common.PlcComEventArgs)
        Try
            '* Fire this event every time data is returned
            OnDataReturned(e)

            For index = 0 To m_PLCAddressValueItems.Count - 1
                If (String.Compare(e.PlcAddress, m_PLCAddressValueItems(index).PLCAddress, True) = 0) And (sender Is m_PLCAddressValueItems(index).ComComponent) Then
                    Dim i As Integer
                    Dim tempString As String = ""
                    Dim tempValue As String = ""
                    While (i < e.Values.Count)
                        Try
                            tempValue = m_PLCAddressValueItems(index).GetScaledValue(e.Values(i))
                        Catch ex As Exception
                            tempValue = "," & "INVALID - Check scale factor/offset - " & e.Values(i)
                        End Try

                        If i > 0 Then
                            tempString &= "," & tempValue
                        Else
                            tempString = tempValue
                        End If
                        i += 1
                    End While

                    If m_PLCAddressValueItems(index).LastValue <> tempString Then
                        m_PLCAddressValueItems(index).LastValue = tempString

                        '* This event is only fired when the returned data has changed
                        OnDataChanged(e)
                    End If
                End If
            Next

        Catch
            DisplayError("INVALID VALUE RETURNED!")
        End Try
    End Sub

    Private Sub DisplaySubscribeError(ByVal sender As Object, ByVal e As MfgControl.AdvancedHMI.Drivers.Common.PlcComEventArgs)
        DisplayError(e.ErrorMessage)

        Dim sh As SubscriptionHandler = DirectCast(sender, SubscriptionHandler)
        '* Put something in the PLCAddressValueItem so the chart can start on com error
        For index = 0 To m_PLCAddressValueItems.Count - 1
            If (String.Compare(e.PlcAddress, m_PLCAddressValueItems(index).PLCAddress, True) = 0) And (sh.ComComponent Is m_PLCAddressValueItems(index).ComComponent) Then
                If String.IsNullOrEmpty(m_PLCAddressValueItems(index).LastValue) Then
                    m_PLCAddressValueItems(index).LastValue = "0"
                End If
            End If
        Next
    End Sub
#End Region

#Region "Error Display"
    '********************************************************
    '* Show an error via the text property for a short time
    '********************************************************
    Private WithEvents ErrorDisplayTime As System.Windows.Forms.Timer
    Private Sub DisplayError(ByVal ErrorMessage As String)
        If ErrorDisplayTime Is Nothing Then
            ErrorDisplayTime = New System.Windows.Forms.Timer
            AddHandler ErrorDisplayTime.Tick, AddressOf ErrorDisplay_Tick
            ErrorDisplayTime.Interval = 5000
        End If

        '* Save the text to return to
        If Not ErrorDisplayTime.Enabled Then
            Text = ErrorMessage
        End If

        OnComError(New MfgControl.AdvancedHMI.Drivers.Common.PlcComEventArgs(1, ErrorMessage))

        ErrorDisplayTime.Enabled = True
    End Sub


    '**************************************************************************************
    '* Return the text back to its original after displaying the error for a few seconds.
    '**************************************************************************************
    Private Sub ErrorDisplay_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If ErrorDisplayTime IsNot Nothing Then
            ErrorDisplayTime.Enabled = False
            ErrorDisplayTime.Dispose()
            ErrorDisplayTime = Nothing
        End If

        Text = ""
    End Sub
#End Region

#Region "Private Methods"
    '* Timer tick interval used to store data at a periodic rate
    Private Sub LogInterval_Tick(sender As System.Object, e As System.EventArgs)
        StoreValue()
    End Sub

    Public Sub StoreValue()
        Try
            If m_EnableLogging Then
                Dim PointNotYetUpdated As Boolean
                Dim DataPointText As String = Now().ToString & ","
                For index = 0 To PLCAddressValueItems.Count - 1
                    DataPointText &= PLCAddressValueItems(index).LastValue
                    If String.IsNullOrEmpty(PLCAddressValueItems(index).LastValue) Then
                        PointNotYetUpdated = True
                    End If
                    If index < PLCAddressValueItems.Count - 1 Then
                        DataPointText &= ","
                    End If
                Next

                If Not PointNotYetUpdated Then
                    AddDataPoint(New MfgControl.AdvancedHMI.Controls.DataPoint(DataPointText))
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
#End Region

#Region "Public Methods"
    Public Function GetValueByName(ByVal name As String) As String
        Dim index As Integer
        While index < m_PLCAddressValueItems.Count
            If String.Compare(m_PLCAddressValueItems(index).Name, name, True) = 0 Then
                Return m_PLCAddressValueItems(index).LastValue
            End If
            index += 1
        End While

        Return ""
    End Function
#End Region

End Class
