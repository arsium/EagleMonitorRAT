
namespace Eagle_Monitor.Forms
{
    partial class ExecuteForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExecuteForm));
            this.managedContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.injectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unmanagedContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.injectToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.labelSize = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.loadingCircle1 = new MRG.Controls.UI.LoadingCircle();
            this.closeButton = new Eagle_Monitor.Controls.WindowsButton();
            this.maximizeButton = new Eagle_Monitor.Controls.WindowsButton();
            this.minimizeButton = new Eagle_Monitor.Controls.WindowsButton();
            this.windowsTabControls1 = new Eagle_Monitor.Controls.WindowsTabControls();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.managedListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.unmanagedListView = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.shellCodeListView = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.shellCodeContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.nativePEListView = new System.Windows.Forms.ListView();
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.nativePEContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.managedContextMenuStrip.SuspendLayout();
            this.unmanagedContextMenuStrip.SuspendLayout();
            this.windowsTabControls1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.shellCodeContextMenuStrip.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.nativePEContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // managedContextMenuStrip
            // 
            this.managedContextMenuStrip.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.managedContextMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.managedContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.injectToolStripMenuItem});
            this.managedContextMenuStrip.Name = "managedContextMenuStrip";
            this.managedContextMenuStrip.Size = new System.Drawing.Size(123, 94);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.addToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.addToolStripMenuItem.Image = global::Eagle_Monitor.Properties.Resources.icons8_add_32;
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(122, 30);
            this.addToolStripMenuItem.Text = "Add";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.removeToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.removeToolStripMenuItem.Image = global::Eagle_Monitor.Properties.Resources.icons8_minus_32;
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(122, 30);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // injectToolStripMenuItem
            // 
            this.injectToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.injectToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.injectToolStripMenuItem.Image = global::Eagle_Monitor.Properties.Resources.setupapi_32;
            this.injectToolStripMenuItem.Name = "injectToolStripMenuItem";
            this.injectToolStripMenuItem.Size = new System.Drawing.Size(122, 30);
            this.injectToolStripMenuItem.Text = "Inject";
            this.injectToolStripMenuItem.Click += new System.EventHandler(this.injectToolStripMenuItem_Click);
            // 
            // unmanagedContextMenuStrip
            // 
            this.unmanagedContextMenuStrip.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.unmanagedContextMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.unmanagedContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem1,
            this.removeToolStripMenuItem1,
            this.injectToolStripMenuItem1});
            this.unmanagedContextMenuStrip.Name = "contextMenuStrip1";
            this.unmanagedContextMenuStrip.Size = new System.Drawing.Size(123, 94);
            // 
            // addToolStripMenuItem1
            // 
            this.addToolStripMenuItem1.BackColor = System.Drawing.Color.White;
            this.addToolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.addToolStripMenuItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.addToolStripMenuItem1.Image = global::Eagle_Monitor.Properties.Resources.icons8_add_32;
            this.addToolStripMenuItem1.Name = "addToolStripMenuItem1";
            this.addToolStripMenuItem1.Size = new System.Drawing.Size(122, 30);
            this.addToolStripMenuItem1.Text = "Add";
            this.addToolStripMenuItem1.Click += new System.EventHandler(this.addToolStripMenuItem1_Click);
            // 
            // removeToolStripMenuItem1
            // 
            this.removeToolStripMenuItem1.BackColor = System.Drawing.Color.White;
            this.removeToolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.removeToolStripMenuItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.removeToolStripMenuItem1.Image = global::Eagle_Monitor.Properties.Resources.icons8_minus_32;
            this.removeToolStripMenuItem1.Name = "removeToolStripMenuItem1";
            this.removeToolStripMenuItem1.Size = new System.Drawing.Size(122, 30);
            this.removeToolStripMenuItem1.Text = "Remove";
            this.removeToolStripMenuItem1.Click += new System.EventHandler(this.removeToolStripMenuItem1_Click);
            // 
            // injectToolStripMenuItem1
            // 
            this.injectToolStripMenuItem1.BackColor = System.Drawing.Color.White;
            this.injectToolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.injectToolStripMenuItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.injectToolStripMenuItem1.Image = global::Eagle_Monitor.Properties.Resources.setupapi_32;
            this.injectToolStripMenuItem1.Name = "injectToolStripMenuItem1";
            this.injectToolStripMenuItem1.Size = new System.Drawing.Size(122, 30);
            this.injectToolStripMenuItem1.Text = "Inject";
            this.injectToolStripMenuItem1.Click += new System.EventHandler(this.injectToolStripMenuItem1_Click);
            // 
            // labelSize
            // 
            this.labelSize.AutoSize = true;
            this.labelSize.BackColor = System.Drawing.Color.Transparent;
            this.labelSize.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.labelSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelSize.Location = new System.Drawing.Point(112, 11);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(13, 13);
            this.labelSize.TabIndex = 25;
            this.labelSize.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(28, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Bytes Received :";
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
            this.loadingCircle1.TabIndex = 23;
            this.loadingCircle1.Text = "loadingCircle1";
            this.loadingCircle1.Visible = false;
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
            this.closeButton.Location = new System.Drawing.Point(764, 0);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(33, 30);
            this.closeButton.TabIndex = 20;
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
            this.maximizeButton.Location = new System.Drawing.Point(730, 0);
            this.maximizeButton.Name = "maximizeButton";
            this.maximizeButton.Size = new System.Drawing.Size(33, 30);
            this.maximizeButton.TabIndex = 19;
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
            this.minimizeButton.Location = new System.Drawing.Point(696, 0);
            this.minimizeButton.Name = "minimizeButton";
            this.minimizeButton.Size = new System.Drawing.Size(33, 30);
            this.minimizeButton.TabIndex = 18;
            this.minimizeButton.Text = "-";
            this.minimizeButton.UseVisualStyleBackColor = false;
            this.minimizeButton.Click += new System.EventHandler(this.minimizeButton_Click);
            // 
            // windowsTabControls1
            // 
            this.windowsTabControls1.CloseBtnColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(51)))));
            this.windowsTabControls1.Controls.Add(this.tabPage1);
            this.windowsTabControls1.Controls.Add(this.tabPage2);
            this.windowsTabControls1.Controls.Add(this.tabPage3);
            this.windowsTabControls1.Controls.Add(this.tabPage4);
            this.windowsTabControls1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.windowsTabControls1.ForeColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.windowsTabControls1.HeadSelectedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(173)))), ((int)(((byte)(239)))));
            this.windowsTabControls1.IsShowCloseBtn = false;
            this.windowsTabControls1.ItemSize = new System.Drawing.Size(0, 50);
            this.windowsTabControls1.Location = new System.Drawing.Point(3, 30);
            this.windowsTabControls1.Name = "windowsTabControls1";
            this.windowsTabControls1.SelectedIndex = 0;
            this.windowsTabControls1.Size = new System.Drawing.Size(794, 417);
            this.windowsTabControls1.TabIndex = 0;
            this.windowsTabControls1.UncloseTabIndexs = null;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.managedListView);
            this.tabPage1.Location = new System.Drawing.Point(4, 54);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(786, 359);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Managed DLLs";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // managedListView
            // 
            this.managedListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.managedListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader5});
            this.managedListView.ContextMenuStrip = this.managedContextMenuStrip;
            this.managedListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.managedListView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.managedListView.HideSelection = false;
            this.managedListView.Location = new System.Drawing.Point(3, 3);
            this.managedListView.Name = "managedListView";
            this.managedListView.Size = new System.Drawing.Size(780, 353);
            this.managedListView.TabIndex = 0;
            this.managedListView.UseCompatibleStateImageBehavior = false;
            this.managedListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Path";
            this.columnHeader1.Width = 350;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Entrypoint";
            this.columnHeader2.Width = 204;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Size";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.unmanagedListView);
            this.tabPage2.Location = new System.Drawing.Point(4, 54);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(786, 359);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Native DLLs";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // unmanagedListView
            // 
            this.unmanagedListView.BackColor = System.Drawing.Color.White;
            this.unmanagedListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.unmanagedListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.unmanagedListView.ContextMenuStrip = this.unmanagedContextMenuStrip;
            this.unmanagedListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.unmanagedListView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.unmanagedListView.HideSelection = false;
            this.unmanagedListView.Location = new System.Drawing.Point(3, 3);
            this.unmanagedListView.Name = "unmanagedListView";
            this.unmanagedListView.Size = new System.Drawing.Size(780, 353);
            this.unmanagedListView.TabIndex = 1;
            this.unmanagedListView.UseCompatibleStateImageBehavior = false;
            this.unmanagedListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Path";
            this.columnHeader3.Width = 528;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Size";
            this.columnHeader4.Width = 204;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.shellCodeListView);
            this.tabPage3.Location = new System.Drawing.Point(4, 54);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(786, 359);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "ShellCode Files";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // shellCodeListView
            // 
            this.shellCodeListView.BackColor = System.Drawing.Color.White;
            this.shellCodeListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.shellCodeListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader7});
            this.shellCodeListView.ContextMenuStrip = this.shellCodeContextMenuStrip;
            this.shellCodeListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shellCodeListView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.shellCodeListView.HideSelection = false;
            this.shellCodeListView.Location = new System.Drawing.Point(3, 3);
            this.shellCodeListView.Name = "shellCodeListView";
            this.shellCodeListView.Size = new System.Drawing.Size(780, 353);
            this.shellCodeListView.TabIndex = 2;
            this.shellCodeListView.UseCompatibleStateImageBehavior = false;
            this.shellCodeListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Path";
            this.columnHeader6.Width = 528;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Size";
            this.columnHeader7.Width = 204;
            // 
            // shellCodeContextMenuStrip
            // 
            this.shellCodeContextMenuStrip.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.shellCodeContextMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.shellCodeContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3});
            this.shellCodeContextMenuStrip.Name = "contextMenuStrip1";
            this.shellCodeContextMenuStrip.Size = new System.Drawing.Size(123, 94);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripMenuItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.toolStripMenuItem1.Image = global::Eagle_Monitor.Properties.Resources.icons8_add_32;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(122, 30);
            this.toolStripMenuItem1.Text = "Add";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem2.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripMenuItem2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.toolStripMenuItem2.Image = global::Eagle_Monitor.Properties.Resources.icons8_minus_32;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(122, 30);
            this.toolStripMenuItem2.Text = "Remove";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem3.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripMenuItem3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.toolStripMenuItem3.Image = global::Eagle_Monitor.Properties.Resources.setupapi_32;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(122, 30);
            this.toolStripMenuItem3.Text = "Inject";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.nativePEListView);
            this.tabPage4.Location = new System.Drawing.Point(4, 54);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(786, 359);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Native PE";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // nativePEListView
            // 
            this.nativePEListView.BackColor = System.Drawing.Color.White;
            this.nativePEListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nativePEListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader8,
            this.columnHeader9});
            this.nativePEListView.ContextMenuStrip = this.nativePEContextMenuStrip;
            this.nativePEListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nativePEListView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.nativePEListView.HideSelection = false;
            this.nativePEListView.Location = new System.Drawing.Point(3, 3);
            this.nativePEListView.Name = "nativePEListView";
            this.nativePEListView.Size = new System.Drawing.Size(780, 353);
            this.nativePEListView.TabIndex = 2;
            this.nativePEListView.UseCompatibleStateImageBehavior = false;
            this.nativePEListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Path";
            this.columnHeader8.Width = 528;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Size";
            this.columnHeader9.Width = 204;
            // 
            // nativePEContextMenuStrip
            // 
            this.nativePEContextMenuStrip.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.nativePEContextMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.nativePEContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6});
            this.nativePEContextMenuStrip.Name = "contextMenuStrip1";
            this.nativePEContextMenuStrip.Size = new System.Drawing.Size(123, 94);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem4.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripMenuItem4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.toolStripMenuItem4.Image = global::Eagle_Monitor.Properties.Resources.icons8_add_32;
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(122, 30);
            this.toolStripMenuItem4.Text = "Add";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem5.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripMenuItem5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.toolStripMenuItem5.Image = global::Eagle_Monitor.Properties.Resources.icons8_minus_32;
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(122, 30);
            this.toolStripMenuItem5.Text = "Remove";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.BackColor = System.Drawing.Color.White;
            this.toolStripMenuItem6.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripMenuItem6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.toolStripMenuItem6.Image = global::Eagle_Monitor.Properties.Resources.setupapi_32;
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(122, 30);
            this.toolStripMenuItem6.Text = "Inject";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.toolStripMenuItem6_Click);
            // 
            // ExecuteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelSize);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.loadingCircle1);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.maximizeButton);
            this.Controls.Add(this.minimizeButton);
            this.Controls.Add(this.windowsTabControls1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ExecuteForm";
            this.Padding = new System.Windows.Forms.Padding(3, 30, 3, 3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ExecuteDllForm";
            this.Load += new System.EventHandler(this.ExecuteDllForm_Load);
            this.managedContextMenuStrip.ResumeLayout(false);
            this.unmanagedContextMenuStrip.ResumeLayout(false);
            this.windowsTabControls1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.shellCodeContextMenuStrip.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.nativePEContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.WindowsTabControls windowsTabControls1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private Controls.WindowsButton closeButton;
        private Controls.WindowsButton maximizeButton;
        private Controls.WindowsButton minimizeButton;
        private System.Windows.Forms.ListView managedListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ListView unmanagedListView;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ContextMenuStrip managedContextMenuStrip;
        private System.Windows.Forms.ContextMenuStrip unmanagedContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem injectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem injectToolStripMenuItem1;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        public System.Windows.Forms.Label labelSize;
        public System.Windows.Forms.Label label1;
        public MRG.Controls.UI.LoadingCircle loadingCircle1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListView shellCodeListView;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ContextMenuStrip shellCodeContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ListView nativePEListView;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ContextMenuStrip nativePEContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
    }
}