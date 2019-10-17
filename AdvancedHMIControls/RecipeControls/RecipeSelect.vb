'****************************************************************************
'* List Buttons
'*
'* Archie Jacobs
'* 18-OCT-18
'*
'****************************************************************************
Public Class RecipeSelect
    Inherits MfgControl.AdvancedHMI.Controls.ListButtons

    Private RecipeFileParser As AdvancedHMIDrivers.IniParser


#Region "Constructor"
    Public Sub New()
        MyBase.New()
    End Sub
#End Region

#Region "Properties"
    Private m_RecipeIniFile As String = ".\Recipes.ini"
    Public Property RecipeIniFile As String
        Get
            Return m_RecipeIniFile
        End Get
        Set(value As String)
            m_RecipeIniFile = value
        End Set
    End Property

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
            m_ComComponent = value
        End Set
    End Property
#End Region

#Region "Events"
    Protected Overrides Sub OnButtonClicked(e As MfgControl.AdvancedHMI.Controls.ListButtonsEventArgs)
        MyBase.OnButtonClicked(e)

        If RecipeFileParser IsNot Nothing Then
            Dim s() As String = RecipeFileParser.ListSettings(e.ButtonText)
            If s IsNot Nothing And m_ComComponent IsNot Nothing Then
                For index = 0 To s.Length - 1
                    Dim v As String = RecipeFileParser.GetSetting(e.ButtonText, s(index))

                    Try
                        m_ComComponent.Write(s(index), v)
                    Catch ex As Exception

                    End Try
                Next
            End If
        End If
    End Sub

    Protected Overrides Sub OnVisibleChanged(e As EventArgs)
        MyBase.OnVisibleChanged(e)

        If Visible Then LoadFromIniFile()
    End Sub

#End Region

#Region "Methods"
    Protected Overrides Sub EndInit()
        LoadFromIniFile()

        MyBase.EndInit()
    End Sub

    Public Sub LoadFromIniFile()
        If Not String.IsNullOrEmpty(m_RecipeIniFile) Then
            If System.IO.File.Exists(m_RecipeIniFile) Then
                RecipeFileParser = New AdvancedHMIDrivers.IniParser(m_RecipeIniFile)
                Dim sections() As String = RecipeFileParser.ListSections
                If sections IsNot Nothing AndAlso sections.Count > 0 Then
                    Me.Items.Clear()
                    For index = 0 To sections.Count - 1
                        Me.Items.Add(sections(index))
                    Next
                End If
                RefreshDisplay()
            Else
                '* File does not exist
                Dim l As New Label()
                l.Text = "Recipe file " & m_RecipeIniFile & " does not exist."
                l.TextAlign = ContentAlignment.MiddleCenter
                l.Width = Width
                l.Height = Height
                l.Font = Font
                l.BackColor = Color.Red
                Controls.Add(l)
                Exit Sub
            End If
        End If
    End Sub
#End Region
End Class
