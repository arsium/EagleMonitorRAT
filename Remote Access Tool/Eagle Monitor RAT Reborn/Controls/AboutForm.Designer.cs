namespace Eagle_Monitor_RAT_Reborn
{
    partial class AboutForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.aboutDataGridView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.closeGuna2ControlBox = new Guna.UI2.WinForms.Guna2ControlBox();
            this.maximizeGuna2ControlBox = new Guna.UI2.WinForms.Guna2ControlBox();
            this.minimizeGuna2ControlBox = new Guna.UI2.WinForms.Guna2ControlBox();
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.aboutDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // aboutDataGridView
            // 
            this.aboutDataGridView.AllowUserToAddRows = false;
            this.aboutDataGridView.AllowUserToDeleteRows = false;
            this.aboutDataGridView.AllowUserToResizeColumns = false;
            this.aboutDataGridView.AllowUserToResizeRows = false;
            this.aboutDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.aboutDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.aboutDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.aboutDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 8F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(115)))), ((int)(((byte)(115)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.aboutDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.aboutDataGridView.ColumnHeadersHeight = 36;
            this.aboutDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.aboutDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aboutDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.aboutDataGridView.EnableHeadersVisualStyles = false;
            this.aboutDataGridView.GridColor = System.Drawing.Color.White;
            this.aboutDataGridView.Location = new System.Drawing.Point(2, 32);
            this.aboutDataGridView.Name = "aboutDataGridView";
            this.aboutDataGridView.ReadOnly = true;
            this.aboutDataGridView.RowHeadersVisible = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(125)))), ((int)(((byte)(125)))));
            this.aboutDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.aboutDataGridView.RowTemplate.Height = 26;
            this.aboutDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.aboutDataGridView.Size = new System.Drawing.Size(434, 190);
            this.aboutDataGridView.TabIndex = 5;
            this.aboutDataGridView.TabStop = false;
            this.aboutDataGridView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseUp);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Column1.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column1.FillWeight = 10F;
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.Column2.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column2.FillWeight = 18F;
            this.Column2.HeaderText = "";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // closeGuna2ControlBox
            // 
            this.closeGuna2ControlBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeGuna2ControlBox.CustomClick = true;
            this.closeGuna2ControlBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(151)))), ((int)(((byte)(249)))));
            this.closeGuna2ControlBox.IconColor = System.Drawing.Color.White;
            this.closeGuna2ControlBox.Location = new System.Drawing.Point(391, 1);
            this.closeGuna2ControlBox.Name = "closeGuna2ControlBox";
            this.closeGuna2ControlBox.Size = new System.Drawing.Size(45, 29);
            this.closeGuna2ControlBox.TabIndex = 2;
            this.closeGuna2ControlBox.Click += new System.EventHandler(this.closeGuna2ControlBox_Click);
            // 
            // maximizeGuna2ControlBox
            // 
            this.maximizeGuna2ControlBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.maximizeGuna2ControlBox.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MaximizeBox;
            this.maximizeGuna2ControlBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(151)))), ((int)(((byte)(249)))));
            this.maximizeGuna2ControlBox.IconColor = System.Drawing.Color.White;
            this.maximizeGuna2ControlBox.Location = new System.Drawing.Point(344, 1);
            this.maximizeGuna2ControlBox.Name = "maximizeGuna2ControlBox";
            this.maximizeGuna2ControlBox.Size = new System.Drawing.Size(45, 29);
            this.maximizeGuna2ControlBox.TabIndex = 3;
            // 
            // minimizeGuna2ControlBox
            // 
            this.minimizeGuna2ControlBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.minimizeGuna2ControlBox.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            this.minimizeGuna2ControlBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(151)))), ((int)(((byte)(249)))));
            this.minimizeGuna2ControlBox.IconColor = System.Drawing.Color.White;
            this.minimizeGuna2ControlBox.Location = new System.Drawing.Point(297, 1);
            this.minimizeGuna2ControlBox.Name = "minimizeGuna2ControlBox";
            this.minimizeGuna2ControlBox.Size = new System.Drawing.Size(45, 29);
            this.minimizeGuna2ControlBox.TabIndex = 4;
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.logoPictureBox.Image = global::Eagle_Monitor_RAT_Reborn.Properties.Resources.eagle2;
            this.logoPictureBox.Location = new System.Drawing.Point(4, 1);
            this.logoPictureBox.Name = "logoPictureBox";
            this.logoPictureBox.Size = new System.Drawing.Size(32, 28);
            this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoPictureBox.TabIndex = 5;
            this.logoPictureBox.TabStop = false;
            this.logoPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.logoPictureBox_MouseDown);
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(151)))), ((int)(((byte)(249)))));
            this.BordersSize = 2;
            this.ClientSize = new System.Drawing.Size(438, 224);
            this.Controls.Add(this.aboutDataGridView);
            this.Controls.Add(this.logoPictureBox);
            this.Controls.Add(this.minimizeGuna2ControlBox);
            this.Controls.Add(this.maximizeGuna2ControlBox);
            this.Controls.Add(this.closeGuna2ControlBox);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 8F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AboutForm";
            this.Padding = new System.Windows.Forms.Padding(2, 32, 2, 2);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Eagle Monitor RAT Reborn";
            this.TopBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(151)))), ((int)(((byte)(249)))));
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MouseLeave += new System.EventHandler(this.MainForm_MouseLeave);
            ((System.ComponentModel.ISupportInitialize)(this.aboutDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2ControlBox closeGuna2ControlBox;
        private Guna.UI2.WinForms.Guna2ControlBox maximizeGuna2ControlBox;
        private Guna.UI2.WinForms.Guna2ControlBox minimizeGuna2ControlBox;
        public System.Windows.Forms.DataGridView aboutDataGridView;
        private System.Windows.Forms.PictureBox logoPictureBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}

