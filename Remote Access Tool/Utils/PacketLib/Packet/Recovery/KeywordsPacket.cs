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
            packetType = PacketType.RECOVERY_KEYWORDS;
        }

        //client
        public KeywordsPacket(List<object[]> keywords, string baseIp, string HWID) : base()
        {
            packetType = PacketType.RECOVERY_KEYWORDS;
            this.baseIp = baseIp;
            this.HWID = HWID;

            this.keywordsList = keywords;
        }

        public string HWID { get; set; }
        public string baseIp { get; set; }
        public byte[] plugin { get; set; }
        public PacketType packetType { get; }
        public PacketState packetState { get; set; }
        public string status { get; set; }
        public string datePacketStatus { get; set; }
        public int packetSize { get; set; }

        public List<object[]> keywordsList { get; set; }
    }
}
