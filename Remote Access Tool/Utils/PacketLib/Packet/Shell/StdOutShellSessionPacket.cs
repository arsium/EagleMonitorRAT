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
            this.PacketType = PacketType.SHELL_COMMAND;
            this.shellStdOut = shellStdOut;   
        }

        public string HWID { get; set; }
        public string BaseIp { get; set; }
        public byte[] Plugin { get; set; }
        public PacketType PacketType { get; }
        public PacketState PacketState { get; set; }
        public string Status { get; set; }
        public string DatePacketStatus { get; set; }
        public int PacketSize { get; set; }

        public string shellStdOut { get; set; }
    }
}
