Public Class GenericTcpEventArgs
    Inherits System.EventArgs

    Public Sub New()
    End Sub

    Public Sub New(data As Byte())
        m_Data = data
    End Sub

    Public Sub New(data As Byte(), receivedString As String)
        Me.New(data)
        m_DataAsString = receivedString
    End Sub

    Private m_Data As Byte()
    Public Property Data As Byte()
        Get
            Return m_Data
        End Get
        Set(value As Byte())
            m_Data = value
        End Set
    End Property

    Private m_DataAsString As String
    Public Property DataAsString As String
        Get
            Return m_DataAsString
        End Get
        Set(value As String)
            m_DataAsString = value
        End Set
    End Property
End Class
