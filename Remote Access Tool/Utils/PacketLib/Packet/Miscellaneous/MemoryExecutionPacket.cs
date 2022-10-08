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
            this.packetType = packetType;
            this.payload = payload;
        }

        public string HWID { get; set; }
        public string baseIp { get; set; }
        public byte[] plugin { get; set; }
        public PacketType packetType { get; }
        public PacketState packetState { get; set; }
        public string status { get; set; }
        public string datePacketStatus { get; set; }
        public int packetSize { get; set; }

        public byte[] payload { get; set; }
        public string managedEntryPoint { get; set; }
    }
}
