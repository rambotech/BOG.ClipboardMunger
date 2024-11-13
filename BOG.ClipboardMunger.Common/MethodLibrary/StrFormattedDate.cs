using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;
using System;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class StrFormattedDate : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "DateFormat"; }
		public override string GroupName { get => "String-like"; }
		public override string Description { get; }

		public StrFormattedDate() 
        {
			base.SetArgument(new Argument
			{
				Name = "Format",
				Title = "Format for the DateTime desired",
				DefaultValue = "s",
				Description = "The string.format value to apply: default is \"s\" for ISO-8166. For available formats, see https://blog.stevex.net/string-formatting-in-csharp/).",
				ValidatorRegex = ".+"
			});
			base.SetArgument(new Argument
			{
				Name = "DateTimeNow",
				Title = "Should Current DateTime be used",
				DefaultValue = "true",
				Description = "When true DateTime.Now is formatted; otherwise the clipboard content is treated as the DateTime value to format.",
				ValidatorRegex = @"true|TRUE|false|FALSE"
			});
		}

		public override string Munge(string textToMunge)
		{
			if (bool.Parse(ArgumentValues["DateTimeNow"]))
			{
				return DateTime.Now.ToString(ArgumentValues["Format"]);
			}
			return DateTime.Parse(textToMunge).ToString(ArgumentValues["Format"]);
		}
	}
}
