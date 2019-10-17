<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SetIP
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SetIP))
        Me.BtnSetIP = New System.Windows.Forms.Button()
        Me.BtnCloseIP = New System.Windows.Forms.Button()
        Me.TbIp = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LblIpAddress = New System.Windows.Forms.Label()
        Me.DrpDwnDriver = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.LblDriver = New System.Windows.Forms.Label()
        Me.LblTrigger = New System.Windows.Forms.Label()
        Me.TbPlcAddress = New System.Windows.Forms.TextBox()
        Me.BtnReadTags = New System.Windows.Forms.Button()
        Me.CBTags = New System.Windows.Forms.ComboBox()
        Me.BtnSaveIpAd = New System.Windows.Forms.Button()
        Me.BtnSaveDriver = New System.Windows.Forms.Button()
        Me.TbDebounceTmr = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SuspendLayout()
        '
        'BtnSetIP
        '
        Me.BtnSetIP.Location = New System.Drawing.Point(179, 233)
        Me.BtnSetIP.Name = "BtnSetIP"
        Me.BtnSetIP.Size = New System.Drawing.Size(75, 30)
        Me.BtnSetIP.TabIndex = 0
        Me.BtnSetIP.Text = "Ok"
        Me.BtnSetIP.UseVisualStyleBackColor = True
        '
        'BtnCloseIP
        '
        Me.BtnCloseIP.Location = New System.Drawing.Point(281, 233)
        Me.BtnCloseIP.Name = "BtnCloseIP"
        Me.BtnCloseIP.Size = New System.Drawing.Size(75, 30)
        Me.BtnCloseIP.TabIndex = 1
        Me.BtnCloseIP.Text = "Cancel"
        Me.BtnCloseIP.UseVisualStyleBackColor = True
        '
        'TbIp
        '
        Me.TbIp.Location = New System.Drawing.Point(179, 42)
        Me.TbIp.Name = "TbIp"
        Me.TbIp.Size = New System.Drawing.Size(177, 22)
        Me.TbIp.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoEllipsis = True
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(72, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(101, 17)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Set IP Address"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblIpAddress
        '
        Me.LblIpAddress.AutoEllipsis = True
        Me.LblIpAddress.AutoSize = True
        Me.LblIpAddress.Location = New System.Drawing.Point(362, 42)
        Me.LblIpAddress.Name = "LblIpAddress"
        Me.LblIpAddress.Size = New System.Drawing.Size(52, 17)
        Me.LblIpAddress.TabIndex = 4
        Me.LblIpAddress.Text = "0.0.0.0"
        Me.LblIpAddress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DrpDwnDriver
        '
        Me.DrpDwnDriver.FormattingEnabled = True
        Me.DrpDwnDriver.Location = New System.Drawing.Point(179, 71)
        Me.DrpDwnDriver.Name = "DrpDwnDriver"
        Me.DrpDwnDriver.Size = New System.Drawing.Size(177, 24)
        Me.DrpDwnDriver.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoEllipsis = True
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(84, 75)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(89, 17)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Select Driver"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.AutoEllipsis = True
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(76, 104)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(97, 17)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Select Trigger"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblDriver
        '
        Me.LblDriver.AutoEllipsis = True
        Me.LblDriver.AutoSize = True
        Me.LblDriver.Location = New System.Drawing.Point(362, 74)
        Me.LblDriver.Name = "LblDriver"
        Me.LblDriver.Size = New System.Drawing.Size(46, 17)
        Me.LblDriver.TabIndex = 9
        Me.LblDriver.Text = "Driver"
        Me.LblDriver.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblTrigger
        '
        Me.LblTrigger.AutoEllipsis = True
        Me.LblTrigger.AutoSize = True
        Me.LblTrigger.Location = New System.Drawing.Point(362, 104)
        Me.LblTrigger.Name = "LblTrigger"
        Me.LblTrigger.Size = New System.Drawing.Size(90, 17)
        Me.LblTrigger.TabIndex = 10
        Me.LblTrigger.Text = "PLC Address"
        Me.LblTrigger.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TbPlcAddress
        '
        Me.TbPlcAddress.Location = New System.Drawing.Point(179, 101)
        Me.TbPlcAddress.Name = "TbPlcAddress"
        Me.TbPlcAddress.Size = New System.Drawing.Size(177, 22)
        Me.TbPlcAddress.TabIndex = 11
        '
        'BtnReadTags
        '
        Me.BtnReadTags.Location = New System.Drawing.Point(207, 190)
        Me.BtnReadTags.Name = "BtnReadTags"
        Me.BtnReadTags.Size = New System.Drawing.Size(109, 29)
        Me.BtnReadTags.TabIndex = 12
        Me.BtnReadTags.Text = "Read Tags"
        Me.BtnReadTags.UseVisualStyleBackColor = True
        '
        'CBTags
        '
        Me.CBTags.FormattingEnabled = True
        Me.CBTags.Location = New System.Drawing.Point(179, 160)
        Me.CBTags.Name = "CBTags"
        Me.CBTags.Size = New System.Drawing.Size(177, 24)
        Me.CBTags.TabIndex = 13
        '
        'BtnSaveIpAd
        '
        Me.BtnSaveIpAd.Location = New System.Drawing.Point(21, 35)
        Me.BtnSaveIpAd.Name = "BtnSaveIpAd"
        Me.BtnSaveIpAd.Size = New System.Drawing.Size(45, 30)
        Me.BtnSaveIpAd.TabIndex = 14
        Me.BtnSaveIpAd.Text = "Set"
        Me.BtnSaveIpAd.UseVisualStyleBackColor = True
        '
        'BtnSaveDriver
        '
        Me.BtnSaveDriver.Location = New System.Drawing.Point(21, 68)
        Me.BtnSaveDriver.Name = "BtnSaveDriver"
        Me.BtnSaveDriver.Size = New System.Drawing.Size(45, 30)
        Me.BtnSaveDriver.TabIndex = 15
        Me.BtnSaveDriver.Text = "Set"
        Me.BtnSaveDriver.UseVisualStyleBackColor = True
        '
        'TbDebounceTmr
        '
        Me.TbDebounceTmr.Location = New System.Drawing.Point(179, 129)
        Me.TbDebounceTmr.Name = "TbDebounceTmr"
        Me.TbDebounceTmr.Size = New System.Drawing.Size(35, 22)
        Me.TbDebounceTmr.TabIndex = 17
        Me.TbDebounceTmr.Text = "0.0"
        '
        'Label4
        '
        Me.Label4.AutoEllipsis = True
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(60, 132)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(113, 17)
        Me.Label4.TabIndex = 16
        Me.Label4.Text = "Debounce Timer"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SetIP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.ClientSize = New System.Drawing.Size(638, 294)
        Me.Controls.Add(Me.TbDebounceTmr)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.BtnSaveDriver)
        Me.Controls.Add(Me.BtnSaveIpAd)
        Me.Controls.Add(Me.CBTags)
        Me.Controls.Add(Me.BtnReadTags)
        Me.Controls.Add(Me.TbPlcAddress)
        Me.Controls.Add(Me.LblTrigger)
        Me.Controls.Add(Me.LblDriver)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DrpDwnDriver)
        Me.Controls.Add(Me.LblIpAddress)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TbIp)
        Me.Controls.Add(Me.BtnCloseIP)
        Me.Controls.Add(Me.BtnSetIP)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "SetIP"
        Me.Text = "SetIP"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnSetIP As Button
    Friend WithEvents BtnCloseIP As Button
    Friend WithEvents TbIp As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents LblIpAddress As Label
    Friend WithEvents DrpDwnDriver As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents LblDriver As Label
    Friend WithEvents LblTrigger As Label
    Friend WithEvents TbPlcAddress As TextBox
    Friend WithEvents BtnReadTags As Button
    Friend WithEvents CBTags As ComboBox
    Friend WithEvents BtnSaveIpAd As Button
    Friend WithEvents BtnSaveDriver As Button
    Friend WithEvents TbDebounceTmr As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents ToolTip1 As ToolTip
End Class
