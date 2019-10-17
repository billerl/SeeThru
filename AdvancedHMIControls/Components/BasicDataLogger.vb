Imports System.ComponentModel
'*****************************************************************************
'* Simple Data Logger
'*
'* Archie Jacobs
'* Manufacturing Automation, LLC
'* 03-MAR-13
'* http://www.advancedhmi.com
'*
'* This component subscribes to a value in the PLC through a comm driver
'* and log it to a text file. It can log either by time interval or
'* data change.
'*
'* 03-MAR-13 Created
'*****************************************************************************
Public Class BasicDataLogger
    Inherits DataSubscriber

    Private sw As System.IO.StreamWriter

#Region "Constructor/Destructor"

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal container As System.ComponentModel.IContainer)
        Me.New()

        'Required for Windows.Forms Class Composition Designer support
        If (container IsNot Nothing) Then
            container.Add(Me)
        End If
    End Sub

    Protected Overrides Sub Dispose(disposing As Boolean)
        If LogTimer IsNot Nothing Then
            LogTimer.Enabled = False
        End If

        SyncLock (SwLock)
            If sw IsNot Nothing Then
                sw.Dispose()
            End If
        End SyncLock

        MyBase.Dispose(disposing)
    End Sub

#End Region

#Region "Properties"

    Private m_FileFolder As String = "." & System.IO.Path.DirectorySeparatorChar
    <BrowsableAttribute(True), EditorAttribute(GetType(FileFolderEditor), GetType(System.Drawing.Design.UITypeEditor))> _
    Public Property FileFolder As String
        Get
            Return m_FileFolder
        End Get
        Set(value As String)
            If value.Length > 0 Then
                '* Remove the last back slash if it is there
                If value.Substring(value.Length - 1, 1) = System.IO.Path.DirectorySeparatorChar Then value = value.Substring(0, value.Length - 1)
                m_FileFolder = value
            End If
        End Set
    End Property

    Private m_FileName As String = "PLCDataLog"
    Public Property FileName As String
        Get
            Return m_FileName
        End Get
        Set(value As String)
            If m_FileName <> value Then
                m_FileName = value
            End If
        End Set
    End Property

    Private m_FileNameIncludesDate As Boolean
    Public Property FileNameIncludesDate As Boolean
        Get
            Return m_FileNameIncludesDate
        End Get
        Set(value As Boolean)
            If m_FileNameIncludesDate <> value Then
                m_FileNameIncludesDate = value
                If m_CreateNewLogFileDaily Then
                    m_FileNameIncludesDate = True
                End If
            End If
        End Set
    End Property

    Public Enum TriggerType
        TimeInterval
        DataChange
        WriteOnTrigger
        EverySample
    End Enum

    Private m_LogTriggerType As TriggerType
    Public Property LogTriggerType As TriggerType
        Get
            Return m_LogTriggerType
        End Get
        Set(value As TriggerType)
            m_LogTriggerType = value
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

    Private m_Prefix As String
    Public Property Prefix As String
        Get
            Return m_Prefix
        End Get
        Set(value As String)
            m_Prefix = value
        End Set
    End Property

    Private m_TimestampFormat As String = "dd-MMM-yy HH:mm:ss"
    Public Property TimestampFormat As String
        Get
            Return m_TimestampFormat
        End Get
        Set(value As String)
            Try
                ' Dim TestString As String = Now.ToString("value")
                m_TimestampFormat = value
            Catch ex As Exception
                System.Windows.Forms.MessageBox.Show("Invalid DateTime format of " & value)
            End Try
        End Set
    End Property

    Private m_MaximumPoints As Integer
    Public Property MaximumPoints As Integer
        Get
            Return m_MaximumPoints
        End Get
        Set(value As Integer)
            m_MaximumPoints = value
        End Set
    End Property

    Private m_CreateNewLogFileAtMaxPoints As Boolean
    Public Property CreateNewLogFileAtMaxPoints As Boolean
        Get
            Return m_CreateNewLogFileAtMaxPoints
        End Get
        Set(value As Boolean)
            m_CreateNewLogFileAtMaxPoints = value
        End Set
    End Property

    Private m_CreateNewLogFileDaily As Boolean
    <System.ComponentModel.Description("Enabling this option will force inclusion of the current Date into the name of the log file")>
    Public Property CreateNewLogFileDaily As Boolean
        Get
            Return m_CreateNewLogFileDaily
        End Get
        Set(value As Boolean)
            m_CreateNewLogFileDaily = value
            If m_CreateNewLogFileDaily Then
                m_FileNameIncludesDate = True
            End If
        End Set
    End Property

    Private m_LogFileCount As Integer
    Public ReadOnly Property LogFileCount As Integer
        Get
            Return m_LogFileCount
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

    '*****************************************
    '* Property - Address in PLC to Link to
    '*****************************************
    Private m_PLCAddressEnableLogging As String = ""
    <System.ComponentModel.Category("PLC Properties")>
    Public Property PLCAddressEnableeLogging() As String
        Get
            Return m_PLCAddressEnableLogging
        End Get
        Set(ByVal value As String)
            If m_PLCAddressEnableLogging <> value Then
                m_PLCAddressEnableLogging = value

                '* When address is changed, re-subscribe to new address
                SubscribeToComDriver()
            End If
        End Set
    End Property



    Private m_WriteTrigger As Boolean
    <System.ComponentModel.Browsable(False)>
    Public Property WriteTrigger As Boolean
        Get
            Return m_WriteTrigger
        End Get
        Set(value As Boolean)
            If Not m_WriteTrigger And value Then
                If value Then
                    StoreValue()
                End If
            End If
            m_WriteTrigger = value

        End Set
    End Property

    '*****************************************
    '* Property - Address in PLC to Link to
    '*****************************************
    Private m_PLCAddressWriteTrigger As String = ""
    <System.ComponentModel.Category("PLC Properties")>
    Public Property PLCAddressWriteTrigger() As String
        Get
            Return m_PLCAddressWriteTrigger
        End Get
        Set(ByVal value As String)
            If m_PLCAddressWriteTrigger <> value Then
                m_PLCAddressWriteTrigger = value

                '* When address is changed, re-subscribe to new address
                SubscribeToComDriver()
            End If
        End Set
    End Property

