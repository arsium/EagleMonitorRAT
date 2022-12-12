using System;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class RansomwareConfirmationPacket : IPacket
    {
        public RansomwareConfirmationPacket(PacketType packetType, string results, string baseIp, string HWID) : base()
        {
            this.PacketType = packetType;
            this.results = results;

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

        public string results { get; set; }
    }
}
