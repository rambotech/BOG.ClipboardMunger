using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class StrToCSharpString : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "To CSharp (encoded) String"; }
		public override string GroupName { get => "String-like"; }
		public override string Description { get; }

		public StrToCSharpString()
		{
		}

		private string TrueTrim(string x)
		{
			string Result = x;
			while (Result.Length > 0 && Result.IndexOfAny(new char[] { '\x20', '\x09' }) == 0) Result = Result.Length == 0 ? string.Empty : Result.Substring(1);
			while (Result.Length > 0 && Result.LastIndexOfAny(new char[] { '\x20', '\x09' }) == Result.Length - 1) Result = Result.Length == 0 ? string.Empty : Result.Substring(0, Result.Length - 1);
			return Result;
		}

		public override string Munge(string clipboardSource)
		{
			const string CSharpPrefix = "\"";
			const string CSharpAppend = " \" + \"\\r\\n\" +";
			const string CSharpEndsWith = " \" + \"\\r\\n\"";
			StringBuilder Result = new StringBuilder();
			string clipboardCleaned = clipboardSource.Trim();

			if (clipboardCleaned.IndexOf(CSharpEndsWith) > -1)
			{
				foreach (string line in clipboardCleaned.Split(new string[] { "\r\n" }, StringSplitOptions.None))
				{
					string WorkLine = TrueTrim(line);
					if (WorkLine.Length == 0)
					{
						Result.AppendLine();
						continue;
					}
					if (WorkLine[0] != '\"')
					{
						Result.AppendLine(WorkLine);
						continue;
					}
					if (WorkLine.LastIndexOf(CSharpEndsWith) < 0)
					{
						// not properly terminated.
						Result.AppendLine(WorkLine);
						continue;
					}

					WorkLine = WorkLine.Substring(1);  // removes leading quote
					WorkLine = WorkLine.Replace("\\\"", "\"");
					WorkLine = WorkLine.Replace("\\\\", "\\");
					Result.AppendLine(WorkLine.Substring(0, WorkLine.LastIndexOf(CSharpEndsWith)));
				}
			}
			else
			{
				foreach (string line in clipboardCleaned.Split(new string[] { "\r\n" }, StringSplitOptions.None))
				{
					if (line.Trim().Length == 0)
					{
						Result.AppendLine("\"" + CSharpAppend);
						continue;
					}
					string WorkLine = line;
					WorkLine = WorkLine.Replace("\\", "\\\\");
					WorkLine = WorkLine.Replace("\"", "\\\"");
					Result.AppendLine(CSharpPrefix + WorkLine + CSharpAppend);
				}
			}
			return Result.ToString();
		}
	}
}
