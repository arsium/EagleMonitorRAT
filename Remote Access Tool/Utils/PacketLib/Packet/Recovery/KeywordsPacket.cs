using System;
using System.Collections.Generic;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class KeywordsPacket : IPacket
    {
        //server
        public KeywordsPacket() : base()
        {
            PacketType = PacketType.RECOVERY_KEYWORDS;
        }

        //client
        public KeywordsPacket(List<object[]> keywords, string baseIp, string HWID) : base()
        {
            PacketType = PacketType.RECOVERY_KEYWORDS;
            this.BaseIp = baseIp;
            this.HWID = HWID;

            this.keywordsList = keywords;
        }

        public string HWID { get; set; }
        public string BaseIp { get; set; }
        public byte[] Plugin { get; set; }
        public PacketType PacketType { get; }
        public PacketState PacketState { get; set; }
        public string Status { get; set; }
        public string DatePacketStatus { get; set; }
        public int PacketSize { get; set; }

        public List<object[]> keywordsList { get; set; }
    }
}
