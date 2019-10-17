<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MeterCompactDesignerForm
    Inherits System.Windows.Forms.Form

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
        Me.ColorGroup = New System.Windows.Forms.GroupBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.NeedleColorPicker = New MfgControl.AdvancedHMI.Controls.ColorPicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.MinorColorPicker = New MfgControl.AdvancedHMI.Controls.ColorPicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.MajorTickColorPicker = New MfgControl.AdvancedHMI.Controls.ColorPicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextColorPicker = New MfgControl.AdvancedHMI.Controls.ColorPicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BackColorPicker = New MfgControl.AdvancedHMI.Controls.ColorPicker()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.MaximumTextBox = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.MinimumTextBox = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.PLCAddressValueTextBox = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.ComComponentSelectComboBox = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.ColorGroup.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button1.Location = New System.Drawing.Point(413, 300)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(91, 32)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "OK"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ColorGroup
        '
        Me.ColorGroup.Controls.Add(Me.Label9)
        Me.ColorGroup.Controls.Add(Me.NeedleColorPicker)
        Me.ColorGroup.Controls.Add(Me.Label6)
        Me.ColorGroup.Controls.Add(Me.MinorColorPicker)
        Me.ColorGroup.Controls.Add(Me.Label5)
        Me.ColorGroup.Controls.Add(Me.MajorTickColorPicker)
        Me.ColorGroup.Controls.Add(Me.Label4)
        Me.ColorGroup.Controls.Add(Me.TextColorPicker)
        Me.ColorGroup.Controls.Add(Me.Label1)
        Me.ColorGroup.Controls.Add(Me.BackColorPicker)
        Me.ColorGroup.Location = New System.Drawing.Point(278, 137)
        Me.ColorGroup.Name = "ColorGroup"
        Me.ColorGroup.Size = New System.Drawing.Size(161, 157)
        Me.ColorGroup.TabIndex = 2
        Me.ColorGroup.TabStop = False
        Me.ColorGroup.Text = "Color"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label9.Location = New System.Drawing.Point(18, 73)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(53, 17)
        Me.Label9.TabIndex = 11
        Me.Label9.Text = "Needle"
        '
        'NeedleColorPicker
        '
        Me.NeedleColorPicker.Location = New System.Drawing.Point(96, 68)
        Me.NeedleColorPicker.Name = "NeedleColorPicker"
        Me.NeedleColorPicker.Size = New System.Drawing.Size(37, 26)
        Me.NeedleColorPicker.TabIndex = 10
        Me.NeedleColorPicker.TextDisplayed = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label6.Location = New System.Drawing.Point(18, 125)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(73, 17)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Minor Tick"
        '
        'MinorColorPicker
        '
        Me.MinorColorPicker.Location = New System.Drawing.Point(96, 120)
        Me.MinorColorPicker.Name = "MinorColorPicker"
        Me.MinorColorPicker.Size = New System.Drawing.Size(37, 26)
        Me.MinorColorPicker.TabIndex = 8
        Me.MinorColorPicker.TextDisplayed = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label5.Location = New System.Drawing.Point(18, 99)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(73, 17)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Major Tick"
        '
        'MajorTickColorPicker
        '
        Me.MajorTickColorPicker.Location = New System.Drawing.Point(96, 94)
        Me.MajorTickColorPicker.Name = "MajorTickColorPicker"
        Me.MajorTickColorPicker.Size = New System.Drawing.Size(37, 26)
        Me.MajorTickColorPicker.TabIndex = 6
        Me.MajorTickColorPicker.TextDisplayed = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label4.Location = New System.Drawing.Point(18, 47)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 17)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Text Color"
        '
        'TextColorPicker
        '
        Me.TextColorPicker.Location = New System.Drawing.Point(96, 42)
        Me.TextColorPicker.Name = "TextColorPicker"
        Me.TextColorPicker.Size = New System.Drawing.Size(37, 26)
        Me.TextColorPicker.TabIndex = 4
        Me.TextColorPicker.TextDisplayed = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label1.Location = New System.Drawing.Point(18, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 17)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "BackColor"
        '
        'BackColorPicker
        '
        Me.BackColorPicker.Location = New System.Drawing.Point(96, 16)
        Me.BackColorPicker.Name = "BackColorPicker"
        Me.BackColorPicker.Size = New System.Drawing.Size(37, 26)
        Me.BackColorPicker.TabIndex = 2
        Me.BackColorPicker.TextDisplayed = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.MaximumTextBox)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.MinimumTextBox)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(278, 27)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(161, 101)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Range"
        '
        'MaximumTextBox
        '
        Me.MaximumTextBox.Location = New System.Drawing.Point(86, 47)
        Me.MaximumTextBox.Name = "MaximumTextBox"
        Me.MaximumTextBox.Size = New System.Drawing.Size(47, 20)
        Me.MaximumTextBox.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label3.Location = New System.Drawing.Point(15, 49)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(66, 17)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Maximum"
        '
        'MinimumTextBox
        '
        Me.MinimumTextBox.Location = New System.Drawing.Point(86, 18)
        Me.MinimumTextBox.Name = "MinimumTextBox"
        Me.MinimumTextBox.Size = New System.Drawing.Size(47, 20)
        Me.MinimumTextBox.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label2.Location = New System.Drawing.Point(15, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 17)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Minimum"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(54, 19)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(192, 20)
        Me.TextBox1.TabIndex = 4
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label7.Location = New System.Drawing.Point(13, 21)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(35, 17)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "Text"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(16, 49)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(230, 37)
        Me.Button2.TabIndex = 6
        Me.Button2.Text = "Font"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Button2)
        Me.GroupBox2.Controls.Add(Me.TextBox1)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 27)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(260, 101)
        Me.GroupBox2.TabIndex = 7
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Label"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.ComComponentSelectComboBox)
        Me.GroupBox3.Controls.Add(Me.PLCAddressValueTextBox)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 137)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(260, 120)
        Me.GroupBox3.TabIndex = 8
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "PLC Tag"
        '
        'PLCAddressValueTextBox
        '
        Me.PLCAddressValueTextBox.Location = New System.Drawing.Point(16, 47)
        Me.PLCAddressValueTextBox.Name = "PLCAddressValueTextBox"
        Me.PLCAddressValueTextBox.Size = New System.Drawing.Size(230, 20)
        Me.PLCAddressValueTextBox.TabIndex = 4
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label8.Location = New System.Drawing.Point(13, 21)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(130, 17)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "PLC Address Value"
        '
        'ComComponentSelectComboBox
        '
        Me.ComComponentSelectComboBox.FormattingEnabled = True
        Me.ComComponentSelectComboBox.Location = New System.Drawing.Point(16, 93)
        Me.ComComponentSelectComboBox.Name = "ComComponentSelectComboBox"
        Me.ComComponentSelectComboBox.Size = New System.Drawing.Size(230, 21)
        Me.ComComponentSelectComboBox.TabIndex = 9
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label10.Location = New System.Drawing.Point(13, 73)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(180, 17)
        Me.Label10.TabIndex = 10
        Me.Label10.Text = "Communication Component"
        '
        'MeterCompactDesignerForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(516, 344)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ColorGroup)
        Me.Controls.Add(Me.Button1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "MeterCompactDesignerForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Meter Properties"
        Me.ColorGroup.ResumeLayout(False)
        Me.ColorGroup.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Button1 As Windows.Forms.Button
    Friend WithEvents ColorGroup As Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents MaximumTextBox As Windows.Forms.TextBox
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents MinimumTextBox As Windows.Forms.TextBox
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents BackColorPicker As MfgControl.AdvancedHMI.Controls.ColorPicker
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents TextColorPicker As MfgControl.AdvancedHMI.Controls.ColorPicker
    Friend WithEvents Label6 As Windows.Forms.Label
    Friend WithEvents MinorColorPicker As MfgControl.AdvancedHMI.Controls.ColorPicker
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents MajorTickColorPicker As MfgControl.AdvancedHMI.Controls.ColorPicker
    Friend WithEvents TextBox1 As Windows.Forms.TextBox
    Friend WithEvents Label7 As Windows.Forms.Label
    Friend WithEvents Button2 As Windows.Forms.Button
    Friend WithEvents GroupBox2 As Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As Windows.Forms.GroupBox
    Friend WithEvents PLCAddressValueTextBox As Windows.Forms.TextBox
    Friend WithEvents Label8 As Windows.Forms.Label
    Friend WithEvents Label9 As Label
    Friend WithEvents NeedleColorPicker As MfgControl.AdvancedHMI.Controls.ColorPicker
    Friend WithEvents ComComponentSelectComboBox As ComboBox
    Friend WithEvents Label10 As Label
End Class
