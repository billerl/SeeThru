Public Class SetIP

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles BtnSetIP.Click

        If TbIp.Text <> "" AndAlso TbPlcAddress.Text <> "" AndAlso DrpDwnDriver.SelectedItem <> Nothing Then
            If TbDebounceTmr.Text = "" Then
                debounceTmr = 0.0
            Else
                debounceTmr = TbDebounceTmr.Text
            End If
            ipAddress = TbIp.Text
            driver = DrpDwnDriver.SelectedItem
            plcAddress = TbPlcAddress.Text
            CheckChanges()
            MainForm.UpdatePref()
            Me.Close()
        End If

    End Sub

    Private Sub BtnCloseIP_Click(sender As Object, e As EventArgs) Handles BtnCloseIP.Click

        Me.Close()

    End Sub

    Private Sub SetIP_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        DrpDwnDriver.Items.Add("ControlLogix / CompactLogix")
        DrpDwnDriver.Items.Add("MicroLogix / SLC")
        TbDebounceTmr.Text = debounceTmr
        DrpDwnDriver.Text = driver
        LblDriver.Text = driver
        LblIpAddress.Text = ipAddress
        TbIp.Text = ipAddress
        LblTrigger.Text = plcAddress
        TbPlcAddress.Text = plcAddress
        ToolTip1.SetToolTip(TbDebounceTmr, "Time PLC address must be on before a image will be saved")
        CheckDriver()

    End Sub
    Private Sub CheckDriver()

        If DrpDwnDriver.Text = "ControlLogix / CompactLogix" And driver <> "" And ipAddress <> "" Then
            CBTags.Visible = True
            BtnReadTags.Visible = True
        Else
            CBTags.Visible = False
            BtnReadTags.Visible = False
        End If
        MainForm.UpdatePref()
    End Sub

    Private Sub BtnReadTags_Click(sender As Object, e As EventArgs) Handles BtnReadTags.Click

        Try
            Dim tags() As MfgControl.AdvancedHMI.Drivers.CLXTag = MainForm.EthernetIPforCLXCom1.GetTagList
            Dim TagIndex As Integer = 0
            For Each TagID In tags
                CBTags.Items.Add(tags(TagIndex).TagName)
                TagIndex = TagIndex + 1
            Next
            CBTags.Sorted = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub CBTags_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBTags.SelectedIndexChanged

        TbPlcAddress.Text = CBTags.SelectedItem

    End Sub

    Private Sub DrpDwnDriver_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DrpDwnDriver.SelectedIndexChanged

        CheckDriver()

    End Sub

    Private Sub BtnSaveIpAd_Click(sender As Object, e As EventArgs) Handles BtnSaveIpAd.Click

        If TbIp.Text <> "" Then
            ipAddress = TbIp.Text
            CheckDriver()
        End If

    End Sub

    Private Sub BtnSaveDriver_Click(sender As Object, e As EventArgs) Handles BtnSaveDriver.Click

        If DrpDwnDriver.Text <> "" Then
            driver = DrpDwnDriver.Text
            CheckDriver()
        End If

    End Sub

End Class