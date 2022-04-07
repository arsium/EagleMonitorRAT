namespace Eagle_Monitor_Tasks_Configurator
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
            this.taskListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.taskContextMenuStrip = new EagleMonitor.Controls.CustomContextMenuStrip();
            this.payloadExecutionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.historyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.passwordsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveTasksToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList();
            this.label1 = new System.Windows.Forms.Label();
            this.guna2GroupBox1.SuspendLayout();
            this.taskContextMenuStrip.SuspendLayout();
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
            this.closeButton.TabIndex = 8;
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
            this.maximizeButton.TabIndex = 7;
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
            this.minimizeButton.TabIndex = 6;
            this.minimizeButton.Text = "🗕";
            this.minimizeButton.Click += new System.EventHandler(this.minimizeButton_Click);
            // 
            // guna2GroupBox1
            // 
            this.guna2GroupBox1.AllowDrop = true;
            this.guna2GroupBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.guna2GroupBox1.Controls.Add(this.taskListView);
            this.guna2GroupBox1.Controls.Add(this.label1);
            this.guna2GroupBox1.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.guna2GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2GroupBox1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.guna2GroupBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2GroupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.guna2GroupBox1.Location = new System.Drawing.Point(3, 32);
            this.guna2GroupBox1.Name = "guna2GroupBox1";
            this.guna2GroupBox1.Padding = new System.Windows.Forms.Padding(2, 40, 2, 2);
            this.guna2GroupBox1.ShadowDecoration.Parent = this.guna2GroupBox1;
            this.guna2GroupBox1.Size = new System.Drawing.Size(794, 415);
            this.guna2GroupBox1.TabIndex = 9;
            this.guna2GroupBox1.Text = "Tasks";
            // 
            // taskListView
            // 
            this.taskListView.AllowDrop = true;
            this.taskListView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.taskListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.taskListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.taskListView.ContextMenuStrip = this.taskContextMenuStrip;
            this.taskListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.taskListView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.taskListView.HideSelection = false;
            this.taskListView.LargeImageList = this.imageList1;
            this.taskListView.Location = new System.Drawing.Point(2, 40);
            this.taskListView.Name = "taskListView";
            this.taskListView.Size = new System.Drawing.Size(790, 358);
            this.taskListView.SmallImageList = this.imageList1;
            this.taskListView.TabIndex = 0;
            this.taskListView.UseCompatibleStateImageBehavior = false;
            this.taskListView.View = System.Windows.Forms.View.Tile;
            this.taskListView.DragDrop += new System.Windows.Forms.DragEventHandler(this.taskListView_DragDrop);
            this.taskListView.DragEnter += new System.Windows.Forms.DragEventHandler(this.taskListView_DragEnter);
            // 
            // taskContextMenuStrip
            // 
            this.taskContextMenuStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.taskContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.payloadExecutionToolStripMenuItem,
            this.historyToolStripMenuItem1,
            this.passwordsToolStripMenuItem,
            this.saveTasksToolStripMenuItem1});
            this.taskContextMenuStrip.Name = "taskContextMenuStrip";
            this.taskContextMenuStrip.Size = new System.Drawing.Size(197, 178);
            // 
            // payloadExecutionToolStripMenuItem
            // 
            this.payloadExecutionToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.payloadExecutionToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.payloadExecutionToolStripMenuItem.Image = global::Eagle_Monitor_Tasks_Configurator.Properties.Resources.type_binary_2x;
            this.payloadExecutionToolStripMenuItem.Name = "payloadExecutionToolStripMenuItem";
            this.payloadExecutionToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.payloadExecutionToolStripMenuItem.Text = "Payload Execution";
            this.payloadExecutionToolStripMenuItem.Click += new System.EventHandler(this.payloadExecutionToolStripMenuItem_Click);
            // 
            // historyToolStripMenuItem1
            // 
            this.historyToolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.historyToolStripMenuItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.historyToolStripMenuItem1.Image = global::Eagle_Monitor_Tasks_Configurator.Properties.Resources.history_2x;
            this.historyToolStripMenuItem1.Name = "historyToolStripMenuItem1";
            this.historyToolStripMenuItem1.Size = new System.Drawing.Size(196, 38);
            this.historyToolStripMenuItem1.Text = "History ";
            this.historyToolStripMenuItem1.Click += new System.EventHandler(this.historyToolStripMenuItem1_Click);
            // 
            // passwordsToolStripMenuItem
            // 
            this.passwordsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.passwordsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.passwordsToolStripMenuItem.Image = global::Eagle_Monitor_Tasks_Configurator.Properties.Resources.control_password_2x;
            this.passwordsToolStripMenuItem.Name = "passwordsToolStripMenuItem";
            this.passwordsToolStripMenuItem.Size = new System.Drawing.Size(196, 38);
            this.passwordsToolStripMenuItem.Text = "Passwords";
            this.passwordsToolStripMenuItem.Click += new System.EventHandler(this.passwordsToolStripMenuItem_Click);
            // 
            // saveTasksToolStripMenuItem1
            // 
            this.saveTasksToolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.saveTasksToolStripMenuItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.saveTasksToolStripMenuItem1.Image = global::Eagle_Monitor_Tasks_Configurator.Properties.Resources.save_2x;
            this.saveTasksToolStripMenuItem1.Name = "saveTasksToolStripMenuItem1";
            this.saveTasksToolStripMenuItem1.Size = new System.Drawing.Size(196, 38);
            this.saveTasksToolStripMenuItem1.Text = "Save Tasks";
            this.saveTasksToolStripMenuItem1.Click += new System.EventHandler(this.saveTasksToolStripMenuItem1_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "executable@2x.png");
            this.imageList1.Images.SetKeyName(1, "file-dll@2x.png");
            this.imageList1.Images.SetKeyName(2, "file-binary@2x.png");
            this.imageList1.Images.SetKeyName(3, "history@2x.png");
            this.imageList1.Images.SetKeyName(4, "control-password@2x.png");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.label1.Location = new System.Drawing.Point(2, 398);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 15);
            this.label1.TabIndex = 44;
            this.label1.Text = "Note: Drag and drop payload";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Main
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.guna2GroupBox1);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.maximizeButton);
            this.Controls.Add(this.minimizeButton);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Padding = new System.Windows.Forms.Padding(3, 32, 3, 3);
            this.Text = "Task Configurator";
            this.Load += new System.EventHandler(this.Main_Load);
            this.Shown += new System.EventHandler(this.Main_Shown);
            this.guna2GroupBox1.ResumeLayout(false);
            this.guna2GroupBox1.PerformLayout();
            this.taskContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button closeButton;
        private Guna.UI2.WinForms.Guna2Button maximizeButton;
        private Guna.UI2.WinForms.Guna2Button minimizeButton;
        private Guna.UI2.WinForms.Guna2GroupBox guna2GroupBox1;
        private System.Windows.Forms.ListView taskListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label1;
        private EagleMonitor.Controls.CustomContextMenuStrip taskContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem historyToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem passwordsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveTasksToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem payloadExecutionToolStripMenuItem;
    }
}

