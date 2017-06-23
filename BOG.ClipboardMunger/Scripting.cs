using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.CodeDom.Compiler;

namespace BOG.ClipboardMunger
{
    public class Scripting
    {

        public enum Languages
        {
            VB,
            CSharp
        }

        public static CompilerResults CompileScript(string Source, string[] Reference, Languages Language)
        {
            CodeDomProvider provider = null;
            CompilerResults results;

            switch (Language)
            {
                case Languages.VB:
                    provider = new Microsoft.VisualBasic.VBCodeProvider();
                    break;
                case Languages.CSharp:
                    provider = new Microsoft.CSharp.CSharpCodeProvider();
                    break;
            }

            // Configure parameters
            CompilerParameters parms = new CompilerParameters();
            parms.GenerateExecutable = false;
            parms.GenerateInMemory = true;
            parms.IncludeDebugInformation = false;
            if (Reference != null)
                foreach (string r in Reference)
                    parms.ReferencedAssemblies.Add(r);

            // Compile
            results = provider.CompileAssemblyFromSource(parms, Source);

            return results;
        }

        public static object FindInterface(System.Reflection.Assembly DLL, string InterfaceName)
        {
            // Loop through types looking for one that implements the given interface
            foreach (Type t in DLL.GetTypes())
            {
                if (t.GetInterface(InterfaceName, true) != null)
                    return DLL.CreateInstance(t.FullName);
            }
            return null;
        }
    }
}
