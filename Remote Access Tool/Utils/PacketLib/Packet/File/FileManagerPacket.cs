using System;
using System.Collections.Generic;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class FileManagerPacket : IPacket
    {
        //server
        public FileManagerPacket(string path) : base()
        {
            packetType = PacketType.FM_GET_FILES_AND_DIRS;

            this.path = path;
        }

        //client
        public FileManagerPacket(Dictionary<ushort, List<object[]>> filesAndDirs, string baseIp, string HWID) : base()
        {
            packetType = PacketType.FM_GET_FILES_AND_DIRS;
            this.baseIp = baseIp;
            this.HWID = HWID;

            this.filesAndDirs = filesAndDirs;
        }

        public string HWID { get; set; }
        public string baseIp { get; set; }
        public byte[] plugin { get; set; }
        public PacketType packetType { get; }
        public PacketState packetState { get; set; }
        public string status { get; set; }
        public string datePacketStatus { get; set; }
        public int packetSize { get; set; }

        public string path { get; set; }
        public Dictionary<ushort, List<object[]>> filesAndDirs { get; set; }
    }
}

