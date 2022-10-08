using System;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class RemoteMousePacket : IPacket
    {
        public enum MouseTypeAction 
        {
            LEFT_DOWN =         0,
            LEFT_UP =           1,
            RIGHT_DOWN =        2,
            RIGHT_UP =          3,
            MOVE_MOUSE =        4,
            MOVE_WHEEL_UP =     5,
            MOVE_WHEEL_DOWN =   6,
            MIDDLE_UP =         7,
            MIDDLE_DOWN =       8,
            UNKNOWN =           9
        }
        public RemoteMousePacket(MouseTypeAction mouseTypeAction, int x = 0, int y = 0) : base()
        {
            this.packetType = PacketType.RM_MOUSE;

            this.mouseTypeAction = mouseTypeAction;
            this.x = x;
            this.y = y;
        }

        public string HWID { get; set; }
        public string baseIp { get; set; }
        public byte[] plugin { get; set; }
        public PacketType packetType { get; }
        public PacketState packetState { get; set; }
        public string status { get; set; }
        public string datePacketStatus { get; set; }
        public int packetSize { get; set; }

        public MouseTypeAction mouseTypeAction { get; set; }
        public int x { get; set; }  
        public int y { get; set; }
    }
}
