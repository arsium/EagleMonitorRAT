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
            try
            {
                ClientHandler.ClientHandlersList[stdOutShellSessionPacket.baseIp].clientForm.remoteShellStdOutRichTextBox.BeginInvoke((MethodInvoker)(() =>
                {
                    ClientHandler.ClientHandlersList[stdOutShellSessionPacket.baseIp].clientForm.remoteShellStdOutRichTextBox.AppendText(stdOutShellSessionPacket.shellStdOut);

                }));
            }
            catch { }
            return;

        }
    }
}
