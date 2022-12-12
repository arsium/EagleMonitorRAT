using System;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.Network
{
    internal class KeyloggerHandler : IDisposable
    {
        internal ClientHandler ClientHandler { get; set; }
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
