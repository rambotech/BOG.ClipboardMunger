using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
    public class RegexExtract : ClipboardMungerProviderBase, IClipboardMungerProvider
    {
        public override string MethodName { get => "Extract items matching a regular expression"; }
        public override string GroupName { get => "Filtering"; }
        public override string Description { get; }

        public RegexExtract()
        {
            base.SetArgument(new Argument
            {
                Name = "Filter",
                Title = "Enter the REGEX expression to match",
                DefaultValue = ".+",
                Description = "Enter a phrase which must exist in a line to remove it",
                ValidatorRegex = ".+"
            });
            base.SetArgument(new Argument
            {
                Name = "IgnoreCase",
                Title = "Ignore Case",
                DefaultValue = @"true",
                Description = @"true for case insensitive match, or false to honor case",
                ValidatorRegex = @"true|TRUE|false|FALSE"
            });
            base.SetArgument(new Argument
            {
                Name = "LineTerminator",
                Title = "Line ending",
                DefaultValue = @"%0D%0A",
                Description = @"Enter the line break sequence (URL escaped, eg. Windows CR+LF \r\n as %0d%0a)",
                ValidatorRegex = @"(%[0-9a-fA-F]{2})+"
            });
        }

        public override string Munge(string textToMunge)
        {
            var filter = ArgumentValues["Filter"];
            var ignoreCase = bool.Parse(ArgumentValues["IgnoreCase"]);
            var lineTerminator = ArgumentValues["LineTerminator"];

            var result = new StringBuilder();
            Regex r = new Regex(filter, ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.None);

            foreach (Match thisMatch in r.Matches(textToMunge))
            {
                result.AppendLine(thisMatch.Value);
            }
            return result.ToString();
        }
    }
}