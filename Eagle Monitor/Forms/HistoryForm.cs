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
    public partial class HistoryForm : FormPattern
    {
        public string ClientHWID { get; set; }
        public string IP_Origin { get; set; }

        public HistoryForm(string ClientHWID, string IP_Origin)
        {
            this.ClientHWID = ClientHWID;
            this.IP_Origin = IP_Origin;
            InitializeComponent();
        }

        private void HistoryForm_Load(object sender, EventArgs e)
        {
            ControlsDrawing.Enable(historyListView);
            //Utilities.SetTheme(this);
            historyListView.ForeColor = Color.FromArgb(64, 64, 64);
            historyListView.BackColor = Color.White;
            ControlsDrawing.colorListViewHeader(ref historyListView, Color.White, Color.FromArgb(64, 64, 64), Color.FromArgb(230, 230, 230));
            historyListView.Scrollable = true;
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
