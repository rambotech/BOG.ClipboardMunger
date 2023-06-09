using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;
using System;
using System.Text;
using System.Web;
using System.Windows.Forms;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class PrefixSuffix : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "PrefixSuffix"; }
		public override string GroupName { get => "String-like"; }
		public override string Description { get => "Pre-pend/Append values to each line: prepend/append values support URLencoded characters."; }

		public PrefixSuffix()
		{
			base.SetArgument(new Argument
			{
				Name = "Prefix",
				Title = "The prepended value",
				DefaultValue = "",
				Description = "URLEncode when needed, e.g.: 100%25",
				ValidatorRegex = @".+"
			});
			base.SetArgument(new Argument
			{
				Name = "Suffix",
				Title = "The appended value",
				DefaultValue = "",
				Description = "URLEncode when needed, e.g.: \"%0d%0a",
				ValidatorRegex = @".+"
			});
		}

		public override string Munge(string clipboardSource)
		{
			var result = new StringBuilder();

			string Prefix = System.Web.HttpUtility.UrlDecode(ArgumentValues["Prefix"]);
			string Suffix = System.Web.HttpUtility.UrlDecode(ArgumentValues["Suffix"]);

			var lineEnding = "\r\n";
			var lines = new string[] { string.Empty };
			if (clipboardSource.IndexOf(lineEnding) >= 0)
			{
				lines = clipboardSource.Split(new string[] { lineEnding }, StringSplitOptions.None);
			}
			else
			{
				lineEnding = "\n";
				if (clipboardSource.IndexOf(lineEnding) >= 0)
				{
					lines = clipboardSource.Split(new string[] { lineEnding }, StringSplitOptions.None);
				}
				else
				{
					lineEnding = "\r";
					if (clipboardSource.IndexOf(lineEnding) >= 0)
					{
						lines = clipboardSource.Split(new string[] { lineEnding }, StringSplitOptions.None);
					}
					else
					{
						lineEnding = string.Empty;
						lines = new string[] { clipboardSource };
					}
				}
			}

			int LineIndex = 0;
			foreach (string Line in lines)
			{
				LineIndex++;
				result.AppendLine(Prefix + Line + Suffix);
			}
			return result.ToString();
		}
	}
}



/*
 * 
 * // SQL to CSharp string, and vice-versa

using System;
using System.Text;
using System.Windows.Forms;
using BOG.Framework;
using System.Web;

public class Script : Interfaces.IClipboard
{
        public string Munge(string clipboardSource)
        {
                string Prefix = string.Empty;
                string Suffix = string.Empty;
                if (DialogResult.OK != BOG.Framework.FormEx.InputBox ("Prefix", "Enter the text to prefix to the beginning of each line", ref Prefix))
                {
                        return clipboardSource;
                }
                if (DialogResult.OK != BOG.Framework.FormEx.InputBox ("Suffix", "Enter the text to suffix to the end of each line", ref Suffix))
                {
                        return clipboardSource;
                }
				
				StringBuilder result = new StringBuilder();
				int LineIndex = 0;
				foreach (string Line in clipboardSource.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
				{
					LineIndex++;
					result.AppendLine(
						HttpUtility.UrlDecode(Prefix.Replace ("%INDEX%", LineIndex.ToString())) + 
						Line + 
						HttpUtility.UrlDecode(Suffix.Replace ("%[INDEX]%", LineIndex.ToString()))
					);
				}
                return result.ToString();
        }
}

*/