using System;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class RemoteAudioCapturePacket : IPacket
    {
        public RemoteAudioCapturePacket(PacketType packetType) : base()
        {
            this.PacketType = packetType;
        }

        public RemoteAudioCapturePacket(byte[] audioCapture, int bytesRecorded) : base()
        {
            this.PacketType = PacketType.AUDIO_RECORD_ON;
            this.audioCapture = audioCapture;
            this.bytesRecorded = bytesRecorded;
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
        public byte[] audioCapture { get; set; }
        public int bytesRecorded { get; set; }
    }
}
