using EagleMonitor.Networking;
using PacketLib.Packet;
using System.Threading;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace EagleMonitor.PacketParser
{
    internal class DeleteFilePacketHandler
    {
        public DeleteFilePacketHandler(DeleteFilePacket deleteFilePacket, ClientHandler clientHandler) : base() 
        {
            new Thread(() =>
            {
                try
                {
                    if (deleteFilePacket.deleted)
                    {
                        clientHandler.fileManagerForm.BeginInvoke((MethodInvoker)(() =>
                        {
                            clientHandler.fileManagerForm.fileListView.Items[deleteFilePacket.name].Remove();

                            deleteFilePacket = null;
                        }));
                    }
                }
                catch { }
            }).Start(); 
        }
    }
}
