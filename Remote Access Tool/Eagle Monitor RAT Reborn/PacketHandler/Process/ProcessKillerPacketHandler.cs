using Eagle_Monitor_RAT_Reborn.Network;
using PacketLib.Packet;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.PacketHandler
{
    internal class ProcessKillerPacketHandler
    {
        public ProcessKillerPacketHandler(ProcessKillerPacket packet) : base()//, ClientHandler clientHandler) : base() 
        {
            try
            {
                if (packet.killed)
                {
                    ClientHandler.ClientHandlersList[packet.baseIp].clientForm.processDataGridView.BeginInvoke((MethodInvoker)(() =>
                    {
                        ClientHandler.ClientHandlersList[packet.baseIp].clientForm.processDataGridView.Rows.Remove(ClientHandler.ClientHandlersList[packet.baseIp].clientForm.processDataGridView.Rows[packet.rowIndex]);
                    }));
                }
            }
            catch { }
            return;
            /*try
            {
                if (packet.killed)
                {
                    clientHandler.clientForm.BeginInvoke((MethodInvoker)(() =>
                    {
                        clientHandler.clientForm.processDataGridView.Rows.Remove(clientHandler.clientForm.processDataGridView.Rows[packet.rowIndex]);
                    }));
                }
            }
            catch { }*/
        }
    }
}
