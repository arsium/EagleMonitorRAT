namespace EagleMonitor.Forms
{
    partial class KeyloggerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeyloggerForm));
            this.closeButton = new Guna.UI2.WinForms.Guna2Button();
            this.maximizeButton = new Guna.UI2.WinForms.Guna2Button();
            this.minimizeButton = new Guna.UI2.WinForms.Guna2Button();
            this.stopKeylogGuna2Button = new Guna.UI2.WinForms.Guna2Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.startKeylogGuna2Button = new Guna.UI2.WinForms.Guna2Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.keystrokeRichTextBox = new System.Windows.Forms.RichTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
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
            this.closeButton.Location = new System.Drawing.Point(765, 0);
            this.closeButton.Name = "closeButton";
            this.closeButton.ShadowDecoration.Enabled = true;
            this.closeButton.ShadowDecoration.Parent = this.closeButton;
            this.closeButton.Size = new System.Drawing.Size(32, 32);
            this.closeButton.TabIndex = 37;
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
            this.maximizeButton.Location = new System.Drawing.Point(727, 0);
            this.maximizeButton.Name = "maximizeButton";
            this.maximizeButton.ShadowDecoration.Enabled = true;
            this.maximizeButton.ShadowDecoration.Parent = this.maximizeButton;
            this.maximizeButton.Size = new System.Drawing.Size(32, 32);
            this.maximizeButton.TabIndex = 36;
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
            this.minimizeButton.Location = new System.Drawing.Point(689, 0);
            this.minimizeButton.Name = "minimizeButton";
            this.minimizeButton.ShadowDecoration.Enabled = true;
            this.minimizeButton.ShadowDecoration.Parent = this.minimizeButton;
            this.minimizeButton.Size = new System.Drawing.Size(32, 32);
            this.minimizeButton.TabIndex = 35;
            this.minimizeButton.Text = "🗕";
            this.minimizeButton.Click += new System.EventHandler(this.minimizeButton_Click);
            // 
            // stopKeylogGuna2Button
            // 
            this.stopKeylogGuna2Button.Animated = true;
            this.stopKeylogGuna2Button.CheckedState.Parent = this.stopKeylogGuna2Button;
            this.stopKeylogGuna2Button.CustomImages.Parent = this.stopKeylogGuna2Button;
            this.stopKeylogGuna2Button.Dock = System.Windows.Forms.DockStyle.Top;
            this.stopKeylogGuna2Button.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.stopKeylogGuna2Button.ForeColor = System.Drawing.Color.White;
            this.stopKeylogGuna2Button.HoverState.Parent = this.stopKeylogGuna2Button;
            this.stopKeylogGuna2Button.Location = new System.Drawing.Point(6, 35);
            this.stopKeylogGuna2Button.Name = "stopKeylogGuna2Button";
            this.stopKeylogGuna2Button.ShadowDecoration.Enabled = true;
            this.stopKeylogGuna2Button.ShadowDecoration.Parent = this.stopKeylogGuna2Button;
            this.stopKeylogGuna2Button.Size = new System.Drawing.Size(788, 32);
            this.stopKeylogGuna2Button.TabIndex = 39;
            this.stopKeylogGuna2Button.Text = "Stop Keylogger";
            this.stopKeylogGuna2Button.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
            this.stopKeylogGuna2Button.Click += new System.EventHandler(this.stopKeylogGuna2Button_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(6, 67);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(788, 10);
            this.panel1.TabIndex = 40;
            // 
            // startKeylogGuna2Button
            // 
            this.startKeylogGuna2Button.Animated = true;
            this.startKeylogGuna2Button.BackColor = System.Drawing.Color.Transparent;
            this.startKeylogGuna2Button.CheckedState.Parent = this.startKeylogGuna2Button;
            this.startKeylogGuna2Button.CustomImages.Parent = this.startKeylogGuna2Button;
            this.startKeylogGuna2Button.Dock = System.Windows.Forms.DockStyle.Top;
            this.startKeylogGuna2Button.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.startKeylogGuna2Button.ForeColor = System.Drawing.Color.White;
            this.startKeylogGuna2Button.HoverState.Parent = this.startKeylogGuna2Button;
            this.startKeylogGuna2Button.Location = new System.Drawing.Point(6, 77);
            this.startKeylogGuna2Button.Name = "startKeylogGuna2Button";
            this.startKeylogGuna2Button.ShadowDecoration.Enabled = true;
            this.startKeylogGuna2Button.ShadowDecoration.Parent = this.startKeylogGuna2Button;
            this.startKeylogGuna2Button.Size = new System.Drawing.Size(788, 32);
            this.startKeylogGuna2Button.TabIndex = 41;
            this.startKeylogGuna2Button.Text = "Start Keylogger";
            this.startKeylogGuna2Button.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
            this.startKeylogGuna2Button.Click += new System.EventHandler(this.startKeylogGuna2Button_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(6, 109);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(788, 10);
            this.panel2.TabIndex = 42;
            // 
            // keystrokeRichTextBox
            // 
            this.keystrokeRichTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.keystrokeRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.keystrokeRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.keystrokeRichTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.keystrokeRichTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.keystrokeRichTextBox.Location = new System.Drawing.Point(10, 10);
            this.keystrokeRichTextBox.Name = "keystrokeRichTextBox";
            this.keystrokeRichTextBox.Size = new System.Drawing.Size(768, 305);
            this.keystrokeRichTextBox.TabIndex = 43;
            this.keystrokeRichTextBox.Text = "";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.keystrokeRichTextBox);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(6, 119);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(10);
            this.panel3.Size = new System.Drawing.Size(788, 325);
            this.panel3.TabIndex = 44;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(235, 32);
            this.label1.TabIndex = 45;
            this.label1.Text = "Eagle Monitor RAT";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // KeyloggerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.startKeylogGuna2Button);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.stopKeylogGuna2Button);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.maximizeButton);
            this.Controls.Add(this.minimizeButton);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "KeyloggerForm";
            this.Padding = new System.Windows.Forms.Padding(6, 35, 6, 6);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KeyloggerForm";
            this.Load += new System.EventHandler(this.KeyloggerForm_Load);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button closeButton;
        private Guna.UI2.WinForms.Guna2Button maximizeButton;
        private Guna.UI2.WinForms.Guna2Button minimizeButton;
        private Guna.UI2.WinForms.Guna2Button stopKeylogGuna2Button;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.RichTextBox keystrokeRichTextBox;
        private System.Windows.Forms.Panel panel3;
        public Guna.UI2.WinForms.Guna2Button startKeylogGuna2Button;
        public System.Windows.Forms.Label label1;
    }
}