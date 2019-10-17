Public Class MeterCompactDesignerForm
    Public ControlInstanceToEdit As MeterCompact


    Private Sub MinimumTextBox_KeyPress(sender As Object, e As Windows.Forms.KeyPressEventArgs) Handles MinimumTextBox.KeyPress, MaximumTextBox.KeyPress
        If (e.KeyChar < "0"c Or e.KeyChar > "9"c) And Convert.ToInt16(e.KeyChar) >= 32 And (e.KeyChar <> "-"c) Then
            e.Handled = True
        End If
    End Sub


    Private Sub MinimumTextBox_Leave(sender As Object, e As EventArgs) Handles MinimumTextBox.Leave, MaximumTextBox.Leave
        ControlInstanceToEdit.Minimum = MinimumTextBox.Text
        ControlInstanceToEdit.Maximum = MaximumTextBox.Text

        '* Let the Property Window know of the change
        Dim ChangedProperty As System.ComponentModel.PropertyDescriptor = System.ComponentModel.TypeDescriptor.GetProperties(ControlInstanceToEdit)("Minimum")
        If (ChangedProperty IsNot Nothing) Then
            ChangedProperty.SetValue(ControlInstanceToEdit, ControlInstanceToEdit.Minimum)
        End If

        ChangedProperty = System.ComponentModel.TypeDescriptor.GetProperties(ControlInstanceToEdit)("Maximum")
        If (ChangedProperty IsNot Nothing) Then
            ChangedProperty.SetValue(ControlInstanceToEdit, ControlInstanceToEdit.Maximum)
        End If
    End Sub



    Private Sub Button3_Paint(sender As Object, e As Windows.Forms.PaintEventArgs)
        Dim x As New System.Drawing.Design.ColorEditor
    End Sub

    Private Sub ColorPicker1_ColorChanged(sender As Object, e As EventArgs) Handles BackColorPicker.ColorChanged
        ControlInstanceToEdit.BackColor = BackColorPicker.Color

        '* Let the Property Window know of the change
        Dim backColorProp As System.ComponentModel.PropertyDescriptor = System.ComponentModel.TypeDescriptor.GetProperties(ControlInstanceToEdit)("BackColor")
        If (backColorProp IsNot Nothing) Then
            backColorProp.SetValue(ControlInstanceToEdit, ControlInstanceToEdit.BackColor)
        End If

    End Sub

    Private Sub TextColorPicker_ColorChanged(sender As Object, e As EventArgs) Handles TextColorPicker.ColorChanged
        ControlInstanceToEdit.ForeColor = TextColorPicker.Color
        '* Let the Property Window know of the change
        Dim ChangedProperty As System.ComponentModel.PropertyDescriptor = System.ComponentModel.TypeDescriptor.GetProperties(ControlInstanceToEdit)("ForeColor")
        If (ChangedProperty IsNot Nothing) Then
            ChangedProperty.SetValue(ControlInstanceToEdit, ControlInstanceToEdit.ForeColor)
        End If
    End Sub

    Private Sub MajorTickColorPicker_ColorChanged(sender As Object, e As EventArgs) Handles MajorTickColorPicker.ColorChanged
        ControlInstanceToEdit.MajorTickColor = MajorTickColorPicker.Color
        '* Let the Property Window know of the change
        Dim ChangedProperty As System.ComponentModel.PropertyDescriptor = System.ComponentModel.TypeDescriptor.GetProperties(ControlInstanceToEdit)("MajorTickColor")
        If (ChangedProperty IsNot Nothing) Then
            ChangedProperty.SetValue(ControlInstanceToEdit, ControlInstanceToEdit.MajorTickColor)
        End If
    End Sub

    Private Sub MinorColorPicker_ColorChanged(sender As Object, e As EventArgs) Handles MinorColorPicker.ColorChanged
        ControlInstanceToEdit.MinorTickColor = MinorColorPicker.Color
        '* Let the Property Window know of the change
        Dim ChangedProperty As System.ComponentModel.PropertyDescriptor = System.ComponentModel.TypeDescriptor.GetProperties(ControlInstanceToEdit)("MinorTickColor")
        If (ChangedProperty IsNot Nothing) Then
            ChangedProperty.SetValue(ControlInstanceToEdit, ControlInstanceToEdit.MinorTickColor)
        End If
    End Sub

    Private Sub MeterCompactDesignerForm_VisibleChanged(sender As Object, e As EventArgs) Handles MyBase.VisibleChanged
        If Visible Then
            MinimumTextBox.Text = ControlInstanceToEdit.Minimum
            MaximumTextBox.Text = ControlInstanceToEdit.Maximum
            BackColorPicker.Color = ControlInstanceToEdit.BackColor
            TextColorPicker.Color = ControlInstanceToEdit.ForeColor
            MajorTickColorPicker.Color = ControlInstanceToEdit.MajorTickColor
            MinorColorPicker.Color = ControlInstanceToEdit.MinorTickColor
            NeedleColorPicker.Color = ControlInstanceToEdit.NeedleColor
            TextBox1.Text = ControlInstanceToEdit.Text
            If ControlInstanceToEdit.PLCAddressValue IsNot Nothing Then
                PLCAddressValueTextBox.Text = ControlInstanceToEdit.PLCAddressValue.PLCAddress
            Else
                PLCAddressValueTextBox.Text = ""
            End If

            '********************************************************
            '* Search for AdvancedHMIDrivers.IComComponent component
            '*   in the Designer Host Container
            '********************************************************
            Dim i As Integer
            While i < ControlInstanceToEdit.Parent.Site.Container.Components.Count
                If ControlInstanceToEdit.Parent.Site.Container.Components(i).GetType.GetInterface("IComComponent") IsNot Nothing Then
                    ComComponentSelectComboBox.Items.Add(ControlInstanceToEdit.Parent.Site.Container.Components(i))
                End If
                i += 1
            End While
            '* Set the ComboBox to the currently selected value
            If ControlInstanceToEdit.ComComponent IsNot Nothing Then
                ComComponentSelectComboBox.SelectedItem = ControlInstanceToEdit.ComComponent
            End If
        End If
    End Sub

    Private Sub TextBox1_Leave(sender As Object, e As EventArgs) Handles TextBox1.Leave
        ControlInstanceToEdit.Text = TextBox1.Text

        '* Let the Property Window know of the change
        Dim ChangedProperty As System.ComponentModel.PropertyDescriptor = System.ComponentModel.TypeDescriptor.GetProperties(ControlInstanceToEdit)("Text")
        If (ChangedProperty IsNot Nothing) Then
            ChangedProperty.SetValue(ControlInstanceToEdit, ControlInstanceToEdit.Text)
        End If
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        Dim fd As New Windows.Forms.FontDialog
        fd.Font = ControlInstanceToEdit.Font
        If fd.ShowDialog = Windows.Forms.DialogResult.OK Then
            ControlInstanceToEdit.Font = fd.Font
        End If

        '* Let the Property Window know of the change
        Dim ChangedProperty As System.ComponentModel.PropertyDescriptor = System.ComponentModel.TypeDescriptor.GetProperties(ControlInstanceToEdit)("Font")
        If (ChangedProperty IsNot Nothing) Then
            ChangedProperty.SetValue(ControlInstanceToEdit, ControlInstanceToEdit.Font)
        End If
    End Sub

    Private Sub PLCAddressValueTextBox_Leave(sender As Object, e As EventArgs) Handles PLCAddressValueTextBox.Leave
        If Not String.IsNullOrEmpty(PLCAddressValueTextBox.Text) Then
            If ControlInstanceToEdit.PLCAddressValue IsNot Nothing Then
                ControlInstanceToEdit.PLCAddressValue.PLCAddress = PLCAddressValueTextBox.Text
            Else
                ControlInstanceToEdit.PLCAddressValue = New MfgControl.AdvancedHMI.Drivers.PLCAddressItem(PLCAddressValueTextBox.Text)
            End If
        Else
            ControlInstanceToEdit = Nothing
        End If

        '* Let the Property Window know of the change
        Dim ChangedProperty As System.ComponentModel.PropertyDescriptor = System.ComponentModel.TypeDescriptor.GetProperties(ControlInstanceToEdit)("PLCAddressValue")
        If (ChangedProperty IsNot Nothing) Then
            ChangedProperty.SetValue(ControlInstanceToEdit, ControlInstanceToEdit.PLCAddressValue)
        End If

    End Sub

    Private Sub NeedleColorPicker_ColorChanged(sender As Object, e As EventArgs) Handles NeedleColorPicker.ColorChanged
        ControlInstanceToEdit.NeedleColor = NeedleColorPicker.Color

        '* Let the Property Window know of the change
        Dim ChangedProperty As System.ComponentModel.PropertyDescriptor = System.ComponentModel.TypeDescriptor.GetProperties(ControlInstanceToEdit)("NeedleColor")
        If (ChangedProperty IsNot Nothing) Then
            ChangedProperty.SetValue(ControlInstanceToEdit, ControlInstanceToEdit.NeedleColor)
        End If
    End Sub

    Private Sub ComComponentSelectComboBox_Leave(sender As Object, e As EventArgs) Handles ComComponentSelectComboBox.Leave
        If ComComponentSelectComboBox.SelectedItem IsNot Nothing Then
            ControlInstanceToEdit.ComComponent = ComComponentSelectComboBox.SelectedItem

            '* Let the Property Window know of the change
            Dim ChangedProperty As System.ComponentModel.PropertyDescriptor = System.ComponentModel.TypeDescriptor.GetProperties(ControlInstanceToEdit)("ComComponent")
            If (ChangedProperty IsNot Nothing) Then
                ChangedProperty.SetValue(ControlInstanceToEdit, ControlInstanceToEdit.ComComponent)
            End If
        End If
    End Sub
End Class