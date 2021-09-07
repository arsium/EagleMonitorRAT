using Eagle_Monitor.Controls;
using System;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor.Forms
{
    public partial class WifiForm : FormPattern
    {
        public string ClientHWID { get; set; }
        public string IP_Origin { get; set; }

        public WifiForm(string ClientHWID, string IP_Origin)
        {
            this.ClientHWID = ClientHWID;
            this.IP_Origin = IP_Origin;
            InitializeComponent();
        }

        private void WifiForm_Load(object sender, EventArgs e)
        {
            ControlsDrawing.Enable(wifiPasswordsListView);
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
    }
}
