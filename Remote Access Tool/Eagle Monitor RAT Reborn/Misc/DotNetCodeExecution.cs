using FastColoredTextBoxNS;
using System;
using System.CodeDom.Compiler;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.Misc
{
    internal static class DotNetCodeExecution
    {
        internal static string GetCode(Language currentLanguage)
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

        internal static void RowAdder(string lib, DataGridView importLib)
        {
            int rowId = importLib.Rows.Add();
            DataGridViewRow row = importLib.Rows[rowId];
            row.Cells["Column31"].Value = lib;
        }

        internal static void TryLocally(CodeDomProvider codeDomProvider, string source, string platform, string[] referencedAssemblies)
        {
            try
            {
                var compilerOptions = $"/target:winexe /platform:{platform} /optimize- /unsafe";

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

    }
}
