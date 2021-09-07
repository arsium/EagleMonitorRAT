
namespace Eagle_Monitor.Forms
{
    partial class WebCamForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WebCamForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.quailitySiticoneTrackBar = new ns1.SiticoneTrackBar();
            this.camPictureBox = new System.Windows.Forms.PictureBox();
            this.labelSize = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.loadingCircle1 = new MRG.Controls.UI.LoadingCircle();
            this.webCamComboBox = new System.Windows.Forms.ComboBox();
            this.saveWindowsButton = new Eagle_Monitor.Controls.WindowsButton();
            this.closeButton = new Eagle_Monitor.Controls.WindowsButton();
            this.maximizeButton = new Eagle_Monitor.Controls.WindowsButton();
            this.minimizeButton = new Eagle_Monitor.Controls.WindowsButton();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.camPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Eagle_Monitor.Properties.Resources.icons8_not_sending_video_frames_32;
            this.pictureBox1.Location = new System.Drawing.Point(211, 58);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 24);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 34;
            this.pictureBox1.TabStop = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(241, 62);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(67, 17);
            this.checkBox1.TabIndex = 33;
            this.checkBox1.Text = "Capture";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // quailitySiticoneTrackBar
            // 
            this.quailitySiticoneTrackBar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(207)))));
            this.quailitySiticoneTrackBar.HoveredState.Parent = this.quailitySiticoneTrackBar;
            this.quailitySiticoneTrackBar.Location = new System.Drawing.Point(61, 33);
            this.quailitySiticoneTrackBar.Name = "quailitySiticoneTrackBar";
            this.quailitySiticoneTrackBar.Size = new System.Drawing.Size(376, 23);
            this.quailitySiticoneTrackBar.TabIndex = 30;
            this.quailitySiticoneTrackBar.ThumbColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(173)))), ((int)(((byte)(239)))));
            this.quailitySiticoneTrackBar.Value = 65;
            // 
            // camPictureBox
            // 
            this.camPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.camPictureBox.Location = new System.Drawing.Point(3, 90);
            this.camPictureBox.Name = "camPictureBox";
            this.camPictureBox.Size = new System.Drawing.Size(540, 357);
            this.camPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.camPictureBox.TabIndex = 29;
            this.camPictureBox.TabStop = false;
            // 
            // labelSize
            // 
            this.labelSize.AutoSize = true;
            this.labelSize.BackColor = System.Drawing.Color.Transparent;
            this.labelSize.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.labelSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelSize.Location = new System.Drawing.Point(110, 11);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(13, 13);
            this.labelSize.TabIndex = 28;
            this.labelSize.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(28, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 27;
            this.label3.Text = "Bytes Received :";
            // 
            // loadingCircle1
            // 
            this.loadingCircle1.Active = false;
            this.loadingCircle1.BackColor = System.Drawing.Color.Transparent;
            this.loadingCircle1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.loadingCircle1.InnerCircleRadius = 6;
            this.loadingCircle1.Location = new System.Drawing.Point(3, 5);
            this.loadingCircle1.Name = "loadingCircle1";
            this.loadingCircle1.NumberSpoke = 9;
            this.loadingCircle1.OuterCircleRadius = 7;
            this.loadingCircle1.RotationSpeed = 100;
            this.loadingCircle1.Size = new System.Drawing.Size(28, 25);
            this.loadingCircle1.SpokeThickness = 4;
            this.loadingCircle1.StylePreset = MRG.Controls.UI.LoadingCircle.StylePresets.Firefox;
            this.loadingCircle1.TabIndex = 26;
            this.loadingCircle1.Text = "loadingCircle1";
            this.loadingCircle1.Visible = false;
            // 
            // webCamComboBox
            // 
            this.webCamComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.webCamComboBox.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.webCamComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.webCamComboBox.FormattingEnabled = true;
            this.webCamComboBox.Location = new System.Drawing.Point(9, 58);
            this.webCamComboBox.Name = "webCamComboBox";
            this.webCamComboBox.Size = new System.Drawing.Size(196, 21);
            this.webCamComboBox.TabIndex = 4;
            // 
            // saveWindowsButton
            // 
            this.saveWindowsButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(173)))), ((int)(((byte)(239)))));
            this.saveWindowsButton.FlatAppearance.BorderSize = 0;
            this.saveWindowsButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(209)))));
            this.saveWindowsButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(193)))), ((int)(((byte)(255)))));
            this.saveWindowsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveWindowsButton.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.saveWindowsButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.saveWindowsButton.Location = new System.Drawing.Point(314, 59);
            this.saveWindowsButton.Name = "saveWindowsButton";
            this.saveWindowsButton.Size = new System.Drawing.Size(130, 23);
            this.saveWindowsButton.TabIndex = 39;
            this.saveWindowsButton.Text = "Save Picture";
            this.saveWindowsButton.UseVisualStyleBackColor = false;
            this.saveWindowsButton.Click += new System.EventHandler(this.saveWindowsButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(173)))), ((int)(((byte)(239)))));
            this.closeButton.FlatAppearance.BorderSize = 0;
            this.closeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(209)))));
            this.closeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(193)))), ((int)(((byte)(255)))));
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.closeButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.closeButton.Location = new System.Drawing.Point(509, 0);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(33, 30);
            this.closeButton.TabIndex = 38;
            this.closeButton.Text = "x";
            this.closeButton.UseVisualStyleBackColor = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // maximizeButton
            // 
            this.maximizeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.maximizeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(173)))), ((int)(((byte)(239)))));
            this.maximizeButton.FlatAppearance.BorderSize = 0;
            this.maximizeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(209)))));
            this.maximizeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(193)))), ((int)(((byte)(255)))));
            this.maximizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.maximizeButton.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.maximizeButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.maximizeButton.Location = new System.Drawing.Point(475, 0);
            this.maximizeButton.Name = "maximizeButton";
            this.maximizeButton.Size = new System.Drawing.Size(33, 30);
            this.maximizeButton.TabIndex = 37;
            this.maximizeButton.Text = "🗖";
            this.maximizeButton.UseVisualStyleBackColor = false;
            this.maximizeButton.Click += new System.EventHandler(this.maximizeButton_Click);
            // 
            // minimizeButton
            // 
            this.minimizeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.minimizeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(173)))), ((int)(((byte)(239)))));
            this.minimizeButton.FlatAppearance.BorderSize = 0;
            this.minimizeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(209)))));
            this.minimizeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(193)))), ((int)(((byte)(255)))));
            this.minimizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minimizeButton.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.minimizeButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.minimizeButton.Location = new System.Drawing.Point(441, 0);
            this.minimizeButton.Name = "minimizeButton";
            this.minimizeButton.Size = new System.Drawing.Size(33, 30);
            this.minimizeButton.TabIndex = 36;
            this.minimizeButton.Text = "-";
            this.minimizeButton.UseVisualStyleBackColor = false;
            this.minimizeButton.Click += new System.EventHandler(this.minimizeButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 40;
            this.label1.Text = "Quality :";
            // 
            // WebCamForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(546, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.saveWindowsButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.maximizeButton);
            this.Controls.Add(this.minimizeButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.quailitySiticoneTrackBar);
            this.Controls.Add(this.camPictureBox);
            this.Controls.Add(this.labelSize);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.loadingCircle1);
            this.Controls.Add(this.webCamComboBox);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WebCamForm";
            this.Padding = new System.Windows.Forms.Padding(3, 90, 3, 3);
            this.Text = "WebCamForm";
            this.Load += new System.EventHandler(this.WebCamForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.camPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox webCamComboBox;
        public System.Windows.Forms.Label labelSize;
        public System.Windows.Forms.Label label3;
        public MRG.Controls.UI.LoadingCircle loadingCircle1;
        public ns1.SiticoneTrackBar quailitySiticoneTrackBar;
        public System.Windows.Forms.PictureBox camPictureBox;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private Controls.WindowsButton closeButton;
        private Controls.WindowsButton maximizeButton;
        private Controls.WindowsButton minimizeButton;
        private Controls.WindowsButton saveWindowsButton;
        private System.Windows.Forms.Label label1;
    }
}