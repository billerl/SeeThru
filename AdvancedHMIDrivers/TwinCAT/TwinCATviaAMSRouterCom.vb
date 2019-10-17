Public Class TwinCATviaAMSRouterCom
    Inherits MfgControl.AdvancedHMI.Drivers.TwinCATADSDll
    Implements System.ComponentModel.IComponent
    Implements System.Windows.Forms.IBindableComponent

    Public Event Disposed As EventHandler Implements System.ComponentModel.IComponent.Disposed


    Private m_synchronizationContext As System.Threading.SynchronizationContext

#Region "Constructor"
    Public Sub New(ByVal container As System.ComponentModel.IContainer)
        Me.New()

        'Required for Windows.Forms Class Composition Designer support
        container.Add(Me)
    End Sub

    Public Sub New()
        MyBase.New()

        m_synchronizationContext = System.Windows.Forms.WindowsFormsSynchronizationContext.Current
    End Sub


    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
    End Sub

    Protected Sub Dispose(disposing As Boolean) 'Implements System.ComponentModel.IComponent.Dispose
        ' MyBase.Dispose(disposing)

        If disposing Then
            SyncLock Me
                If Site IsNot Nothing AndAlso Site.Container IsNot Nothing Then
                    Site.Container.Remove(Me)
                End If
                'If events IsNot Nothing Then
                'Dim handler As EventHandler = DirectCast(events(EventDisposed), EventHandler)
                RaiseEvent Disposed(Me, EventArgs.Empty)
                'End If
            End SyncLock
        End If
    End Sub
#End Region

#Region "properties"
    Private m_site As System.ComponentModel.ISite
    <System.ComponentModel.Browsable(False), System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
    Public Overridable Property Site() As System.ComponentModel.ISite Implements System.ComponentModel.IComponent.Site
        Get
            Return m_site
        End Get
        Set(value As System.ComponentModel.ISite)
            m_site = value
        End Set
    End Property


    <System.ComponentModel.Browsable(False), System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>
    Protected ReadOnly Property DesignMode() As Boolean
        Get
            Dim s As System.ComponentModel.ISite = m_site
            If s Is Nothing Then
                Return False
            Else
                Return s.DesignMode
            End If
            ' Return If((s Is Nothing), False, s.DesignMode)
        End Get
    End Property
#End Region


#Region "Binding Properties"
    Private m_bindingContext As System.Windows.Forms.BindingContext
    Private m_dataBindings As System.Windows.Forms.ControlBindingsCollection
    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content)>
    Public ReadOnly Property DataBindings As System.Windows.Forms.ControlBindingsCollection Implements System.Windows.Forms.IBindableComponent.DataBindings
        Get
            If (m_dataBindings Is Nothing) Then
                m_dataBindings = New System.Windows.Forms.ControlBindingsCollection(Me)
            End If
            Return m_dataBindings
        End Get
    End Property

    <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content)>
    Public Property BindingContext As System.Windows.Forms.BindingContext Implements System.Windows.Forms.IBindableComponent.BindingContext
        Get
            If m_bindingContext Is Nothing Then
                m_bindingContext = New System.Windows.Forms.BindingContext()
            End If
            Return m_bindingContext
        End Get
        Set(value As System.Windows.Forms.BindingContext)
            m_bindingContext = value
            ' Throw New NotImplementedException()
        End Set
    End Property
#End Region


    Protected Overrides Sub OnSubscriptionDataReceived(e As MfgControl.AdvancedHMI.Drivers.Common.SubscriptionEventArgs)
        If m_synchronizationContext IsNot Nothing Then
            m_synchronizationContext.Post(AddressOf DataRecSync, e)
        Else
            MyBase.OnSubscriptionDataReceived(e)
        End If
    End Sub


    Private Sub DataRecSync(ByVal e As Object)
        Dim e1 As MfgControl.AdvancedHMI.Drivers.Common.SubscriptionEventArgs = DirectCast(e, MfgControl.AdvancedHMI.Drivers.Common.SubscriptionEventArgs)
        Try
            e1.dlgCallBack(Me, e1)
        Catch ex As Exception
        End Try
    End Sub
End Class
