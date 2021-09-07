using System;
using System.Drawing;
using System.Windows.Forms;

namespace Eagle_Monitor.Controls
{
	//https://github.com/peters/winforms-modernui/blob/master/MetroFramework/Forms/MetroForm.cs
	public class MetroDropShadow : Form
	{
		public MetroDropShadow()
		{
			InitializeComponent();
		}

		private Form shadowTargetForm;

		public MetroDropShadow(Form targetForm)
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);

			shadowTargetForm = targetForm;
			shadowTargetForm.Activated += shadowTargetForm_Activated;
			shadowTargetForm.ResizeBegin += shadowTargetForm_ResizeBegin;
			shadowTargetForm.ResizeEnd += shadowTargetForm_ResizeEnd;
			shadowTargetForm.VisibleChanged += shadowTargetForm_VisibleChanged;

			Opacity = 0.2;
			ShowInTaskbar = false;
			ShowIcon = false;
			FormBorderStyle = FormBorderStyle.None;
			StartPosition = shadowTargetForm.StartPosition;

			if (shadowTargetForm.Owner != null)
			{
				Owner = shadowTargetForm.Owner;
			}

			shadowTargetForm.Owner = this;
		}

		private void shadowTargetForm_VisibleChanged(object sender, EventArgs e)
		{
			Visible = shadowTargetForm.Visible;
		}

		private void shadowTargetForm_Activated(object sender, EventArgs e)
		{
			Bounds = new Rectangle(shadowTargetForm.Location.X - 5, shadowTargetForm.Location.Y - 5, shadowTargetForm.Width + 10, shadowTargetForm.Height + 10);

			Visible = (shadowTargetForm.WindowState == FormWindowState.Normal);
			if (Visible)
			{
				Show();
			}
		}

		private void shadowTargetForm_ResizeBegin(object sender, EventArgs e)
		{
			Visible = false;
			Hide();
		}

		private void shadowTargetForm_ResizeEnd(object sender, EventArgs e)
		{
			Bounds = new Rectangle(shadowTargetForm.Location.X - 5, shadowTargetForm.Location.Y - 5, shadowTargetForm.Width + 10, shadowTargetForm.Height + 10);

			Visible = (shadowTargetForm.WindowState == FormWindowState.Normal);
			if (Visible)
			{
				Show();
			}
		}

		private const int WS_EX_TRANSPARENT = 0x20;
		private const int WS_EX_NOACTIVATE = 0x8000000;

		protected override System.Windows.Forms.CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				cp.ExStyle |= WS_EX_TRANSPARENT | WS_EX_NOACTIVATE;
				return cp;
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.Clear(Color.FromArgb(0, 173, 239));//16, 26, 39

			using (Brush b = new SolidBrush(Color.FromArgb(0, 173, 239)))//0; 173; 239
			{
				e.Graphics.FillRectangle(b, new Rectangle(4, 4, ClientRectangle.Width - 8, ClientRectangle.Height - 8));
			}
		}

		private void InitializeComponent()
		{
			this.SuspendLayout();
			//
			//MetroDropShadow
			//
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.Name = "MetroDropShadow";
			this.ResumeLayout(false);
		}
	}
}
