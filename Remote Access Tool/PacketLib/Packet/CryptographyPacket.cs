using PacketLib.Utils;
using System;
using System.Collections.Generic;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class CryptographyPacket : IPacket
    {
        public CryptographyPacket(PacketType packetType) : base()
        {
            this.packetType = packetType;
        }

        public CryptographyPacket(PacketType packetType, string key, int keySize, Dictionary<string, bool> filesPath) : base()
        {
            this.packetType = packetType;
            this.key = key;
            this.keySize = keySize;
            this.filesPath = filesPath;
            this.isPathAFolder = false;
        }

        public CryptographyPacket(PacketType packetType, string key, int keySize, string path) : base()
        {
            this.packetType = packetType;
            this.key = key;
            this.keySize = keySize;
            this.path = path;
            this.isPathAFolder = true;
        }

        public string HWID { get; set; }
        public string baseIp { get; set; }
        public byte[] plugin { get; set; }
        public PacketType packetType { get; }
        public string status { get; set; }
        public string datePacketStatus { get; set; }

        public Algorithm algorithm { get; set; }
        public string key { get; set; }
        public int keySize { get; set; }
        //for files in a dir
        public Dictionary<string, bool> filesPath { get; set; }
        //For folder or single file
        public string path { get; set; }
        public bool isPathAFolder { get; set; }
        public bool success { get; set; }
    }
}
