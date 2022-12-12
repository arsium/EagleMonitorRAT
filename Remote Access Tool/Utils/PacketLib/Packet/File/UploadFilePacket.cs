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
            this.PacketType = PacketType.FM_UPLOAD_FILE;
            this.path = path;
            this.file = file;
        }

        //client
        public UploadFilePacket(string fileName, bool uploaded, string baseIp, string HWID) : base()
        {
            this.PacketType = PacketType.FM_UPLOAD_FILE;
            this.BaseIp = baseIp;
            this.HWID = HWID;
            this.path = fileName;
            this.uploaded = uploaded;
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
        public bool uploaded { get; set; }
        public byte[] file { get; set; }
    }
}
