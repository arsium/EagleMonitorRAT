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
            this.packetType = packetType;

            this.compilerOptions = compilerOptions;
            this.code = code;
            this.references = references;
        }

        public string HWID { get; set; }
        public string baseIp { get; set; }
        public byte[] plugin { get; set; }
        public PacketType packetType { get; }
        public PacketState packetState { get; set; }
        public string status { get; set; }
        public string datePacketStatus { get; set; }
        public int packetSize { get; set; }

        public string compilerOptions { get; set; }
        public string code { get; set; }
        public List<string> references { get; set; }
    }
}
