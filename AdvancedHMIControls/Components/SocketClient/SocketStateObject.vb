Option Strict On
Imports System.Net.Sockets
'******************************************************************************
'* Socket Communication State Object
'*
'* Archie Jacobs
'* Manufacturing Automation, LLC
'* support@advancedhmi.com
'* 20-JAN-18
'*
'* Copyright 2018 Archie Jacobs
'*
'* This class hold data returned from TCP socket communications
'* It's purpose is to accumulate data when all data is not recieved
'*  from a single DataReceived event
'******************************************************************************
Namespace Common
    Public Class SocketStateObject

#Region "Properties"
        Private m_workSocket As Socket
        Public Property WorkSocket() As Socket
            Get
                Return m_workSocket
            End Get
            Set(ByVal value As Socket)
                m_workSocket = value
            End Set
        End Property

        '*********************************
        '* The received data byte stream
        '*********************************
        Friend data(4095) As Byte

        '**********************************
        '* Current Index within data array
        '**********************************
        Private m_CurrentIndex As Integer
        Public Property CurrentIndex() As Integer
            Get
                Return m_CurrentIndex
            End Get
            Set(ByVal value As Integer)
                If value >= data.Length Then
                    Throw New ArgumentException("TCP State object can only hold up to 4096 bytes")
                    Exit Property
                End If
                m_CurrentIndex = value
            End Set
        End Property
#End Region

#Region "Constructors"
        Public Sub New()
        End Sub

        Public Sub New(workSocket As Socket)
            m_workSocket = workSocket
        End Sub
#End Region

    End Class
End Namespace

