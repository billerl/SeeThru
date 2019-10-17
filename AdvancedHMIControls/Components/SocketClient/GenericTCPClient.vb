Option Strict On
Imports System.Net.Sockets
'**********************************************************************************************
'* Generic TCp Client
'*
'* Archie Jacobs
'* Manufacturing Automation, LLC
'* support@advancedhmi.com
'* 19-JAN-18
'*
'* This client will connect to a TCP server and listen for data
'* When data is received, it will buffer until receing the value
'* specified by TerminatingByteValue. At that point it will fire the
'* DataReceived event and send the received data
'* 
'* Copyright 2018 Archie Jacobs
'* Licensed under GPL v3
'*
'* Reference : 
'*
'**********************************************************************************************
<System.ComponentModel.DefaultEvent("DataReceived")>
Public Class GenericTcpClient
    Inherits System.ComponentModel.Component
    Implements IDisposable
    Implements System.ComponentModel.ISupportInitialize

    Protected Shared GTCDLL As System.Collections.Concurrent.ConcurrentDictionary(Of Integer, SharedSocket)
    Protected MyDLLInstance As Integer
    Protected Shared NextDLLInstance As Integer
    Protected EventHandlerDLLInstance As Integer
    'Private WorkSocket As System.Net.Sockets.Socket

    Public Event DataReceived As EventHandler(Of GenericTcpEventArgs)
    Public Event ConnectionClosed As EventHandler
    Public Event ConnectionEstablished As EventHandler
    Public Event ComError As EventHandler(Of MessageEventArgs)

    Private m_synchronizationContext As System.Threading.SynchronizationContext

    Private TestConnectionTask As System.Threading.Tasks.Task
    Private EndTaskToken As New Threading.CancellationTokenSource




#Region "Constructor/Destructors"
    Public Sub New()
        'DataReceivedCallBackDelegate = New AsyncCallback(AddressOf DataReceivedCallback)
        m_ProtocolType = Net.Sockets.ProtocolType.Tcp
        m_synchronizationContext = System.Windows.Forms.WindowsFormsSynchronizationContext.Current

        If GTCDLL Is Nothing Then
            GTCDLL = New System.Collections.Concurrent.ConcurrentDictionary(Of Integer, SharedSocket)
        End If
    End Sub

    Public Sub New(ByVal container As System.ComponentModel.IContainer)
        Me.New()

        'Required for Windows.Forms Class Composition Designer support
        container.Add(Me)
    End Sub



    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        MyBase.Dispose(disposing)

        If disposing Then
            EndTaskToken.Cancel(True)
            '*V3.99y - check for null
            If TestConnectionTask IsNot Nothing Then
                TestConnectionTask.Wait()
            End If

            CloseConnection()

            If EndTaskToken IsNot Nothing Then EndTaskToken.Dispose()
        End If
    End Sub
#End Region

#Region "Properties"
    Private m_IPAddress As String = "192.168.0.1"   '* this is a default value
    Public Property IPAddress() As String
        Get
            Return m_IPAddress.ToString
        End Get
        Set(ByVal value As String)
            If Not String.IsNullOrEmpty(value) Then
                If m_IPAddress <> value Then
                    m_IPAddress = value

                    If EventHandlerDLLInstance = (MyDLLInstance + 1) Then
                        RemoveDLLConnection(MyDLLInstance)
                    End If


                    '* If a new instance needs to be created, such as a different IP Address
                    CreateDLLInstance()

                    'If EventHandlerDLLInstance > 0 Then
                    '    If DLL(MyDLLInstance) IsNot Nothing Then
                    '        DLL(MyDLLInstance).IPAddress = m_IPAddress
                    '    End If
                    'End If
                End If
            End If
        End Set
    End Property

    Private m_Port As UShort = 23
    Public Property Port As UShort
        Get
            Return m_Port
        End Get
        Set(value As UShort)
            If m_Port <> value Then
                If GTCDLL(MyDLLInstance) IsNot Nothing AndAlso GTCDLL(MyDLLInstance).Connected Then
                    CloseConnection()
                End If
                m_Port = value
            End If
        End Set
    End Property

    Private Property m_ProtocolType As ProtocolType = ProtocolType.Tcp
    Public Property ProtocolType As ProtocolType
        Get
            Return m_ProtocolType
        End Get
        Set(value As ProtocolType)
            m_ProtocolType = value
        End Set
    End Property

    Private m_TerminatingCharacters As String = "\r"
    Private m_ParsedTerminators As String = Chr(13)
    Public Property TerminatingCharacters As String
        Get
            Return m_TerminatingCharacters
        End Get
        Set(value As String)
            m_TerminatingCharacters = value
            m_ParsedTerminators = System.Text.RegularExpressions.Regex.Replace(value, "\\r", Chr(13))
            m_ParsedTerminators = System.Text.RegularExpressions.Regex.Replace(m_ParsedTerminators, "\\l", Chr(10))
            m_ParsedTerminators = System.Text.RegularExpressions.Regex.Replace(m_ParsedTerminators, "\\03", Chr(3))
        End Set
    End Property

    Private m_AutoConnect As Boolean = True
    Public Property AutoConnect As Boolean
        Get
            Return m_AutoConnect
        End Get
        Set(value As Boolean)
            m_AutoConnect = value
        End Set
    End Property
