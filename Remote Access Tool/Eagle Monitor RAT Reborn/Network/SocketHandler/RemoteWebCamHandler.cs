using System;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.Network
{
    internal class RemoteWebCamHandler : IDisposable
    {
        internal ClientHandler ClientHandler { get; set; }
        internal bool HasAlreadyConnected { get; set; }
        //internal int vResol { get; set; }
        //internal int hResol { get; set; }
        internal string BaseIp { get; set; }

        public void Dispose()
        {
            ClientHandler.Socket.Close();
            if (ClientHandler.Socket != null)
            {
                ClientHandler.Socket.Dispose();
                ClientHandler.Socket = null;
                ClientHandler = null;
            }
        }
    }
}
