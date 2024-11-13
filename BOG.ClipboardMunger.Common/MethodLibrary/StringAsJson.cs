using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Interface;
using BOG.Framework;
using System;
using System.Text;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
    public class StringAsJson : ClipboardMungerProviderBase, IClipboardMungerProvider
    {
        public override string MethodName { get => "String as JSON"; }
        public override string GroupName { get => "String-like"; }
        public override string Description { get => "For geting the encoded value of a string"; }

        public StringAsJson() 
        {
        }

        public override string Munge(string textToMunge)
        {
            return BOG.Framework.ObjectJsonSerializer<string>.CreateDocumentFormat(textToMunge);    
        }
    }
}
