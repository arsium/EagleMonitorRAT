using System;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class KeylogPacket : IPacket
    {
        public KeylogPacket() : base()
        {
            packetType = PacketType.KEYLOG_ON;
        }

        public KeylogPacket(string keyStroke, string baseIp, string HWID) : base()
        {
    
            packetType = PacketType.KEYLOG_ON;
            this.baseIp = baseIp;
            this.HWID = HWID;
            
            this.keyStroke = keyStroke;
        }

        public KeylogPacket(string baseIp, string HWID) : base()
        {
            packetType = PacketType.KEYLOG_OFF;
            this.baseIp = baseIp;
            this.HWID = HWID;
        }

        public string HWID { get; set; }
        public string baseIp { get; set; }
        public byte[] plugin { get; set; }
        public PacketType packetType { get; }
        public PacketState packetState { get; set; }
        public string status { get; set; }
        public string datePacketStatus { get; set; }
        public int packetSize { get; set; }

        public string keyStroke { get; set; }
    }
}
