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
        public ChatPacketHandler(RemoteChatPacket chatPacket): base()//, ClientHandler clientHandler) : base()
        {
            ClientHandler.ClientHandlersList[chatPacket.baseIp].clientForm.messageRichTextBox.BeginInvoke((MethodInvoker)(() =>
            {
                ClientHandler.ClientHandlersList[chatPacket.baseIp].clientForm.messageRichTextBox.SelectionColor = Color.FromArgb(66, 182, 245);
                ClientHandler.ClientHandlersList[chatPacket.baseIp].clientForm.messageRichTextBox.AppendText(chatPacket.msg);
            }));
            /*ClientHandler.ClientHandlersList[chatPacket.baseIp].clientForm.messageRichTextBox.BeginInvoke((MethodInvoker)(() =>
            {
                ClientHandler.ClientHandlersList[chatPacket.baseIp].clientForm.messageRichTextBox.SelectionColor = Color.FromArgb(66, 182, 245);
                ClientHandler.ClientHandlersList[chatPacket.baseIp].clientForm.messageRichTextBox.AppendText(chatPacket.msg);
            }));*/
        }
    }
}
