using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;
using System;
using System.Text;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class ExtractOffsetLength : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "ExtractOffsetLength"; }
		public override string GroupName { get => "Investigation"; }
		public override string Description { get => "Extract the same character block from every line on the clipboard."; }

		public ExtractOffsetLength()
		{
			base.SetArgument(new Argument
			{
				Name = "StartOffset",
				Title = "The starting offset",
				DefaultValue = "0",
				Description = " (0 to string.length - 1)",
				ValidatorRegex = @"\d+"
			});
			base.SetArgument(new Argument
			{
				Name = "Length",
				Title = "The number of characters to extract from the start",
				DefaultValue = "0",
				Description = string.Empty,
				ValidatorRegex = @"\d+"
			});
		}

		public override string Munge(string textToMunge)
		{
			int startingOffset = int.Parse(ArgumentValues["StartOffset"]);
			int length = int.Parse(ArgumentValues["Length"]);

			StringBuilder result = new StringBuilder();

			foreach (string line in textToMunge.Split(new string[] { "\r\n" }, StringSplitOptions.None))
			{
				string columnValue = string.Empty;
				if (line.Length >= startingOffset + length)
				{
					columnValue = line.Substring(startingOffset, length);
				}
				else
				{
					if (line.Length >= startingOffset) columnValue = line.Substring(startingOffset);
				}

				result.AppendLine(columnValue);
			}
			return result.ToString();
		}
	}
}