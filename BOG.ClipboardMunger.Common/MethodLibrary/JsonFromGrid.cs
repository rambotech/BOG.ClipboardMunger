using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class JsonFromGrid : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "JSON from Grid"; }
		public override string GroupName { get => "Wizardry"; }
		public override string Description { get; }

		public JsonFromGrid() : base()
        {
		}

		private string C_EncodedString(string source)
		{
			StringBuilder result = new StringBuilder();
			for (int i = 0; i < source.Length; i++)
			{
				string test = source.Substring(i, 1);
				if (test == "\\")
				{
					result.Append("\\\\");
				}
				else if (test == "\"")
				{
					result.Append("\\\"");
				}
				else if (test == "/")
				{
					result.Append("\\/");
				}
				else if (test == "\x08")
				{
					result.Append("\\b");
				}
				else if (test == "\x0c")
				{
					result.Append("\\f");
				}
				else if (test == "\x0d")
				{
					result.Append("\\r");
				}
				else if (test == "\x0a")
				{
					result.Append("\\n");
				}
				else if (test == "\x09")
				{
					result.Append("\\t");
				}
				else
					result.Append(test);
			}
			return result.ToString();
		}

		public override string Munge(string textToMunge)
		{
			StringBuilder result = new StringBuilder();
			int LineIndex = 0;
			Dictionary<int, string> DataIndex = new Dictionary<int, string>();
			result.AppendLine("[");

			if (textToMunge.Length > 0)
			{
				foreach (string ThisLine in textToMunge.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
				{
					if (string.IsNullOrEmpty(ThisLine)) continue;
					LineIndex++;
					string[] ColumnData = ThisLine.Split(new char[] { '\t' }, StringSplitOptions.None);
					if (LineIndex > 1)
					{
						if (LineIndex > 2)
						{
							result.AppendLine(",");
						}
						result.AppendLine("  {");
					}
					for (int ColumnIndex = 0; ColumnIndex < ColumnData.Length; ColumnIndex++)
					{
						if (LineIndex == 1)
						{
							// for the header line, populate the column names for use as column tags.
							DataIndex.Add(ColumnIndex, ColumnData[ColumnIndex].Trim());
							continue;
						}
						if (ColumnIndex > 0)
						{
							result.AppendLine(",");
						}
						result.Append(string.Format("    \"{0}\": \"{1}\"",
							C_EncodedString(DataIndex[ColumnIndex]),
							C_EncodedString(ColumnData[ColumnIndex])));
					}
					if (LineIndex > 1)
					{
						result.AppendLine();
						result.Append("  }");
					}
				}
			}
			result.AppendLine();
			result.AppendLine("]");
			return result.ToString();
		}
	}
}
