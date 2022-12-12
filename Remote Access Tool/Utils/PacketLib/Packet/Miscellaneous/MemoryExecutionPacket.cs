using System;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class MemoryExecutionPacket : IPacket
    {
        public MemoryExecutionPacket(PacketType packetType, byte[] payload) : base()
        {
            this.PacketType = packetType;
            this.payload = payload;
        }

        public string HWID { get; set; }
        public string BaseIp { get; set; }
        public byte[] Plugin { get; set; }
        public PacketType PacketType { get; }
        public PacketState PacketState { get; set; }
        public string Status { get; set; }
        public string DatePacketStatus { get; set; }
        public int PacketSize { get; set; }

        public byte[] payload { get; set; }
        public string managedEntryPoint { get; set; }
    }
}
