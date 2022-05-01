namespace EagleMonitor.Forms
{
    partial class RemoteChatForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RemoteChatForm));
            this.messageRichTextBox = new System.Windows.Forms.RichTextBox();
            this.sendMsgGuna2Button = new Guna.UI2.WinForms.Guna2Button();
            this.stopGuna2Button = new Guna.UI2.WinForms.Guna2Button();
            this.startGuna2Button = new Guna.UI2.WinForms.Guna2Button();
            this.closeButton = new Guna.UI2.WinForms.Guna2Button();
            this.maximizeButton = new Guna.UI2.WinForms.Guna2Button();
            this.minimizeButton = new Guna.UI2.WinForms.Guna2Button();
            this.messageGuna2TextBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // messageRichTextBox
            // 
            this.messageRichTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.messageRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.messageRichTextBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messageRichTextBox.Location = new System.Drawing.Point(6, 107);
            this.messageRichTextBox.Name = "messageRichTextBox";
            this.messageRichTextBox.ReadOnly = true;
            this.messageRichTextBox.Size = new System.Drawing.Size(551, 174);
            this.messageRichTextBox.TabIndex = 12;
            this.messageRichTextBox.Text = "";
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
            this.sendMsgGuna2Button.Location = new System.Drawing.Point(281, 38);
            this.sendMsgGuna2Button.Name = "sendMsgGuna2Button";
            this.sendMsgGuna2Button.ShadowDecoration.Enabled = true;
            this.sendMsgGuna2Button.ShadowDecoration.Parent = this.sendMsgGuna2Button;
            this.sendMsgGuna2Button.Size = new System.Drawing.Size(276, 32);
            this.sendMsgGuna2Button.TabIndex = 8;
            this.sendMsgGuna2Button.Text = "Send message";
            this.sendMsgGuna2Button.Click += new System.EventHandler(this.sendMsgGuna2Button_Click);
            // 
            // stopGuna2Button
            // 
            this.stopGuna2Button.Animated = true;
            this.stopGuna2Button.CheckedState.Parent = this.stopGuna2Button;
            this.stopGuna2Button.CustomImages.Parent = this.stopGuna2Button;
            this.stopGuna2Button.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stopGuna2Button.ForeColor = System.Drawing.Color.White;
            this.stopGuna2Button.HoverState.Parent = this.stopGuna2Button;
            this.stopGuna2Button.Location = new System.Drawing.Point(144, 38);
            this.stopGuna2Button.Name = "stopGuna2Button";
            this.stopGuna2Button.ShadowDecoration.Enabled = true;
            this.stopGuna2Button.ShadowDecoration.Parent = this.stopGuna2Button;
            this.stopGuna2Button.Size = new System.Drawing.Size(131, 32);
            this.stopGuna2Button.TabIndex = 7;
            this.stopGuna2Button.Text = "Stop chat";
            this.stopGuna2Button.Click += new System.EventHandler(this.stopGuna2Button_Click);
            // 
            // startGuna2Button
            // 
            this.startGuna2Button.Animated = true;
            this.startGuna2Button.CheckedState.Parent = this.startGuna2Button;
            this.startGuna2Button.CustomImages.Parent = this.startGuna2Button;
            this.startGuna2Button.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startGuna2Button.ForeColor = System.Drawing.Color.White;
            this.startGuna2Button.HoverState.Parent = this.startGuna2Button;
            this.startGuna2Button.Location = new System.Drawing.Point(7, 38);
            this.startGuna2Button.Name = "startGuna2Button";
            this.startGuna2Button.ShadowDecoration.Enabled = true;
            this.startGuna2Button.ShadowDecoration.Parent = this.startGuna2Button;
            this.startGuna2Button.Size = new System.Drawing.Size(131, 32);
            this.startGuna2Button.TabIndex = 6;
            this.startGuna2Button.Text = "Start chat";
            this.startGuna2Button.Click += new System.EventHandler(this.startGuna2Button_Click);
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.Animated = true;
            this.closeButton.CheckedState.Parent = this.closeButton;
            this.closeButton.CustomImages.Parent = this.closeButton;
            this.closeButton.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeButton.ForeColor = System.Drawing.Color.White;
            this.closeButton.HoverState.Parent = this.closeButton;
            this.closeButton.Location = new System.Drawing.Point(531, 0);
            this.closeButton.Name = "closeButton";
            this.closeButton.ShadowDecoration.Enabled = true;
            this.closeButton.ShadowDecoration.Parent = this.closeButton;
            this.closeButton.Size = new System.Drawing.Size(32, 32);
            this.closeButton.TabIndex = 5;
            this.closeButton.Text = "╳";
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // maximizeButton
            // 
            this.maximizeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.maximizeButton.Animated = true;
            this.maximizeButton.CheckedState.Parent = this.maximizeButton;
            this.maximizeButton.CustomImages.Parent = this.maximizeButton;
            this.maximizeButton.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maximizeButton.ForeColor = System.Drawing.Color.White;
            this.maximizeButton.HoverState.Parent = this.maximizeButton;
            this.maximizeButton.Location = new System.Drawing.Point(493, 0);
            this.maximizeButton.Name = "maximizeButton";
            this.maximizeButton.ShadowDecoration.Enabled = true;
            this.maximizeButton.ShadowDecoration.Parent = this.maximizeButton;
            this.maximizeButton.Size = new System.Drawing.Size(32, 32);
            this.maximizeButton.TabIndex = 4;
            this.maximizeButton.Text = "🗖";
            this.maximizeButton.Click += new System.EventHandler(this.maximizeButton_Click);
            // 
            // minimizeButton
            // 
            this.minimizeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.minimizeButton.Animated = true;
            this.minimizeButton.CheckedState.Parent = this.minimizeButton;
            this.minimizeButton.CustomImages.Parent = this.minimizeButton;
            this.minimizeButton.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minimizeButton.ForeColor = System.Drawing.Color.White;
            this.minimizeButton.HoverState.Parent = this.minimizeButton;
            this.minimizeButton.Location = new System.Drawing.Point(455, 0);
            this.minimizeButton.Name = "minimizeButton";
            this.minimizeButton.ShadowDecoration.Enabled = true;
            this.minimizeButton.ShadowDecoration.Parent = this.minimizeButton;
            this.minimizeButton.Size = new System.Drawing.Size(32, 32);
            this.minimizeButton.TabIndex = 3;
            this.minimizeButton.Text = "🗕";
            this.minimizeButton.Click += new System.EventHandler(this.minimizeButton_Click);
            // 
            // messageGuna2TextBox
            // 
            this.messageGuna2TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageGuna2TextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.messageGuna2TextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.messageGuna2TextBox.DefaultText = "";
            this.messageGuna2TextBox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.messageGuna2TextBox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.messageGuna2TextBox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.messageGuna2TextBox.DisabledState.Parent = this.messageGuna2TextBox;
            this.messageGuna2TextBox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.messageGuna2TextBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.messageGuna2TextBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.messageGuna2TextBox.FocusedState.Parent = this.messageGuna2TextBox;
            this.messageGuna2TextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.messageGuna2TextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.messageGuna2TextBox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.messageGuna2TextBox.HoverState.Parent = this.messageGuna2TextBox;
            this.messageGuna2TextBox.Location = new System.Drawing.Point(7, 76);
            this.messageGuna2TextBox.Name = "messageGuna2TextBox";
            this.messageGuna2TextBox.PasswordChar = '\0';
            this.messageGuna2TextBox.PlaceholderText = "Message...";
            this.messageGuna2TextBox.SelectedText = "";
            this.messageGuna2TextBox.ShadowDecoration.Parent = this.messageGuna2TextBox;
            this.messageGuna2TextBox.Size = new System.Drawing.Size(551, 25);
            this.messageGuna2TextBox.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(446, 32);
            this.label3.TabIndex = 50;
            this.label3.Text = "Eagle Monitor RAT";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label3_MouseDown);
            // 
            // RemoteChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(564, 290);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.messageGuna2TextBox);
            this.Controls.Add(this.messageRichTextBox);
            this.Controls.Add(this.sendMsgGuna2Button);
            this.Controls.Add(this.stopGuna2Button);
            this.Controls.Add(this.startGuna2Button);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.maximizeButton);
            this.Controls.Add(this.minimizeButton);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RemoteChatForm";
            this.Padding = new System.Windows.Forms.Padding(3, 32, 3, 3);
            this.Text = "RemoteChatForm";
            this.Load += new System.EventHandler(this.ChatForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button closeButton;
        private Guna.UI2.WinForms.Guna2Button maximizeButton;
        private Guna.UI2.WinForms.Guna2Button minimizeButton;
        private Guna.UI2.WinForms.Guna2Button startGuna2Button;
        private Guna.UI2.WinForms.Guna2Button stopGuna2Button;
        private Guna.UI2.WinForms.Guna2Button sendMsgGuna2Button;
        internal System.Windows.Forms.RichTextBox messageRichTextBox;
        private Guna.UI2.WinForms.Guna2TextBox messageGuna2TextBox;
        public System.Windows.Forms.Label label3;
    }
}