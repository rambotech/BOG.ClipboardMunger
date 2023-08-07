using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class PhoeneticizedPassword : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "Password as Phonetics"; }
		public override string GroupName { get => "String-Magic"; }
		public override string Description { get => "Changes a password to a phoenetic string, e.g. \"Ab\" => \"ALPHA bravo\".  Covers ASCII 32 to 126"; }

		public PhoeneticizedPassword()
		{
		}

		public override string Munge()
		{
			Dictionary<char, string> Phoenetic = new Dictionary<char, string>();

			Phoenetic.Add(' ', "Space");
			Phoenetic.Add('!', "Exclamation");
			Phoenetic.Add('"', "DoubleQuote");
			Phoenetic.Add('#', "Hashtag");
			Phoenetic.Add('$', "Dollar");
			Phoenetic.Add('%', "Percent");
			Phoenetic.Add('&', "Ampersand");
			Phoenetic.Add('\'', "SingleQuote");
			Phoenetic.Add('(', "Left-Paren");
			Phoenetic.Add(')', "Right-Paren");
			Phoenetic.Add('*', "Asterisk");
			Phoenetic.Add('+', "Plus");
			Phoenetic.Add(',', "Comma");
			Phoenetic.Add('-', "Hyphen");
			Phoenetic.Add('.', "Period");
			Phoenetic.Add('/', "Slash");
			Phoenetic.Add('0', "Zero");
			Phoenetic.Add('1', "One");
			Phoenetic.Add('2', "Two");
			Phoenetic.Add('3', "Three");
			Phoenetic.Add('4', "Four");
			Phoenetic.Add('5', "Five");
			Phoenetic.Add('6', "Six");
			Phoenetic.Add('7', "Seven");
			Phoenetic.Add('8', "Eight");
			Phoenetic.Add('9', "Nine");
			Phoenetic.Add(':', "Colon");
			Phoenetic.Add(';', "Semicolon");
			Phoenetic.Add('<', "Less-Than");
			Phoenetic.Add('=', "Equal");
			Phoenetic.Add('>', "Greater-Than");
			Phoenetic.Add('?', "Question");
			Phoenetic.Add('@', "At-Sign");
			Phoenetic.Add('A', "ALPHA");
			Phoenetic.Add('B', "BRAVO");
			Phoenetic.Add('C', "CHARLIE");
			Phoenetic.Add('D', "DELTA");
			Phoenetic.Add('E', "ECHO");
			Phoenetic.Add('F', "FOXTROT");
			Phoenetic.Add('G', "GOLF");
			Phoenetic.Add('H', "HOTEL");
			Phoenetic.Add('I', "INDIA");
			Phoenetic.Add('J', "JULIETT");
			Phoenetic.Add('K', "KILO");
			Phoenetic.Add('L', "LIMA");
			Phoenetic.Add('M', "MIKE");
			Phoenetic.Add('N', "NOVEMBER");
			Phoenetic.Add('O', "OSCAR");
			Phoenetic.Add('P', "PAPA");
			Phoenetic.Add('Q', "QUEBEC");
			Phoenetic.Add('R', "ROMEO");
			Phoenetic.Add('S', "SIERRA");
			Phoenetic.Add('T', "TANGO");
			Phoenetic.Add('U', "UNIFORM");
			Phoenetic.Add('V', "VICTOR");
			Phoenetic.Add('W', "WHISKEY");
			Phoenetic.Add('X', "XRAY");
			Phoenetic.Add('Y', "YANKEE");
			Phoenetic.Add('Z', "ZULU");
			Phoenetic.Add('[', "Left-Bracket");
			Phoenetic.Add('\\', "Backslash");
			Phoenetic.Add(']', "Right-Bracket");
			Phoenetic.Add('^', "Circumflex");
			Phoenetic.Add('_', "Underscore");
			Phoenetic.Add('`', "Grave");
			Phoenetic.Add('a', "alpha");
			Phoenetic.Add('b', "bravo");
			Phoenetic.Add('c', "charlie");
			Phoenetic.Add('d', "delta");
			Phoenetic.Add('e', "echo");
			Phoenetic.Add('f', "foxtrot");
			Phoenetic.Add('g', "golf");
			Phoenetic.Add('h', "hotel");
			Phoenetic.Add('i', "india");
			Phoenetic.Add('j', "juliett");
			Phoenetic.Add('k', "kilo");
			Phoenetic.Add('l', "lima");
			Phoenetic.Add('m', "mike");
			Phoenetic.Add('n', "november");
			Phoenetic.Add('o', "oscar");
			Phoenetic.Add('p', "papa");
			Phoenetic.Add('q', "quebec");
			Phoenetic.Add('r', "romeo");
			Phoenetic.Add('s', "sierra");
			Phoenetic.Add('t', "tango");
			Phoenetic.Add('u', "uniform");
			Phoenetic.Add('v', "victor");
			Phoenetic.Add('w', "whiskey");
			Phoenetic.Add('x', "xray");
			Phoenetic.Add('y', "yankee");
			Phoenetic.Add('z', "zulu");
			Phoenetic.Add('{', "Left-Curly");
			Phoenetic.Add('}', "Right-Curly");
			Phoenetic.Add('|', "Vertical");
			Phoenetic.Add('~', "Tilde");

			StringBuilder output = new StringBuilder();
			for (int Index = 0; Index < base.ClipboardSource.Length; Index++)
			{
				char ToTranslate = base.ClipboardSource[Index];
				if (Phoenetic.ContainsKey(ToTranslate))
				{
					output.Append(Phoenetic[ToTranslate]);
				}
				else
				{
					output.Append(ToTranslate);
				}
				output.Append(" ");
			}
			return output.ToString();
		}
	}
}
