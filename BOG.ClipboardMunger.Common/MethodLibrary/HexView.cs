using System.Text;
using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Interface;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class HexView : ClipboardMungerProvider, IClipboardMungerProvider
	{
		public override string MethodName { get => "Hex View"; }
		public override string GroupName { get => "Investigation"; }
		public override string Description { get; }

		public HexView()
		{
		}

		public override string Munge(string textToMunge)
		{
			int Offset = 0;
			int Index = 0;
			var Result = new StringBuilder();

			while (true)
			{
				Result.Append(string.Format("{0:x4}: ", Offset));

				for (Index = Offset; Index < Offset + 16; Index++)
				{
					if (Index != Offset && Index % 8 == 0)
					{
						Result.Append("| ");
					}
					if (Index < textToMunge.Length)
					{
						Result.Append(string.Format("{0:x2} ", (byte)textToMunge[Index]));
					}
					else
					{
						Result.Append("   ");
					}
				}

				Result.Append(" .. ");

				for (Index = Offset; Index < Offset + 16; Index++)
				{
					if (Index != Offset && Index % 8 == 0)
					{
						Result.Append("|");
					}
					if (Index < textToMunge.Length)
					{
						var thisChar = textToMunge[Index];
						if (thisChar < 0x21 || thisChar > 0x7E)
						{
							thisChar = '.';
						}
						Result.Append(thisChar);
					}
					else
					{
						Result.Append(" ");
					}
				}
				Result.AppendLine();
				if (Index >= textToMunge.Length)
				{
					break;
				}
				Offset += 16;
			}
			return Result.ToString();
		}
	}
}
