using System;
using System.Text;
using System.Text.RegularExpressions;
using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class HexViewUndo : ClipboardMungerProvider, IClipboardMungerProvider
	{
		public override string MethodName { get => "Hex View Undo"; }
		public override string GroupName { get => "Investigation"; }
		public override string Description { get => "Reverses HexView action back to string"; }

		public HexViewUndo()
		{
			base.Examples.Add("The Quick Brown Fox ...", new Example
			{
				Input = @"
0000: 54 68 65 20 71 75 69 63 | 6b 20 62 72 6f 77 6e 20  .. The.quic|k.brown.
0010: 66 6f 78 20 6a 75 6d 70 | 65 64 20 6f 76 65 72 20  .. fox.jump|ed.over.
0020: 74 68 65 20 6c 61 7a 79 | 20 64 6f 67 73 20 62 61  .. the.lazy|.dogs.ba

0030: 63 6b 20 21             |                          .. ck.!    |        
"
			});
		}

		public override string Munge(string textToMunge)
		{
			var lineRegEx = new Regex(
				@"^[0-9a-fA-F]{4}:(\s[0-9a-fA-F\s]{24})\|(\s[0-9a-fA-F\s]{24})\s[\.]{2}",
				RegexOptions.Multiline | RegexOptions.IgnoreCase);

			var Result = new StringBuilder();
			foreach (var line in textToMunge.Split(new string[] { "\r\n", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries))
			{
				if (!lineRegEx.IsMatch(line))
				{
					throw new ArgumentException("The clipboard does not contain a valid HexView output");
				}
				var section = line.Substring(6, 50).Replace("| ", string.Empty);
				var values = section.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

				for (var index=0; index < values.Length; index++ )
				{
					Result.Append((char)byte.Parse(values[index], System.Globalization.NumberStyles.HexNumber));
				}
			}
			return Result.ToString();
		}
	}
}
