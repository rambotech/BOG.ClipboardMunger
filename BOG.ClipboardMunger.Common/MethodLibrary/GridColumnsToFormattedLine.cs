using System;
using System.Collections.Generic;
using System.Text;
using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
    public class GridColumnsToFormattedLine : ClipboardMungerProviderBase, IClipboardMungerProvider
    {
        public override string MethodName { get => "Formatted Rows for Column Data"; }
        public override string GroupName { get => "Wizardry"; }
        public override string Description { get => @"A\tB\tC\tD to This has :ColD={3},ColC={2},ColB={1},ColA={0};"; }

        public GridColumnsToFormattedLine()
        {
            base.SetArgument(new Argument
            {
                Name = "LineFormat",
                Title = "Line format specifier",
                DefaultValue = "{0},{1}",
                Description = "Column a is {0}, Column b is {1}, etc",
                ValidatorRegex = @".+"
            });

            base.Examples.Add("Good Example", new Example
            {
                Name = "Format has all provided column data for format",
                Input = "A\tB\tC,41\tZ4\r\nOther\tTikTok\tKim",
                ArgumentValues = new Dictionary<string, string> {
                    { "LineFormat", "Object.Add(\"{0}\", \"{2}\", \"{1}\")" }
                }
            });

            base.Examples.Add("Bad Example", new Example
            {
                Name = "Format does not have all column data for format",
                Input = "A\tB\tC,41\tZ4\r\nOther\tTikTok\tKim",
                ArgumentValues = new Dictionary<string, string> {
                    { "LineFormat", "Object.Add(\"{0}\", \"{2}\", \"{1}\", \"{3}\")" }
                }
            });
        }

        public override string Munge(string textToMunge)
        {
            var lineFormat= System.Web.HttpUtility.UrlDecode(ArgumentValues["LineFormat"]);

            StringBuilder result = new StringBuilder();
            int LineIndex = 0;

            if (textToMunge.Length > 0)
            {
                foreach (string ThisLine in textToMunge.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (string.IsNullOrEmpty(ThisLine)) continue;
                    if (LineIndex++ > 0) result.AppendLine();
                    string[] ColumnData = ThisLine.Split(new char[] { '\t' }, StringSplitOptions.None);
                    result.AppendFormat(lineFormat, ColumnData);
                }
            }
            result.AppendLine();
            return result.ToString();
        }
    }
}
