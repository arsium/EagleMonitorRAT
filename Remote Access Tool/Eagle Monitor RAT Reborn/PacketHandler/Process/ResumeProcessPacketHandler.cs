using Eagle_Monitor_RAT_Reborn.Network;
using PacketLib.Packet;
using System.Drawing;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.PacketHandler
{
    internal class ResumeProcessPacketHandler
    {
        public ResumeProcessPacketHandler(ResumeProcessPacket resumeProcessPacket) : base ()//, ClientHandler clientHandler ) : base() 
        {
            try
            {
                if (resumeProcessPacket.resumed)
                {
                    ClientHandler.ClientHandlersList[resumeProcessPacket.baseIp].clientForm.processDataGridView.BeginInvoke((MethodInvoker)(() =>
                    {
                        ClientHandler.ClientHandlersList[resumeProcessPacket.baseIp].clientForm.processDataGridView.Rows[resumeProcessPacket.rowIndex].DefaultCellStyle.BackColor = Color.White;
                    }));
                }
            }
            catch { }
            return;
            /*try
            {
                if (resumeProcessPacket.resumed)
                {
                    clientHandler.clientForm.BeginInvoke((MethodInvoker)(() =>
                    {
                        clientHandler.clientForm.processDataGridView.Rows[resumeProcessPacket.rowIndex].DefaultCellStyle.BackColor = Color.White;
                    }));
                }
            }
            catch { }*/
        }
    }
}
