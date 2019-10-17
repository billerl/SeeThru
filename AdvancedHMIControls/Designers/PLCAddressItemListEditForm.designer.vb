<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PLCAddressItemListEditForm
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
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.PropertyGrid1 = New System.Windows.Forms.PropertyGrid()
        Me.AddButton = New System.Windows.Forms.Button()
        Me.DeleteButton = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button1.Location = New System.Drawing.Point(365, 424)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(95, 31)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "OK"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(45, 44)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(213, 316)
        Me.ListBox1.TabIndex = 1
        '
        'PropertyGrid1
        '
        Me.PropertyGrid1.HelpVisible = False
        Me.PropertyGrid1.Location = New System.Drawing.Point(281, 17)
        Me.PropertyGrid1.Name = "PropertyGrid1"
        Me.PropertyGrid1.Size = New System.Drawing.Size(293, 392)
        Me.PropertyGrid1.TabIndex = 2
        '
        'AddButton
        '
        Me.AddButton.Location = New System.Drawing.Point(45, 375)
        Me.AddButton.Name = "AddButton"
        Me.AddButton.Size = New System.Drawing.Size(102, 36)
        Me.AddButton.TabIndex = 3
        Me.AddButton.Text = "Add"
        Me.AddButton.UseVisualStyleBackColor = True
        '
        'DeleteButton
        '
        Me.DeleteButton.Location = New System.Drawing.Point(156, 375)
        Me.DeleteButton.Name = "DeleteButton"
        Me.DeleteButton.Size = New System.Drawing.Size(102, 36)
        Me.DeleteButton.TabIndex = 4
        Me.DeleteButton.Text = "Delete"
        Me.DeleteButton.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button4.Location = New System.Drawing.Point(479, 424)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(95, 31)
        Me.Button4.TabIndex = 5
        Me.Button4.Text = "Cancel"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'PLCAddressItemListEditForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(608, 476)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.DeleteButton)
        Me.Controls.Add(Me.AddButton)
        Me.Controls.Add(Me.PropertyGrid1)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.Button1)
        Me.Name = "PLCAddressItemListEditForm"
        Me.Text = "PLCAddressItemListEditForm"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents PropertyGrid1 As PropertyGrid
    Friend WithEvents AddButton As Button
    Friend WithEvents DeleteButton As Button
    Friend WithEvents Button4 As Button
End Class
