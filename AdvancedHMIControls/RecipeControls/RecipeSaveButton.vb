Public Class RecipeSaveButton
    Inherits Button

    Public Event RecipeSaved As EventHandler(Of EventArgs)

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

    Private m_PLCAddressItems As New System.Collections.ObjectModel.Collection(Of String)
    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content)>
    Public ReadOnly Property PLCAddressItems As System.Collections.ObjectModel.Collection(Of String)
        Get
            Return m_PLCAddressItems
        End Get
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
    Protected Overrides Sub OnClick(e As EventArgs)
        MyBase.OnClick(e)

        If m_PLCAddressItems.Count > 0 Then
            Dim RecipeINI As AdvancedHMIDrivers.IniParser
            If String.IsNullOrEmpty(m_RecipeIniFile) Then
                m_RecipeIniFile = ".\Recipes.ini"
            End If

            If System.IO.File.Exists(m_RecipeIniFile) Then
                RecipeINI = New AdvancedHMIDrivers.IniParser(m_RecipeIniFile)
            Else
                RecipeINI = New AdvancedHMIDrivers.IniParser()
                RecipeINI.FilePath = m_RecipeIniFile
            End If


            Dim kpd As New MfgControl.AdvancedHMI.Controls.AlphaKeyboard3()
            If kpd.ShowDialog = DialogResult.OK Then
                If Not String.IsNullOrEmpty(kpd.Value) Then
                    Dim v As String
                    For index = 0 To m_PLCAddressItems.Count - 1
                        Try
                            If m_ComComponent IsNot Nothing Then
                                v = m_ComComponent.Read(m_PLCAddressItems(index), 1)(0)
                                RecipeINI.AddSetting(kpd.Value, m_PLCAddressItems(index), v)
                            Else
                                MsgBox("ComComponent property not set.")
                            End If
                        Catch ex As Exception
                        End Try
                    Next

                    RecipeINI.SaveSettings()

                    OnRecipeSaved(EventArgs.Empty)
                End If
            End If
        End If
    End Sub

    Protected Overridable Sub OnRecipeSaved(ByVal e As EventArgs)
        RaiseEvent RecipeSaved(Me, e)
    End Sub
#End Region


End Class
