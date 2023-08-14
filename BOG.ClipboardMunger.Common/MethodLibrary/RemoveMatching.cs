using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;
using System;
using System.Text;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class RemoveMatching : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "Remove all lines with matching text"; }
		public override string GroupName { get => "Filtering"; }
		public override string Description { get; }

		public RemoveMatching()
		{
			base.SetArgument(new Argument
			{
				Name = "Filter",
				Title = "Text filter to qualify a line",
				DefaultValue = string.Empty,
				Description = "Enter a phrase which must exist in a line to remove it",
				ValidatorRegex = ".+"
			});
			base.SetArgument(new Argument
			{
				Name = "LineTerminator",
				Title = "Line ending",
				DefaultValue = @"%0D%0A",
				Description = @"Enter the line break sequence (URL escaped, eg. Windows CR+LF \r\n as %0d%0a)",
				ValidatorRegex = @"(%[0-9a-fA-F]{2})+"
			});
			base.SetArgument(new Argument
			{
				Name = "AreLineNumbersPrefixed",
				Title = "Use line numbers",
				DefaultValue = "false",
				Description = "Whether line numbers should prefix the matched lines.",
				ValidatorRegex = @"true|TRUE|false|FALSE"
			});
		}

		public override string Munge()
		{
			var filter = ArgumentValues["Filter"];
			var lineEnding = System.Web.HttpUtility.UrlDecode(ArgumentValues["LineTerminator"]);
			var useLineNumbers = bool.Parse(ArgumentValues["AreLineNumbersPrefixed"]);
			StringBuilder result = new StringBuilder();
			var lineIndex = 0;
			foreach (string ThisLine in base.ClipboardSource.Split(new string[] { lineEnding }, StringSplitOptions.RemoveEmptyEntries))
			{
				lineIndex++;
				if (ThisLine.IndexOf(filter) == -1)
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
