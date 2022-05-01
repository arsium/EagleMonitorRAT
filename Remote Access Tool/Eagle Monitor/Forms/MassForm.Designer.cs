namespace EagleMonitor.Forms
{
    partial class MassForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MassForm));
            this.closeButton = new Guna.UI2.WinForms.Guna2Button();
            this.maximizeButton = new Guna.UI2.WinForms.Guna2Button();
            this.minimizeButton = new Guna.UI2.WinForms.Guna2Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.massTaskContextMenuStrip = new EagleMonitor.Controls.CustomContextMenuStrip();
            this.closeStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.addPacketGuna2Button = new Guna.UI2.WinForms.Guna2Button();
            this.packetGuna2ComboBox = new Guna.UI2.WinForms.Guna2ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.massTaskContextMenuStrip.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.maximizeButton.Location = new System.Drawing.Point(727, 0);
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
            this.minimizeButton.Location = new System.Drawing.Point(689, 0);
            this.minimizeButton.Name = "minimizeButton";
            this.minimizeButton.ShadowDecoration.Enabled = true;
            this.minimizeButton.ShadowDecoration.Parent = this.minimizeButton;
            this.minimizeButton.Size = new System.Drawing.Size(32, 32);
            this.minimizeButton.TabIndex = 3;
            this.minimizeButton.Text = "🗕";
            this.minimizeButton.Click += new System.EventHandler(this.minimizeButton_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.Color.White;
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(3, 98);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(794, 349);
            this.listView1.TabIndex = 6;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Tile;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dataGridView1.ContextMenuStrip = this.massTaskContextMenuStrip;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.Color.White;
            this.dataGridView1.Location = new System.Drawing.Point(3, 98);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.RowTemplate.Height = 26;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(794, 349);
            this.dataGridView1.TabIndex = 7;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseUp);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "N";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.FillWeight = 15F;
            this.Column2.HeaderText = "Packet";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.FillWeight = 15F;
            this.Column3.HeaderText = "Param";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // massTaskContextMenuStrip
            // 
            this.massTaskContextMenuStrip.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.massTaskContextMenuStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.massTaskContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeStripMenuItem});
            this.massTaskContextMenuStrip.Name = "customContextMenuStrip1";
            this.massTaskContextMenuStrip.Size = new System.Drawing.Size(128, 42);
            // 
            // closeStripMenuItem
            // 
            this.closeStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.closeStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.closeStripMenuItem.Image = global::EagleMonitor.Properties.Resources.external_link_2x;
            this.closeStripMenuItem.Name = "closeStripMenuItem";
            this.closeStripMenuItem.Size = new System.Drawing.Size(127, 38);
            this.closeStripMenuItem.Text = "Launch";
            this.closeStripMenuItem.Click += new System.EventHandler(this.closeStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.addPacketGuna2Button);
            this.panel1.Controls.Add(this.packetGuna2ComboBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(794, 66);
            this.panel1.TabIndex = 8;
            // 
            // addPacketGuna2Button
            // 
            this.addPacketGuna2Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addPacketGuna2Button.Animated = true;
            this.addPacketGuna2Button.CheckedState.Parent = this.addPacketGuna2Button;
            this.addPacketGuna2Button.CustomImages.Parent = this.addPacketGuna2Button;
            this.addPacketGuna2Button.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addPacketGuna2Button.ForeColor = System.Drawing.Color.White;
            this.addPacketGuna2Button.HoverState.Parent = this.addPacketGuna2Button;
            this.addPacketGuna2Button.Location = new System.Drawing.Point(254, 14);
            this.addPacketGuna2Button.Name = "addPacketGuna2Button";
            this.addPacketGuna2Button.ShadowDecoration.Parent = this.addPacketGuna2Button;
            this.addPacketGuna2Button.Size = new System.Drawing.Size(537, 36);
            this.addPacketGuna2Button.TabIndex = 9;
            this.addPacketGuna2Button.Text = "Add Packet";
            this.addPacketGuna2Button.Click += new System.EventHandler(this.addPacketGuna2Button_Click);
            // 
            // packetGuna2ComboBox
            // 
            this.packetGuna2ComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.packetGuna2ComboBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.packetGuna2ComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.packetGuna2ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.packetGuna2ComboBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.packetGuna2ComboBox.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.packetGuna2ComboBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.packetGuna2ComboBox.FocusedState.Parent = this.packetGuna2ComboBox;
            this.packetGuna2ComboBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.packetGuna2ComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.packetGuna2ComboBox.HoverState.Parent = this.packetGuna2ComboBox;
            this.packetGuna2ComboBox.ItemHeight = 30;
            this.packetGuna2ComboBox.ItemsAppearance.Parent = this.packetGuna2ComboBox;
            this.packetGuna2ComboBox.Location = new System.Drawing.Point(3, 14);
            this.packetGuna2ComboBox.Name = "packetGuna2ComboBox";
            this.packetGuna2ComboBox.ShadowDecoration.Parent = this.packetGuna2ComboBox;
            this.packetGuna2ComboBox.Size = new System.Drawing.Size(245, 36);
            this.packetGuna2ComboBox.TabIndex = 0;
            // 
            // MassForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.maximizeButton);
            this.Controls.Add(this.minimizeButton);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MassForm";
            this.Padding = new System.Windows.Forms.Padding(3, 32, 3, 3);
            this.Text = "MassForm";
            this.Load += new System.EventHandler(this.MassForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.massTaskContextMenuStrip.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button closeButton;
        private Guna.UI2.WinForms.Guna2Button maximizeButton;
        private Guna.UI2.WinForms.Guna2Button minimizeButton;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private Guna.UI2.WinForms.Guna2Button addPacketGuna2Button;
        private Guna.UI2.WinForms.Guna2ComboBox packetGuna2ComboBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private Controls.CustomContextMenuStrip massTaskContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem closeStripMenuItem;
    }
}