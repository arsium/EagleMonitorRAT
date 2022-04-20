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
                            try
                            {
                                clientHandler.fileManagerForm.fileListView.Items[deleteFilePacket.name].Remove();
                            }
                            catch { }
                        }));
                    }
                }
                catch { }
            }).Start(); 
        }
    }
}
