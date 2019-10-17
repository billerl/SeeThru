Public Class SharedSocket
    Inherits Net.Sockets.Socket

    Public Sub New(addressFamily As Net.Sockets.AddressFamily, socketType As Net.Sockets.SocketType, protocolType As Net.Sockets.ProtocolType)
        MyBase.New(addressFamily, socketType, protocolType)
    End Sub

    Private m_ConnectionCount As Integer
    Public Property ConnectionCount As Integer
        Get
            Return m_ConnectionCount
        End Get
        Set(value As Integer)
            m_ConnectionCount = value
        End Set
    End Property

    Private m_IPAddress As String = ""
    Public Property IPAddress As String
        Get
            Return m_IPAddress
        End Get
        Set(value As String)
            m_IPAddress = value
        End Set
    End Property

    Private m_Port As Integer
    Public Property Port As Integer
        Get
            Return m_Port
        End Get
        Set(value As Integer)
            m_Port = value
        End Set
    End Property


    Public Shadows Sub Connect()
        Dim endPoint As System.Net.IPEndPoint
        Dim IP As System.Net.IPHostEntry

        Dim address As New System.Net.IPAddress(0)
        If System.Net.IPAddress.TryParse(m_IPAddress, address) Then
            endPoint = New System.Net.IPEndPoint(address, m_Port)
        Else
            Try
                IP = System.Net.Dns.GetHostEntry(m_IPAddress)
                '* Ethernet/IP uses port AF12 (44818)
                endPoint = New System.Net.IPEndPoint(IP.AddressList(0), m_Port)
            Catch ex As Exception
                Throw New FormatException("Can't resolve the address " & m_IPAddress)
                Exit Sub
            End Try
        End If


        MyBase.Connect(endPoint)
    End Sub
End Class
