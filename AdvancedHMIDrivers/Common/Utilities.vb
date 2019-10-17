Public NotInheritable Class Utilities
    Private Sub New()

    End Sub

    Public Shared Sub StopComsOnHidden(ByVal components As System.ComponentModel.IContainer, ByVal form As System.Windows.Forms.Control)
        '* V3.97d - moved this to a shared sub to reduce code in the form
        If components IsNot Nothing Then
            Dim drv As MfgControl.AdvancedHMI.Drivers.IComComponent
            '*****************************
            '* Search for comm components
            '*****************************
            For i As Integer = 0 To components.Components.Count - 1
                If components.Components(i).GetType.GetInterface("IComComponent") IsNot Nothing Then
                    drv = DirectCast(components.Components.Item(i), MfgControl.AdvancedHMI.Drivers.IComComponent)
                    '* Stop/Start polling based on form visibility
                    drv.DisableSubscriptions = Not form.Visible
                End If
            Next
        End If
    End Sub

    Public Shared Sub SetPropertiesByIniFile(ByVal targetObject As Object, ByVal iniFileName As String, ByVal iniFileSection As String)
        If targetObject Is Nothing Then
            Throw New System.ArgumentNullException("targetObject", "SetPropertiesByIniFile null parameter of targetObject")
        End If

        If Not String.IsNullOrEmpty(iniFileName) Then
            Dim p As New IniParser(iniFileName)
            Dim settings() As String = p.ListSettings(iniFileSection)
            '* Loop thtough all the settings in this section
            For index = 0 To settings.Length - 1
                Dim pi As System.Reflection.PropertyInfo
                pi = targetObject.GetType().GetProperty(settings(index), Reflection.BindingFlags.IgnoreCase Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance)
                '* Check if a matching property name exists in the targetObject
                If pi IsNot Nothing Then
                    Dim value As Object = Nothing
                    If pi.PropertyType.IsEnum Then
                        Try
                            '* V3.99y - Added Enum capability
                            '* Enum type have to be converted from string to the Enum option
                            value = [Enum].Parse(pi.PropertyType, p.GetSetting(iniFileSection, settings(index)), True)
                        Catch ex As Exception
                            System.Windows.Forms.MessageBox.Show("Ini File Error - " & settings(index) & " is an an Enum. Values of " & p.GetSetting(iniFileSection, settings(index)) & " is not a valid option.")
                        End Try
                    Else
                        value = Convert.ChangeType(p.GetSetting(iniFileSection, settings(index)), targetObject.GetType().GetProperty(pi.Name).PropertyType, Globalization.CultureInfo.InvariantCulture)
                    End If
                    pi.SetValue(targetObject, value, Nothing)
                Else
                    System.Windows.Forms.MessageBox.Show("Ini File Error - " & settings(index) & " is not a valid property.")
                End If
            Next
        End If
    End Sub


    Public Shared Function IsImplemented(objectType As Type, interfaceType As Type) As Boolean
        For Each thisInterface As Type In objectType.GetInterfaces
            If thisInterface Is interfaceType Then
                Return True
            End If
        Next

        Return False
    End Function

    '*********************************************************************
    '* Used to set the ComComponent property in design mode
    '* If one doesn't exist, add a CLX driver
    '*********************************************************************
    Public Shared Function GetComComponent(ByVal container As ComponentModel.IContainer) As MfgControl.AdvancedHMI.Drivers.IComComponent
        If container Is Nothing Then Return Nothing

        Dim Result As MfgControl.AdvancedHMI.Drivers.IComComponent = Nothing
        '********************************************************
        '* Search for AdvancedHMIDrivers.IComComponent component in parent form
        '* If one exists, set the client of this component to it
        '********************************************************
        Dim ItemIndex As Integer
        Dim ContainerComponentCount As Integer = container.Components.Count
        While Result Is Nothing And ItemIndex < ContainerComponentCount
            'If Me.Site.Container.Components(ItemIndex).GetType.GetInterface("IComComponent") IsNot Nothing Then
            If AdvancedHMIDrivers.Utilities.IsImplemented((container.Components(ItemIndex).GetType), GetType(MfgControl.AdvancedHMI.Drivers.IComComponent)) Then
                Result = CType(container.Components(ItemIndex), MfgControl.AdvancedHMI.Drivers.IComComponent)
            End If
            ItemIndex += 1
        End While

        '************************************************
        '* If no comm component was found, then add one and
        '* point the ComComponent property to it
        '*********************************************
        If Result Is Nothing Then
            Result = New AdvancedHMIDrivers.EthernetIPforCLXCom(container)
        End If

        Return Result
    End Function
End Class
