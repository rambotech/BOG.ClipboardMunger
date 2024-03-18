using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
    public class EqualSwap : ClipboardMungerProviderBase, IClipboardMungerProvider
    {
        public override string MethodName { get => "Equal Swap"; }
        public override string GroupName { get => "String-Magic"; }
        public override string Description { get => "Swap values on each side of the equal sign"; }

        public EqualSwap()
        {
            base.Examples.Add("Single Line", new Example
            {
                Input = "A = 47 ; 902=B;Database=Repository;;",
                ArgumentValues = new Dictionary<string, string> { },
                Name = "One Continuous Line"
            });
            base.Examples.Add("Multiple Line", new Example
            {
                Input =
                    "HJ=45Tr3=;PRTRU=234TH\r\n" +
                    "\r\n" +
                    "Provider=SQLNCLI11;Server=myServerAddress;Database=myDataBase;Uid=myUsername;Pwd=myPassword;\r\n" +
                    "Provider=SQLNCLI11;Server=myServerAddress;DataBase=myDataBase;\r\n",
                ArgumentValues = new Dictionary<string, string> { },
                Name = "Multiple Lines"
            });
        }

        public override string Munge(string textToMunge)
        {
            StringBuilder output = new StringBuilder();
            foreach (string line in textToMunge.Split(new string[] { "\r\n" }, StringSplitOptions.None))
            {
                string work = line.Trim();
                if (work.Length == 0 || !work.Contains("="))
                {
                    output.AppendLine(line);
                    continue;
                }
                if (work[work.Length - 1] == ';')
                {
                    work = work.Substring(0, work.Length - 1);
                }
                StringBuilder InReverse = new StringBuilder();
                string[] segments = work.Split(new string[] { "=" }, StringSplitOptions.None);
                for (int index = segments.Length - 1; index >= 0; index--)
                {
                    InReverse.Append(segments[index]);
                    if (index > 0) InReverse.Append("=");
                }
                output.AppendLine(line.Replace(work, InReverse.ToString()));
            }
            return output.ToString();
        }
    }
}
