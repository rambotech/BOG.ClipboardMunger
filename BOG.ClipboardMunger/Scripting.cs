using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.CodeDom.Compiler;

namespace BOG.ClipboardMunger
{
    public class Scripting
    {
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
