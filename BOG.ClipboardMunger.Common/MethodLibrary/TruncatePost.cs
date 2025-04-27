using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
    public class TruncatePost : ClipboardMungerProviderBase, IClipboardMungerProvider
    {
        public override string MethodName { get => "TruncatePost"; }
        public override string GroupName { get => "String-like"; }
        public override string Description { get; }

        public TruncatePost()
        {
            base.SetArgument(new Argument
            {
                Name = "Delimiter",
                Title = "Split by delimiter",
                DefaultValue = string.Empty,
                Description = @"Enter the delimiter to use (URL escaped, eg. \t as %09, % as %25)",
                ValidatorRegex = ".+"
            });
            base.SetArgument(new Argument
            {
                Name = "LineTerminator",
                Title = "Line terminator",
                DefaultValue = @"%0D%0A",
                Description = @"Enter the line break sequence to append to each extracted item (URL escaped, eg. Windows CR+LF \r\n as %0d%0a)",
                ValidatorRegex = @"(%[0-9a-fA-F]{2})+"
            });

            #region Examples
            base.Examples.Add("Comma Separated, Empty removed", new Example
            {
                Name = "Comma Separated, Empty removed",
                ArgumentValues = new Dictionary<string, string>
                {
                    {"Delimiter", "," },
                    {"AreEmptyRemoved", "True" },
                    {"LineTerminator", "%0d%0a" }
                },
                Input = "Item,Fix\r\nItem,,Fix\r\n,\r\n\r\nThingBop 42 ,Jim"
            });
            base.Examples.Add("Comma Separated, Empty kept", new Example
            {
                Name = "Comma Separated, Empty kept",
                ArgumentValues = new Dictionary<string, string>
                {
                    {"Delimiter", "," },
                    {"AreEmptyRemoved", "False" },
                    {"LineTerminator", "%0d%0a" }
                },
                Input = "Item,Fix\r\nItem,,Fix\r\n,\r\n\r\nThingBop 42 ,Jim"
            });
            #endregion
        }

        public override string Munge(string textToMunge)
        {
            var delimiter = System.Web.HttpUtility.UrlDecode(ArgumentValues["Delimiter"]);
            var lineTerminator = System.Web.HttpUtility.UrlDecode(ArgumentValues["LineTerminator"]);

            StringBuilder Result = new StringBuilder();
            foreach (string line in textToMunge.Split(new string[] { "\r\n" }, StringSplitOptions.None))
            {
                int ChopPoint = line.IndexOf(delimiter);
                if (ChopPoint == -1)
                {
                    Result.AppendLine(line);
                }
                else
                {
                    Result.AppendLine(line.Substring(0, ChopPoint));
                }
            }
            return Result.ToString();
        }
    }
}
