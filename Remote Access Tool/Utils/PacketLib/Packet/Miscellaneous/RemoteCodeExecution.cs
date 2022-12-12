using System;
using System.Collections.Generic;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class RemoteCodeExecution : IPacket
    {
        public RemoteCodeExecution(PacketType packetType, string compilerOptions, string code, List<string> references) : base()
        {
            this.PacketType = packetType;

            this.compilerOptions = compilerOptions;
            this.code = code;
            this.references = references;
        }

        public string HWID { get; set; }
        public string BaseIp { get; set; }
        public byte[] Plugin { get; set; }
        public PacketType PacketType { get; }
        public PacketState PacketState { get; set; }
        public string Status { get; set; }
        public string DatePacketStatus { get; set; }
        public int PacketSize { get; set; }

        public string compilerOptions { get; set; }
        public string code { get; set; }
        public List<string> references { get; set; }
    }
}
