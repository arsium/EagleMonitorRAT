using Eagle_Monitor.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor.Forms
{
    public partial class PasswordsForm : FormPattern
    {
        public string ClientHWID { get; set; }
        public string IP_Origin { get; set; }

        public PasswordsForm(string ClientHWID, string IP_Origin)
        {
            this.ClientHWID = ClientHWID;
            this.IP_Origin = IP_Origin;
            InitializeComponent();
        }

        private void PasswordsForm_Load(object sender, EventArgs e)
        {
            ControlsDrawing.Enable(passwordsListView);
            //Utilities.SetTheme(this);
            passwordsListView.ForeColor = Color.FromArgb(64, 64, 64);
            passwordsListView.BackColor = Color.White;
            ControlsDrawing.colorListViewHeader(ref passwordsListView, Color.White, Color.FromArgb(64, 64, 64), Color.FromArgb(230, 230, 230));
            passwordsListView.Scrollable = true;
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
