using Eagle_Monitor_RAT_Reborn.Network;
using Leaf.xNet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.Misc
{
    internal class Utils
    {
        internal class CoreAssembly
        {
            internal static readonly Assembly Reference = typeof(CoreAssembly).Assembly;
            internal static readonly Version Version = Reference.GetName().Version;
            internal static readonly string Name = Reference.GetName().Name;
        }

        internal static readonly string GPath = Application.StartupPath;
        internal static readonly string StubPath = GPath + "\\Stubs\\Client";
        ///internal static byte[] NotificationSound = File.ReadAllBytes(GPath + "\\Notification\\notification.wav");

        internal static string SplitPath(string P)
        {
            string[] spl = P.Split('\\');
            return spl[spl.Length - 1];
        }

        internal static void ReadSettings()
        {
            if (System.IO.File.Exists(GPath + "\\config.json"))
            {
                string json = File.ReadAllText(GPath + "\\config.json");
                Program.settings = JsonConvert.DeserializeObject<Settings>(json);
                Program.mainForm.keyGuna2TextBox.Text = Program.settings.key;
                Program.mainForm.builderKeyGuna2TextBox.Text = Program.settings.key;
                foreach (int p in Program.settings.ports)
                {
                    Program.mainForm.portListView.Items.Add(p.ToString());
                }
                foreach (ListViewItem tp in Program.mainForm.portListView.Items)
                    if (tp.Text == Convert.ToString(Program.settings.torPort))
                        tp.ForeColor = Color.Orange;
                foreach (Tuple<string, string> host in Program.settings.hosts)
                {
                    int rowId = Program.mainForm.hostsDataGridView.Rows.Add();
                    DataGridViewRow row = Program.mainForm.hostsDataGridView.Rows[rowId];
                    row.Cells["Column22"].Value = host.Item1;
                    row.Cells["Column23"].Value = host.Item2;
                }
                Program.mainForm.torRouteCheckBox.Checked = Program.settings.torRouting;
                Program.mainForm.notificationSoundGuna2CheckBox.Checked = Program.settings.notificationSound;
                Program.mainForm.notificationIconGuna2CheckBox.Checked = Program.settings.notificationIcon;
                Program.mainForm.autoSaveRecoveryGuna2CheckBox.Checked = Program.settings.autoSaveRecovery;
                Program.mainForm.flagsPackGuna2ComboBox.Text = Program.settings.flagsPackName;
                Program.mainForm.processInjectionGuna2ComboBox.SelectedIndex = (int)Program.settings.processInjectionMethod;
                Program.mainForm.autoRemoveDownloadGuna2CheckBox.Checked = Program.settings.autoRemoveRowWhenFileIsDownloaded;
                Program.mainForm.ransomKeyGuna2CheckBox.Checked = Program.settings.autoGenerateRSAKey;


                switch (Program.settings.bufferSize)
                {
                    case 524288:
                        Program.mainForm.bufferSizeGuna2ComboBox.SelectedIndex = 0;
                        break;

                    case 1048576:
                        Program.mainForm.bufferSizeGuna2ComboBox.SelectedIndex = 1;
                        break;

                    case 2097152:
                        Program.mainForm.bufferSizeGuna2ComboBox.SelectedIndex = 2;
                        break;

                    case 4194304:
                        Program.mainForm.bufferSizeGuna2ComboBox.SelectedIndex = 3;
                        break;

                    case 8388608:
                        Program.mainForm.bufferSizeGuna2ComboBox.SelectedIndex = 4;
                        break;

                    case 10485760:
                        Program.mainForm.bufferSizeGuna2ComboBox.SelectedIndex = 5;
                        break;
                }

                /*if (Program.settings.onConnectPackets != null)
                {
                    foreach (IPacket packet in Program.settings.onConnectPackets)
                    {
                        int rowId = Program.mainForm.tasksDataGridView.Rows.Add();

                        DataGridViewRow row = Program.mainForm.tasksDataGridView.Rows[rowId];

                        row.Cells["Column20"].Value = packet.packetType.ToString();

                    }
                }
                else
                    Program.settings.onConnectPackets = new List<IPacket>();*/
            }
            else 
            {
                Program.settings = new Settings();
            }
        }

        internal static void SaveSettings() 
        {
            Program.settings.ports = new List<int>();
            Program.settings.key = Program.mainForm.keyGuna2TextBox.Text;
            Program.settings.notificationSound = Program.mainForm.notificationSoundGuna2CheckBox.Checked;
            Program.settings.autoRemoveRowWhenFileIsDownloaded = Program.mainForm.autoRemoveDownloadGuna2CheckBox.Checked;
            Program.settings.autoSaveRecovery = Program.mainForm.autoSaveRecoveryGuna2CheckBox.Checked;
            Program.settings.flagsPackName = Program.mainForm.flagsPackGuna2ComboBox.Text;
            Program.settings.autoGenerateRSAKey = Program.mainForm.ransomKeyGuna2CheckBox.Checked;
            Program.settings.notificationIcon = Program.mainForm.notificationIconGuna2CheckBox.Checked;
            Program.settings.hosts = new List<Tuple<string, string>>();
            Program.settings.torRouting = Program.mainForm.torRouteCheckBox.Checked;
            foreach (DataGridViewRow host in Program.mainForm.hostsDataGridView.Rows)
            {
                // hostsList += $"\"{host.Cells[0].Value}:{host.Cells[1].Value}\",";
                Program.settings.hosts.Add(new Tuple<string, string>(host.Cells[0].Value.ToString(), host.Cells[1].Value.ToString()));
            }

            foreach (ListViewItem I in Program.mainForm.portListView.Items)
            {
                Program.settings.ports.Add(int.Parse(I.Text));
                if (I.ForeColor == Color.Orange)
                    Program.settings.torPort = int.Parse(I.Text);
            }

            switch (Program.mainForm.processInjectionGuna2ComboBox.SelectedIndex) 
            {
                case 0:
                    Program.settings.processInjectionMethod = PacketLib.Packet.ProcessInjectionPacket.INJECTION_METHODS.CLASSIC;
                    break;
                case 1:
                    Program.settings.processInjectionMethod = PacketLib.Packet.ProcessInjectionPacket.INJECTION_METHODS.MAP_VIEW;
                    break;
            }

            switch (Program.mainForm.bufferSizeGuna2ComboBox.SelectedIndex) 
            {
/*
512 kB (524288)
1 MB (1048576)
2 MB (2097152)
4 MB (4194304)
8 MB (8388608)
10 MB (10485760)
*/
                case 0:
                    Program.settings.bufferSize = 524288;
                    break;

                case 1:
                    Program.settings.bufferSize = 1048576;
                    break;

                case 2:
                    Program.settings.bufferSize = 2097152;
                    break;

                case 3:
                    Program.settings.bufferSize = 4194304;
                    break;

                case 4:
                    Program.settings.bufferSize = 8388608;
                    break;

                case 5:
                    Program.settings.bufferSize = 10485760;
                    break;
            }
            string savedSettings = JsonConvert.SerializeObject(Program.settings);
            File.WriteAllText(GPath + "\\config.json", savedSettings);
        }

        internal static void SaveLogs(DataGridView logDataGridView) 
        {
            Utils.ToCSV(logDataGridView , Application.StartupPath + "\\Logs\\" + DateFormater() + ".csv");
        }

        internal static Image ResizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        internal static void StartServers() 
        {
            foreach (int p in Program.settings.ports)
            {
                new Thread(() =>
                {
                    ServerHandler s = new ServerHandler();
                    s.Listen(p);
                }).Start();
            }
            if (Program.settings.torRouting)
                Router.StartProxy();
        }

        internal static void ToCSV(DataGridView dataGridView, string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath, false, System.Text.Encoding.Default))
            {
                /* sw.Write(string.Format("{0};", dataGridView.Columns[0].HeaderText));
                 sw.Write(string.Format("{0};", dataGridView.Columns[1].HeaderText));
                 sw.Write(string.Format("{0};", dataGridView.Columns[2].HeaderText));
                 sw.Write(string.Format("{0};", dataGridView.Columns[3].HeaderText));*/

                foreach (DataGridViewColumn column in dataGridView.Columns)
                    sw.Write(string.Format("{0};", column.HeaderText));


                sw.WriteLine("");

                foreach (DataGridViewRow item in dataGridView.Rows)
                {
                    foreach (DataGridViewCell cell in item.Cells)
                        sw.Write(string.Format("{0};", cell.Value));
                    sw.WriteLine("");
                }
            }
        }

        internal static void ToCSV(List<object[]> elemList, string filePath, string[] columnNames)
        {
            using (StreamWriter sw = new StreamWriter(filePath, false, System.Text.Encoding.Default))
            {
                // sw.Write(string.Format("{0};", "URL"));
                // sw.Write(string.Format("{0};", "Username"));
                // sw.Write(string.Format("{0};", "Password"));
                // sw.Write(string.Format("{0};", "Application"));
                foreach (string columnName in columnNames)
                    sw.Write(string.Format("{0};", columnName));
                /*sw.Write(string.Format("{0};", columnName[0]));
                sw.Write(string.Format("{0};", columnName[1]));
                sw.Write(string.Format("{0};", columnName[2]));
                sw.Write(string.Format("{0};", columnName[3]));*/
                sw.WriteLine("");

                foreach (object[] item in elemList)
                {
                    foreach (object item2 in item)
                        sw.Write(string.Format("{0};", item2.ToString()));
                    sw.WriteLine("");
                }
            }
        }

        internal static string DateFormater()
        {
            DateTime now = DateTime.Now;
            return $"{now.Year}-{now.Month}-{now.Day}-{now.Hour}H{now.Minute}-{now.Second}{now.Millisecond}";
        }

        internal static string Numeric2Bytes(double b)
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

            double b2 = (double)b;

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

        internal async static Task<string> CheckVersionAsync()
        {
            await Task.Run(() =>
            {
                using (HttpRequest httpRequest = new HttpRequest())
                {
                    httpRequest.IgnoreProtocolErrors = true;
                    httpRequest.UserAgent = Http.ChromeUserAgent();
                    httpRequest.ConnectTimeout = 2500;
                    string request = httpRequest.Get("https://api.github.com/repos/arsium/EagleMonitorRAT/releases").ToString();
                    return request;
                }
            });
            return await CheckVersionAsync();
        }

        internal static string SHA512(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            using (var hash = System.Security.Cryptography.SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);
                var hashedInputStringBuilder = new System.Text.StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                return hashedInputStringBuilder.ToString().ToUpper();
            }
        }
    }
}
