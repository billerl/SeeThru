Public Class MessageListByItem
    Inherits MfgControl.AdvancedHMI.Controls.MessageListByItem

#Region "Properties"
    'Public Property PLCAddressItems
#End Region


#Region "Constructor"
    Public Sub New()
        MyBase.New()

        ' m_PLCAddressItems = New List(Of MfgControl.AdvancedHMI.Drivers.PLCAddressItem)
    End Sub

    '********************************************************************
    '* When an instance is added to the form, set the comm component
    '* property. If a comm component does not exist, add one to the form
    '********************************************************************
    Protected Overrides Sub OnCreateControl()
        MyBase.OnCreateControl()

        If Me.DesignMode Then
            If m_ComComponent Is Nothing Then
                m_ComComponent = AdvancedHMIDrivers.Utilities.GetComComponent(Me.Site.Container)
            End If
        Else
            SubscribeToComDriver()
        End If

        MyBase.Items.Clear()
    End Sub

#End Region

#Region "PLC Related Properties"
    '*****************************************************
    '* Property - Component to communicate to PLC through
    '*****************************************************
    Private m_ComComponent As MfgControl.AdvancedHMI.Drivers.IComComponent
    <System.ComponentModel.Description("Driver Instance for data reading and writing")>
    <System.ComponentModel.Category("PLC Properties")>
    Public Property ComComponent() As MfgControl.AdvancedHMI.Drivers.IComComponent
        Get
            Return m_ComComponent
        End Get
        Set(ByVal value As MfgControl.AdvancedHMI.Drivers.IComComponent)
            If m_ComComponent IsNot value Then
                If SubScriptions IsNot Nothing Then
                    SubScriptions.UnsubscribeAll()
                End If

                m_ComComponent = value

                SubscribeToComDriver()
            End If
        End Set
    End Property

    'Private m_PLCAddressItems As List(Of MfgControl.AdvancedHMI.Drivers.PLCAddressItem)
    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content)>
    Public ReadOnly Property PLCAddressItems As System.Collections.ObjectModel.Collection(Of MfgControl.AdvancedHMI.Controls.MessageByItem) ' MfgControl.AdvancedHMI.Drivers.PLCAddressItem)
        Get
            Return MyBase.Messages
        End Get
    End Property


    Private m_SuppressErrorDisplay As Boolean
    <System.ComponentModel.DefaultValue(False)>
    Public Property SuppressErrorDisplay As Boolean
        Get
            Return m_SuppressErrorDisplay
        End Get
        Set(value As Boolean)
            m_SuppressErrorDisplay = value
        End Set
    End Property
#End Region


#Region "Subscribing and PLC data receiving"
    Private SubScriptions As SubscriptionHandler
    '*******************************************************************************
    '* Subscribe to addresses in the Comm(PLC) Driver
    '* This code will look at properties to find the "PLCAddress" + property name
    '*
    '*******************************************************************************
    Private SubscriptionsCreated As Boolean
    Private Sub SubscribeToComDriver()
        If Not DesignMode And IsHandleCreated Then
            If Not SubscriptionsCreated Then
                '* Create a subscription handler object
                If SubScriptions Is Nothing Then
                    SubScriptions = New SubscriptionHandler
                    SubScriptions.Parent = Me
                    AddHandler SubScriptions.DisplayError, AddressOf DisplaySubscribeError
                End If
                SubScriptions.ComComponent = m_ComComponent

                Dim index As Integer
                While index < MyBase.Messages.Count ' PLCAddressItems.Count
                    If Not String.IsNullOrEmpty(MyBase.Messages(index).PLCAddress) Then
                        SubScriptions.SubscribeTo(MyBase.Messages(index).PLCAddress, 1, AddressOf PolledDataReturned, MyBase.Messages(index).PLCAddress, 1, 0)
                    End If
                    index += 1
                End While
                SubscriptionsCreated = True
            End If
            SubScriptions.SubscribeAutoProperties()

        End If
    End Sub

    '***************************************
    '* Call backs for returned data
    '***************************************
    Private OriginalText As String
    Private Sub PolledDataReturned(ByVal sender As Object, ByVal e As SubscriptionHandlerEventArgs)
        Try
            If (Me Is Nothing) Then Exit Sub
            If e.PLCComEventArgs.ErrorId <> 0 Then Exit Sub
        Catch ex As Exception
            Exit Sub
        End Try

        '* Find which item in the list this is from
        Dim index As Integer
        While index < MyBase.Messages.Count AndAlso String.Compare(MyBase.Messages(index).PLCAddress, e.PLCComEventArgs.PlcAddress, True) <> 0
            index += 1
        End While

        If index < MyBase.Messages.Count Then
            If (String.Compare(e.PLCComEventArgs.Values(0), "false", True) = 0) Or e.PLCComEventArgs.Values(0).Trim = "0" Then
                MyBase.Messages(index).Value = False
            Else
                MyBase.Messages(index).Value = True
            End If
        End If
    End Sub
#End Region

#Region "Error Display"
    Private Sub DisplaySubscribeError(ByVal sender As Object, ByVal e As MfgControl.AdvancedHMI.Drivers.Common.PlcComEventArgs)
        DisplayError(e.ErrorMessage)
    End Sub

    '********************************************************
    '* Show an error via the text property for a short time
    '********************************************************
    Private ErrorDisplayTime As System.Windows.Forms.Timer
    Private ErrorLock As New Object
    Private Sub DisplayError(ByVal ErrorMessage As String)
        If Not m_SuppressErrorDisplay Then
            '* Create the error display timer
            If ErrorDisplayTime Is Nothing Then
                ErrorDisplayTime = New System.Windows.Forms.Timer
                AddHandler ErrorDisplayTime.Tick, AddressOf ErrorDisplay_Tick
                ErrorDisplayTime.Interval = 5000
            End If

            '* Save the text to return to
            SyncLock (ErrorLock)
                If Not ErrorDisplayTime.Enabled Then
                    ErrorDisplayTime.Enabled = True
                    OriginalText = MyBase.Text
                    MyBase.Text = ErrorMessage
                End If
            End SyncLock
        End If
    End Sub


    '**************************************************************************************
    '* Return the text back to its original after displaying the error for a few seconds.
    '**************************************************************************************
    Private Sub ErrorDisplay_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SyncLock (ErrorLock)
            MyBase.Text = OriginalText
            ErrorDisplayTime.Enabled = False
        End SyncLock
    End Sub
#End Region

#Region "Private Methods"

#End Region

End Class
