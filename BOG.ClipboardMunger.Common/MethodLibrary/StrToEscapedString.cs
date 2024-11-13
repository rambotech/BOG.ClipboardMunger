using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Interface;
using System.Text;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class StrToEscapedString : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => @"To Escaped String (e.g. tab -> \t"; }
		public override string GroupName { get => "String-like"; }
		public override string Description { get; }

		public StrToEscapedString() : base()
        {
		}

		public override string Munge(string textToMunge)
		{
			StringBuilder result = new StringBuilder();
			for (int i = 0; i < textToMunge.Length; i++)
			{
				string test = textToMunge.Substring(i, 1);
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
	}
}
