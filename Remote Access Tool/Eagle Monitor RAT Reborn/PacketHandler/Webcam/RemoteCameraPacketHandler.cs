using Eagle_Monitor_RAT_Reborn.Network;
using PacketLib.Packet;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.PacketHandler
{
    internal class RemoteCameraPacketHandler
    {
        public RemoteCameraPacketHandler(RemoteCameraPacket remoteCameraPacket) : base()//, ClientHandler clientHandler) : base() 
        {
            if (ClientHandler.ClientHandlersList[remoteCameraPacket.BaseIp].ClientForm != null)
            {
                try
                {
                    ClientHandler.ClientHandlersList[remoteCameraPacket.BaseIp].ClientForm.camerasGuna2ComboBox.BeginInvoke((MethodInvoker)(() =>
                    {
                        foreach (string camera in remoteCameraPacket.cameras)
                        {
                            ClientHandler.ClientHandlersList[remoteCameraPacket.BaseIp].ClientForm.camerasGuna2ComboBox.Items.Add(camera);
                        }

                        if (remoteCameraPacket.cameras.Count > 0)
                            ClientHandler.ClientHandlersList[remoteCameraPacket.BaseIp].ClientForm.camerasGuna2ComboBox.SelectedIndex = 0;
                    }));
                }
                catch { }
                return;
            }
        }
    }
}
