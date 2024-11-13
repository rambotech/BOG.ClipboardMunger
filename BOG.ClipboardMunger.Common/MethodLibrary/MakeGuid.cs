using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;
using System.Collections.Generic;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class MakeGuid : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "Make GUID"; }
		public override string GroupName { get => "Miscellaneous"; }
		public override string Description { get; }

		public MakeGuid() : base()
        {
			base.SetArgument(new Argument
			{
				Name = "Format",
				Title = "Format of the generated GUID",
				DefaultValue = "N",
				Description = "N=32 digits (default), D=with hyphens, B=hyphens+brackets, P=hyphens+parentheses",
				ValidatorRegex = "N|D|B|P"
			});
			base.SetArgument(new Argument
			{
				Name = "Case",
				Title = "Casing of letters",
				DefaultValue = "L",
				Description = "U=All Upper, L=All Lower (default)",
				ValidatorRegex = "U|L"
			});

			base.Examples.Add("Example 1", new Example
			{
				Name = "Format as 32 hex digits, upper case",
				Input = string.Empty,
				ArgumentValues = new Dictionary<string, string>
				{
					{"Format", "N" },
					{"Case", "U" }
				}
			});
			base.Examples.Add("Example 2", new Example
			{
				Name = "Format as 32 hex digits, hyphens, lower case",
				Input = "",
				ArgumentValues = new Dictionary<string, string>
				{
					{"Format", "D" },
					{"Case", "L" }
				}
			});
		}

		public override string Munge(string textToMunge)
		{
			string YourGUID = System.Guid.NewGuid().ToString(ArgumentValues["Format"]);
			if (ArgumentValues["Case"].ToUpper() == "U") YourGUID = YourGUID.ToUpper();
			if (ArgumentValues["Case"].ToUpper() == "L") YourGUID = YourGUID.ToLower();
			return YourGUID;
		}
	}
}
