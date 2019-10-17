Public Class MessageEventArgs
    Inherits System.EventArgs

    Public Sub New()
        MyBase.New
    End Sub

    Public Sub New(ByVal message As String)
        Me.New
        m_Message = message
    End Sub

    Private m_Message As String
    Public Property Message As String
        Get
            Return m_Message
        End Get
        Set(value As String)
            m_Message = value
        End Set
    End Property
End Class
