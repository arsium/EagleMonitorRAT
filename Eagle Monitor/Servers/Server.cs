using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor
{
    public class Server : IDisposable
    {
        public static List<Server> ServerList = new List<Server>();
        public static bool Launched = false;
        public Socket S;
        private int Port { get; set; }
        public Server(int Port)
        {
            this.Port = Port;
            Task.Run(() => AcceptClient());
        }
        private void AcceptClient()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, Port);
            S = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            S.ReceiveBufferSize = Shared.Utils.BufferSize;
            S.SendBufferSize = Shared.Utils.BufferSize;
            /*
             S = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
             S.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.IPv6Only, false);
             */
            S.Bind(ep);
            S.Listen(int.MaxValue);
            while (Launched == true)
            {
                try
                {

                    Clients.Client C = new Clients.Client(S.Accept());
                    C.S.ReceiveBufferSize = Shared.Utils.BufferSize;
                    C.S.SendBufferSize = Shared.Utils.BufferSize;
                    C.Port = this.Port;
                }
                catch (Exception)
                {}
             }
        }

        private bool _disposed = false;

        // Instantiate a SafeHandle instance.
        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose() => Dispose(true);

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // Dispose managed state (managed objects).
                _safeHandle?.Dispose();
            }

            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
