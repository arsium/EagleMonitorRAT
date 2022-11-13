using Eagle_Monitor_RAT_Reborn.Network;
using PacketLib.Packet;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.PacketHandler
{
    internal class RemoteStartShellPacketHandler
    {
        public RemoteStartShellPacketHandler(StartShellSessionPacket startShellSessionPacket) : base()
        {
            try
            {
                ClientHandler.ClientHandlersList[startShellSessionPacket.baseIp].clientForm.remoteShellGuna2ToggleSwitch.Enabled = false;
                ClientHandler.ClientHandlersList[startShellSessionPacket.baseIp].clientForm.remoteShellGuna2TextBox.Enabled = true;
                ClientHandler.ClientHandlersList[startShellSessionPacket.baseIp].clientForm.remoteShellStdOutRichTextBox.BeginInvoke((MethodInvoker)(() =>
                {
                    ClientHandler.ClientHandlersList[startShellSessionPacket.baseIp].clientForm.remoteShellStdOutRichTextBox.Text += "\n-------------New session started !-------------\n";

                }));
            }
            catch { }
            return;

        }
    }
}
