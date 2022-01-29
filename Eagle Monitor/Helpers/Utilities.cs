using Eagle_Monitor.Controls;
using Shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

#pragma warning disable CS0197
namespace Eagle_Monitor
{ 
    //Some of those functions come directly from stackoverflow or other sources
    public static class Utilities
    {
        public static string GPath = Application.StartupPath;
        public static string RSMKey;
        public static bool DarkTheme = false;
        public static Algorithm algorithm;
        public static string randomString = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz123456789/*-+=:;,ùµ$^-)àç!è§('é&|@#{[^{}[]`´~<>¶¥¤¼»º§¦©ª«¬­®¯°±²Þß÷ö";
        public static Random randomGenerator = new Random();
        public static string key;
        public static Settings settings;

        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern uint NtTerminateProcess(IntPtr hProcess, int errorStatus);

        [DllImport("user32.dll")]
        public extern static bool AnimateWindow(IntPtr hwnd, int time, dwFlags flags);
        [Flags]
        public enum dwFlags 
        {
            AW_ACTIVATE = 0x00020000,
            AW_BLEND = 0x00080000,
            AW_CENTER = 0x00000010,
            AW_HIDE = 0x00010000,
            AW_HOR_POSITIVE = 0x00000001,
            AW_HOR_NEGATIVE = 0x00000002,
            AW_SLIDE = 0x00040000,
            AW_VER_POSITIVE = 0x00000004,
            AW_VER_NEGATIVE = 0x00000008
        }
        public static void Log(string l, Color C) 
        {
            ListViewItem I = new ListViewItem(l);
            I.ForeColor = C;
            StartForm.M.logsListView.Items.Add(I);              
        }
        public static Image BytesToImage(byte[] bytes)
        {
            using (MemoryStream mStream = new MemoryStream(bytes))
            {
                return Image.FromStream(mStream);
            }
        }
        public static byte[] ImageToBytes(Image img)
        {
            using (var ms = new MemoryStream())
            {
                (new Bitmap(img)).Save(ms, img.RawFormat);
                return ms.ToArray();
            }
        }
        public static string SplitPath(string P)
        {
            string[] spl = P.Split('\\');
            return spl[spl.Length - 1];
        }
   
        public static string Numeric2Bytes(double b)
		{
			string tempNumeric2Bytes = null;
			string[] bSize = new string[9];
			int i = 0;

			bSize[0] = "Bytes";
			bSize[1] = "KB"; //Kilobytes
			bSize[2] = "MB"; //Megabytes
			bSize[3] = "GB"; //Gigabytes
			bSize[4] = "TB"; //Terabytes
			bSize[5] = "PB"; //Petabytes
			bSize[6] = "EB"; //Exabytes
			bSize[7] = "ZB"; //Zettabytes
			bSize[8] = "YB"; //Yottabytes

			double b2 = (double)b; // Make sure var is a Double (not just
						   // variant)
			for (i = bSize.GetUpperBound(0); i >= 0; i--)
			{
				if (b2 >= (Math.Pow(1024, i)))
				{
					tempNumeric2Bytes = ThreeNonZeroDigits(b2 / (Math.Pow(1024, i))) + " " + bSize[i];
					break;
				}
			}
			return tempNumeric2Bytes;
		}

		private static string ThreeNonZeroDigits(double value)
		{
			if (value >= 100)
			{
				// No digits after the decimal.
				return Microsoft.VisualBasic.Strings.Format(Convert.ToInt32(value));
			}
			else if (value >= 10)
			{
				// One digit after the decimal.
				return value.ToString("0.0");
			}
			else
			{
				return value.ToString("0.00");
			}
		}

        public static void ToCSV(ListView listView, string filePath)
        {
            //make header string
            using (StreamWriter sw = new StreamWriter(filePath, false, System.Text.Encoding.Default))
            {
                //ajout du titre des colonnes
                foreach (ColumnHeader c in listView.Columns)
                    sw.Write(string.Format("{0};", c.Text));
                sw.WriteLine("");

                // ajout des données
                foreach (ListViewItem item in listView.Items)
                {
                    foreach (ListViewItem.ListViewSubItem subitem in item.SubItems)
                        sw.Write(string.Format("{0};", subitem.Text));
                    sw.WriteLine("");
                }
            }
        }
        public static void CloseForm(Form any) 
        {
            try
            {
                any.Close();
            }
            catch {}     
        }

        public static void Theme() 
        {
            if (DarkTheme)
            {
               /* StartForm.M.panel5.BackColor = Color.FromArgb(64, 64, 64);

                StartForm.M.clientsListView.BackColor = Color.FromArgb(30, 30, 30);
                StartForm.M.clientsListView.ForeColor = Color.FromArgb(205, 205, 205);

                StartForm.M.tasksListView.BackColor = Color.FromArgb(30, 30, 30);
                StartForm.M.tasksListView.ForeColor = Color.FromArgb(205, 205, 205);

                StartForm.M.massListView.BackColor = Color.FromArgb(30, 30, 30);
                StartForm.M.massListView.ForeColor = Color.FromArgb(205, 205, 205);

                StartForm.M.logsListView.BackColor = Color.FromArgb(30, 30, 30);
                StartForm.M.logsListView.ForeColor = Color.FromArgb(205, 205, 205);

                StartForm.M.algoComboBox.BackColor = Color.FromArgb(30, 30, 30);
                StartForm.M.algoComboBox.ForeColor = Color.FromArgb(205, 205, 205);

                StartForm.M.keyTextBox.BackColor = Color.FromArgb(30, 30, 30);
                StartForm.M.keyTextBox.ForeColor = Color.FromArgb(205, 205, 205);

                StartForm.M.keySizeComboBox.BackColor = Color.FromArgb(30, 30, 30);
                StartForm.M.keySizeComboBox.ForeColor = Color.FromArgb(205, 205, 205);

                StartForm.M.label3.ForeColor = Color.FromArgb(205, 205, 205);
                StartForm.M.label4.ForeColor = Color.FromArgb(205, 205, 205);

                ControlsDrawing.colorListViewHeader(ref StartForm.M.clientsListView, Color.FromArgb(30, 30, 30), Color.FromArgb(205, 205, 205), Color.FromArgb(60, 60, 60));
                ControlsDrawing.colorListViewHeader(ref StartForm.M.tasksListView, Color.FromArgb(30, 30, 30), Color.FromArgb(205, 205, 205), Color.FromArgb(60, 60, 60));
                ControlsDrawing.colorListViewHeader(ref StartForm.M.massListView, Color.FromArgb(30, 30, 30), Color.FromArgb(205, 205, 205), Color.FromArgb(60, 60, 60));
                ControlsDrawing.colorListViewHeader(ref StartForm.M.logsListView, Color.FromArgb(30, 30, 30), Color.FromArgb(205, 205, 205), Color.FromArgb(60, 60, 60));

                //StartForm.M.panel1.BackColor = Color.FromArgb(30, 30, 30);
                StartForm.M.panel2.BackColor = Color.FromArgb(30, 30, 30);
                StartForm.M.panel3.BackColor = Color.FromArgb(30, 30, 30);
                StartForm.M.panel4.BackColor = Color.FromArgb(30, 30, 30);
                StartForm.M.countryImageList.TransparentColor = Color.FromArgb(30, 30, 30);

                foreach (ToolStripMenuItem I in StartForm.M.clientMenuStrip.Items)
                {

                    I.BackColor = Color.FromArgb(30, 30, 30);
                    I.ForeColor = Color.FromArgb(205, 205, 205);
                }

                StartForm.M.passwordsToolStripMenuItem.BackColor = Color.FromArgb(30, 30, 30);
                StartForm.M.passwordsToolStripMenuItem.ForeColor = Color.FromArgb(205, 205, 205);

                StartForm.M.wifiToolStripMenuItem.BackColor = Color.FromArgb(30, 30, 30);
                StartForm.M.wifiToolStripMenuItem.ForeColor = Color.FromArgb(205, 205, 205);

                StartForm.M.historyToolStripMenuItem.BackColor = Color.FromArgb(30, 30, 30);
                StartForm.M.historyToolStripMenuItem.ForeColor = Color.FromArgb(205, 205, 205);

                StartForm.M.fileManagerToolStripMenuItem.BackColor = Color.FromArgb(30, 30, 30);
                StartForm.M.fileManagerToolStripMenuItem.ForeColor = Color.FromArgb(205, 205, 205);

                StartForm.M.remoteDesktopToolStripMenuItem.BackColor = Color.FromArgb(30, 30, 30);
                StartForm.M.remoteDesktopToolStripMenuItem.ForeColor = Color.FromArgb(205, 205, 205);

                StartForm.M.processManagerToolStripMenuItem.BackColor = Color.FromArgb(30, 30, 30);
                StartForm.M.processManagerToolStripMenuItem.ForeColor = Color.FromArgb(205, 205, 205);

                StartForm.M.injectionToolStripMenuItem.BackColor = Color.FromArgb(30, 30, 30);
                StartForm.M.injectionToolStripMenuItem.ForeColor = Color.FromArgb(205, 205, 205);

                StartForm.M.panelToolStripMenuItem.BackColor = Color.FromArgb(30, 30, 30);
                StartForm.M.panelToolStripMenuItem.ForeColor = Color.FromArgb(205, 205, 205);

                StartForm.M.webCamToolStripMenuItem.BackColor = Color.FromArgb(30, 30, 30);
                StartForm.M.webCamToolStripMenuItem.ForeColor = Color.FromArgb(205, 205, 205);

                StartForm.M.clientToolStripMenuItem.BackColor = Color.FromArgb(30, 30, 30);
                StartForm.M.clientToolStripMenuItem.ForeColor = Color.FromArgb(205, 205, 205);

                StartForm.M.powerToolStripMenuItem.BackColor = Color.FromArgb(30, 30, 30);
                StartForm.M.powerToolStripMenuItem.ForeColor = Color.FromArgb(205, 205, 205);

                StartForm.M.rebootToolStripMenuItem.BackColor = Color.FromArgb(30, 30, 30);
                StartForm.M.rebootToolStripMenuItem.ForeColor = Color.FromArgb(205, 205, 205);

                StartForm.M.shutdownToolStripMenuItem.BackColor = Color.FromArgb(30, 30, 30);
                StartForm.M.shutdownToolStripMenuItem.ForeColor = Color.FromArgb(205, 205, 205);

                StartForm.M.suspendToolStripMenuItem.BackColor = Color.FromArgb(30, 30, 30);
                StartForm.M.suspendToolStripMenuItem.ForeColor = Color.FromArgb(205, 205, 205);

                StartForm.M.hibernateToolStripMenuItem.BackColor = Color.FromArgb(30, 30, 30);
                StartForm.M.hibernateToolStripMenuItem.ForeColor = Color.FromArgb(205, 205, 205);

                StartForm.M.logoutToolStripMenuItem.BackColor = Color.FromArgb(30, 30, 30);
                StartForm.M.logoutToolStripMenuItem.ForeColor = Color.FromArgb(205, 205, 205);

                StartForm.M.closeToolStripMenuItem.BackColor = Color.FromArgb(30, 30, 30);
                StartForm.M.closeToolStripMenuItem.ForeColor = Color.FromArgb(205, 205, 205);

                foreach (ToolStripMenuItem I in StartForm.M.tasksContextMenuStrip.Items)
                {
                    I.BackColor = Color.FromArgb(30, 30, 30);
                    I.ForeColor = Color.FromArgb(205, 205, 205);
                }

                foreach (ToolStripMenuItem I in StartForm.M.massContextMenuStrip.Items)
                {
                    I.BackColor = Color.FromArgb(30, 30, 30);
                    I.ForeColor = Color.FromArgb(205, 205, 205);
                }

                StartForm.M.label2.ForeColor = Color.FromArgb(205, 205, 205); 
                StartForm.M.BackColor = Color.FromArgb(30, 30, 30);

                StartForm.M.windowsTabControls1.BackColor = Color.FromArgb(30, 30, 30);
                StartForm.M.windowsTabControls1.BorderColor = Color.FromArgb(60, 60, 60);
                StartForm.M.windowsTabControls1.ForeColorBase = Color.FromArgb(205, 205, 205);
                StartForm.M.windowsTabControls1.HeaderBackColor = Color.FromArgb(30, 30, 30);
                StartForm.M.windowsTabControls1.HeadSelectedBorderColor = Color.FromArgb(60, 60, 60);

                StartForm.M.clientMenuStrip.Renderer = new ToolStripProfessionalRenderer(new ControlsDrawing.DarkMenuColorTable());
                StartForm.M.tasksContextMenuStrip.Renderer = new ToolStripProfessionalRenderer(new ControlsDrawing.DarkMenuColorTable());
                StartForm.M.massContextMenuStrip.Renderer = new ToolStripProfessionalRenderer(new ControlsDrawing.DarkMenuColorTable());
               */
            }
            else
            {
                StartForm.M.clientsListView.BackColor = Color.White;
                StartForm.M.clientsListView.ForeColor = Color.FromArgb(64, 64, 64);

                StartForm.M.tasksListView.BackColor = Color.White;
                StartForm.M.tasksListView.ForeColor = Color.FromArgb(64, 64, 64);

                StartForm.M.massListView.BackColor = Color.White;
                StartForm.M.massListView.ForeColor = Color.FromArgb(64, 64, 64);

                StartForm.M.logsListView.BackColor = Color.White;
                StartForm.M.logsListView.ForeColor = Color.FromArgb(64, 64, 64);

                ControlsDrawing.colorListViewHeader(ref StartForm.M.clientsListView, Color.White, Color.FromArgb(64, 64, 64), Color.FromArgb(230, 230, 230));
                ControlsDrawing.colorListViewHeader(ref StartForm.M.tasksListView, Color.White, Color.FromArgb(64, 64, 64), Color.FromArgb(230, 230, 230));
                ControlsDrawing.colorListViewHeader(ref StartForm.M.massListView, Color.White, Color.FromArgb(64, 64, 64), Color.FromArgb(230, 230, 230));
                ControlsDrawing.colorListViewHeader(ref StartForm.M.logsListView, Color.White, Color.FromArgb(64, 64, 64), Color.FromArgb(230, 230, 230));

                //StartForm.M.panel1.BackColor = Color.White;
                StartForm.M.panel2.BackColor = Color.White;
                StartForm.M.panel3.BackColor = Color.White;
                StartForm.M.panel4.BackColor = Color.White;
                StartForm.M.countryImageList.TransparentColor = Color.White;

                foreach (ToolStripMenuItem I in StartForm.M.clientMenuStrip.Items)
                {
                    I.BackColor = Color.White;
                    I.ForeColor = Color.FromArgb(64, 64, 64);
                }

                StartForm.M.passwordsToolStripMenuItem.BackColor = Color.White;
                StartForm.M.passwordsToolStripMenuItem.ForeColor = Color.FromArgb(64, 64, 64);

                StartForm.M.wifiToolStripMenuItem.BackColor = Color.White;
                StartForm.M.wifiToolStripMenuItem.ForeColor = Color.FromArgb(64, 64, 64);

                StartForm.M.historyToolStripMenuItem.BackColor = Color.White;
                StartForm.M.historyToolStripMenuItem.ForeColor = Color.FromArgb(64, 64, 64);

                StartForm.M.fileManagerToolStripMenuItem.BackColor = Color.White;
                StartForm.M.fileManagerToolStripMenuItem.ForeColor = Color.FromArgb(64, 64, 64);

                StartForm.M.remoteDesktopToolStripMenuItem.BackColor = Color.White;
                StartForm.M.remoteDesktopToolStripMenuItem.ForeColor = Color.FromArgb(64, 64, 64);

                StartForm.M.processManagerToolStripMenuItem.BackColor = Color.White;
                StartForm.M.processManagerToolStripMenuItem.ForeColor = Color.FromArgb(64, 64, 64);

                StartForm.M.injectionToolStripMenuItem.BackColor = Color.White;
                StartForm.M.injectionToolStripMenuItem.ForeColor = Color.FromArgb(64, 64, 64);

                StartForm.M.panelToolStripMenuItem.BackColor = Color.White;
                StartForm.M.panelToolStripMenuItem.ForeColor = Color.FromArgb(64, 64, 64);

                StartForm.M.webCamToolStripMenuItem.BackColor = Color.White;
                StartForm.M.webCamToolStripMenuItem.ForeColor = Color.FromArgb(64, 64, 64);

                StartForm.M.clientToolStripMenuItem.BackColor = Color.White;
                StartForm.M.clientToolStripMenuItem.ForeColor = Color.FromArgb(64, 64, 64);

                StartForm.M.powerToolStripMenuItem.BackColor = Color.White;
                StartForm.M.powerToolStripMenuItem.ForeColor = Color.FromArgb(64, 64, 64);

                StartForm.M.rebootToolStripMenuItem.BackColor = Color.White;
                StartForm.M.rebootToolStripMenuItem.ForeColor = Color.FromArgb(64, 64, 64);

                StartForm.M.shutdownToolStripMenuItem.BackColor = Color.White;
                StartForm.M.shutdownToolStripMenuItem.ForeColor = Color.FromArgb(64, 64, 64);

                StartForm.M.suspendToolStripMenuItem.BackColor = Color.White;
                StartForm.M.suspendToolStripMenuItem.ForeColor = Color.FromArgb(64, 64, 64);

                StartForm.M.hibernateToolStripMenuItem.BackColor = Color.White;
                StartForm.M.hibernateToolStripMenuItem.ForeColor = Color.FromArgb(64, 64, 64);

                StartForm.M.logoutToolStripMenuItem.BackColor = Color.White;
                StartForm.M.logoutToolStripMenuItem.ForeColor = Color.FromArgb(64, 64, 64);

                StartForm.M.closeToolStripMenuItem.BackColor = Color.White;
                StartForm.M.closeToolStripMenuItem.ForeColor = Color.FromArgb(64, 64, 64);

                foreach (ToolStripMenuItem I in StartForm.M.tasksContextMenuStrip.Items)
                {
                    I.BackColor = Color.White;
                    I.ForeColor = Color.FromArgb(64, 64, 64);
                }

                foreach (ToolStripMenuItem I in StartForm.M.massContextMenuStrip.Items)
                {
                    I.BackColor = Color.White;
                    I.ForeColor = Color.FromArgb(64, 64, 64);
                }

                StartForm.M.label2.ForeColor = Color.FromArgb(64, 64, 64);
                StartForm.M.BackColor = Color.White;

                StartForm.M.windowsTabControls1.BackColor = Color.White;
                StartForm.M.windowsTabControls1.BorderColor = Color.FromArgb(232,232,232);
                StartForm.M.windowsTabControls1.ForeColorBase = Color.FromArgb(64, 64, 64);
                StartForm.M.windowsTabControls1.HeaderBackColor = Color.White;
                StartForm.M.windowsTabControls1.HeadSelectedBorderColor = Color.FromArgb(232, 232, 232);

                StartForm.M.clientMenuStrip.Renderer = new ToolStripProfessionalRenderer(new ControlsDrawing.LightMenuColorTable());
                StartForm.M.tasksContextMenuStrip.Renderer = new ToolStripProfessionalRenderer(new ControlsDrawing.LightMenuColorTable());
                StartForm.M.massContextMenuStrip.Renderer = new ToolStripProfessionalRenderer(new ControlsDrawing.LightMenuColorTable());

            }
        }
    }

	public static class Plugins 
    {
        public static byte[] Recovery = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utilities.GPath + "\\Plugins\\Recovery.dll"), 1);
        public static byte[] Miscellaneous = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utilities.GPath + "\\Plugins\\Miscellaneous.dll"), 1);
        public static byte[] ProcessManager = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utilities.GPath + "\\Plugins\\ProcessManager.dll"), 1);
        public static byte[] FilesManager = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utilities.GPath + "\\Plugins\\FileManager.dll"), 1);
        public static byte[] RemoteDesktop = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utilities.GPath + "\\Plugins\\RemoteDesktop.dll"), 1);
        public static byte[] Execute = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utilities.GPath + "\\Plugins\\Execute.dll"), 1);
        public static byte[] WebCam = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utilities.GPath + "\\Plugins\\RemoteWebCam.dll"), 1);
        public static byte[] FileEncryption = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utilities.GPath + "\\Plugins\\FileEncryption.dll"), 1);

        public static byte[] CPUInformation = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utilities.GPath + "\\Plugins\\CPU.dll"), 1);
        public static byte[] CPUInformation_64 = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utilities.GPath + "\\Plugins\\CPU64.dll"), 1);
    }

    public static class ShellCodePlugins 
    {
        public static byte[] ProcessCrash_32 = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utilities.GPath + "\\Plugins\\ProcessCrash32.bin"), 1);
        public static byte[] ProcessCrash_64 = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utilities.GPath + "\\Plugins\\ProcessCrash64.bin"), 1);
    }

    public class Settings 
    {
        public List <int> Ports { get; set; }
        public string Key { get; set; }
        public Algorithm algorithm { get; set; }
        public string encryptionFileManagerKey { get; set; }
        public string encryptionFileManagerKeySize { get; set; }
    }
}
