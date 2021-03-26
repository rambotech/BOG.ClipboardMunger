using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class SplitByDelimiter : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "SplitByDelimiter"; }
		public override string GroupName { get => "String-like"; }
		public override string Description { get; }

		public SplitByDelimiter()
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
				Name = "AreEmptyRemoved",
				Title = "Remove empty entries",
				DefaultValue = "true",
				Description = "Should empty entries be excluded from the output?",
				ValidatorRegex = @"true|TRUE|false|FALSE"
			});
			base.SetArgument(new Argument
			{
				Name = "LineTerminator",
				Title = "Line terminator",
				DefaultValue = @"%0D%0A",
				Description = @"Enter the line break sequence to append to each extracted item (URL escaped, eg. Windows CR+LF \r\n as %0d%0a)",
				ValidatorRegex = @"(%[0-9a-fA-F]{2})+"
			});
		}

		public override string Munge(string clipboardSource)
		{
			var delimiter = System.Web.HttpUtility.UrlDecode(ArgumentValues["Delimiter"]);
			var removeBlankLines = bool.Parse(ArgumentValues["AreEmptyRemoved"]);
			var lineTerminator = System.Web.HttpUtility.UrlDecode(ArgumentValues["LineTerminator"]);

			return string.Join(
				lineTerminator,
				clipboardSource.Split(
				new string[] { delimiter },
				removeBlankLines ? StringSplitOptions.RemoveEmptyEntries: StringSplitOptions.None));
		}
	}
}
