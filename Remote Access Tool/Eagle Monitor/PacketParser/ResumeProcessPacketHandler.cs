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
    internal class ResumeProcessPacketHandler
    {
        public ResumeProcessPacketHandler(ResumeProcessPacket resumeProcessPacket, ClientHandler clientHandler ) : base() 
        {
            try
            {
                if (resumeProcessPacket.resumed)
                {
                    clientHandler.processManagerForm.BeginInvoke((MethodInvoker)(() =>
                    {
                        clientHandler.processManagerForm.processDataGridView.Rows[resumeProcessPacket.rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(45, 45, 45);
                    }));
                }
            }
            catch { }
        }
    }
}
