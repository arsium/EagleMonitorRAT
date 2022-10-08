using Eagle_Monitor_RAT_Reborn.Network;
using PacketLib.Packet;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.PacketHandler
{
    internal class RestorePointPacketHandler
    {
        public RestorePointPacketHandler(RestorePointPacket restorePointPacket) : base()//, ClientHandler clientHandler)
        {
            if (restorePointPacket.restorePoints != null)
            {
                try
                {
                    ClientHandler.ClientHandlersList[restorePointPacket.baseIp].clientForm.restorePointDataGridView.BeginInvoke((MethodInvoker)(() =>
                    {
                        ClientHandler.ClientHandlersList[restorePointPacket.baseIp].clientForm.restorePointDataGridView.Rows.Clear();
                        foreach (RestorePoint restorePoint in restorePointPacket.restorePoints)
                        {
                            int rowId = ClientHandler.ClientHandlersList[restorePointPacket.baseIp].clientForm.restorePointDataGridView.Rows.Add();
                            DataGridViewRow row = ClientHandler.ClientHandlersList[restorePointPacket.baseIp].clientForm.restorePointDataGridView.Rows[rowId];
                            row.Tag = restorePoint.index;
                            row.Cells["Column42"].Value = restorePoint.index.ToString();
                            row.Cells["Column43"].Value = restorePoint.description;
                            row.Cells["Column44"].Value = restorePoint.type.ToString();
                            row.Cells["Column45"].Value = restorePoint.creationTime;
                        }
                    }));
                }
                catch { }
            }
            else
                return;
            /*if (restorePointPacket.restorePoints != null)
            {
                try
                {
                    ClientHandler.ClientHandlersList[restorePointPacket.baseIp].clientForm.restorePointDataGridView.BeginInvoke((MethodInvoker)(() =>
                    {
                        ClientHandler.ClientHandlersList[restorePointPacket.baseIp].clientForm.restorePointDataGridView.Rows.Clear();
                        foreach (RestorePoint restorePoint in restorePointPacket.restorePoints)
                        {
                            int rowId = ClientHandler.ClientHandlersList[restorePointPacket.baseIp].clientForm.restorePointDataGridView.Rows.Add();
                            DataGridViewRow row = ClientHandler.ClientHandlersList[restorePointPacket.baseIp].clientForm.restorePointDataGridView.Rows[rowId];
                            row.Tag = restorePoint.index;
                            row.Cells["Column42"].Value = restorePoint.index.ToString();
                            row.Cells["Column43"].Value = restorePoint.description;
                            row.Cells["Column44"].Value = restorePoint.type.ToString();
                            row.Cells["Column45"].Value = restorePoint.creationTime;
                        }
                    }));
                }
                catch {  }
            }
            else
                return;*/
        }
    }
}
