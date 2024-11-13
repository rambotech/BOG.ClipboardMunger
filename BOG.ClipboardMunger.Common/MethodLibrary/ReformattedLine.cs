using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class ReformattedLine : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "Reformatted Line"; }
		public override string GroupName { get => "String-like"; }
		public override string Description { get => "takes each line as {0} input for string.Format method call."; }

		public ReformattedLine() 
        {
			base.SetArgument(new Argument
			{
				Name = "FormatString",
				Title = "Enter the format string for string.Format()",
				DefaultValue = "{0}",
				Description = "E.g.  \"Col1\": \"{0}\", \"MapCol1\": \"target.{0}\" \"",
				ValidatorRegex = @".+"
			});
            base.SetArgument(new Argument
            {
                Name = "IsMulticolumn",
                Title = "Treat clipboard as multi-column TSV (True) or single line TSV (False)",
                DefaultValue = "true",
                Description = "Whether to include COMMIT / ROLLBACK.",
                ValidatorRegex = @"true|TRUE|false|FALSE"
            });

            base.Examples.Add("Data Mapping", new Example
			{
				Input = "Bob\r\nTed\r\nAlice\r\nKaren",
				ArgumentValues = new Dictionary<string, string> {
					{ "FormatString", "this.{0} = that.{0}" },
					{ "IsMulticolumn","false"}
				},
				Name = "Data Mapping Use"
			});
			base.Examples.Add("Complex Format", new Example
			{
				Input = "Bob\r\nTed\r\nAlice\r\nKaren",
				ArgumentValues = new Dictionary<string, string> { 
					{ "FormatString", "%22{0}%22: that.{0}"},
                    { "IsMulticolumn","true"}
				},
				Name = "Complex Format Use"
			});
            base.Examples.Add("TSV columns", new Example
            {
                Input = "Bob\tTed\tAlice\tKaren",
                ArgumentValues = new Dictionary<string, string> { 
					{ "FormatString", "{0} with {2}, and {1} with {3}" },
                    { "IsMulticolumn","true" }
				},
                Name = "Data Mapping Use"
            });
        }

        public override string Munge(string textToMunge)
		{
			var FormattedAs = HttpUtility.UrlDecode(this.ArgumentValues["FormatString"]);
			var IsMulticolumn = bool.Parse(this.ArgumentValues["IsMulticolumn"]);

            StringBuilder output = new StringBuilder();
			foreach (string line in textToMunge.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
			{
				if (!IsMulticolumn)
				{
					output.AppendLine(string.Format(FormattedAs, line));
					continue;
				}
				var lineArgs = line.Split(new char[] { '\t' }, StringSplitOptions.None);
                output.AppendLine(string.Format(FormattedAs, lineArgs));
            }
            return output.ToString();
		}
	}
}
