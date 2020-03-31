using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;
using System;
using System.Text;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class FilterByLine : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "Extract line content text block"; }
		public override string GroupName { get => "Filtering"; }
		public override string Description { get; }

		public FilterByLine()
		{
			base.SetArgument(new Argument
			{
				Name = "Filter",
				DefaultValue = string.Empty,
				Description = "Enter a phrase which must exist in a line to keep it",
				ValidatorRegex = ".+"
			});
			base.SetArgument(new Argument
			{
				Name = "Line ending",
				DefaultValue = @"%0D%0A",
				Description = @"Enter the line break sequence (URL escaped, eg. Windows CR+LF \r\n as %0d%0a)",
				ValidatorRegex = @"(%[0-9a-fA-F]{2})+"
			});
			base.SetArgument(new Argument
			{
				Name = "Use line numbers",
				DefaultValue = "false",
				Description = "Whether line numbers should prefix the matched lines.",
				ValidatorRegex = @"true|TRUE|false|FALSE"
			});
		}

		public override string Munge(string clipboardSource)
		{
			var filter = ArgumentValues["Filter"];
			var lineEnding = System.Web.HttpUtility.UrlDecode(ArgumentValues["Line ending"]);
			var useLineNumbers = bool.Parse(ArgumentValues["Use line numbers"]);
			StringBuilder result = new StringBuilder();
			var lineIndex = 0;
			foreach (string ThisLine in clipboardSource.Split(new string[] { lineEnding }, StringSplitOptions.RemoveEmptyEntries))
			{
				lineIndex++;
				if (ThisLine.IndexOf(filter) >= 0)
				{
					if (useLineNumbers)
					{
						result.Append(string.Format("{0,6}: ", lineIndex));
					}
					result.AppendLine(ThisLine);
				}
			}
			return result.ToString();
		}
	}
}
