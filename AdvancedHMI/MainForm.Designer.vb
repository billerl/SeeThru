<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
    Inherits System.Windows.Forms.Form
    'Inherits CommonDialog

    'Form overrides dispose to clean up the component list.
    '   <System.Diagnostics.DebuggerNonUserCode()> _
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
    ' <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.BtnSaveStart = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PreferencesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DisplayNameOnBorderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UsePLCToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CommunicationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EthernetIPforCLXCom1 = New AdvancedHMIDrivers.EthernetIPforCLXCom(Me.components)
        Me.EthernetIPforSLCMicroCom1 = New AdvancedHMIDrivers.EthernetIPforSLCMicroCom(Me.components)
        Me.DataSubscriber1 = New AdvancedHMIControls.DataSubscriber(Me.components)
        Me.PilotLight1 = New AdvancedHMIControls.PilotLight()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.MenuStrip1.SuspendLayout()
        CType(Me.EthernetIPforCLXCom1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EthernetIPforSLCMicroCom1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataSubscriber1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnSaveStart
        '
        Me.BtnSaveStart.BackColor = System.Drawing.Color.DimGray
        Me.BtnSaveStart.Location = New System.Drawing.Point(103, 95)
        Me.BtnSaveStart.Name = "BtnSaveStart"
        Me.BtnSaveStart.Size = New System.Drawing.Size(111, 53)
        Me.BtnSaveStart.TabIndex = 0
        Me.BtnSaveStart.Text = "SAVE"
        Me.BtnSaveStart.UseVisualStyleBackColor = False
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(12, 59)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(315, 30)
        Me.TextBox1.TabIndex = 1
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.Color.DimGray
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveToolStripMenuItem, Me.EditToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1362, 28)
        Me.MenuStrip1.TabIndex = 2
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveToolStripMenuItem1, Me.LoadToolStripMenuItem})
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(44, 24)
        Me.SaveToolStripMenuItem.Text = "File"
        '
        'SaveToolStripMenuItem1
        '
        Me.SaveToolStripMenuItem1.Enabled = False
        Me.SaveToolStripMenuItem1.Name = "SaveToolStripMenuItem1"
        Me.SaveToolStripMenuItem1.Size = New System.Drawing.Size(117, 26)
        Me.SaveToolStripMenuItem1.Text = "Save"
        '
        'LoadToolStripMenuItem
        '
        Me.LoadToolStripMenuItem.Enabled = False
        Me.LoadToolStripMenuItem.Name = "LoadToolStripMenuItem"
        Me.LoadToolStripMenuItem.Size = New System.Drawing.Size(117, 26)
        Me.LoadToolStripMenuItem.Text = "Load"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PreferencesToolStripMenuItem})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(47, 24)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'PreferencesToolStripMenuItem
        '
        Me.PreferencesToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.PreferencesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DisplayNameOnBorderToolStripMenuItem, Me.UsePLCToolStripMenuItem, Me.CommunicationToolStripMenuItem})
        Me.PreferencesToolStripMenuItem.Name = "PreferencesToolStripMenuItem"
        Me.PreferencesToolStripMenuItem.Size = New System.Drawing.Size(216, 26)
        Me.PreferencesToolStripMenuItem.Text = "Preferences"
        '
        'DisplayNameOnBorderToolStripMenuItem
        '
        Me.DisplayNameOnBorderToolStripMenuItem.Checked = True
        Me.DisplayNameOnBorderToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.DisplayNameOnBorderToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DisplayNameOnBorderToolStripMenuItem.Name = "DisplayNameOnBorderToolStripMenuItem"
        Me.DisplayNameOnBorderToolStripMenuItem.Size = New System.Drawing.Size(247, 26)
        Me.DisplayNameOnBorderToolStripMenuItem.Text = "Display Name on Border"
        '
        'UsePLCToolStripMenuItem
        '
        Me.UsePLCToolStripMenuItem.Checked = True
        Me.UsePLCToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.UsePLCToolStripMenuItem.Name = "UsePLCToolStripMenuItem"
        Me.UsePLCToolStripMenuItem.Size = New System.Drawing.Size(247, 26)
        Me.UsePLCToolStripMenuItem.Text = "Use PLC"
        '
        'CommunicationToolStripMenuItem
        '
        Me.CommunicationToolStripMenuItem.Name = "CommunicationToolStripMenuItem"
        Me.CommunicationToolStripMenuItem.Size = New System.Drawing.Size(247, 26)
        Me.CommunicationToolStripMenuItem.Text = "Communication"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(53, 24)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(125, 26)
        Me.AboutToolStripMenuItem.Text = "About"
        '
        'EthernetIPforCLXCom1
        '
        Me.EthernetIPforCLXCom1.CIPConnectionSize = 508
        Me.EthernetIPforCLXCom1.DisableMultiServiceRequest = False
        Me.EthernetIPforCLXCom1.DisableSubscriptions = False
        Me.EthernetIPforCLXCom1.IniFileName = ""
        Me.EthernetIPforCLXCom1.IniFileSection = Nothing
        Me.EthernetIPforCLXCom1.IPAddress = "0.0.0.0"
        Me.EthernetIPforCLXCom1.PollRateOverride = 500
        Me.EthernetIPforCLXCom1.Port = 44818
        Me.EthernetIPforCLXCom1.ProcessorSlot = 0
        Me.EthernetIPforCLXCom1.RoutePath = Nothing
        Me.EthernetIPforCLXCom1.Timeout = 4000
        '
        'EthernetIPforSLCMicroCom1
        '
        Me.EthernetIPforSLCMicroCom1.CIPConnectionSize = 508
        Me.EthernetIPforSLCMicroCom1.DisableSubscriptions = False
        Me.EthernetIPforSLCMicroCom1.IniFileName = ""
        Me.EthernetIPforSLCMicroCom1.IniFileSection = Nothing
        Me.EthernetIPforSLCMicroCom1.IPAddress = "0.0.0.0"
        Me.EthernetIPforSLCMicroCom1.IsPLC5 = False
        Me.EthernetIPforSLCMicroCom1.MaxPCCCPacketSize = 236
        Me.EthernetIPforSLCMicroCom1.PollRateOverride = 500
        Me.EthernetIPforSLCMicroCom1.Port = 44818
        Me.EthernetIPforSLCMicroCom1.RoutePath = Nothing
        Me.EthernetIPforSLCMicroCom1.TagAliasFile = ".\TagAlias.txt"
        Me.EthernetIPforSLCMicroCom1.Timeout = 5000
        '
        'DataSubscriber1
        '
        Me.DataSubscriber1.ComComponent = Me.EthernetIPforCLXCom1
        Me.DataSubscriber1.PLCAddressValue = Nothing
        Me.DataSubscriber1.Value = Nothing
        '
        'PilotLight1
        '
        Me.PilotLight1.Blink = False
        Me.PilotLight1.ComComponent = Me.EthernetIPforCLXCom1
        Me.PilotLight1.Font = New System.Drawing.Font("Arial", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PilotLight1.ForeColor = System.Drawing.Color.Transparent
        Me.PilotLight1.LegendPlate = MfgControl.AdvancedHMI.Controls.PilotLight.LegendPlates.None
        Me.PilotLight1.LightColor = MfgControl.AdvancedHMI.Controls.PilotLight.LightColorOption.Green
        Me.PilotLight1.LightColorOff = MfgControl.AdvancedHMI.Controls.PilotLight.LightColorOption.White
        Me.PilotLight1.Location = New System.Drawing.Point(333, 52)
        Me.PilotLight1.Name = "PilotLight1"
        Me.PilotLight1.OutputType = MfgControl.AdvancedHMI.Controls.OutputType.MomentarySet
        Me.PilotLight1.PLCAddressClick = ""
        Me.PilotLight1.PLCAddressText = ""
        Me.PilotLight1.PLCAddressValue = ""
        Me.PilotLight1.PLCAddressVisible = ""
        Me.PilotLight1.Size = New System.Drawing.Size(43, 46)
        Me.PilotLight1.TabIndex = 3
        Me.PilotLight1.Value = False
        Me.PilotLight1.ValueToWrite = 0
        '
        'MainForm
        '
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.Lime
        Me.ClientSize = New System.Drawing.Size(1362, 815)
        Me.Controls.Add(Me.PilotLight1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.BtnSaveStart)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.ForeColor = System.Drawing.Color.Black
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(441, 86)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "SeeThru"
        Me.TransparencyKey = System.Drawing.Color.Lime
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.EthernetIPforCLXCom1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EthernetIPforSLCMicroCom1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataSubscriber1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnSaveStart As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents SaveToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PreferencesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DisplayNameOnBorderToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents LoadToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents UsePLCToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CommunicationToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EthernetIPforCLXCom1 As AdvancedHMIDrivers.EthernetIPforCLXCom
    Friend WithEvents EthernetIPforSLCMicroCom1 As AdvancedHMIDrivers.EthernetIPforSLCMicroCom
    Friend WithEvents DataSubscriber1 As AdvancedHMIControls.DataSubscriber
    Friend WithEvents PilotLight1 As AdvancedHMIControls.PilotLight
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolTip1 As ToolTip
End Class
