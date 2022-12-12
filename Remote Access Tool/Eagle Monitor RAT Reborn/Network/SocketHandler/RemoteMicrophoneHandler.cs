using NAudio.Wave;
using System;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.Network
{
    internal class RemoteMicrophoneHandler : IDisposable
    {
        internal RemoteMicrophoneHandler()
        {
            WaveOut = new WaveOut();
            BufferedWaveProvider = new BufferedWaveProvider(new WaveFormat(44100, 1));
        }
        internal ClientHandler ClientHandler { get; set; }
        internal WaveOut WaveOut { get; set; }
        internal BufferedWaveProvider BufferedWaveProvider { get; set; }
        internal string CurrentFileName { get; set; }
        internal int CurrentOffset { get; set; }
        internal WaveFileWriter WaveFileWriter { get; set; }
        internal bool HasAlreadyConnected { get; set; }
        public void Dispose() 
        {
            ClientHandler.Socket.Close();
            if (ClientHandler.Socket != null)
            {
                ClientHandler.Socket.Dispose();
                ClientHandler.Socket = null;
                ClientHandler = null;
            }

            this.WaveFileWriter.Close();
            this.BufferedWaveProvider.ClearBuffer();
            this.CurrentFileName = "";
        }
    }
}
