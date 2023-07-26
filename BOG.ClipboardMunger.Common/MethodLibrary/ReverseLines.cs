using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	internal class ReverseLines : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "Reverse Lines"; }
		public override string GroupName { get => "String-like"; }
		public override string Description { get; }
		public ReverseLines()
		{
			base.SetArgument(new Argument
			{
				Name = "IgnoreBlankLines",
				Title = "Ignore blank lines",
				DefaultValue = "false",
				Description = "Whether empty lines should be discarded from the output",
				ValidatorRegex = @"true|TRUE|false|FALSE"
			});
		}

		public override string Munge(string clipboardSource)
		{
			var ignoreBlankLines = bool.Parse(ArgumentValues["IgnoreBlankLines"]);

			StringBuilder output = new StringBuilder();
			Stack<string> stack = new Stack<string>();
			foreach (string s in clipboardSource.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
			{
				if (ignoreBlankLines && string.IsNullOrWhiteSpace(s)) continue;
				stack.Push(s);
			}
			while (stack.Count > 0)
			{
				output.AppendLine(stack.Pop());
			}
			return output.ToString();
		}
	}
}
