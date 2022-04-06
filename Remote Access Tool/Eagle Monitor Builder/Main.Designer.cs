namespace Eagle_Monitor_Builder
{
    partial class Main
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.closeButton = new Guna.UI2.WinForms.Guna2Button();
            this.maximizeButton = new Guna.UI2.WinForms.Guna2Button();
            this.minimizeButton = new Guna.UI2.WinForms.Guna2Button();
            this.guna2GroupBox1 = new Guna.UI2.WinForms.Guna2GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.keyGuna2TextBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.portGuna2TextBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dnsGuna2TextBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2GroupBox2 = new Guna.UI2.WinForms.Guna2GroupBox();
            this.vbStubGuna2CheckBox = new Guna.UI2.WinForms.Guna2CheckBox();
            this.offKeyloguna2CheckBox = new Guna.UI2.WinForms.Guna2CheckBox();
            this.taskNameGuna2TextBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.persistenceGuna2CheckBox = new Guna.UI2.WinForms.Guna2CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.timeTaskGuna2TextBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buildGuna2Button = new Guna.UI2.WinForms.Guna2Button();
            this.x64StubGuna2CheckBox = new Guna.UI2.WinForms.Guna2CheckBox();
            this.guna2GroupBox1.SuspendLayout();
            this.guna2GroupBox2.SuspendLayout();
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
            this.closeButton.Location = new System.Drawing.Point(702, 0);
            this.closeButton.Name = "closeButton";
            this.closeButton.ShadowDecoration.Enabled = true;
            this.closeButton.ShadowDecoration.Parent = this.closeButton;
            this.closeButton.Size = new System.Drawing.Size(32, 32);
            this.closeButton.TabIndex = 11;
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
            this.maximizeButton.Location = new System.Drawing.Point(664, 0);
            this.maximizeButton.Name = "maximizeButton";
            this.maximizeButton.ShadowDecoration.Enabled = true;
            this.maximizeButton.ShadowDecoration.Parent = this.maximizeButton;
            this.maximizeButton.Size = new System.Drawing.Size(32, 32);
            this.maximizeButton.TabIndex = 10;
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
            this.minimizeButton.Location = new System.Drawing.Point(626, 0);
            this.minimizeButton.Name = "minimizeButton";
            this.minimizeButton.ShadowDecoration.Enabled = true;
            this.minimizeButton.ShadowDecoration.Parent = this.minimizeButton;
            this.minimizeButton.Size = new System.Drawing.Size(32, 32);
            this.minimizeButton.TabIndex = 9;
            this.minimizeButton.Text = "🗕";
            this.minimizeButton.Click += new System.EventHandler(this.minimizeButton_Click);
            // 
            // guna2GroupBox1
            // 
            this.guna2GroupBox1.AllowDrop = true;
            this.guna2GroupBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.guna2GroupBox1.Controls.Add(this.label5);
            this.guna2GroupBox1.Controls.Add(this.keyGuna2TextBox);
            this.guna2GroupBox1.Controls.Add(this.label2);
            this.guna2GroupBox1.Controls.Add(this.portGuna2TextBox);
            this.guna2GroupBox1.Controls.Add(this.label1);
            this.guna2GroupBox1.Controls.Add(this.dnsGuna2TextBox);
            this.guna2GroupBox1.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.guna2GroupBox1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.guna2GroupBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2GroupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.guna2GroupBox1.Location = new System.Drawing.Point(6, 38);
            this.guna2GroupBox1.Name = "guna2GroupBox1";
            this.guna2GroupBox1.Padding = new System.Windows.Forms.Padding(2, 40, 2, 2);
            this.guna2GroupBox1.ShadowDecoration.Parent = this.guna2GroupBox1;
            this.guna2GroupBox1.Size = new System.Drawing.Size(725, 184);
            this.guna2GroupBox1.TabIndex = 12;
            this.guna2GroupBox1.Text = "Network";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(5, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 36);
            this.label5.TabIndex = 18;
            this.label5.Text = "KEY:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // keyGuna2TextBox
            // 
            this.keyGuna2TextBox.Animated = true;
            this.keyGuna2TextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.keyGuna2TextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.keyGuna2TextBox.DefaultText = "";
            this.keyGuna2TextBox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.keyGuna2TextBox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.keyGuna2TextBox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.keyGuna2TextBox.DisabledState.Parent = this.keyGuna2TextBox;
            this.keyGuna2TextBox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.keyGuna2TextBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.keyGuna2TextBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.keyGuna2TextBox.FocusedState.Parent = this.keyGuna2TextBox;
            this.keyGuna2TextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.keyGuna2TextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.keyGuna2TextBox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.keyGuna2TextBox.HoverState.Parent = this.keyGuna2TextBox;
            this.keyGuna2TextBox.Location = new System.Drawing.Point(95, 134);
            this.keyGuna2TextBox.Name = "keyGuna2TextBox";
            this.keyGuna2TextBox.PasswordChar = '\0';
            this.keyGuna2TextBox.PlaceholderForeColor = System.Drawing.Color.Gainsboro;
            this.keyGuna2TextBox.PlaceholderText = "...";
            this.keyGuna2TextBox.SelectedText = "";
            this.keyGuna2TextBox.ShadowDecoration.Parent = this.keyGuna2TextBox;
            this.keyGuna2TextBox.Size = new System.Drawing.Size(625, 36);
            this.keyGuna2TextBox.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(5, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 36);
            this.label2.TabIndex = 16;
            this.label2.Text = "PORT:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // portGuna2TextBox
            // 
            this.portGuna2TextBox.Animated = true;
            this.portGuna2TextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.portGuna2TextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.portGuna2TextBox.DefaultText = "";
            this.portGuna2TextBox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.portGuna2TextBox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.portGuna2TextBox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.portGuna2TextBox.DisabledState.Parent = this.portGuna2TextBox;
            this.portGuna2TextBox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.portGuna2TextBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.portGuna2TextBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.portGuna2TextBox.FocusedState.Parent = this.portGuna2TextBox;
            this.portGuna2TextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.portGuna2TextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.portGuna2TextBox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.portGuna2TextBox.HoverState.Parent = this.portGuna2TextBox;
            this.portGuna2TextBox.Location = new System.Drawing.Point(95, 92);
            this.portGuna2TextBox.Name = "portGuna2TextBox";
            this.portGuna2TextBox.PasswordChar = '\0';
            this.portGuna2TextBox.PlaceholderForeColor = System.Drawing.Color.Gainsboro;
            this.portGuna2TextBox.PlaceholderText = "...";
            this.portGuna2TextBox.SelectedText = "";
            this.portGuna2TextBox.ShadowDecoration.Parent = this.portGuna2TextBox;
            this.portGuna2TextBox.Size = new System.Drawing.Size(625, 36);
            this.portGuna2TextBox.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(5, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 36);
            this.label1.TabIndex = 14;
            this.label1.Text = "IP/DNS:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dnsGuna2TextBox
            // 
            this.dnsGuna2TextBox.Animated = true;
            this.dnsGuna2TextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.dnsGuna2TextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.dnsGuna2TextBox.DefaultText = "";
            this.dnsGuna2TextBox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.dnsGuna2TextBox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.dnsGuna2TextBox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.dnsGuna2TextBox.DisabledState.Parent = this.dnsGuna2TextBox;
            this.dnsGuna2TextBox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.dnsGuna2TextBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.dnsGuna2TextBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.dnsGuna2TextBox.FocusedState.Parent = this.dnsGuna2TextBox;
            this.dnsGuna2TextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dnsGuna2TextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.dnsGuna2TextBox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.dnsGuna2TextBox.HoverState.Parent = this.dnsGuna2TextBox;
            this.dnsGuna2TextBox.Location = new System.Drawing.Point(95, 50);
            this.dnsGuna2TextBox.Name = "dnsGuna2TextBox";
            this.dnsGuna2TextBox.PasswordChar = '\0';
            this.dnsGuna2TextBox.PlaceholderForeColor = System.Drawing.Color.Gainsboro;
            this.dnsGuna2TextBox.PlaceholderText = "...";
            this.dnsGuna2TextBox.SelectedText = "";
            this.dnsGuna2TextBox.ShadowDecoration.Parent = this.dnsGuna2TextBox;
            this.dnsGuna2TextBox.Size = new System.Drawing.Size(625, 36);
            this.dnsGuna2TextBox.TabIndex = 13;
            // 
            // guna2GroupBox2
            // 
            this.guna2GroupBox2.AllowDrop = true;
            this.guna2GroupBox2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.guna2GroupBox2.Controls.Add(this.x64StubGuna2CheckBox);
            this.guna2GroupBox2.Controls.Add(this.vbStubGuna2CheckBox);
            this.guna2GroupBox2.Controls.Add(this.offKeyloguna2CheckBox);
            this.guna2GroupBox2.Controls.Add(this.taskNameGuna2TextBox);
            this.guna2GroupBox2.Controls.Add(this.persistenceGuna2CheckBox);
            this.guna2GroupBox2.Controls.Add(this.label3);
            this.guna2GroupBox2.Controls.Add(this.timeTaskGuna2TextBox);
            this.guna2GroupBox2.Controls.Add(this.label4);
            this.guna2GroupBox2.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.guna2GroupBox2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.guna2GroupBox2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2GroupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.guna2GroupBox2.Location = new System.Drawing.Point(6, 228);
            this.guna2GroupBox2.Name = "guna2GroupBox2";
            this.guna2GroupBox2.Padding = new System.Windows.Forms.Padding(2, 40, 2, 2);
            this.guna2GroupBox2.ShadowDecoration.Parent = this.guna2GroupBox2;
            this.guna2GroupBox2.Size = new System.Drawing.Size(725, 197);
            this.guna2GroupBox2.TabIndex = 17;
            this.guna2GroupBox2.Text = "Miscellaneous";
            // 
            // vbStubGuna2CheckBox
            // 
            this.vbStubGuna2CheckBox.AutoSize = true;
            this.vbStubGuna2CheckBox.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.vbStubGuna2CheckBox.CheckedState.BorderRadius = 0;
            this.vbStubGuna2CheckBox.CheckedState.BorderThickness = 0;
            this.vbStubGuna2CheckBox.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.vbStubGuna2CheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.vbStubGuna2CheckBox.Location = new System.Drawing.Point(223, 53);
            this.vbStubGuna2CheckBox.Name = "vbStubGuna2CheckBox";
            this.vbStubGuna2CheckBox.Size = new System.Drawing.Size(114, 19);
            this.vbStubGuna2CheckBox.TabIndex = 21;
            this.vbStubGuna2CheckBox.Text = "Visual Basic Stub";
            this.vbStubGuna2CheckBox.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.vbStubGuna2CheckBox.UncheckedState.BorderRadius = 0;
            this.vbStubGuna2CheckBox.UncheckedState.BorderThickness = 0;
            this.vbStubGuna2CheckBox.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            // 
            // offKeyloguna2CheckBox
            // 
            this.offKeyloguna2CheckBox.AutoSize = true;
            this.offKeyloguna2CheckBox.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.offKeyloguna2CheckBox.CheckedState.BorderRadius = 0;
            this.offKeyloguna2CheckBox.CheckedState.BorderThickness = 0;
            this.offKeyloguna2CheckBox.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.offKeyloguna2CheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.offKeyloguna2CheckBox.Location = new System.Drawing.Point(99, 53);
            this.offKeyloguna2CheckBox.Name = "offKeyloguna2CheckBox";
            this.offKeyloguna2CheckBox.Size = new System.Drawing.Size(118, 19);
            this.offKeyloguna2CheckBox.TabIndex = 20;
            this.offKeyloguna2CheckBox.Text = "Offline Keylogger";
            this.offKeyloguna2CheckBox.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.offKeyloguna2CheckBox.UncheckedState.BorderRadius = 0;
            this.offKeyloguna2CheckBox.UncheckedState.BorderThickness = 0;
            this.offKeyloguna2CheckBox.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            // 
            // taskNameGuna2TextBox
            // 
            this.taskNameGuna2TextBox.Animated = true;
            this.taskNameGuna2TextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.taskNameGuna2TextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.taskNameGuna2TextBox.DefaultText = "";
            this.taskNameGuna2TextBox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.taskNameGuna2TextBox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.taskNameGuna2TextBox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.taskNameGuna2TextBox.DisabledState.Parent = this.taskNameGuna2TextBox;
            this.taskNameGuna2TextBox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.taskNameGuna2TextBox.Enabled = false;
            this.taskNameGuna2TextBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.taskNameGuna2TextBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.taskNameGuna2TextBox.FocusedState.Parent = this.taskNameGuna2TextBox;
            this.taskNameGuna2TextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.taskNameGuna2TextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.taskNameGuna2TextBox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.taskNameGuna2TextBox.HoverState.Parent = this.taskNameGuna2TextBox;
            this.taskNameGuna2TextBox.Location = new System.Drawing.Point(95, 98);
            this.taskNameGuna2TextBox.Name = "taskNameGuna2TextBox";
            this.taskNameGuna2TextBox.PasswordChar = '\0';
            this.taskNameGuna2TextBox.PlaceholderForeColor = System.Drawing.Color.Gainsboro;
            this.taskNameGuna2TextBox.PlaceholderText = "...";
            this.taskNameGuna2TextBox.SelectedText = "";
            this.taskNameGuna2TextBox.ShadowDecoration.Parent = this.taskNameGuna2TextBox;
            this.taskNameGuna2TextBox.Size = new System.Drawing.Size(625, 36);
            this.taskNameGuna2TextBox.TabIndex = 13;
            // 
            // persistenceGuna2CheckBox
            // 
            this.persistenceGuna2CheckBox.AutoSize = true;
            this.persistenceGuna2CheckBox.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.persistenceGuna2CheckBox.CheckedState.BorderRadius = 0;
            this.persistenceGuna2CheckBox.CheckedState.BorderThickness = 0;
            this.persistenceGuna2CheckBox.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.persistenceGuna2CheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.persistenceGuna2CheckBox.Location = new System.Drawing.Point(8, 53);
            this.persistenceGuna2CheckBox.Name = "persistenceGuna2CheckBox";
            this.persistenceGuna2CheckBox.Size = new System.Drawing.Size(85, 19);
            this.persistenceGuna2CheckBox.TabIndex = 19;
            this.persistenceGuna2CheckBox.Text = "Persistence";
            this.persistenceGuna2CheckBox.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.persistenceGuna2CheckBox.UncheckedState.BorderRadius = 0;
            this.persistenceGuna2CheckBox.UncheckedState.BorderThickness = 0;
            this.persistenceGuna2CheckBox.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.persistenceGuna2CheckBox.CheckedChanged += new System.EventHandler(this.persistenceSoundGuna2CheckBox_CheckedChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(5, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 36);
            this.label3.TabIndex = 16;
            this.label3.Text = "Time (in min) :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timeTaskGuna2TextBox
            // 
            this.timeTaskGuna2TextBox.Animated = true;
            this.timeTaskGuna2TextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.timeTaskGuna2TextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.timeTaskGuna2TextBox.DefaultText = "";
            this.timeTaskGuna2TextBox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.timeTaskGuna2TextBox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.timeTaskGuna2TextBox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.timeTaskGuna2TextBox.DisabledState.Parent = this.timeTaskGuna2TextBox;
            this.timeTaskGuna2TextBox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.timeTaskGuna2TextBox.Enabled = false;
            this.timeTaskGuna2TextBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.timeTaskGuna2TextBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.timeTaskGuna2TextBox.FocusedState.Parent = this.timeTaskGuna2TextBox;
            this.timeTaskGuna2TextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.timeTaskGuna2TextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.timeTaskGuna2TextBox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.timeTaskGuna2TextBox.HoverState.Parent = this.timeTaskGuna2TextBox;
            this.timeTaskGuna2TextBox.Location = new System.Drawing.Point(95, 146);
            this.timeTaskGuna2TextBox.Name = "timeTaskGuna2TextBox";
            this.timeTaskGuna2TextBox.PasswordChar = '\0';
            this.timeTaskGuna2TextBox.PlaceholderForeColor = System.Drawing.Color.Gainsboro;
            this.timeTaskGuna2TextBox.PlaceholderText = "...";
            this.timeTaskGuna2TextBox.SelectedText = "";
            this.timeTaskGuna2TextBox.ShadowDecoration.Parent = this.timeTaskGuna2TextBox;
            this.timeTaskGuna2TextBox.Size = new System.Drawing.Size(625, 36);
            this.timeTaskGuna2TextBox.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(5, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 36);
            this.label4.TabIndex = 14;
            this.label4.Text = "Task name:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buildGuna2Button
            // 
            this.buildGuna2Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buildGuna2Button.Animated = true;
            this.buildGuna2Button.CheckedState.Parent = this.buildGuna2Button;
            this.buildGuna2Button.CustomImages.Parent = this.buildGuna2Button;
            this.buildGuna2Button.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buildGuna2Button.ForeColor = System.Drawing.Color.White;
            this.buildGuna2Button.HoverState.Parent = this.buildGuna2Button;
            this.buildGuna2Button.Location = new System.Drawing.Point(6, 431);
            this.buildGuna2Button.Name = "buildGuna2Button";
            this.buildGuna2Button.ShadowDecoration.Enabled = true;
            this.buildGuna2Button.ShadowDecoration.Parent = this.buildGuna2Button;
            this.buildGuna2Button.Size = new System.Drawing.Size(725, 32);
            this.buildGuna2Button.TabIndex = 18;
            this.buildGuna2Button.Text = "Build !";
            this.buildGuna2Button.Click += new System.EventHandler(this.buildGuna2Button_Click);
            // 
            // x64StubGuna2CheckBox
            // 
            this.x64StubGuna2CheckBox.AutoSize = true;
            this.x64StubGuna2CheckBox.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.x64StubGuna2CheckBox.CheckedState.BorderRadius = 0;
            this.x64StubGuna2CheckBox.CheckedState.BorderThickness = 0;
            this.x64StubGuna2CheckBox.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.x64StubGuna2CheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.x64StubGuna2CheckBox.Location = new System.Drawing.Point(343, 53);
            this.x64StubGuna2CheckBox.Name = "x64StubGuna2CheckBox";
            this.x64StubGuna2CheckBox.Size = new System.Drawing.Size(70, 19);
            this.x64StubGuna2CheckBox.TabIndex = 22;
            this.x64StubGuna2CheckBox.Text = "x64 stub";
            this.x64StubGuna2CheckBox.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.x64StubGuna2CheckBox.UncheckedState.BorderRadius = 0;
            this.x64StubGuna2CheckBox.UncheckedState.BorderThickness = 0;
            this.x64StubGuna2CheckBox.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(737, 473);
            this.Controls.Add(this.buildGuna2Button);
            this.Controls.Add(this.guna2GroupBox2);
            this.Controls.Add(this.guna2GroupBox1);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.maximizeButton);
            this.Controls.Add(this.minimizeButton);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Padding = new System.Windows.Forms.Padding(3, 32, 3, 3);
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Main_Load);
            this.guna2GroupBox1.ResumeLayout(false);
            this.guna2GroupBox2.ResumeLayout(false);
            this.guna2GroupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button closeButton;
        private Guna.UI2.WinForms.Guna2Button maximizeButton;
        private Guna.UI2.WinForms.Guna2Button minimizeButton;
        private Guna.UI2.WinForms.Guna2GroupBox guna2GroupBox1;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2TextBox portGuna2TextBox;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2TextBox dnsGuna2TextBox;
        private Guna.UI2.WinForms.Guna2GroupBox guna2GroupBox2;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2TextBox timeTaskGuna2TextBox;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2TextBox taskNameGuna2TextBox;
        private System.Windows.Forms.Label label5;
        private Guna.UI2.WinForms.Guna2TextBox keyGuna2TextBox;
        private Guna.UI2.WinForms.Guna2CheckBox persistenceGuna2CheckBox;
        private Guna.UI2.WinForms.Guna2Button buildGuna2Button;
        private Guna.UI2.WinForms.Guna2CheckBox offKeyloguna2CheckBox;
        private Guna.UI2.WinForms.Guna2CheckBox vbStubGuna2CheckBox;
        private Guna.UI2.WinForms.Guna2CheckBox x64StubGuna2CheckBox;
    }
}

