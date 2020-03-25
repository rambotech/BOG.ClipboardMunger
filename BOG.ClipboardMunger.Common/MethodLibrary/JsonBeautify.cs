using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;
using System;
using System.Text;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class JsonBeautify : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "JSON readable"; }
		public override string GroupName { get => "Rectify"; }
		public override string Description { get; }

		public JsonBeautify()
		{
		}

		public override string Munge(string clipboardSource)
		{
			StringBuilder result = new StringBuilder();

			if (clipboardSource.Length > 0)
			{
				bool inQuote = false;
				bool nextIsEscaped = false;
				int escapeBypass = 0;
				int indentLevel = 0;
				char prevChar = '\x00';

				for (int index = 0; index < clipboardSource.Length; index++)
				{
					char thisChar = clipboardSource[index];
					string prefix = string.Empty;
					string suffix = string.Empty;

					if (result.Length == 0 && (thisChar == ' ' || thisChar == '\t'))
					{
						continue;
					}
					else if (!inQuote && (thisChar == ' ' || thisChar == '\t') && prevChar != ':')
					{
						continue;
					}
					else if (!inQuote && (thisChar == '\r' || thisChar == '\n'))
					{
						continue;
					}

					if (!inQuote && thisChar == ':')
					{
						suffix = " ";  // put a space after the colon separator, which makes readability a bit better.
					}

					if (inQuote && escapeBypass > 0)
					{
						if ("0123456789ABCDEF".IndexOf(char.ToUpper(thisChar)) < 0)
						{
							throw new ArgumentException("Invalid hex character in \\u##### escape sequence");
						}
						escapeBypass--;
					}
					else if (inQuote && !nextIsEscaped && thisChar == '\\')
					{
						// JSON encoding character is either "\uHHHH" or "\c", where HHHH is 4-digits of hex and c is a single character other than 'u'
						// Ref: https://tools.ietf.org/html/rfc7159#section-7
						nextIsEscaped = true;
					}
					else if (inQuote && nextIsEscaped)
					{
						nextIsEscaped = false;
						if (thisChar == 'u')
						{
							escapeBypass = 4;
						}
						else
						{
							escapeBypass = 0;
							if ("\"\\/bfnrt".IndexOf(char.ToLower(thisChar)) < 0)
							{
								throw new ArgumentException("Invalid character in single character escape (\\) sequence: must be one of { \" \\ / b f n r t }");
							}
						}
					}
					else if (thisChar == '"')
					{
						inQuote = !inQuote;
						nextIsEscaped = false;
						escapeBypass = 0;
					}
					else if (!inQuote && (thisChar == '[' || thisChar == '{'))
					{
						prefix = (prevChar == ']' || prevChar == '}') ? "\r\n" : string.Empty;
						indentLevel++;
						suffix = "\r\n" + new string(' ', indentLevel * 2);
					}
					else if (!inQuote && thisChar == ',')
					{
						suffix = "\r\n" + new string(' ', indentLevel * 2);
					}
					else if (!inQuote && (thisChar == ']' || thisChar == '}'))
					{
						indentLevel--;
						prefix = (result.ToString().Length == 0 ? string.Empty : "\r\n") + new string(' ', indentLevel * 2);
					}

					prevChar = thisChar;
					result.Append(prefix);
					result.Append(thisChar);
					result.Append(suffix);
				}
			}
			return result.ToString();
		}
	}
}
