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
        internal static string key;
        internal static string baseIp;
        internal static string HWID;

        internal static RemoteViewerPacket remoteViewerBasePacket;
        public static void Main(LoadingAPI loadingAPI)
        {
            Launch.key = loadingAPI.key;
            Launch.baseIp = loadingAPI.baseIp;
            Launch.HWID = loadingAPI.HWID;

            switch (loadingAPI.currentPacket.packetType)
            {
                case PacketType.RM_VIEW_ON:
                    remoteViewerBasePacket = (RemoteViewerPacket)loadingAPI.currentPacket;
                    clientHandler = new ClientHandler(loadingAPI.host, key);
                    clientHandler.ConnectStart();
                    break;

                default:
                    return;
            }
            Miscellaneous.CleanMemory();
        }
    }
}
