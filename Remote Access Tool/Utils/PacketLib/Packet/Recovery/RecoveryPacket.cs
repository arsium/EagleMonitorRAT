using System;

namespace PacketLib.Packet
{
    [Serializable]
    public class RecoveryPacket : IPacket
    {
        public RecoveryPacket() : base()
        {
            this.PacketType = PacketType.RECOVERY_ALL;
        }

        public string HWID { get; set; }
        public string BaseIp { get; set; }
        public byte[] Plugin { get; set; }
        public PacketType PacketType { get; }
        public PacketState PacketState { get; set; }
        public string Status { get; set; }
        public string DatePacketStatus { get; set; }
        public int PacketSize { get; set; }
    }
}
