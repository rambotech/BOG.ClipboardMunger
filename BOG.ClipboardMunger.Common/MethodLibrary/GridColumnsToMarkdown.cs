// Take a tab+crlf delimited grid of data from clipboard.  Line 1 MUST be headers
// Examine column lengths, and record max + 1
// Rebuild the grid as fixed spaces, with markdown framing.

using System;
using System.Collections.Generic;
using System.Text;
using System.Web.ModelBinding;
using System.Windows.Forms;
using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class GridColumnsToMarkdown : ClipboardMungerProvider, IClipboardMungerProvider
	{
		public override string MethodName { get => "Grid Columns to Markdown table"; }
		public override string GroupName { get => "Wizardry"; }
		public override string Description { get => "| col1 | col2 | col3 |"; }

		public GridColumnsToMarkdown()
		{

		}

		public override string Munge(string textToMunge)
		{
			var result = new StringBuilder();

			var columnCount = -1;
			var ColumnWidths = new Dictionary<int, int>();
			var LineColumnsContent = new Dictionary<int, string[]>();
			int rowIndex = 0;

			foreach (string ThisLine in textToMunge.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
			{
				rowIndex++;
				string[] ColumnContent = ThisLine.Split(new string[] { "\t" }, StringSplitOptions.None);
				if (columnCount == -1)
				{
					columnCount = ColumnContent.Length;
				}
				else if (columnCount != ColumnContent.Length)
				{
					throw new Exception(string.Format("Line {0}: Expected {1} columns, but got {2}", rowIndex, columnCount, ColumnContent.Length));
				}
				LineColumnsContent.Add(LineColumnsContent.Count, ColumnContent);
				for (int colIndex = 0; colIndex < ColumnContent.Length; colIndex++)
				{
					switch (rowIndex)
					{
						case 1:
							ColumnWidths.Add(ColumnWidths.Count, ColumnContent[colIndex].Length);
							break;
						default:
							if (ColumnWidths[colIndex] < ColumnContent[colIndex].Length)
								ColumnWidths[colIndex] = ColumnContent[colIndex].Length;
							break;
					}
				}
			}

			var LineColumnContentFmt = new Dictionary<int, string>();
			var fmtStr = " {0:-**VALUE**} |";
			for (int colIndex = 0; colIndex < columnCount; colIndex++)
			{
				LineColumnContentFmt.Add(colIndex, fmtStr.Replace("**VALUE**", $"{ColumnWidths[colIndex]}"));
			}

			for (int lineIndex = 0; lineIndex < LineColumnsContent.Count; lineIndex++)
			{
				result.Append("|");
				for (int colIndex = 0; colIndex < ColumnWidths.Count; colIndex++)
				{
					result.Append(string.Format(LineColumnContentFmt[colIndex], new string[] { LineColumnsContent[lineIndex][colIndex] }));
				}
				result.AppendLine();
				if (lineIndex == 0)
				{
					result.Append("|");
					for (int colIndex = 0; colIndex < ColumnWidths.Count; colIndex++)
					{
						result.Append(string.Format(LineColumnContentFmt[colIndex], new string[] { ":-" }));
					}
					result.AppendLine();
				}
			}

			return result.ToString();
		}
	}
}
