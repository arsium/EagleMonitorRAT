namespace EagleMonitor.Forms
{
    partial class RemoteDesktopForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RemoteDesktopForm));
            this.label3 = new System.Windows.Forms.Label();
            this.loadingCircle1 = new MRG.Controls.UI.LoadingCircle();
            this.viewerPictureBox = new System.Windows.Forms.PictureBox();
            this.settingsContextMenuStrip = new EagleMonitor.Controls.CustomContextMenuStrip();
            this.hidePanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showPanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCurrentPcitureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.qualityGuna2TrackBar = new Guna.UI2.WinForms.Guna2TrackBar();
            this.keyboardGuna2ToggleSwitch = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            this.mouseGuna2ToggleSwitch = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            this.captureGuna2ToggleSwitch = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.closeButton = new Guna.UI2.WinForms.Guna2Button();
            this.maximizeButton = new Guna.UI2.WinForms.Guna2Button();
            this.minimizeButton = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)(this.viewerPictureBox)).BeginInit();
            this.settingsContextMenuStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(41, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(446, 32);
            this.label3.TabIndex = 46;
            this.label3.Text = "Eagle Monitor RAT";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label3_MouseDown);
            // 
            // loadingCircle1
            // 
            this.loadingCircle1.Active = false;
            this.loadingCircle1.BackColor = System.Drawing.Color.Transparent;
            this.loadingCircle1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.loadingCircle1.InnerCircleRadius = 5;
            this.loadingCircle1.Location = new System.Drawing.Point(3, 0);
            this.loadingCircle1.Name = "loadingCircle1";
            this.loadingCircle1.NumberSpoke = 12;
            this.loadingCircle1.OuterCircleRadius = 11;
            this.loadingCircle1.RotationSpeed = 100;
            this.loadingCircle1.Size = new System.Drawing.Size(32, 32);
            this.loadingCircle1.SpokeThickness = 2;
            this.loadingCircle1.StylePreset = MRG.Controls.UI.LoadingCircle.StylePresets.MacOSX;
            this.loadingCircle1.TabIndex = 45;
            this.loadingCircle1.Text = "loadingCircle1";
            this.loadingCircle1.Visible = false;
            // 
            // viewerPictureBox
            // 
            this.viewerPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewerPictureBox.Location = new System.Drawing.Point(3, 75);
            this.viewerPictureBox.Name = "viewerPictureBox";
            this.viewerPictureBox.Size = new System.Drawing.Size(598, 238);
            this.viewerPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.viewerPictureBox.TabIndex = 41;
            this.viewerPictureBox.TabStop = false;
            this.viewerPictureBox.SizeChanged += new System.EventHandler(this.viewerPictureBox_SizeChanged);
            this.viewerPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.viewerPictureBox_MouseDown);
            this.viewerPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.viewerPictureBox_MouseMove);
            this.viewerPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.viewerPictureBox_MouseUp);
            // 
            // settingsContextMenuStrip
            // 
            this.settingsContextMenuStrip.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.settingsContextMenuStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.settingsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hidePanelToolStripMenuItem,
            this.showPanelToolStripMenuItem,
            this.saveCurrentPcitureToolStripMenuItem});
            this.settingsContextMenuStrip.Name = "customContextMenuStrip1";
            this.settingsContextMenuStrip.Size = new System.Drawing.Size(193, 118);
            // 
            // hidePanelToolStripMenuItem
            // 
            this.hidePanelToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.hidePanelToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.hidePanelToolStripMenuItem.Image = global::EagleMonitor.Properties.Resources.window_hide_large_2x;
            this.hidePanelToolStripMenuItem.Name = "hidePanelToolStripMenuItem";
            this.hidePanelToolStripMenuItem.Size = new System.Drawing.Size(192, 38);
            this.hidePanelToolStripMenuItem.Text = "Hide Settings";
            this.hidePanelToolStripMenuItem.Click += new System.EventHandler(this.hidePanelToolStripMenuItem_Click);
            // 
            // showPanelToolStripMenuItem
            // 
            this.showPanelToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.showPanelToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.showPanelToolStripMenuItem.Image = global::EagleMonitor.Properties.Resources.window_large_2x;
            this.showPanelToolStripMenuItem.Name = "showPanelToolStripMenuItem";
            this.showPanelToolStripMenuItem.Size = new System.Drawing.Size(192, 38);
            this.showPanelToolStripMenuItem.Text = "Show Settings";
            this.showPanelToolStripMenuItem.Click += new System.EventHandler(this.showPanelToolStripMenuItem_Click);
            // 
            // saveCurrentPcitureToolStripMenuItem
            // 
            this.saveCurrentPcitureToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.saveCurrentPcitureToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.saveCurrentPcitureToolStripMenuItem.Image = global::EagleMonitor.Properties.Resources.window_picture_large_2x;
            this.saveCurrentPcitureToolStripMenuItem.Name = "saveCurrentPcitureToolStripMenuItem";
            this.saveCurrentPcitureToolStripMenuItem.Size = new System.Drawing.Size(192, 38);
            this.saveCurrentPcitureToolStripMenuItem.Text = "Save current picture";
            this.saveCurrentPcitureToolStripMenuItem.Click += new System.EventHandler(this.saveCurrentPcitureToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.ContextMenuStrip = this.settingsContextMenuStrip;
            this.panel1.Controls.Add(this.qualityGuna2TrackBar);
            this.panel1.Controls.Add(this.keyboardGuna2ToggleSwitch);
            this.panel1.Controls.Add(this.mouseGuna2ToggleSwitch);
            this.panel1.Controls.Add(this.captureGuna2ToggleSwitch);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(598, 43);
            this.panel1.TabIndex = 44;
            // 
            // qualityGuna2TrackBar
            // 
            this.qualityGuna2TrackBar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(207)))));
            this.qualityGuna2TrackBar.HoverState.Parent = this.qualityGuna2TrackBar;
            this.qualityGuna2TrackBar.IndicateFocus = false;
            this.qualityGuna2TrackBar.Location = new System.Drawing.Point(321, 10);
            this.qualityGuna2TrackBar.Name = "qualityGuna2TrackBar";
            this.qualityGuna2TrackBar.Size = new System.Drawing.Size(97, 20);
            this.qualityGuna2TrackBar.TabIndex = 47;
            this.qualityGuna2TrackBar.ThumbColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.qualityGuna2TrackBar.ValueChanged += new System.EventHandler(this.qualityGuna2TrackBar_ValueChanged);
            // 
            // keyboardGuna2ToggleSwitch
            // 
            this.keyboardGuna2ToggleSwitch.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.keyboardGuna2ToggleSwitch.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.keyboardGuna2ToggleSwitch.CheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.keyboardGuna2ToggleSwitch.CheckedState.InnerColor = System.Drawing.Color.White;
            this.keyboardGuna2ToggleSwitch.CheckedState.Parent = this.keyboardGuna2ToggleSwitch;
            this.keyboardGuna2ToggleSwitch.Location = new System.Drawing.Point(233, 9);
            this.keyboardGuna2ToggleSwitch.Name = "keyboardGuna2ToggleSwitch";
            this.keyboardGuna2ToggleSwitch.ShadowDecoration.Parent = this.keyboardGuna2ToggleSwitch;
            this.keyboardGuna2ToggleSwitch.Size = new System.Drawing.Size(35, 20);
            this.keyboardGuna2ToggleSwitch.TabIndex = 53;
            this.keyboardGuna2ToggleSwitch.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.keyboardGuna2ToggleSwitch.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.keyboardGuna2ToggleSwitch.UncheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.keyboardGuna2ToggleSwitch.UncheckedState.InnerColor = System.Drawing.Color.White;
            this.keyboardGuna2ToggleSwitch.UncheckedState.Parent = this.keyboardGuna2ToggleSwitch;
            this.keyboardGuna2ToggleSwitch.CheckedChanged += new System.EventHandler(this.keyboardGuna2ToggleSwitch_CheckedChanged);
            // 
            // mouseGuna2ToggleSwitch
            // 
            this.mouseGuna2ToggleSwitch.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.mouseGuna2ToggleSwitch.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.mouseGuna2ToggleSwitch.CheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.mouseGuna2ToggleSwitch.CheckedState.InnerColor = System.Drawing.Color.White;
            this.mouseGuna2ToggleSwitch.CheckedState.Parent = this.mouseGuna2ToggleSwitch;
            this.mouseGuna2ToggleSwitch.Location = new System.Drawing.Point(137, 9);
            this.mouseGuna2ToggleSwitch.Name = "mouseGuna2ToggleSwitch";
            this.mouseGuna2ToggleSwitch.ShadowDecoration.Parent = this.mouseGuna2ToggleSwitch;
            this.mouseGuna2ToggleSwitch.Size = new System.Drawing.Size(35, 20);
            this.mouseGuna2ToggleSwitch.TabIndex = 52;
            this.mouseGuna2ToggleSwitch.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.mouseGuna2ToggleSwitch.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.mouseGuna2ToggleSwitch.UncheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.mouseGuna2ToggleSwitch.UncheckedState.InnerColor = System.Drawing.Color.White;
            this.mouseGuna2ToggleSwitch.UncheckedState.Parent = this.mouseGuna2ToggleSwitch;
            this.mouseGuna2ToggleSwitch.CheckedChanged += new System.EventHandler(this.mouseGuna2ToggleSwitch_CheckedChanged);
            // 
            // captureGuna2ToggleSwitch
            // 
            this.captureGuna2ToggleSwitch.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.captureGuna2ToggleSwitch.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.captureGuna2ToggleSwitch.CheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.captureGuna2ToggleSwitch.CheckedState.InnerColor = System.Drawing.Color.White;
            this.captureGuna2ToggleSwitch.CheckedState.Parent = this.captureGuna2ToggleSwitch;
            this.captureGuna2ToggleSwitch.Location = new System.Drawing.Point(51, 9);
            this.captureGuna2ToggleSwitch.Name = "captureGuna2ToggleSwitch";
            this.captureGuna2ToggleSwitch.ShadowDecoration.Parent = this.captureGuna2ToggleSwitch;
            this.captureGuna2ToggleSwitch.Size = new System.Drawing.Size(35, 20);
            this.captureGuna2ToggleSwitch.TabIndex = 44;
            this.captureGuna2ToggleSwitch.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.captureGuna2ToggleSwitch.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.captureGuna2ToggleSwitch.UncheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.captureGuna2ToggleSwitch.UncheckedState.InnerColor = System.Drawing.Color.White;
            this.captureGuna2ToggleSwitch.UncheckedState.Parent = this.captureGuna2ToggleSwitch;
            this.captureGuna2ToggleSwitch.CheckedChanged += new System.EventHandler(this.captureGuna2ToggleSwitch_CheckedChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(274, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 20);
            this.label6.TabIndex = 56;
            this.label6.Text = "Quality:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(178, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 20);
            this.label5.TabIndex = 55;
            this.label5.Text = "Keyboard:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(92, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 20);
            this.label4.TabIndex = 54;
            this.label4.Text = "Mouse:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 38);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(598, 5);
            this.panel2.TabIndex = 51;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 20);
            this.label1.TabIndex = 45;
            this.label1.Text = "Capture:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.closeButton.Location = new System.Drawing.Point(569, 0);
            this.closeButton.Name = "closeButton";
            this.closeButton.ShadowDecoration.Enabled = true;
            this.closeButton.ShadowDecoration.Parent = this.closeButton;
            this.closeButton.Size = new System.Drawing.Size(32, 32);
            this.closeButton.TabIndex = 40;
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
            this.maximizeButton.Location = new System.Drawing.Point(531, 0);
            this.maximizeButton.Name = "maximizeButton";
            this.maximizeButton.ShadowDecoration.Enabled = true;
            this.maximizeButton.ShadowDecoration.Parent = this.maximizeButton;
            this.maximizeButton.Size = new System.Drawing.Size(32, 32);
            this.maximizeButton.TabIndex = 39;
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
            this.minimizeButton.Location = new System.Drawing.Point(493, 0);
            this.minimizeButton.Name = "minimizeButton";
            this.minimizeButton.ShadowDecoration.Enabled = true;
            this.minimizeButton.ShadowDecoration.Parent = this.minimizeButton;
            this.minimizeButton.Size = new System.Drawing.Size(32, 32);
            this.minimizeButton.TabIndex = 38;
            this.minimizeButton.Text = "🗕";
            this.minimizeButton.Click += new System.EventHandler(this.minimizeButton_Click);
            // 
            // RemoteDesktopForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(604, 316);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.loadingCircle1);
            this.Controls.Add(this.viewerPictureBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.maximizeButton);
            this.Controls.Add(this.minimizeButton);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "RemoteDesktopForm";
            this.Padding = new System.Windows.Forms.Padding(3, 32, 3, 3);
            this.Text = "RemoteDesktopForm";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RemoteDesktopForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.RemoteDesktopForm_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.viewerPictureBox)).EndInit();
            this.settingsContextMenuStrip.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button closeButton;
        private Guna.UI2.WinForms.Guna2Button maximizeButton;
        private Guna.UI2.WinForms.Guna2Button minimizeButton;
        public System.Windows.Forms.PictureBox viewerPictureBox;
        private System.Windows.Forms.Panel panel1;
        private Guna.UI2.WinForms.Guna2ToggleSwitch captureGuna2ToggleSwitch;
        private Guna.UI2.WinForms.Guna2TrackBar qualityGuna2TrackBar;
        private System.Windows.Forms.Label label1;
        public MRG.Controls.UI.LoadingCircle loadingCircle1;
        private Controls.CustomContextMenuStrip settingsContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem hidePanelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showPanelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveCurrentPcitureToolStripMenuItem;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2ToggleSwitch mouseGuna2ToggleSwitch;
        private Guna.UI2.WinForms.Guna2ToggleSwitch keyboardGuna2ToggleSwitch;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
    }
}