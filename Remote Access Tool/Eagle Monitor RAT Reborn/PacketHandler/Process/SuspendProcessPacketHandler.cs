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
    internal class SuspendProcessPacketHandler
    {
        public SuspendProcessPacketHandler(SuspendProcessPacket suspendProcessPacket) : base()//, ClientHandler clientHandler) 
        {
            try
            {
                if (suspendProcessPacket.suspended)
                {
                    ClientHandler.ClientHandlersList[suspendProcessPacket.BaseIp].ClientForm.processDataGridView.BeginInvoke((MethodInvoker)(() =>
                    {
                        ClientHandler.ClientHandlersList[suspendProcessPacket.BaseIp].ClientForm.processDataGridView.Rows[suspendProcessPacket.rowIndex].DefaultCellStyle.BackColor = Color.Red;
                    }));
                }
            }
            catch { }
            return;
            /*try
            {
                if (suspendProcessPacket.suspended)
                {
                    clientHandler.clientForm.BeginInvoke((MethodInvoker)(() =>
                    {
                        clientHandler.clientForm.processDataGridView.Rows[suspendProcessPacket.rowIndex].DefaultCellStyle.BackColor = Color.Red;
                    }));
                }
            }
            catch { }*/
        }
    }
}
