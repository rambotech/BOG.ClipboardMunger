using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;
using System;
using System.Text;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class Join : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "Join"; }
		public override string GroupName { get => "String-like"; }
		public override string Description { get => "Join multiple lines with delimiter"; }

		public Join()
		{
			base.SetArgument(new Argument
			{
				Name = "Delimiter",
				Title = "Char/String to use for the delimiter",
				DefaultValue = @"\x09",
				Description = "use '\x09' for tab, ',' for comma, etc",
				ValidatorRegex = ".+"
			});
			base.SetArgument(new Argument
			{
				Name = "LineBreakCount",
				Title = "Lines before adding line break.",
				DefaultValue = "0",
				Description = "Leave as 0 for no line breaks added",
				ValidatorRegex = @"\d+"
			});
		}

		public override string Munge(string clipboardSource)
		{
			var delimiter = ArgumentValues["Delimiter"];
			var LinesBeforeAddingLineBreak = int.Parse(ArgumentValues["LineBreakCount"]);

			StringBuilder s = new StringBuilder();
			if (LinesBeforeAddingLineBreak == 0)
			{
				s.Append(string.Join(System.Web.HttpUtility.UrlDecode(delimiter), clipboardSource.Split(new string[] { "\r\n" }, StringSplitOptions.None)));
			}
			else
			{
				int LineIndex = 0;
				StringBuilder LineSet = new StringBuilder();
				foreach (string line in clipboardSource.Split(new string[] { "\r\n" }, StringSplitOptions.None))
				{
					LineIndex++;
					LineSet.AppendLine(line);
					if (LineIndex % LinesBeforeAddingLineBreak == 0)  // this set complete... commit it.
					{
						s.AppendLine(string.Join(System.Web.HttpUtility.UrlDecode(delimiter), LineSet.ToString().Split(new string[] { "\r\n" }, StringSplitOptions.None)));
						LineSet = new StringBuilder();
					}
				}
			}
			return s.ToString();
		}
	}
}
