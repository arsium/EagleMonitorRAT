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
            this.PacketType = packetType;
        }

        public RansomwareEncryptionPacket(string publicRSAServerKey, List<string> paths, bool subDirectories, bool checkExtensions) : base()
        {
            this.PacketType = PacketType.RANSOMWARE_ENCRYPTION;
            this.publicRSAServerKey = publicRSAServerKey;
            this.paths = paths;
            this.subDirectories = subDirectories;
            this.checkExtensions = checkExtensions;
        }

        public string HWID { get; set; }
        public string BaseIp { get; set; }
        public byte[] Plugin { get; set; }
        public PacketType PacketType { get; }
        public PacketState PacketState { get; set; }
        public string Status { get; set; }
        public string DatePacketStatus { get; set; }
        public int PacketSize { get; set; }

        public string publicRSAServerKey { get; set; }
        public List<string> paths { get; set; }
        public List<string> extensions { get; set; }
        public bool subDirectories { get; set; }
        public bool checkExtensions { get; set; }
        public string wallet { get; set; }
        public string msg { get; set; }
    }
}
