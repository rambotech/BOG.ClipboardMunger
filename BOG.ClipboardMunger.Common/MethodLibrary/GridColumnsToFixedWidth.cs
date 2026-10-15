using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class GridColumnsToFixedWidth : ClipboardMungerProvider, IClipboardMungerProvider
	{
		public override string MethodName { get => "Grid Rows to Fixed-width Columns"; }
		public override string GroupName { get => "Wizardry"; }
		public override string Description { get => @"ABC\tB\tCX\tDEADTY to this: ABC    CX     DEADTY "; }

		public GridColumnsToFixedWidth()
		{
			base.SetArgument(new Argument
			{
				Name = "ColumnWidthLimit",
				Title = "Maximum characters to allow in a column",
				DefaultValue = "0",
				Description = "The column width causing truncation, and appending an elipses to mark dropped ending characteres. Use 0 for unlimited.",
				ValidatorRegex = @"\d{1,3}"
			});
			base.SetArgument(new Argument
			{
				Name = "RowSeparationCharacter",
				Title = "Character(s) used to separate rows",
				DefaultValue = "%0d%0a",
				Description = "The character used to separate rows in the input data.",
				ValidatorRegex = ".+"
			});
			base.SetArgument(new Argument
			{
				Name = "ColumnSeparationCharacter",
				Title = "Character used to separate column data in the row",
				DefaultValue = "%09",
				Description = "The character used to separate columns in the input data.",
				ValidatorRegex = ".+"
			});
		}

		public override string Munge(string textToMunge)
		{
			var columnWidthLimit = ArgumentValues["ColumnWidthLimit"];
			var separationCharacter = System.Web.HttpUtility.UrlDecode(ArgumentValues["SeparationCharacter"]);

			StringBuilder result = new StringBuilder();
			int LineIndex = 0;

			if (textToMunge.Length > 0)
			{
				var columnMaxWidths = new Dictionary<int, int>();
				foreach (string ThisLine in textToMunge.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
				{
					if (string.IsNullOrEmpty(ThisLine)) continue;
					if (LineIndex++ > 0) result.AppendLine();
					string[] ColumnData = ThisLine.Split(new char[] { '\t' }, StringSplitOptions.None);
					result.AppendFormat(columnWidthLimit, ColumnData);
				}
			}
			result.AppendLine();
			return result.ToString();
		}
	}
}