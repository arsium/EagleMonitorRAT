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
            this.PacketType = PacketType.FM_SHORTCUT_PATH;
            this.shortCuts = shortCuts;
        }

        //client
        public ShortCutFileManagersPacket(Dictionary<ushort, List<object[]>> filesAndDirs, string baseIp, string HWID) : base()
        {
            PacketType = PacketType.FM_SHORTCUT_PATH;
            this.BaseIp = baseIp;
            this.HWID = HWID;

            this.filesAndDirs = filesAndDirs;
        }

        public string HWID { get; set; }
        public string BaseIp { get; set; }
        public byte[] Plugin { get; set; }
        public PacketType PacketType { get; }
        public PacketState PacketState { get; set; }
        public string Status { get; set; }
        public string DatePacketStatus { get; set; }
        public int PacketSize { get; set; }

        public string path { get; set; }
        public ShortCuts shortCuts { get; set; }
        public Dictionary<ushort, List<object[]>> filesAndDirs { get; set; }
    }
}
