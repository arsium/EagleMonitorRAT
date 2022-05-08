using PacketLib;
using PacketLib.Packet;
using PacketLib.Utils;
using System.Threading;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    public static class Launch
    {
        internal static ClientHandler clientHandler;

        internal static ChatForm chatForm;
        internal static Thread threadForm;

        public static void Main(LoadingAPI loadingAPI)
        {
            switch (loadingAPI.currentPacket.packetType)
            {
                case PacketType.CHAT_ON:
                    clientHandler = new ClientHandler(loadingAPI.host, loadingAPI.key, loadingAPI.baseIp, loadingAPI.HWID);
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
            Launch.chatForm.ShowDialog();
        }

        internal static void ExitChatForm() 
        {
            clientHandler.Dispose();
            chatForm.Invoke((MethodInvoker)(() =>
            {
                chatForm?.Close();
                chatForm?.Dispose();
            }));
        }
    }
}
