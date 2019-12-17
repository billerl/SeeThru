<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainMenu
    Inherits MfgControl.AdvancedHMI.Controls.MainMenu

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.MainMenuButton1 = New MfgControl.AdvancedHMI.MainMenuButton()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(410, 33)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(145, 21)
        Me.Button1.TabIndex = 11
        Me.Button1.Text = "Exit"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.LightGray
        Me.Label1.Location = New System.Drawing.Point(332, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 32)
        Me.Label1.TabIndex = 13
        '
        'MainMenuButton1
        '
        Me.MainMenuButton1.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.MainMenuButton1.ComComponent = Nothing
        Me.MainMenuButton1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MainMenuButton1.ForeColor = System.Drawing.Color.Black
        Me.MainMenuButton1.FormToOpen = GetType(MfgControl.AdvancedHMI.SeeThruForm)
        Me.MainMenuButton1.KeypadWidth = 300
        Me.MainMenuButton1.Location = New System.Drawing.Point(2, 12)
        Me.MainMenuButton1.Name = "MainMenuButton1"
        Me.MainMenuButton1.OpenOnStartup = True
        Me.MainMenuButton1.Passcode = Nothing
        Me.MainMenuButton1.PasswordChar = False
        Me.MainMenuButton1.PLCAddressVisible = ""
        Me.MainMenuButton1.Size = New System.Drawing.Size(145, 42)
        Me.MainMenuButton1.TabIndex = 8
        Me.MainMenuButton1.Text = "Startup Form"
        Me.MainMenuButton1.UseVisualStyleBackColor = False
        '
        'MainMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Silver
        Me.ClientSize = New System.Drawing.Size(477, 532)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.MainMenuButton1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MenuPosition = MfgControl.AdvancedHMI.Controls.MenuPos.RightOrBottom
        Me.Name = "MainMenu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "MainMenu"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MainMenuButton1 As MainMenuButton
    Friend WithEvents Button1 As Button
    Friend WithEvents Label1 As Label
End Class
