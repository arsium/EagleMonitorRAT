using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace EagleMonitor.Controls
{
    internal class BlurForm : Form
    {
        public BlurForm() 
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // BlurForm
            // 
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(262, 239);
            this.Name = "BlurForm";
            this.ResumeLayout(false);

        }


		private Form blurTargetForm;
		public BlurForm(Form targetForm)
		{
			//this.BackColor = Color.FromArgb(125, 125, 125);
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);

			blurTargetForm = targetForm;
			blurTargetForm.Activated += blurTargetForm_Activated;
			blurTargetForm.ResizeBegin += blurTargetForm_ResizeBegin;
			blurTargetForm.ResizeEnd += blurTargetForm_ResizeEnd;
			blurTargetForm.VisibleChanged += blurTargetForm_VisibleChanged;

			Opacity = 0.2;
			ShowInTaskbar = false;
			ShowIcon = false;
			FormBorderStyle = FormBorderStyle.None;
			StartPosition = blurTargetForm.StartPosition;

			if (blurTargetForm.Owner != null)
			{
				Owner = blurTargetForm.Owner;
			}

			blurTargetForm.Owner = this;

			EnableBlur();
		}

		private void blurTargetForm_VisibleChanged(object sender, EventArgs e)
		{
			Visible = blurTargetForm.Visible;
		}

		private void blurTargetForm_Activated(object sender, EventArgs e)
		{
			Bounds = new Rectangle(blurTargetForm.Location.X - 5, blurTargetForm.Location.Y - 5, blurTargetForm.Width + 10, blurTargetForm.Height + 10);

			Visible = (blurTargetForm.WindowState == FormWindowState.Normal);
			if (Visible)
			{
				Show();
			}
		}

		private void blurTargetForm_ResizeBegin(object sender, EventArgs e)
		{
			Visible = false;
			Hide();
		}

		private void blurTargetForm_ResizeEnd(object sender, EventArgs e)
		{
			Bounds = new Rectangle(blurTargetForm.Location.X - 5, blurTargetForm.Location.Y - 5, blurTargetForm.Width + 10, blurTargetForm.Height + 10);

			Visible = (blurTargetForm.WindowState == FormWindowState.Normal);
			if (Visible)
			{
				Show();
			}
		}


		internal enum AccentState
		{
			ACCENT_DISABLED = 0,
			ACCENT_ENABLE_GRADIENT = 1,
			ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
			ACCENT_ENABLE_BLURBEHIND = 3,
			ACCENT_ENABLE_ACRYLICBLURBEHIND = 4,
			ACCENT_INVALID_STATE = 5
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct AccentPolicy
		{
			public AccentState AccentState;
			public uint AccentFlags;
			public uint GradientColor;
			public uint AnimationId;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct WindowCompositionAttributeData
		{
			public WindowCompositionAttribute Attribute;
			public IntPtr Data;
			public int SizeOfData;
		}

		internal enum WindowCompositionAttribute
		{
			// ...
			WCA_ACCENT_POLICY = 19
			// ...
		}

		[DllImport("user32.dll")]
		internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

		private uint _blurOpacity;
		public double BlurOpacity
		{
			get { return _blurOpacity; }
			set { _blurOpacity = (uint)value; EnableBlur(); }
		}

		private uint _blurBackgroundColor = 0x990000; /* BGR color format */

		internal void EnableBlur()
		{


			var accent = new AccentPolicy();
			accent.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND;
			accent.GradientColor = (_blurOpacity << 24) | (_blurBackgroundColor & 0xFFFFFF);

			var accentStructSize = Marshal.SizeOf(accent);

			var accentPtr = Marshal.AllocHGlobal(accentStructSize);
			Marshal.StructureToPtr(accent, accentPtr, false);

			var data = new WindowCompositionAttributeData();
			data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
			data.SizeOfData = accentStructSize;
			data.Data = accentPtr;

			SetWindowCompositionAttribute(this.Handle, ref data);

			Marshal.FreeHGlobal(accentPtr);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			EnableBlur();
		}
    }
}
