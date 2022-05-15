namespace EagleMonitor.Forms
{
    partial class RemoteAudioForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RemoteAudioForm));
            this.closeButton = new Guna.UI2.WinForms.Guna2Button();
            this.maximizeButton = new Guna.UI2.WinForms.Guna2Button();
            this.minimizeButton = new Guna.UI2.WinForms.Guna2Button();
            this.currentMachineDevicesGuna2ComboBox = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.captureGuna2ToggleSwitch = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            this.audioDevicesGuna2ComboBox = new Guna.UI2.WinForms.Guna2ComboBox();
            this.loadingCircle1 = new MRG.Controls.UI.LoadingCircle();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.Animated = true;
            this.closeButton.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeButton.ForeColor = System.Drawing.Color.White;
            this.closeButton.Location = new System.Drawing.Point(370, 0);
            this.closeButton.Name = "closeButton";
            this.closeButton.ShadowDecoration.Enabled = true;
            this.closeButton.Size = new System.Drawing.Size(32, 32);
            this.closeButton.TabIndex = 43;
            this.closeButton.Text = "╳";
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // maximizeButton
            // 
            this.maximizeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.maximizeButton.Animated = true;
            this.maximizeButton.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maximizeButton.ForeColor = System.Drawing.Color.White;
            this.maximizeButton.Location = new System.Drawing.Point(332, 0);
            this.maximizeButton.Name = "maximizeButton";
            this.maximizeButton.ShadowDecoration.Enabled = true;
            this.maximizeButton.Size = new System.Drawing.Size(32, 32);
            this.maximizeButton.TabIndex = 42;
            this.maximizeButton.Text = "🗗";
            this.maximizeButton.Click += new System.EventHandler(this.maximizeButton_Click);
            // 
            // minimizeButton
            // 
            this.minimizeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.minimizeButton.Animated = true;
            this.minimizeButton.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minimizeButton.ForeColor = System.Drawing.Color.White;
            this.minimizeButton.Location = new System.Drawing.Point(294, 0);
            this.minimizeButton.Name = "minimizeButton";
            this.minimizeButton.ShadowDecoration.Enabled = true;
            this.minimizeButton.Size = new System.Drawing.Size(32, 32);
            this.minimizeButton.TabIndex = 41;
            this.minimizeButton.Text = "🗕";
            this.minimizeButton.Click += new System.EventHandler(this.minimizeButton_Click);
            // 
            // currentMachineDevicesGuna2ComboBox
            // 
            this.currentMachineDevicesGuna2ComboBox.Animated = true;
            this.currentMachineDevicesGuna2ComboBox.AutoRoundedCorners = true;
            this.currentMachineDevicesGuna2ComboBox.BackColor = System.Drawing.Color.Transparent;
            this.currentMachineDevicesGuna2ComboBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.currentMachineDevicesGuna2ComboBox.BorderRadius = 17;
            this.currentMachineDevicesGuna2ComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.currentMachineDevicesGuna2ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.currentMachineDevicesGuna2ComboBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.currentMachineDevicesGuna2ComboBox.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.currentMachineDevicesGuna2ComboBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.currentMachineDevicesGuna2ComboBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.currentMachineDevicesGuna2ComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.currentMachineDevicesGuna2ComboBox.ItemHeight = 30;
            this.currentMachineDevicesGuna2ComboBox.Location = new System.Drawing.Point(9, 152);
            this.currentMachineDevicesGuna2ComboBox.Name = "currentMachineDevicesGuna2ComboBox";
            this.currentMachineDevicesGuna2ComboBox.Size = new System.Drawing.Size(390, 36);
            this.currentMachineDevicesGuna2ComboBox.TabIndex = 56;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 36);
            this.label3.TabIndex = 55;
            this.label3.Text = "Listen on:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 191);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 36);
            this.label1.TabIndex = 54;
            this.label1.Text = "Capture:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // captureGuna2ToggleSwitch
            // 
            this.captureGuna2ToggleSwitch.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.captureGuna2ToggleSwitch.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.captureGuna2ToggleSwitch.CheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.captureGuna2ToggleSwitch.CheckedState.InnerColor = System.Drawing.Color.White;
            this.captureGuna2ToggleSwitch.Location = new System.Drawing.Point(65, 200);
            this.captureGuna2ToggleSwitch.Name = "captureGuna2ToggleSwitch";
            this.captureGuna2ToggleSwitch.Size = new System.Drawing.Size(35, 22);
            this.captureGuna2ToggleSwitch.TabIndex = 53;
            this.captureGuna2ToggleSwitch.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.captureGuna2ToggleSwitch.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.captureGuna2ToggleSwitch.UncheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.captureGuna2ToggleSwitch.UncheckedState.InnerColor = System.Drawing.Color.White;
            this.captureGuna2ToggleSwitch.CheckedChanged += new System.EventHandler(this.captureGuna2ToggleSwitch_CheckedChanged);
            // 
            // audioDevicesGuna2ComboBox
            // 
            this.audioDevicesGuna2ComboBox.Animated = true;
            this.audioDevicesGuna2ComboBox.AutoRoundedCorners = true;
            this.audioDevicesGuna2ComboBox.BackColor = System.Drawing.Color.Transparent;
            this.audioDevicesGuna2ComboBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.audioDevicesGuna2ComboBox.BorderRadius = 17;
            this.audioDevicesGuna2ComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.audioDevicesGuna2ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.audioDevicesGuna2ComboBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.audioDevicesGuna2ComboBox.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.audioDevicesGuna2ComboBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.audioDevicesGuna2ComboBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.audioDevicesGuna2ComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.audioDevicesGuna2ComboBox.ItemHeight = 30;
            this.audioDevicesGuna2ComboBox.Location = new System.Drawing.Point(9, 74);
            this.audioDevicesGuna2ComboBox.Name = "audioDevicesGuna2ComboBox";
            this.audioDevicesGuna2ComboBox.Size = new System.Drawing.Size(390, 36);
            this.audioDevicesGuna2ComboBox.TabIndex = 46;
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
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(41, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(247, 32);
            this.label2.TabIndex = 48;
            this.label2.Text = "Eagle Monitor RAT";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label2_MouseDown);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 36);
            this.label4.TabIndex = 57;
            this.label4.Text = "Remote devices:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RemoteAudioForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(405, 239);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.audioDevicesGuna2ComboBox);
            this.Controls.Add(this.currentMachineDevicesGuna2ComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.captureGuna2ToggleSwitch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.loadingCircle1);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.maximizeButton);
            this.Controls.Add(this.minimizeButton);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RemoteAudioForm";
            this.Padding = new System.Windows.Forms.Padding(3, 32, 3, 3);
            this.Text = "RemoteAudio";
            this.Load += new System.EventHandler(this.RemoteAudioForm_Load);
            this.Shown += new System.EventHandler(this.RemoteAudio_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button closeButton;
        private Guna.UI2.WinForms.Guna2Button maximizeButton;
        private Guna.UI2.WinForms.Guna2Button minimizeButton;
        public Guna.UI2.WinForms.Guna2ComboBox audioDevicesGuna2ComboBox;
        public MRG.Controls.UI.LoadingCircle loadingCircle1;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2ToggleSwitch captureGuna2ToggleSwitch;
        public System.Windows.Forms.Label label2;
        public Guna.UI2.WinForms.Guna2ComboBox currentMachineDevicesGuna2ComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}