#End Region


#Region "Private Methods"
    '***************************************************************
    '* Create the Data Link Layer Instances
    '* if the IP Address is the same, then resuse a common instance
    '***************************************************************
    Private Shared ReadOnly CreateDLLLock As New Object
    Protected Overridable Sub CreateDLLInstance()
        SyncLock (CreateDLLLock)
            If m_IPAddress = "0.0.0.0" Then
                Exit Sub
            End If

            Dim endPoint As System.Net.IPEndPoint
            Dim IP As System.Net.IPHostEntry

            Dim address As New System.Net.IPAddress(0)
            If System.Net.IPAddress.TryParse(m_IPAddress, address) Then
                endPoint = New System.Net.IPEndPoint(address, m_Port)
            Else
                Try
                    IP = System.Net.Dns.GetHostEntry(m_IPAddress)
                    endPoint = New System.Net.IPEndPoint(IP.AddressList(0), m_Port)
                Catch ex As Exception
                    Throw New FormatException("Can't resolve the address " & m_IPAddress)
                    Exit Sub
                End Try
            End If



            '* Check to see if it has the same Port
            '* if so, reuse the instance, otherwise create a new one
            Dim KeyFound As Boolean
            'Dim rep As System.Net.IPEndPoint
            For Each d In GTCDLL
                If d.Value IsNot Nothing Then
                    ' rep = DirectCast(d.Value.RemoteEndPoint, System.Net.IPEndPoint)
                    Dim IP1 As String = ""
                    Try
                        IP1 = Net.IPAddress.Parse(d.Value.IPAddress).ToString()
                    Catch ex As Exception
                    End Try

                    Dim IP2 As String = ""
                    Try
                        IP2 = Net.IPAddress.Parse(m_IPAddress).ToString()
                    Catch ex As Exception
                    End Try

                    If (IP1 = IP2) And (d.Value.Port = m_Port) And (d.Value.ProtocolType = m_ProtocolType) Then
                        MyDLLInstance = d.Key
                        KeyFound = True
                        Exit For
                    End If
                End If
            Next

            If Not KeyFound Then
                NextDLLInstance += 1
                MyDLLInstance = NextDLLInstance
            End If

            If (Not GTCDLL.ContainsKey(MyDLLInstance) OrElse (GTCDLL(MyDLLInstance) Is Nothing)) Then
                If m_ProtocolType = Net.Sockets.ProtocolType.Tcp Then
                    GTCDLL(MyDLLInstance) = New SharedSocket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp)
                    '* Comment these out for Compact Framework
                    GTCDLL(MyDLLInstance).SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, 5000)
                    GTCDLL(MyDLLInstance).SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, True)
                    'DLL(MyDLLInstance).RemoteEndPoint = endPoint
                Else
                    GTCDLL(MyDLLInstance) = New SharedSocket(endPoint.AddressFamily, SocketType.Dgram, m_ProtocolType)
                End If

                GTCDLL(MyDLLInstance).IPAddress = m_IPAddress
                GTCDLL(MyDLLInstance).Port = m_Port
            End If




            '* Have we already attached event handler to this data link layer?
            If EventHandlerDLLInstance <> (MyDLLInstance + 1) Then
                '* If event handler to another layer has been created, remove them
                If EventHandlerDLLInstance > 0 Then
                    If GTCDLL.ContainsKey(EventHandlerDLLInstance - 1) Then
                        RemoveDLLConnection(EventHandlerDLLInstance - 1)
                    End If
                End If


                'AddHandler DLL(MyDLLInstance).DataReceived, AddressOf DataLinkLayerDataReceived
                EventHandlerDLLInstance = MyDLLInstance + 1

                '* Track how many instanced use this DLL, so we know when to dispose
                GTCDLL(MyDLLInstance).ConnectionCount += 1
            End If
        End SyncLock
    End Sub

    Protected Sub RemoveDLLConnection(ByVal instance As Integer)
        SyncLock (CreateDLLLock)
            '* The handle linked to the DataLink Layer has to be removed, otherwise it causes a problem when a form is closed
            If GTCDLL.ContainsKey(instance) AndAlso GTCDLL(instance) IsNot Nothing Then
                'RemoveHandler DLL(instance).DataReceive, AddressOf DataLinkLayerDataReceived
                EventHandlerDLLInstance = 0

                GTCDLL(instance).ConnectionCount -= 1

                If GTCDLL(instance).ConnectionCount <= 0 Then
                    GTCDLL(instance).Dispose()
                    GTCDLL(instance) = Nothing
                    Dim x As SharedSocket = Nothing
                    GTCDLL.TryRemove(instance, x)
                End If
            End If
        End SyncLock
    End Sub

    '*********************************************
    '* Connect to the socket and begin listening
    '* for responses
    '********************************************
    Private Sub Connect()
        'Dim endPoint As System.Net.IPEndPoint
        'Dim IP As System.Net.IPHostEntry

        'Dim address As New System.Net.IPAddress(0)
        'If System.Net.IPAddress.TryParse(m_IPAddress, address) Then
        '    endPoint = New System.Net.IPEndPoint(address, m_Port)
        'Else
        '    Try
        '        IP = System.Net.Dns.GetHostEntry(m_IPAddress)
        '        '* Ethernet/IP uses port AF12 (44818)
        '        endPoint = New System.Net.IPEndPoint(IP.AddressList(0), m_Port)
        '    Catch ex As Exception
        '        Throw New FormatException("Can't resolve the address " & m_IPAddress)
        '        Exit Sub
        '    End Try
        'End If


        If GTCDLL(MyDLLInstance) Is Nothing Then ' OrElse Not GTCDLL(MyDLLInstance).Connected Then
            If m_ProtocolType = Net.Sockets.ProtocolType.Tcp Then
                GTCDLL(MyDLLInstance) = New SharedSocket(System.Net.Sockets.AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                '* Comment these out for Compact Framework
                GTCDLL(MyDLLInstance).SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, 5000)
                GTCDLL(MyDLLInstance).SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, True)
            Else
                GTCDLL(MyDLLInstance) = New SharedSocket(System.Net.Sockets.AddressFamily.InterNetwork, SocketType.Dgram, m_ProtocolType)
            End If

            GTCDLL(MyDLLInstance).SendTimeout = 2000
            GTCDLL(MyDLLInstance).ReceiveBufferSize = &H5000
            GTCDLL(MyDLLInstance).IPAddress = m_IPAddress
            GTCDLL(MyDLLInstance).Port = m_Port
        End If

        If Not GTCDLL(MyDLLInstance).Connected Then
            Try
                Try
                    GTCDLL(MyDLLInstance).Connect()
                Catch ex As SocketException
                    '* Return an error code
                    OnComError("Could Not Connect to Server : " & ex.Message)

                    CloseConnection()
                    Throw
                End Try


                OnConnectionEstablished(System.EventArgs.Empty)

                StartTestThread()
            Catch ex As SocketException
                ' 10035 == WSAEWOULDBLOCK
                If ex.NativeErrorCode.Equals(10035) Then
                    'Throw
                Else
                    Throw 'New Exception(m_IPAddress & " " & ex.Message)
                End If
            End Try
        End If


        GTCDLL(MyDLLInstance).Blocking = True
        If m_ProtocolType = Net.Sockets.ProtocolType.Tcp Then
            GTCDLL(MyDLLInstance).LingerState = New System.Net.Sockets.LingerOption(True, 1000)
        End If


        '* Don't buffer the data, so it goes out immediately
        '* Otherwise packets send really fast will get grouped
        '* And the PLC will not respond to all of them
        GTCDLL(MyDLLInstance).SendBufferSize = 1

        Dim so As New Common.SocketStateObject
        so.WorkSocket = GTCDLL(MyDLLInstance)

        GTCDLL(MyDLLInstance).BeginReceive(so.data, 0, so.data.Length, SocketFlags.None, AddressOf DataLinkLayerDataReceived, so)
    End Sub

    Private Sub StartTestThread()
        If TestConnectionTask Is Nothing OrElse (Not TestConnectionTask.Status = Threading.Tasks.TaskStatus.Created And
                                    Not TestConnectionTask.Status = Threading.Tasks.TaskStatus.Running And
                                    Not TestConnectionTask.Status = Threading.Tasks.TaskStatus.WaitingToRun) Then
            TestConnectionTask = System.Threading.Tasks.Task.Factory.StartNew(AddressOf TestConnection, EndTaskToken.Token)
        End If
    End Sub

    '* Runs as a background task to reconnect if connection is lost
    Private Sub TestConnection(ByVal cancelToken As Object)
        Dim token As Threading.CancellationToken = DirectCast(cancelToken, Threading.CancellationToken)

        While Not token.IsCancellationRequested
            'Console.WriteLine("Top of While")
            If m_AutoConnect Then
                If GTCDLL(MyDLLInstance) IsNot Nothing Then
                    If (GTCDLL(MyDLLInstance).Poll(2000, SelectMode.SelectRead)) And (GTCDLL(MyDLLInstance).Available = 0) Then
                        Try
                            CloseConnection()
                            Connect()
                        Catch ex As Exception
                        End Try
                        'Console.WriteLine(Now & "Open again")
                    End If
                Else
                    Try
                        Connect()
                    Catch ex As Exception
                    End Try
                End If
            End If

            token.WaitHandle.WaitOne(3000)
            'Threading.Thread.SpinWait(2000)
        End While
    End Sub

    '************************************************************
    '* Call back procedure - called when data comes back
    '* This is the procedure pointed to by the BeginWrite method
    '************************************************************
    Private ReceivedDataPacketBuilder As New List(Of Byte)
    Private ReceivedPacketString As String = ""
    Private DataReceivedLock As New Object
    Private Sub DataLinkLayerDataReceived(ByVal ar As System.IAsyncResult)
        ' Retrieve the state object and the client socket 
        ' from the asynchronous state object.
        Dim StateObject As Common.SocketStateObject = CType(ar.AsyncState, Common.SocketStateObject)


        '* If the socket was closed, then we cannot do anything
        If Not StateObject.WorkSocket.Connected Then
            Exit Sub
        End If

        '* Get the number of bytes read and add it to the state object accumulator
        Try
            '* Add the byte count to the state object
            StateObject.CurrentIndex += StateObject.WorkSocket.EndReceive(ar)
        Catch ex As Exception
            '* Return an error code
            OnComError("Socket Error : " & ex.Message)
            Exit Sub
        End Try

        SyncLock (DataReceivedLock)
            ' Console.WriteLine("DataReceived Received - Index=" & StateObject.CurrentIndex)

            '*************************************************************************************************************************
            Dim i As Integer = 0
            Dim CurrentByte As Byte

            '* No terminating characters specified so send on completed packet
            If String.IsNullOrEmpty(m_ParsedTerminators) Then
                Dim dataArray(StateObject.CurrentIndex - 2) As Byte
                Dim dataString As String = ""
                Array.Copy(StateObject.data, dataArray, StateObject.CurrentIndex - 1)
                For index2 = 0 To dataArray.Length - 1
                    If dataArray(index2) >= 32 And dataArray(index2) < 128 Then
                        dataString &= Chr(dataArray(index2))
                    End If
                Next
                OnDataReceived(New GenericTcpEventArgs(dataArray, dataString))
            Else
                While i < StateObject.CurrentIndex
                    CurrentByte = StateObject.data(i)

                    If m_ParsedTerminators = ReceivedPacketString.Substring(ReceivedPacketString.Length - m_ParsedTerminators.Length - 1, m_ParsedTerminators.Length) Then
                        Dim dataArray As Byte() = ReceivedDataPacketBuilder.ToArray

                        OnDataReceived(New GenericTcpEventArgs(dataArray, ReceivedPacketString))

                        CurrentByte = 0 'make sure last byte isn't falsely 16
                        ReceivedDataPacketBuilder.Clear()
                        ReceivedPacketString = ""
                    Else
                        ReceivedDataPacketBuilder.Add(CurrentByte)
                        '* Only add printable characters
                        If CurrentByte >= 32 And CurrentByte < 128 Then
                            ReceivedPacketString &= Chr(CurrentByte)
                        End If
                    End If

                    i += 1
                End While
            End If
        End SyncLock

        Dim so As New Common.SocketStateObject(GTCDLL(MyDLLInstance))

        GTCDLL(MyDLLInstance).BeginReceive(so.data, 0, so.data.Length, SocketFlags.None, AddressOf DataLinkLayerDataReceived, so)
    End Sub



