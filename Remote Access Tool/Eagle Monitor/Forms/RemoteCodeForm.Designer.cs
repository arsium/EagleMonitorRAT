namespace EagleMonitor
{
    partial class RemoteCodeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RemoteCodeForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.languageComboBox = new Guna.UI2.WinForms.Guna2ComboBox();
            this.codeTextBox = new FastColoredTextBoxNS.FastColoredTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.referenceContextMenuStrip = new EagleMonitor.Controls.CustomContextMenuStrip();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.closeButton = new Guna.UI2.WinForms.Guna2Button();
            this.maximizeButton = new Guna.UI2.WinForms.Guna2Button();
            this.minimizeButton = new Guna.UI2.WinForms.Guna2Button();
            this.testGuna2Button = new Guna.UI2.WinForms.Guna2Button();
            this.sendGuna2Button = new Guna.UI2.WinForms.Guna2Button();
            this.label3 = new System.Windows.Forms.Label();
            this.platformGuna2ComboBox = new Guna.UI2.WinForms.Guna2ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.codeTextBox)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.referenceContextMenuStrip.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // languageComboBox
            // 
            this.languageComboBox.Animated = true;
            this.languageComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.languageComboBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.languageComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.languageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.languageComboBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.languageComboBox.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.languageComboBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.languageComboBox.FocusedState.Parent = this.languageComboBox;
            this.languageComboBox.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.languageComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.languageComboBox.HoverState.Parent = this.languageComboBox;
            this.languageComboBox.ItemHeight = 30;
            this.languageComboBox.Items.AddRange(new object[] {
            "C#",
            "VB"});
            this.languageComboBox.ItemsAppearance.Parent = this.languageComboBox;
            this.languageComboBox.Location = new System.Drawing.Point(6, 40);
            this.languageComboBox.Name = "languageComboBox";
            this.languageComboBox.ShadowDecoration.Parent = this.languageComboBox;
            this.languageComboBox.Size = new System.Drawing.Size(79, 36);
            this.languageComboBox.StartIndex = 0;
            this.languageComboBox.TabIndex = 27;
            this.languageComboBox.SelectedIndexChanged += new System.EventHandler(this.languageComboBox_SelectedIndexChanged);
            // 
            // codeTextBox
            // 
            this.codeTextBox.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.codeTextBox.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\r\n^\\s*(case|default)\\s*[^:]" +
    "*(?<range>:)\\s*(?<range>[^;]+);\r\n";
            this.codeTextBox.AutoScrollMinSize = new System.Drawing.Size(379, 168);
            this.codeTextBox.BackBrush = null;
            this.codeTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.codeTextBox.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.codeTextBox.CaretColor = System.Drawing.Color.White;
            this.codeTextBox.CharHeight = 14;
            this.codeTextBox.CharWidth = 8;
            this.codeTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.codeTextBox.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.codeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.codeTextBox.IndentBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.codeTextBox.IsReplaceMode = false;
            this.codeTextBox.Language = FastColoredTextBoxNS.Language.CSharp;
            this.codeTextBox.LeftBracket = '(';
            this.codeTextBox.LeftBracket2 = '{';
            this.codeTextBox.LineNumberColor = System.Drawing.Color.Cyan;
            this.codeTextBox.Location = new System.Drawing.Point(3, 3);
            this.codeTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.codeTextBox.Name = "codeTextBox";
            this.codeTextBox.Paddings = new System.Windows.Forms.Padding(0);
            this.codeTextBox.RightBracket = ')';
            this.codeTextBox.RightBracket2 = '}';
            this.codeTextBox.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.codeTextBox.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("codeTextBox.ServiceColors")));
            this.codeTextBox.ServiceLinesColor = System.Drawing.Color.Black;
            this.codeTextBox.Size = new System.Drawing.Size(526, 223);
            this.codeTextBox.TabIndex = 28;
            this.codeTextBox.Text = resources.GetString("codeTextBox.Text");
            this.codeTextBox.TextAreaBorderColor = System.Drawing.Color.Red;
            this.codeTextBox.Zoom = 100;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.panel1.Controls.Add(this.codeTextBox);
            this.panel1.Location = new System.Drawing.Point(6, 82);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(532, 229);
            this.panel1.TabIndex = 29;
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
            this.Column1});
            this.dataGridView1.ContextMenuStrip = this.referenceContextMenuStrip;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.Color.White;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
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
            this.dataGridView1.Size = new System.Drawing.Size(198, 265);
            this.dataGridView1.TabIndex = 30;
            this.dataGridView1.TabStop = false;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.FillWeight = 15F;
            this.Column1.HeaderText = "Imports";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // referenceContextMenuStrip
            // 
            this.referenceContextMenuStrip.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.referenceContextMenuStrip.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.referenceContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.removeToolStripMenuItem});
            this.referenceContextMenuStrip.Name = "customContextMenuStrip1";
            this.referenceContextMenuStrip.Size = new System.Drawing.Size(115, 48);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.addToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.addToolStripMenuItem.Text = "Add";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.removeToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Location = new System.Drawing.Point(541, 40);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3);
            this.panel2.Size = new System.Drawing.Size(204, 271);
            this.panel2.TabIndex = 31;
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
            this.closeButton.Location = new System.Drawing.Point(716, 0);
            this.closeButton.Name = "closeButton";
            this.closeButton.ShadowDecoration.Enabled = true;
            this.closeButton.ShadowDecoration.Parent = this.closeButton;
            this.closeButton.Size = new System.Drawing.Size(32, 32);
            this.closeButton.TabIndex = 46;
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
            this.maximizeButton.Location = new System.Drawing.Point(678, 0);
            this.maximizeButton.Name = "maximizeButton";
            this.maximizeButton.ShadowDecoration.Enabled = true;
            this.maximizeButton.ShadowDecoration.Parent = this.maximizeButton;
            this.maximizeButton.Size = new System.Drawing.Size(32, 32);
            this.maximizeButton.TabIndex = 45;
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
            this.minimizeButton.Location = new System.Drawing.Point(640, 0);
            this.minimizeButton.Name = "minimizeButton";
            this.minimizeButton.ShadowDecoration.Enabled = true;
            this.minimizeButton.ShadowDecoration.Parent = this.minimizeButton;
            this.minimizeButton.Size = new System.Drawing.Size(32, 32);
            this.minimizeButton.TabIndex = 44;
            this.minimizeButton.Text = "🗕";
            this.minimizeButton.Click += new System.EventHandler(this.minimizeButton_Click);
            // 
            // testGuna2Button
            // 
            this.testGuna2Button.Animated = true;
            this.testGuna2Button.CheckedState.Parent = this.testGuna2Button;
            this.testGuna2Button.CustomImages.Parent = this.testGuna2Button;
            this.testGuna2Button.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.testGuna2Button.ForeColor = System.Drawing.Color.White;
            this.testGuna2Button.HoverState.Parent = this.testGuna2Button;
            this.testGuna2Button.Location = new System.Drawing.Point(191, 40);
            this.testGuna2Button.Name = "testGuna2Button";
            this.testGuna2Button.ShadowDecoration.Enabled = true;
            this.testGuna2Button.ShadowDecoration.Parent = this.testGuna2Button;
            this.testGuna2Button.Size = new System.Drawing.Size(90, 36);
            this.testGuna2Button.TabIndex = 47;
            this.testGuna2Button.Text = "Test";
            this.testGuna2Button.Click += new System.EventHandler(this.testGuna2Button_Click);
            // 
            // sendGuna2Button
            // 
            this.sendGuna2Button.Animated = true;
            this.sendGuna2Button.CheckedState.Parent = this.sendGuna2Button;
            this.sendGuna2Button.CustomImages.Parent = this.sendGuna2Button;
            this.sendGuna2Button.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.sendGuna2Button.ForeColor = System.Drawing.Color.White;
            this.sendGuna2Button.HoverState.Parent = this.sendGuna2Button;
            this.sendGuna2Button.Location = new System.Drawing.Point(287, 40);
            this.sendGuna2Button.Name = "sendGuna2Button";
            this.sendGuna2Button.ShadowDecoration.Enabled = true;
            this.sendGuna2Button.ShadowDecoration.Parent = this.sendGuna2Button;
            this.sendGuna2Button.Size = new System.Drawing.Size(90, 36);
            this.sendGuna2Button.TabIndex = 48;
            this.sendGuna2Button.Text = "Send !";
            this.sendGuna2Button.Click += new System.EventHandler(this.sendGuna2Button_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(631, 32);
            this.label3.TabIndex = 49;
            this.label3.Text = "Eagle Monitor RAT";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label3_MouseDown);
            // 
            // platformGuna2ComboBox
            // 
            this.platformGuna2ComboBox.Animated = true;
            this.platformGuna2ComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.platformGuna2ComboBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.platformGuna2ComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.platformGuna2ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.platformGuna2ComboBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.platformGuna2ComboBox.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.platformGuna2ComboBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.platformGuna2ComboBox.FocusedState.Parent = this.platformGuna2ComboBox;
            this.platformGuna2ComboBox.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.platformGuna2ComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.platformGuna2ComboBox.HoverState.Parent = this.platformGuna2ComboBox;
            this.platformGuna2ComboBox.ItemHeight = 30;
            this.platformGuna2ComboBox.Items.AddRange(new object[] {
            "x86",
            "x64",
            "anycpu"});
            this.platformGuna2ComboBox.ItemsAppearance.Parent = this.platformGuna2ComboBox;
            this.platformGuna2ComboBox.Location = new System.Drawing.Point(91, 40);
            this.platformGuna2ComboBox.Name = "platformGuna2ComboBox";
            this.platformGuna2ComboBox.ShadowDecoration.Parent = this.platformGuna2ComboBox;
            this.platformGuna2ComboBox.Size = new System.Drawing.Size(94, 36);
            this.platformGuna2ComboBox.StartIndex = 2;
            this.platformGuna2ComboBox.TabIndex = 50;
            // 
            // RemoteCodeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(751, 317);
            this.Controls.Add(this.platformGuna2ComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.sendGuna2Button);
            this.Controls.Add(this.testGuna2Button);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.maximizeButton);
            this.Controls.Add(this.minimizeButton);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.languageComboBox);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RemoteCodeForm";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "RemoteCodeForm";
            this.Load += new System.EventHandler(this.RemoteCodeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.codeTextBox)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.referenceContextMenuStrip.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public Guna.UI2.WinForms.Guna2ComboBox languageComboBox;
        private FastColoredTextBoxNS.FastColoredTextBox codeTextBox;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.Panel panel2;
        private Guna.UI2.WinForms.Guna2Button closeButton;
        private Guna.UI2.WinForms.Guna2Button maximizeButton;
        private Guna.UI2.WinForms.Guna2Button minimizeButton;
        private Guna.UI2.WinForms.Guna2Button testGuna2Button;
        private Guna.UI2.WinForms.Guna2Button sendGuna2Button;
        public System.Windows.Forms.Label label3;
        private Controls.CustomContextMenuStrip referenceContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        public Guna.UI2.WinForms.Guna2ComboBox platformGuna2ComboBox;
    }
}