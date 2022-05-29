using EagleMonitor.Networking;
using PacketLib.Packet;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace EagleMonitor.PacketParser
{
    internal class RestorePointPacketHandler
    {
        public RestorePointPacketHandler(RestorePointPacket restorePointPacket, ClientHandler clientHandler)
        {
            if (restorePointPacket.restorePoints != null)
            {
                try
                {
                    if (clientHandler.restorePointForm.dataGridView1 != null)
                    {
                        clientHandler.restorePointForm.dataGridView1.BeginInvoke((MethodInvoker)(() =>
                        {
                            clientHandler.restorePointForm.dataGridView1.Rows.Clear();
                            foreach (RestorePoint restorePoint in restorePointPacket.restorePoints)
                            {
                                int rowId = clientHandler.restorePointForm.dataGridView1.Rows.Add();
                                DataGridViewRow row = clientHandler.restorePointForm.dataGridView1.Rows[rowId];
                                row.Tag = restorePoint.index;
                                row.Cells["Column1"].Value = restorePoint.index.ToString();
                                row.Cells["Column2"].Value = restorePoint.description;
                                row.Cells["Column3"].Value = restorePoint.type.ToString();
                                row.Cells["Column4"].Value = restorePoint.creationTime;
                            }
                        }));
                    }
                }
                catch {  }
            }
            else
                return;
        }
    }
}
