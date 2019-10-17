Public Class MainMenu
    'Private m_MenuPosition As MenuPos
    'Public Property MenuPosition As MenuPos
    '    Get
    '        Return m_MenuPosition
    '    End Get
    '    Set(value As MenuPos)
    '        m_MenuPosition = value
    '    End Set
    'End Property

    '***************************************************************************************
    '* Close all forms when the exit button is clicked in order to close all communications
    '***************************************************************************************
    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

    '***************************************************************************************
    '* Open an initial form as designated by the OpenOnStartup property of the button
    '***************************************************************************************
    Private Sub MainMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If MenuPosition = AdvancedHMI.Controls.MenuPos.LeftOrTop Then
            Me.Location = New Point(0, 0)
        Else
            '* Right position
            If Width < Height Then
                Me.Location = New Point(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Width - Width, 0)
            Else
                Me.Location = New Point(0, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height - Height)
            End If
        End If
        CheckContainerControlsForOpenOnStartip(Me)
    End Sub

    '* V3.99y - added this in case button is in container such as panel
    Private Sub CheckContainerControlsForOpenOnStartip(ByVal container As Control)
        Dim index As Integer
        While index < container.Controls.Count
            ' If TypeOf container.Controls(index) Is Panel Then
            If container.Controls(index).HasChildren Then
                CheckContainerControlsForOpenOnStartip(container.Controls(index))
            Else

                If TypeOf container.Controls(index) Is MainMenuButton Then
                    If DirectCast(container.Controls(index), MainMenuButton).OpenOnStartup Then
                        DirectCast(container.Controls(index), MainMenuButton).PerformClick()
                        Exit While
                    End If
                End If
            End If

            index += 1
        End While
    End Sub

    Private Sub Label1_DoubleClick(sender As Object, e As EventArgs) Handles Label1.DoubleClick
        Dim index As Integer = 0
        While index < My.Application.OpenForms.Count
            If My.Application.OpenForms(index) IsNot Me Then
                My.Application.OpenForms(index).Close()
            End If
            index += 1
        End While

        Me.Close()
    End Sub
End Class

'Public Enum MenuPos
'    LeftOrTop
'    RightOrBottom
'End Enum