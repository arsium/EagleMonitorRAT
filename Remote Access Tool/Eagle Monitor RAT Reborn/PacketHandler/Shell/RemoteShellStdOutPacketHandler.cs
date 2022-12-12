using Eagle_Monitor_RAT_Reborn.Network;
using PacketLib.Packet;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.PacketHandler
{
    internal class RemoteShellStdOutPacketHandler
    {
        public RemoteShellStdOutPacketHandler(StdOutShellSessionPacket stdOutShellSessionPacket) : base()
        {
            if (ClientHandler.ClientHandlersList[stdOutShellSessionPacket.BaseIp].ClientForm != null)
            {
                try
                {
                    ClientHandler.ClientHandlersList[stdOutShellSessionPacket.BaseIp].ClientForm.remoteShellStdOutRichTextBox.BeginInvoke((MethodInvoker)(() =>
                    {
                        ClientHandler.ClientHandlersList[stdOutShellSessionPacket.BaseIp].ClientForm.remoteShellStdOutRichTextBox.AppendText(stdOutShellSessionPacket.shellStdOut);

                    }));
                }
                catch { }
                return;
            }

        }
    }
}
