using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class MakeGuid : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "Make GUID"; }
		public override string GroupName { get => "Miscellaneous"; }
		public override string Description { get; }

		public MakeGuid()
		{
			base.SetArgument(new Argument
			{
				Name = "Format",
				DefaultValue = "N",
				Description = "N=32 digits (default), D=with hyphens, B=hyphens+brackets, P=hyphens+parentheses",
				ValidatorRegex = "N|D|B|P"
			});
			base.SetArgument(new Argument
			{
				Name = "Case",
				DefaultValue = "L",
				Description = "U=All Upper, L=All Lower (default)",
				ValidatorRegex = "U|L"
			});
		}

		public override string Munge(string clipboardSource)
		{
			string YourGUID = System.Guid.NewGuid().ToString(ArgumentValues["Format"]);
			if (ArgumentValues["Case"].ToUpper() == "U") YourGUID = YourGUID.ToUpper();
			if (ArgumentValues["Case"].ToUpper() == "L") YourGUID = YourGUID.ToLower();
			return YourGUID;
		}
	}
}
