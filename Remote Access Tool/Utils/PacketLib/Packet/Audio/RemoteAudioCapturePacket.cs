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
            this.packetType = packetType;
        }

        public RemoteAudioCapturePacket(byte[] audioCapture, int bytesRecorded) : base()
        {
            this.packetType = PacketType.AUDIO_RECORD_ON;
            this.audioCapture = audioCapture;
            this.bytesRecorded = bytesRecorded;
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
        public byte[] audioCapture { get; set; }
        public int bytesRecorded { get; set; }
    }
}
