using NAudio.Wave;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace EagleMonitor.Utils
{
    internal class AudioHelpers
    {
        internal WaveOut waveOut { get; set; }
        internal BufferedWaveProvider bufferedWaveProvider { get; set; }
        internal string currentFileName { get; set; }
        internal int currentOffset { get; set; }
        internal WaveFileWriter waveFileWriter { get; set; }
        internal AudioHelpers()
        {
            waveOut = new WaveOut();
            bufferedWaveProvider = new BufferedWaveProvider(new WaveFormat(44100, 1));
        }
    }
}
