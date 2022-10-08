using Eagle_Monitor_RAT_Reborn.Network;
using PacketLib.Packet;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.PacketHandler
{
    internal class RemoteAudioPacketHandler
    {
        public RemoteAudioPacketHandler(RemoteAudioPacket remoteAudioPacket) : base()//, ClientHandler clientHandler) : base()
        {

            try
            {
                ClientHandler.ClientHandlersList[remoteAudioPacket.baseIp].clientForm.audioDevicesGuna2ComboBox.BeginInvoke((MethodInvoker)(() =>
                {
                    foreach (string device in remoteAudioPacket.audioDevices)
                    {
                        ClientHandler.ClientHandlersList[remoteAudioPacket.baseIp].clientForm.audioDevicesGuna2ComboBox.Items.Add(device);
                    }

                    if (remoteAudioPacket.audioDevices.Count > 0)
                        ClientHandler.ClientHandlersList[remoteAudioPacket.baseIp].clientForm.audioDevicesGuna2ComboBox.SelectedIndex = 0;
                }));
            }
            catch { }
            return;
            /*new Thread(() =>
            {
                try
                {
                    ClientHandler.ClientHandlersList[remoteAudioPacket.baseIp].clientForm.BeginInvoke((MethodInvoker)(() =>
                    {
                        foreach (string device in remoteAudioPacket.audioDevices)
                        {
                            ClientHandler.ClientHandlersList[remoteAudioPacket.baseIp].clientForm.audioDevicesGuna2ComboBox.Items.Add(device);
                        }

                        if (remoteAudioPacket.audioDevices.Count > 0)
                            ClientHandler.ClientHandlersList[remoteAudioPacket.baseIp].clientForm.audioDevicesGuna2ComboBox.SelectedIndex = 0;
                    }));
                }
                catch { }
            }).Start();*/
        }
    }
}
