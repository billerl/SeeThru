Imports System.ComponentModel
'******************************************************************************************
'* Recipe Button
'*
'* Archie Jacobs
'* Manufacturing Automation, LLC
'* support@advancedhmi.com
'* 13-FEB-16
'*
'*
'* Copyright 2016 Archie Jacobs
'*
'* This control will read an INI file to retreive settings for a recipe button.
'* The INI file contains the PLC Address and values to be written
'*
'* Example INI File:
'*
'* [Recipe1]  '* This is the name specified in INIFileSection property
'* Tag1=100
'* Tag2=99
'* Tag3=Recipe for XYZ
'* ButtonText=XYZ
'*
'********************************************************************************************
Public Class RecipeButton
    Inherits Button
    Implements System.ComponentModel.ISupportInitialize

#Region "PLC Related Properties"
    '*****************************************************
    '* Property - Component to communicate to PLC through
    '*****************************************************
    Private m_ComComponent As MfgControl.AdvancedHMI.Drivers.IComComponent
    <System.ComponentModel.Category("PLC Properties")>
    Public Property ComComponent() As MfgControl.AdvancedHMI.Drivers.IComComponent
        Get
            Return m_ComComponent
        End Get
        Set(ByVal value As MfgControl.AdvancedHMI.Drivers.IComComponent)
            If m_ComComponent IsNot value Then
                m_ComComponent = value

                SubscribeToComDriver()
            End If
        End Set
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

    Public Property PLCAddressIniFileSection As String
#End Region

#Region "Events"
    Protected Overrides Sub OnClick(e As EventArgs)
        MyBase.OnClick(e)

        '* Make sure an INI file was specified
        If Not String.IsNullOrEmpty(m_IniFileName) Then
            If String.IsNullOrEmpty(RecipeFileError) Then
                If m_ComComponent IsNot Nothing Then
                    For i = 0 To Settings.Count - 1
                        Try
                            m_ComComponent.Write(Settings(i).PLCAddress, Settings(i).Value)
                        Catch ex As Exception
                            System.Windows.Forms.MessageBox.Show("Faile to write " & Settings(i).Value & " to " & Settings(i).PLCAddress & " ." & ex.Message)
                        End Try
                    Next
                Else
                    System.Windows.Forms.MessageBox.Show("ComComponent Property must be set.")
                End If
            Else
                System.Windows.Forms.MessageBox.Show("INI File Error - " & RecipeFileError)
            End If
        End If
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
    End Sub
#End Region


#Region "INI File Handling"
    Private m_IniFileName As String = ""
    Public Property IniFileName As String
        Get
            Return m_IniFileName
        End Get
        Set(value As String)
            If m_IniFileName <> value Then
                m_IniFileName = value
                RetreiveSettings()
            End If
        End Set
    End Property

    Private m_IniFileSection As String
    Public Property IniFileSection As String
        Get
            Return m_IniFileSection
        End Get
        Set(value As String)
            If m_IniFileSection <> value Then
                m_IniFileSection = value
                RetreiveSettings()
            End If
        End Set
    End Property


    Private Initializing As Boolean
    Public Sub BeginInit() Implements System.ComponentModel.ISupportInitialize.BeginInit
        Initializing = True
    End Sub

    Public Sub EndInit() Implements System.ComponentModel.ISupportInitialize.EndInit
        If Not Me.DesignMode Then
            If Not String.IsNullOrEmpty(m_IniFileName) Then
                Try
                    RetreiveSettings()
                Catch ex As Exception
                    System.Windows.Forms.MessageBox.Show("INI File - " & ex.Message)
                End Try
            End If
        End If
        Initializing = False
    End Sub

    Private Settings As New List(Of RecipeSetting)
    Private RecipeFileError As String
    Private Sub RetreiveSettings()
        If Not String.IsNullOrEmpty(m_IniFileName) Then
            Try
                RecipeFileError = ""
                Dim p As New AdvancedHMIDrivers.IniParser(m_IniFileName)
                If Not String.IsNullOrEmpty(m_IniFileSection) Then
                    Dim c() As Char = {vbNullChar}
                    Dim TrimmedSection As String = m_IniFileSection.Trim(c)
                    Dim SettingList() As String = p.ListSettings(m_IniFileSection.Trim(c))
                    Settings.Clear()

                    For index = 0 To SettingList.Length - 1
                        If String.Compare(SettingList(index), "ButtonText", True) <> 0 Then
                            Dim s As New RecipeSetting(SettingList(index), p.GetSetting(TrimmedSection, SettingList(index)))
                            Settings.Add(s)
                        Else
                            Text = p.GetSetting(TrimmedSection, SettingList(index))
                        End If
                    Next
                End If
            Catch ex As Exception
                RecipeFileError = ex.Message
            End Try
        End If
    End Sub
#End Region

#Region "Subscribing and PLC data receiving"
    Private SubScriptions As SubscriptionHandler
    '**************************************************
    '* Subscribe to addresses in the Comm(PLC) Driver
    '**************************************************
    Private Sub SubscribeToComDriver()
        If Not DesignMode And IsHandleCreated Then
            '* Create a subscription handler object
            If SubScriptions Is Nothing Then
                SubScriptions = New SubscriptionHandler
                SubScriptions.Parent = Me
                AddHandler SubScriptions.DisplayError, AddressOf DisplaySubscribeError
            End If
            SubScriptions.ComComponent = m_ComComponent

            SubScriptions.SubscribeAutoProperties()
        End If
    End Sub


    Private Sub DisplaySubscribeError(ByVal sender As Object, ByVal e As MfgControl.AdvancedHMI.Drivers.Common.PlcComEventArgs)
        DisplayError(e.ErrorMessage)
    End Sub
#End Region


#Region "Error Display"
    Private OriginalText As String

    '********************************************************
    '* Show an error via the text property for a short time
    '********************************************************
    Private WithEvents ErrorDisplayTime As System.Windows.Forms.Timer
    Private Sub DisplayError(ByVal ErrorMessage As String)
        If Not m_SuppressErrorDisplay Then
            If ErrorDisplayTime Is Nothing Then
                ErrorDisplayTime = New System.Windows.Forms.Timer
                AddHandler ErrorDisplayTime.Tick, AddressOf ErrorDisplay_Tick
                ErrorDisplayTime.Interval = 5000
            End If

            '* Save the text to return to
            If Not ErrorDisplayTime.Enabled Then
                OriginalText = Me.Text
            End If

            ErrorDisplayTime.Enabled = True

            Me.Text = ErrorMessage
        End If
    End Sub


    '**************************************************************************************
    '* Return the text back to its original after displaying the error for a few seconds.
    '**************************************************************************************
    Private Sub ErrorDisplay_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Text = OriginalText

        If ErrorDisplayTime IsNot Nothing Then
            ErrorDisplayTime.Enabled = False
            ErrorDisplayTime.Dispose()
            ErrorDisplayTime = Nothing
        End If
    End Sub
#End Region


End Class

Public Class RecipeSetting
    Public Sub New()

    End Sub

    Public Sub New(plcAddress As String, valueToWrite As String)
        m_PLCAddress = plcAddress
        m_Value = valueToWrite
    End Sub

    Private m_PLCAddress As String
    Public Property PLCAddress As String
        Get
            Return m_PLCAddress
        End Get
        Set(value As String)
            m_PLCAddress = value
        End Set
    End Property

    Private m_Value As String
    Public Property Value As String
        Get
            Return m_Value
        End Get
        Set(value As String)
            m_Value = value
        End Set
    End Property
End Class