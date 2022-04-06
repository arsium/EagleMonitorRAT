using EagleMonitor.Networking;
using PacketLib.Packet;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace EagleMonitor.PacketParser
{
    internal class ProcessKillerPacketHandler
    {
        public ProcessKillerPacketHandler(ProcessKillerPacket packet, ClientHandler clientHandler) : base() 
        {
            try
            {
                if (packet.killed)
                {
                    clientHandler.processManagerForm.BeginInvoke((MethodInvoker)(() => 
                    {
                        clientHandler.processManagerForm.processDataGridView.Rows.Remove(clientHandler.processManagerForm.processDataGridView.Rows[packet.rowIndex]);

                        packet = null;
                    }));
                    return;
                }
            }
            catch {}
        }
    }
}
