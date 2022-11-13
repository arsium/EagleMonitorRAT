using System;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class StartShellSessionPacket : IPacket
    {
        public StartShellSessionPacket(bool isPWS) : base()
        {
            this.packetType = PacketType.SHELL_START;
            this.isPWS = isPWS;
        }

        public string HWID { get; set; }
        public string baseIp { get; set; }
        public byte[] plugin { get; set; }
        public PacketType packetType { get; }
        public PacketState packetState { get; set; }
        public string status { get; set; }
        public string datePacketStatus { get; set; }
        public int packetSize { get; set; }
        public bool isPWS { get; set; }
    }
}
