namespace EagleMonitor.Forms
{
    partial class ProcessManagerForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessManagerForm));
            this.label1 = new System.Windows.Forms.Label();
            this.guna2VScrollBar1 = new Guna.UI2.WinForms.Guna2VScrollBar();
            this.loadingCircle1 = new MRG.Controls.UI.LoadingCircle();
            this.closeButton = new Guna.UI2.WinForms.Guna2Button();
            this.maximizeButton = new Guna.UI2.WinForms.Guna2Button();
            this.minimizeButton = new Guna.UI2.WinForms.Guna2Button();
            this.processDataGridView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.processContextMenuStrip = new EagleMonitor.Controls.CustomContextMenuStrip();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.killProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.suspendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resumeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.injectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.injectionCustomContextMenuStrip = new EagleMonitor.Controls.CustomContextMenuStrip();
            this.classicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.processDataGridView)).BeginInit();
            this.processContextMenuStrip.SuspendLayout();
            this.injectionCustomContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(41, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(642, 32);
            this.label1.TabIndex = 40;
            this.label1.Text = "Eagle Monitor RAT";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label1_MouseDown);
            // 
            // guna2VScrollBar1
            // 
            this.guna2VScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.guna2VScrollBar1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.guna2VScrollBar1.InUpdate = false;
            this.guna2VScrollBar1.LargeChange = 10;
            this.guna2VScrollBar1.Location = new System.Drawing.Point(779, 32);
            this.guna2VScrollBar1.Name = "guna2VScrollBar1";
            this.guna2VScrollBar1.ScrollbarSize = 18;
            this.guna2VScrollBar1.Size = new System.Drawing.Size(18, 415);
            this.guna2VScrollBar1.TabIndex = 39;
            this.guna2VScrollBar1.ThumbColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            // 
            // loadingCircle1
            // 
            this.loadingCircle1.Active = true;
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
            this.loadingCircle1.TabIndex = 38;
            this.loadingCircle1.Text = "loadingCircle1";
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.Animated = true;
            this.closeButton.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeButton.ForeColor = System.Drawing.Color.White;
            this.closeButton.Location = new System.Drawing.Point(765, 0);
            this.closeButton.Name = "closeButton";
            this.closeButton.ShadowDecoration.Enabled = true;
            this.closeButton.Size = new System.Drawing.Size(32, 32);
            this.closeButton.TabIndex = 37;
            this.closeButton.Text = "╳";
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // maximizeButton
            // 
            this.maximizeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.maximizeButton.Animated = true;
            this.maximizeButton.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maximizeButton.ForeColor = System.Drawing.Color.White;
            this.maximizeButton.Location = new System.Drawing.Point(727, 0);
            this.maximizeButton.Name = "maximizeButton";
            this.maximizeButton.ShadowDecoration.Enabled = true;
            this.maximizeButton.Size = new System.Drawing.Size(32, 32);
            this.maximizeButton.TabIndex = 36;
            this.maximizeButton.Text = "🗗";
            this.maximizeButton.Click += new System.EventHandler(this.maximizeButton_Click);
            // 
            // minimizeButton
            // 
            this.minimizeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.minimizeButton.Animated = true;
            this.minimizeButton.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minimizeButton.ForeColor = System.Drawing.Color.White;
            this.minimizeButton.Location = new System.Drawing.Point(689, 0);
            this.minimizeButton.Name = "minimizeButton";
            this.minimizeButton.ShadowDecoration.Enabled = true;
            this.minimizeButton.Size = new System.Drawing.Size(32, 32);
            this.minimizeButton.TabIndex = 35;
            this.minimizeButton.Text = "🗕";
            this.minimizeButton.Click += new System.EventHandler(this.minimizeButton_Click);
            // 
            // processDataGridView
            // 
            this.processDataGridView.AllowUserToAddRows = false;
            this.processDataGridView.AllowUserToDeleteRows = false;
            this.processDataGridView.AllowUserToResizeColumns = false;
            this.processDataGridView.AllowUserToResizeRows = false;
            this.processDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.processDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.processDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.processDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.processDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.processDataGridView.ColumnHeadersHeight = 30;
            this.processDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6});
            this.processDataGridView.ContextMenuStrip = this.processContextMenuStrip;
            this.processDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.processDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.processDataGridView.EnableHeadersVisualStyles = false;
            this.processDataGridView.GridColor = System.Drawing.Color.White;
            this.processDataGridView.Location = new System.Drawing.Point(3, 32);
            this.processDataGridView.Name = "processDataGridView";
            this.processDataGridView.ReadOnly = true;
            this.processDataGridView.RowHeadersVisible = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.processDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.processDataGridView.RowTemplate.Height = 26;
            this.processDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.processDataGridView.Size = new System.Drawing.Size(794, 415);
            this.processDataGridView.TabIndex = 4;
            this.processDataGridView.TabStop = false;
            this.processDataGridView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.processDataGridView_MouseUp);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.NullValue = null;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(15, 4, 4, 4);
            this.Column1.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column1.FillWeight = 10F;
            this.Column1.HeaderText = "Icon";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Column2.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column2.FillWeight = 25F;
            this.Column2.HeaderText = "ID";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.Column3.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column3.FillWeight = 18F;
            this.Column3.HeaderText = "Name";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column4.FillWeight = 25F;
            this.Column4.HeaderText = "Window Title";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column5.FillWeight = 20F;
            this.Column5.HeaderText = "Window Handle";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column6.FillWeight = 20F;
            this.Column6.HeaderText = "Is64Bit";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // processContextMenuStrip
            // 
            this.processContextMenuStrip.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.processContextMenuStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.processContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem,
            this.killProcessToolStripMenuItem,
            this.suspendToolStripMenuItem,
            this.resumeToolStripMenuItem,
            this.injectionToolStripMenuItem});
            this.processContextMenuStrip.Name = "processContextMenuStrip";
            this.processContextMenuStrip.Size = new System.Drawing.Size(189, 194);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.refreshToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.refreshToolStripMenuItem.Image = global::EagleMonitor.Properties.Resources.action_repeat_2x;
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(188, 38);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // killProcessToolStripMenuItem
            // 
            this.killProcessToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.killProcessToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.killProcessToolStripMenuItem.Image = global::EagleMonitor.Properties.Resources.close_2x;
            this.killProcessToolStripMenuItem.Name = "killProcessToolStripMenuItem";
            this.killProcessToolStripMenuItem.Size = new System.Drawing.Size(188, 38);
            this.killProcessToolStripMenuItem.Text = "Kill Process";
            this.killProcessToolStripMenuItem.Click += new System.EventHandler(this.killProcessToolStripMenuItem_Click);
            // 
            // suspendToolStripMenuItem
            // 
            this.suspendToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.suspendToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.suspendToolStripMenuItem.Image = global::EagleMonitor.Properties.Resources.btn_grey_play_pause_2x;
            this.suspendToolStripMenuItem.Name = "suspendToolStripMenuItem";
            this.suspendToolStripMenuItem.Size = new System.Drawing.Size(188, 38);
            this.suspendToolStripMenuItem.Text = "Suspend Process";
            this.suspendToolStripMenuItem.Click += new System.EventHandler(this.suspendToolStripMenuItem_Click);
            // 
            // resumeToolStripMenuItem
            // 
            this.resumeToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.resumeToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.resumeToolStripMenuItem.Image = global::EagleMonitor.Properties.Resources.btn_grey_play_2x;
            this.resumeToolStripMenuItem.Name = "resumeToolStripMenuItem";
            this.resumeToolStripMenuItem.Size = new System.Drawing.Size(188, 38);
            this.resumeToolStripMenuItem.Text = "Resume Process";
            this.resumeToolStripMenuItem.Click += new System.EventHandler(this.resumeToolStripMenuItem_Click);
            // 
            // injectionToolStripMenuItem
            // 
            this.injectionToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.injectionToolStripMenuItem.DropDown = this.injectionCustomContextMenuStrip;
            this.injectionToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.injectionToolStripMenuItem.Image = global::EagleMonitor.Properties.Resources.file_binary_2x;
            this.injectionToolStripMenuItem.Name = "injectionToolStripMenuItem";
            this.injectionToolStripMenuItem.Size = new System.Drawing.Size(188, 38);
            this.injectionToolStripMenuItem.Text = "Shellcode Injection";
            // 
            // injectionCustomContextMenuStrip
            // 
            this.injectionCustomContextMenuStrip.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.injectionCustomContextMenuStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.injectionCustomContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.classicToolStripMenuItem,
            this.mapViewToolStripMenuItem});
            this.injectionCustomContextMenuStrip.Name = "processContextMenuStrip";
            this.injectionCustomContextMenuStrip.OwnerItem = this.injectionToolStripMenuItem;
            this.injectionCustomContextMenuStrip.Size = new System.Drawing.Size(142, 80);
            // 
            // classicToolStripMenuItem
            // 
            this.classicToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.classicToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.classicToolStripMenuItem.Image = global::EagleMonitor.Properties.Resources.memory_2x1;
            this.classicToolStripMenuItem.Name = "classicToolStripMenuItem";
            this.classicToolStripMenuItem.Size = new System.Drawing.Size(141, 38);
            this.classicToolStripMenuItem.Text = "Classic";
            this.classicToolStripMenuItem.Click += new System.EventHandler(this.classicToolStripMenuItem_Click);
            // 
            // mapViewToolStripMenuItem
            // 
            this.mapViewToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.mapViewToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.mapViewToolStripMenuItem.Name = "mapViewToolStripMenuItem";
            this.mapViewToolStripMenuItem.Size = new System.Drawing.Size(141, 38);
            this.mapViewToolStripMenuItem.Text = "Map View";
            this.mapViewToolStripMenuItem.Click += new System.EventHandler(this.mapViewToolStripMenuItem_Click);
            // 
            // ProcessManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.guna2VScrollBar1);
            this.Controls.Add(this.loadingCircle1);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.maximizeButton);
            this.Controls.Add(this.minimizeButton);
            this.Controls.Add(this.processDataGridView);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProcessManagerForm";
            this.Padding = new System.Windows.Forms.Padding(3, 32, 3, 3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProcessManagerForm";
            this.Load += new System.EventHandler(this.ProcessManagerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.processDataGridView)).EndInit();
            this.processContextMenuStrip.ResumeLayout(false);
            this.injectionCustomContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView processDataGridView;
        private System.Windows.Forms.DataGridViewImageColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private Guna.UI2.WinForms.Guna2Button closeButton;
        private Guna.UI2.WinForms.Guna2Button maximizeButton;
        private Guna.UI2.WinForms.Guna2Button minimizeButton;
        private Controls.CustomContextMenuStrip processContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem killProcessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem suspendToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resumeToolStripMenuItem;
        public MRG.Controls.UI.LoadingCircle loadingCircle1;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem injectionToolStripMenuItem;
        private Controls.CustomContextMenuStrip injectionCustomContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem classicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mapViewToolStripMenuItem;
        private Guna.UI2.WinForms.Guna2VScrollBar guna2VScrollBar1;
        public System.Windows.Forms.Label label1;
    }
}