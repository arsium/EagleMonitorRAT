using EagleMonitor.Config;
using EagleMonitor.Controls;
using PacketLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace EagleMonitor.Utils
{
    internal static class Miscellaneous
    {
        internal static string GPath = Application.StartupPath;
        internal static Settings settings;

        static Miscellaneous()
        {
            settings = new Settings();
        }

        internal static string SplitPath(string P)
        {
            string[] spl = P.Split('\\');
            return spl[spl.Length - 1];
        }

        [DllImport("ntdll.dll", SetLastError = true)]
        internal static extern uint NtTerminateProcess(IntPtr hProcess, int errorStatus);

        internal static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        internal static void Enable(ListView listView)
        {
            PropertyInfo aProp = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            aProp.SetValue(listView, true, null);
        }

        internal static void Enable(DataGridView dataGridView)
        {
            PropertyInfo aProp = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            aProp.SetValue(dataGridView, true, null);
        }

        internal static void CloseForm(FormPattern any)
        {
            try
            {
                any.Close();
            }
            catch { }
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

        internal static void ToCSV(DataGridView dataGridView)
        {
            using (StreamWriter sw = new StreamWriter(GPath + "\\Logs\\" + Utils.Miscellaneous.DateFormater() + ".csv", false, System.Text.Encoding.Default))
            {
                /*sw.Write(string.Format("{0};", dataGridView.Columns[0].HeaderText));
                sw.Write(string.Format("{0};", dataGridView.Columns[1].HeaderText));
                sw.Write(string.Format("{0};", dataGridView.Columns[2].HeaderText));
                sw.Write(string.Format("{0};", dataGridView.Columns[3].HeaderText));
                sw.Write(string.Format("{0};", dataGridView.Columns[4].HeaderText));
                sw.Write(string.Format("{0};", dataGridView.Columns[5].HeaderText));*/
                foreach(DataGridViewColumn column in dataGridView.Columns)
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
                foreach(string columnName in columnNames)
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
    }
}
