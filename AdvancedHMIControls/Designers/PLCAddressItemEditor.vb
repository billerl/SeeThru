Public Class PLCAddressItemEditor
    Inherits System.Drawing.Design.UITypeEditor


    Public Sub New()
    End Sub

    ' Indicates whether the UITypeEditor provides a form-based (modal) dialog,  
    ' drop down dialog, or no UI outside of the properties window. 
    Public Overloads Overrides Function GetEditStyle(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.Drawing.Design.UITypeEditorEditStyle
        Return System.Drawing.Design.UITypeEditorEditStyle.Modal
    End Function

    ' Displays the UI for value selection. 
    Public Overloads Overrides Function EditValue(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal provider As System.IServiceProvider, ByVal value As Object) As Object
        '* Create the editor form and pass it the PLC address items
        Dim ef As New PLCAddressItemListEditForm(DirectCast(value, System.Collections.ObjectModel.ObservableCollection(Of MfgControl.AdvancedHMI.Drivers.PLCAddressItemEx)))

        '* Get the first available IComComponent to use as the default
        ef.ComComponent = AdvancedHMIDrivers.Utilities.GetComComponent(context.Container)
        ef.PropertyGrid1.Site = context.Instance.site
        'For Each i In context.Container.Components
        '    If i.GetType.GetInterface("IComComponent") IsNot Nothing Then
        '        ef.ComComponent = i
        '        Exit For
        '        ' ef.ListBox1.Items.Add(i)
        '    End If
        'Next


        If ef.ShowDialog() = DialogResult.OK Then
            '* Force an acknowledement of changes in value
            context.OnComponentChanged()


            'Dim ChangedProperty As System.ComponentModel.PropertyDescriptor
            'ChangedProperty = System.ComponentModel.TypeDescriptor.GetProperties(context.Instance)("PLCAddressValueItems")
            'If (ChangedProperty IsNot Nothing) Then
            '    ChangedProperty.SetValue(context.Instance, ef.Items) ' context.Instance.PLCAddressValueItems)
            'End If

            Return ef.Items
        Else
            Return MyBase.EditValue(context, provider, value)
        End If
    End Function

    Public Overrides Function GetPaintValueSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return False
    End Function




End Class
