using EagleMonitor.Networking;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace EagleMonitor
{
    internal class Server : IDisposable
    {
        private readonly static List<Server> Servers;
        internal static int CurrentServersNumber { get { return Servers.Count; } }

        static Server() 
        {
            Servers = new List<Server>();
        }

        internal bool stopServer { get; set; }
        private int serverPort { get; set; }
        private Socket socket;
        //private delegate Task <ClientHandler> AcceptClient();
        private delegate ClientHandler AcceptClient();
        private readonly AcceptClient acceptClient;
        internal Server() : base()
        {
            this.stopServer = false;
            Servers.Add(this);
            acceptClient = new AcceptClient(BeginClientAccepted);
        }

        internal bool Listen(int port)
        {
            serverPort = port;
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
            socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                socket.Bind(endPoint);
                socket.Listen(int.MaxValue);
                AcceptClientAsync();      
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        internal void AcceptClientAsync()
        {
            acceptClient.BeginInvoke(new AsyncCallback(EndAcceptedClient), null);
        }

        private ClientHandler BeginClientAccepted()
        {
            return new ClientHandler(socket.Accept(), this.serverPort);
        }

        private void EndAcceptedClient(IAsyncResult ar) 
        {
            acceptClient.EndInvoke(ar);
            if (!stopServer)
                AcceptClientAsync();
            else
            {
                this.Dispose();
                return;
            }
        }

        public void Dispose()
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            socket.Dispose();
        }
    }
}