#End Region

#Region "Public Methods"
    Public Sub CloseConnection()
        Try
            If GTCDLL(MyDLLInstance) IsNot Nothing Then
                RemoveDLLConnection(MyDLLInstance)

                'If DLL(MyDLLInstance).Connected Then
                '    Try
                '        DLL(MyDLLInstance).Shutdown(System.Net.Sockets.SocketShutdown.Send)
                '    Catch ex As Exception
                '    End Try
                '    DLL(MyDLLInstance).Close()
                '    OnConnectionClosed(System.EventArgs.Empty)
                'End If
                'If DLL(MyDLLInstance) IsNot Nothing Then
                '    DLL(MyDLLInstance).Dispose()
                '    DLL(MyDLLInstance) = Nothing
                'End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    '*********************************
    '* Send data out the tcp socket
    '*********************************
    Public Sub SendPacket(ByVal data() As Byte) 'As Boolean ' System.IAsyncResult
        '* connect if it has not been already
        If data IsNot Nothing Then
            If GTCDLL(MyDLLInstance) Is Nothing OrElse Not GTCDLL(MyDLLInstance).Connected Then
                Connect()
            End If


            Try
                GTCDLL(MyDLLInstance).Send(data, data.Length, SocketFlags.None)
            Catch ex As Exception
                CloseConnection()
                Throw
            End Try
        End If
    End Sub

    Public Sub SendString(ByVal s As String)
        Dim data() As Byte = System.Text.ASCIIEncoding.ASCII.GetBytes(s) ' & CStr((m_TerminatingCharacter)))
        SendPacket(data)
    End Sub

    Public Sub BeginInit() Implements System.ComponentModel.ISupportInitialize.BeginInit
    End Sub

    Public Sub EndInit() Implements System.ComponentModel.ISupportInitialize.EndInit
        Try
            If Not DesignMode And AutoConnect Then
                'StartTestThread()
                'Connect()
            End If
        Catch
        End Try
    End Sub
#End Region

#Region "Events"
    Protected Sub OnDataReceived(ByVal e As GenericTcpEventArgs)
        If m_synchronizationContext IsNot Nothing Then
            Try
                m_synchronizationContext.Post(AddressOf DataReceivedSync, e)
            Catch
            End Try
        Else
            RaiseEvent DataReceived(Me, e)
        End If
    End Sub

    Private Sub DataReceivedSync(ByVal e As Object)
        Try
            Dim e1 As GenericTcpEventArgs = DirectCast(e, GenericTcpEventArgs)
            RaiseEvent DataReceived(Me, e1)
        Catch ex As Exception
            'Dim dbg = 0
        End Try
    End Sub


    Protected Overridable Sub OnComError(ByVal description As String)
        RaiseEvent ComError(Me, New MessageEventArgs(description))
    End Sub

    Protected Overridable Sub OnConnectionClosed(ByVal e As EventArgs)
        RaiseEvent ConnectionClosed(Me, e)
    End Sub

    Protected Overridable Sub OnConnectionEstablished(ByVal e As EventArgs)
        RaiseEvent ConnectionEstablished(Me, e)
    End Sub
#End Region
End Class
