using Eagle_Monitor_RAT_Reborn.Network;
using PacketLib.Packet;
using System.Drawing;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.PacketHandler
{
    internal class ChatPacketHandler
    {
        public ChatPacketHandler(RemoteChatPacket chatPacket): base()
        {
            ClientHandler.ClientHandlersList[chatPacket.BaseIp].ClientForm.messageRichTextBox.BeginInvoke((MethodInvoker)(() =>
            {
                ClientHandler.ClientHandlersList[chatPacket.BaseIp].ClientForm.messageRichTextBox.SelectionColor = Color.FromArgb(66, 182, 245);
                ClientHandler.ClientHandlersList[chatPacket.BaseIp].ClientForm.messageRichTextBox.AppendText(chatPacket.msg);
            }));
        }
    }
}
