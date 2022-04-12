using System;

namespace PacketLib.Packet
{
    [Serializable]
    public class BaseIpPacket : IPacket
    {
        public BaseIpPacket(string baseIp) : base()
        {
            this.packetType = PacketType.CONNECTED;
            this.baseIp = baseIp;
        }

        public string HWID { get; set; }
        public string baseIp { get; set; }
        public byte[] plugin { get; set; }
        public PacketType packetType { get; }
        public string status { get; set; }
        public string datePacketStatus { get; set; }
    }
}
