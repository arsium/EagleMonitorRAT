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
            if (ClientHandler.ClientHandlersList[startShellSessionPacket.BaseIp].ClientForm != null)
            {
                try
                {
                    ClientHandler.ClientHandlersList[startShellSessionPacket.BaseIp].ClientForm.remoteShellGuna2ToggleSwitch.Enabled = false;
                    ClientHandler.ClientHandlersList[startShellSessionPacket.BaseIp].ClientForm.remoteShellGuna2TextBox.Enabled = true;
                    ClientHandler.ClientHandlersList[startShellSessionPacket.BaseIp].ClientForm.remoteShellStdOutRichTextBox.BeginInvoke((MethodInvoker)(() =>
                    {
                        ClientHandler.ClientHandlersList[startShellSessionPacket.BaseIp].ClientForm.remoteShellStdOutRichTextBox.Text += "\n-------------New session started !-------------\n";

                    }));
                }
                catch { }
                return;
            }
        }
    }
}