#End Region

#Region "Events"

    Private PointCount As Integer
    Protected Overrides Sub onDataChanged(ByVal e As MfgControl.AdvancedHMI.Drivers.Common.PlcComEventArgs)
        MyBase.OnDataChanged(e)

        If (m_LogTriggerType = TriggerType.DataChange) Then
            If m_MaximumPoints = 0 OrElse PointCount < m_MaximumPoints Then
                StoreValue()
            Else
                CreateNewLog()
            End If
        End If
    End Sub

    Protected Overrides Sub OnDataReturned(e As MfgControl.AdvancedHMI.Drivers.Common.PlcComEventArgs)
        MyBase.OnDataReturned(e)

        If (m_LogTriggerType = TriggerType.EverySample) Then
            If m_MaximumPoints = 0 OrElse PointCount < m_MaximumPoints Then
                StoreValue()
            Else
                CreateNewLog()
            End If
        End If
    End Sub

    Private PreviousDate As String = ""
    Private DateTimeNow As String = ""
    Private fileNameToUse As String = ""
    Private Sub CreateNewLog()
        If Not DesignMode Then
            DateTimeNow = DateTime.Now.ToString("dd-MMM-yyyy")
            If m_CreateNewLogFileDaily AndAlso PreviousDate <> DateTimeNow Then
                PreviousDate = DateTimeNow
                PointCount = 0
                m_LogFileCount = 0
            End If
            If Me.m_CreateNewLogFileAtMaxPoints Then
                fileNameToUse = m_FileName & m_LogFileCount.ToString("000")
                PointCount = 0
                m_LogFileCount += 1
                If m_LogFileCount > 999 Then m_LogFileCount = 0
            Else
                fileNameToUse = m_FileName
                m_LogFileCount = 1
            End If
        End If
    End Sub

    Private SwLock As New Object
    '* When the subscription with the PLC succeeded, setup for logging
    Protected Overrides Sub OnSuccessfulSubscription(e As MfgControl.AdvancedHMI.Drivers.Common.PlcComEventArgs)
        MyBase.OnSuccessfulSubscription(e)

        '* create the timer to log the data
        If m_LogTriggerType = TriggerType.TimeInterval Then
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
        End If
    End Sub

    '* Timer tick interval used to store data at a periodic rate
    Private Sub LogInterval_Tick(sender As System.Object, e As System.EventArgs)
        If m_MaximumPoints = 0 OrElse PointCount < m_MaximumPoints Then
            StoreValue()
        Else
            CreateNewLog()
        End If
    End Sub


    Public Sub StoreValue()
        Try
            If m_EnableLogging Then
                Dim StringToWrite As String = m_Prefix
                If m_TimestampFormat IsNot Nothing AndAlso Not String.IsNullOrEmpty(m_TimestampFormat) Then
                    StringToWrite &= Date.Now.ToString(m_TimestampFormat)
                End If

                For Each item In SubscribedValues
                    If Not String.IsNullOrEmpty(item.Value) Then
                        StringToWrite &= "," & item.Value
                    Else
                        StringToWrite &= "," & "Empty Value"
                    End If
                Next

                SyncLock (SwLock)
                    If String.IsNullOrEmpty(fileNameToUse) Then CreateNewLog()

                    Dim FileName As String
                    If m_FileNameIncludesDate Then
                        FileName = m_FileFolder & System.IO.Path.DirectorySeparatorChar & fileNameToUse & "-" & DateTimeNow
                    Else
                        FileName = m_FileFolder & System.IO.Path.DirectorySeparatorChar & fileNameToUse
                    End If

                    If FileName.IndexOf(".") < 0 Then
                        FileName &= ".log"
                    End If

                    Using sw As New System.IO.StreamWriter(FileName, True)
                        sw.WriteLine(StringToWrite)
                    End Using
                End SyncLock

                PointCount += 1
            End If
        Catch ex As Exception
        End Try
    End Sub

#End Region

End Class

Public Class FileFolderEditor
    Inherits System.Drawing.Design.UITypeEditor

    Public Sub New()
    End Sub

    ' Indicates whether the UITypeEditor provides a form-based (modal) dialog,  
    ' drop down dialog, or no UI outside of the properties window. 
    Public Overloads Overrides Function GetEditStyle(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.Drawing.Design.UITypeEditorEditStyle
        Return System.Drawing.Design.UITypeEditorEditStyle.Modal
    End Function

    ' Displays the UI for value selection. 
    Public Overloads Overrides Function EditValue(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal provider As System.IServiceProvider, ByVal value As Object) As Object
        Using fb As New FolderBrowserDialog
            fb.ShowDialog()

            Return fb.SelectedPath
        End Using
    End Function

    Public Overrides Function GetPaintValueSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return False
    End Function

End Class

