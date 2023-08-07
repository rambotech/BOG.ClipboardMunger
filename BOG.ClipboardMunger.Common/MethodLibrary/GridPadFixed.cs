using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Windows.Forms;
using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class GridPadFixed : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "Grid to Fixed Width Display"; }
		public override string GroupName { get => "Rectify"; }
		public override string Description { get; }

		public GridPadFixed()
		{
			base.SetArgument(new Argument
			{
				Name = "LineTerminator",
				Title = "Line Termination Sequence",
				DefaultValue = "%0d%0a",
				Description = "%0d%0a for Windows file, %0a for Linux files",
				ValidatorRegex = @"[\%0-9A-Fa-f].+"
			});
		}

		public override string Munge()
		{
			var lineEnding = System.Web.HttpUtility.UrlDecode(ArgumentValues["LineTerminator"]);

			StringBuilder result = new StringBuilder();

			var lines = base.ClipboardSource.Split(new string[] { lineEnding }, StringSplitOptions.RemoveEmptyEntries);
			if (lines.Length == 1)
			{
				throw new Exception("Parsing failure: only see 1 line of text.  Requires a header line, and data lines.  Check that line termination value is correct.");
			}

			var columnMaxSizes = new Dictionary<int, int>();

			// pass 1: size 'em up
			for (var lineIndex = 0; lineIndex < lines.Length; lineIndex++)
			{
				var columns = lines[lineIndex].Split(new string[] { "\t" }, StringSplitOptions.None);
				if (lineIndex == 0) // header
				{
					for (var colIndex = 0; colIndex < columns.Length; colIndex++)
					{
						columnMaxSizes.Add(colIndex, columns[colIndex].Length);
					}
				}
				else
				{
					if (columns.Length != columnMaxSizes.Keys.Count)
					{
						throw new Exception($"Line {lineIndex}: expecting same column count as header ({columnMaxSizes.Keys.Count}) but has {columns.Length}");
					}
					for (var colIndex = 0; colIndex < columns.Length; colIndex++)
					{
						if (columnMaxSizes[colIndex] < columns[colIndex].Length)
						{
							columnMaxSizes[colIndex] = columns[colIndex].Length;
						}
					}
				}
			}

			// pass 2: rebuild a fixed-column width output
			for (var lineIndex = 0; lineIndex < lines.Length; lineIndex++)
			{
				var columns = lines[lineIndex].Split(new string[] { "\t" }, StringSplitOptions.None);
				if (lineIndex == 1) // add underline characters under the header values
				{
					for (var colIndex = 0; colIndex < columnMaxSizes.Keys.Count; colIndex++)
					{
						if (colIndex > 0)
						{
							result.Append(" ");
						}
						result.Append(new string('=', columnMaxSizes[colIndex]));
					}
					result.AppendLine();
				}
				for (var colIndex = 0; colIndex < columnMaxSizes.Keys.Count; colIndex++)
				{
					if (colIndex > 0)
					{
						result.Append(" ");
					}
					result.Append(columns[colIndex].PadRight(columnMaxSizes[colIndex]));
				}
				result.AppendLine();
			}
			return result.ToString();
		}
	}
}
