using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	// Take lines and sort them (asc or desc) with optional duplicate removal.
	//
	// E.g.
	//
	// C
	// A
	// Z
	// B
	// A

	public class Sorted : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "Sorted"; }
		public override string GroupName { get => "String-like"; }
		public override string Description { get => "Ascending or Descending, w/optional dupe removal"; }

		public Sorted()
		{
			base.SetArgument(new Argument
			{
				Name = "Direction",
				Title = "Ascending or Descending Order",
				DefaultValue = "A",
				Description = @"Enter A for ascending or D for descending",
				ValidatorRegex = "A|a|D|d"
			});
			base.SetArgument(new Argument
			{
				Name = "UniqueValuesOnly",
				Title = "Include only unique values",
				DefaultValue = "true",
				Description = "Should duplicate entries be excluded from the output?",
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
			var isAscending = string.Compare("A", System.Web.HttpUtility.UrlDecode(ArgumentValues["Direction"]), true) == 0;
			var uniqueValues = bool.Parse(ArgumentValues["UniqueValuesOnly"]);
			var lineTerminator = System.Web.HttpUtility.UrlDecode(ArgumentValues["LineTerminator"]);
			var Lines = new List<string>(clipboardSource.Split(new string[] { lineTerminator }, StringSplitOptions.None));

			if (isAscending)
			{
				Lines= Lines.OrderBy(i => i).ToList();
			}
			else
			{
				Lines = Lines.OrderByDescending(i => i).ToList();
			}
			if (uniqueValues)
			{
				Lines = Lines.Distinct().ToList(); 
			}
			return string.Join(lineTerminator, Lines);
		}
	}
}
