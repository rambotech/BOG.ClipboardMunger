using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
    public class HashMD5 : ClipboardMungerProviderBase, IClipboardMungerProvider
    {
        public override string MethodName { get => "Hash value (MD5)"; }
        public override string GroupName { get => "Encoding"; }
        public override string Description { get => "Return the MD5 hash value of the clipboard content"; }

        public HashMD5() 
        {
            base.SetArgument(new Argument
            {
                Name = "Output case",
                Title = "Use Upper Case for hex value?",
                DefaultValue = "false",
                Description = "Leave as false for lower case",
                ValidatorRegex = @"true|TRUE|false|FALSE"
            });
        }

        public override string Munge(string textToMunge)
        {
            var outputUpperCase = bool.Parse(this.ArgumentValues["Output case"]);
            var hasher = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(textToMunge);
            byte[] hash = hasher.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString(outputUpperCase ? "X2" : "x2"));
            }
            return sb.ToString();
        }
    }
}
