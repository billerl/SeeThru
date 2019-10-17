Imports System.ComponentModel.Design

Public Class MeterCompactDesigner
    Inherits System.Windows.Forms.Design.ControlDesigner

    'Implements System.ComponentModel.Design.IDesigner

    '' Local reference to the designer's component.
    'Private m_component As System.ComponentModel.IComponent

    '' Public accessor to the designer's component.
    'Public ReadOnly Property Component() As System.ComponentModel.IComponent Implements IDesigner.Component
    '    Get
    '        Return m_component
    '    End Get
    'End Property


    Public Overrides ReadOnly Property Verbs As DesignerVerbCollection 'Implements IDesigner.Verbs
        Get
            Dim verbs_ As New DesignerVerbCollection()
            Dim dv1 As New DesignerVerb("Editor", New EventHandler(AddressOf Me.ShowComponentName))
            verbs_.Add(dv1)
            Return verbs_
        End Get
    End Property

    ' Event handler for displaying a message box showing the designer's component's name.
    Private Sub ShowComponentName(ByVal sender As Object, ByVal e As EventArgs)
        If (Me.Component IsNot Nothing) Then
            Dim mcdf As New MeterCompactDesignerForm
            mcdf.ControlInstanceToEdit = Component
            mcdf.ShowDialog()
        End If
    End Sub


    Public Overrides Sub DoDefaultAction() 'Implements IDesigner.DoDefaultAction
        'Throw New NotImplementedException()
        If (Me.Component IsNot Nothing) Then
            Dim mcdf As New MeterCompactDesignerForm
            mcdf.ControlInstanceToEdit = Component
            mcdf.ShowDialog()
            'Windows.Forms.MessageBox.Show(Me.Component.Site.Name, "Designer Component's Name")
        End If
    End Sub

    'Public Sub Initialize(component As System.ComponentModel.IComponent) Implements IDesigner.Initialize
    '    m_component = component
    'End Sub
End Class
