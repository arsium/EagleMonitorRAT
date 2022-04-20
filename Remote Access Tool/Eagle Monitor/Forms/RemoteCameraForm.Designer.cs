namespace EagleMonitor.Forms
{
    partial class RemoteCameraForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RemoteCameraForm));
            this.label3 = new System.Windows.Forms.Label();
            this.loadingCircle1 = new MRG.Controls.UI.LoadingCircle();
            this.cameraViewerPictureBox = new System.Windows.Forms.PictureBox();
            this.settingsContextMenuStrip = new EagleMonitor.Controls.CustomContextMenuStrip();
            this.hidePanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showPanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCurrentPcitureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.qualityGuna2TrackBar = new Guna.UI2.WinForms.Guna2TrackBar();
            this.panel2 = new System.Windows.Forms.Panel();
            this.intervalGuna2TextBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.camerasGuna2ComboBox = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.captureGuna2ToggleSwitch = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            this.closeButton = new Guna.UI2.WinForms.Guna2Button();
            this.maximizeButton = new Guna.UI2.WinForms.Guna2Button();
            this.minimizeButton = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)(this.cameraViewerPictureBox)).BeginInit();
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
            this.label3.Size = new System.Drawing.Size(642, 32);
            this.label3.TabIndex = 48;
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
            this.loadingCircle1.TabIndex = 47;
            this.loadingCircle1.Text = "loadingCircle1";
            this.loadingCircle1.Visible = false;
            // 
            // cameraViewerPictureBox
            // 
            this.cameraViewerPictureBox.ContextMenuStrip = this.settingsContextMenuStrip;
            this.cameraViewerPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cameraViewerPictureBox.Location = new System.Drawing.Point(3, 85);
            this.cameraViewerPictureBox.Name = "cameraViewerPictureBox";
            this.cameraViewerPictureBox.Size = new System.Drawing.Size(794, 362);
            this.cameraViewerPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.cameraViewerPictureBox.TabIndex = 46;
            this.cameraViewerPictureBox.TabStop = false;
            // 
            // settingsContextMenuStrip
            // 
            this.settingsContextMenuStrip.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.settingsContextMenuStrip.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.settingsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hidePanelToolStripMenuItem,
            this.showPanelToolStripMenuItem,
            this.saveCurrentPcitureToolStripMenuItem});
            this.settingsContextMenuStrip.Name = "customContextMenuStrip1";
            this.settingsContextMenuStrip.Size = new System.Drawing.Size(189, 106);
            // 
            // hidePanelToolStripMenuItem
            // 
            this.hidePanelToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.hidePanelToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.hidePanelToolStripMenuItem.Image = global::EagleMonitor.Properties.Resources.window_hide_large_2x;
            this.hidePanelToolStripMenuItem.Name = "hidePanelToolStripMenuItem";
            this.hidePanelToolStripMenuItem.Size = new System.Drawing.Size(188, 34);
            this.hidePanelToolStripMenuItem.Text = "Hide Settings";
            this.hidePanelToolStripMenuItem.Click += new System.EventHandler(this.hidePanelToolStripMenuItem_Click);
            // 
            // showPanelToolStripMenuItem
            // 
            this.showPanelToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.showPanelToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.showPanelToolStripMenuItem.Image = global::EagleMonitor.Properties.Resources.window_large_2x;
            this.showPanelToolStripMenuItem.Name = "showPanelToolStripMenuItem";
            this.showPanelToolStripMenuItem.Size = new System.Drawing.Size(188, 34);
            this.showPanelToolStripMenuItem.Text = "Show Settings";
            this.showPanelToolStripMenuItem.Click += new System.EventHandler(this.showPanelToolStripMenuItem_Click);
            // 
            // saveCurrentPcitureToolStripMenuItem
            // 
            this.saveCurrentPcitureToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.saveCurrentPcitureToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.saveCurrentPcitureToolStripMenuItem.Image = global::EagleMonitor.Properties.Resources.window_picture_large_2x;
            this.saveCurrentPcitureToolStripMenuItem.Name = "saveCurrentPcitureToolStripMenuItem";
            this.saveCurrentPcitureToolStripMenuItem.Size = new System.Drawing.Size(188, 34);
            this.saveCurrentPcitureToolStripMenuItem.Text = "Save current picture";
            this.saveCurrentPcitureToolStripMenuItem.Click += new System.EventHandler(this.saveCurrentPcitureToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.qualityGuna2TrackBar);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.intervalGuna2TextBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.camerasGuna2ComboBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.captureGuna2ToggleSwitch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(794, 53);
            this.panel1.TabIndex = 45;
            // 
            // qualityGuna2TrackBar
            // 
            this.qualityGuna2TrackBar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(207)))));
            this.qualityGuna2TrackBar.HoverState.Parent = this.qualityGuna2TrackBar;
            this.qualityGuna2TrackBar.IndicateFocus = false;
            this.qualityGuna2TrackBar.Location = new System.Drawing.Point(641, 18);
            this.qualityGuna2TrackBar.Name = "qualityGuna2TrackBar";
            this.qualityGuna2TrackBar.Size = new System.Drawing.Size(115, 20);
            this.qualityGuna2TrackBar.TabIndex = 53;
            this.qualityGuna2TrackBar.ThumbColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 48);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(794, 5);
            this.panel2.TabIndex = 52;
            // 
            // intervalGuna2TextBox
            // 
            this.intervalGuna2TextBox.Animated = true;
            this.intervalGuna2TextBox.AutoRoundedCorners = true;
            this.intervalGuna2TextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.intervalGuna2TextBox.BorderRadius = 17;
            this.intervalGuna2TextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.intervalGuna2TextBox.DefaultText = "30";
            this.intervalGuna2TextBox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.intervalGuna2TextBox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.intervalGuna2TextBox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.intervalGuna2TextBox.DisabledState.Parent = this.intervalGuna2TextBox;
            this.intervalGuna2TextBox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.intervalGuna2TextBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.intervalGuna2TextBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.intervalGuna2TextBox.FocusedState.Parent = this.intervalGuna2TextBox;
            this.intervalGuna2TextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.intervalGuna2TextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.intervalGuna2TextBox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.intervalGuna2TextBox.HoverState.Parent = this.intervalGuna2TextBox;
            this.intervalGuna2TextBox.Location = new System.Drawing.Point(433, 7);
            this.intervalGuna2TextBox.Name = "intervalGuna2TextBox";
            this.intervalGuna2TextBox.PasswordChar = '\0';
            this.intervalGuna2TextBox.PlaceholderText = "";
            this.intervalGuna2TextBox.SelectedText = "";
            this.intervalGuna2TextBox.SelectionStart = 2;
            this.intervalGuna2TextBox.ShadowDecoration.Parent = this.intervalGuna2TextBox;
            this.intervalGuna2TextBox.Size = new System.Drawing.Size(200, 36);
            this.intervalGuna2TextBox.TabIndex = 48;
            this.intervalGuna2TextBox.TextChanged += new System.EventHandler(this.intervalGuna2TextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.label2.Location = new System.Drawing.Point(384, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 34);
            this.label2.TabIndex = 47;
            this.label2.Text = "Interval:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // camerasGuna2ComboBox
            // 
            this.camerasGuna2ComboBox.Animated = true;
            this.camerasGuna2ComboBox.AutoRoundedCorners = true;
            this.camerasGuna2ComboBox.BackColor = System.Drawing.Color.Transparent;
            this.camerasGuna2ComboBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.camerasGuna2ComboBox.BorderRadius = 17;
            this.camerasGuna2ComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.camerasGuna2ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.camerasGuna2ComboBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.camerasGuna2ComboBox.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.camerasGuna2ComboBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.camerasGuna2ComboBox.FocusedState.Parent = this.camerasGuna2ComboBox;
            this.camerasGuna2ComboBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.camerasGuna2ComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.camerasGuna2ComboBox.HoverState.Parent = this.camerasGuna2ComboBox;
            this.camerasGuna2ComboBox.ItemHeight = 30;
            this.camerasGuna2ComboBox.ItemsAppearance.Parent = this.camerasGuna2ComboBox;
            this.camerasGuna2ComboBox.Location = new System.Drawing.Point(103, 7);
            this.camerasGuna2ComboBox.Name = "camerasGuna2ComboBox";
            this.camerasGuna2ComboBox.ShadowDecoration.Parent = this.camerasGuna2ComboBox;
            this.camerasGuna2ComboBox.Size = new System.Drawing.Size(275, 36);
            this.camerasGuna2ComboBox.TabIndex = 46;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 34);
            this.label1.TabIndex = 45;
            this.label1.Text = "Capture:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // captureGuna2ToggleSwitch
            // 
            this.captureGuna2ToggleSwitch.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.captureGuna2ToggleSwitch.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.captureGuna2ToggleSwitch.CheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.captureGuna2ToggleSwitch.CheckedState.InnerColor = System.Drawing.Color.White;
            this.captureGuna2ToggleSwitch.CheckedState.Parent = this.captureGuna2ToggleSwitch;
            this.captureGuna2ToggleSwitch.Location = new System.Drawing.Point(62, 18);
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
            this.closeButton.TabIndex = 43;
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
            this.maximizeButton.TabIndex = 42;
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
            this.minimizeButton.TabIndex = 41;
            this.minimizeButton.Text = "🗕";
            this.minimizeButton.Click += new System.EventHandler(this.minimizeButton_Click);
            // 
            // RemoteCameraForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.loadingCircle1);
            this.Controls.Add(this.cameraViewerPictureBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.maximizeButton);
            this.Controls.Add(this.minimizeButton);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RemoteCameraForm";
            this.Padding = new System.Windows.Forms.Padding(3, 32, 3, 3);
            this.Text = "RemoteCamera";
            this.Shown += new System.EventHandler(this.RemoteCamera_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.cameraViewerPictureBox)).EndInit();
            this.settingsContextMenuStrip.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button closeButton;
        private Guna.UI2.WinForms.Guna2Button maximizeButton;
        private Guna.UI2.WinForms.Guna2Button minimizeButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2ToggleSwitch captureGuna2ToggleSwitch;
        public System.Windows.Forms.PictureBox cameraViewerPictureBox;
        public Guna.UI2.WinForms.Guna2ComboBox camerasGuna2ComboBox;
        public MRG.Controls.UI.LoadingCircle loadingCircle1;
        private Guna.UI2.WinForms.Guna2TextBox intervalGuna2TextBox;
        private System.Windows.Forms.Label label2;
        private Controls.CustomContextMenuStrip settingsContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem hidePanelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showPanelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveCurrentPcitureToolStripMenuItem;
        private System.Windows.Forms.Panel panel2;
        private Guna.UI2.WinForms.Guna2TrackBar qualityGuna2TrackBar;
        public System.Windows.Forms.Label label3;
    }
}