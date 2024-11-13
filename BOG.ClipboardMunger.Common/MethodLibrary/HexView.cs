using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Interface;
using System;
using System.Text;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class HexView : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "Hex View"; }
		public override string GroupName { get => "Investigation"; }
		public override string Description { get; }

		public HexView() : base()
        {

		}

		public override string Munge(string textToMunge)
		{
			StringBuilder s = new StringBuilder();
			if (textToMunge.Length > 0)
			{
				for (int iRow = 0; iRow <= ((textToMunge.Length - 1) / 16); iRow++)
				{
					s.Append(String.Format("{0:x4}: ", iRow * 16));
					for (int iCol = 0; iCol < 16; iCol++)
					{
						if (textToMunge.Length > iRow * 16 + iCol)
						{
							byte b = (byte)textToMunge[iRow * 16 + iCol];
							s.Append(String.Format("{0:x2} ", b));
						}
						else
						{
							s.Append("   ");  // 3 spaces
						}
					}
					s.Append(" | ");
					for (int iCol = 0; iCol < 16; iCol++)
					{
						if (textToMunge.Length > iRow * 16 + iCol)
						{
							byte b = (byte)textToMunge[iRow * 16 + iCol];
							s.Append((b < 32 || b > 128) ? '.' : (char)b);
						}
						else
						{
							s.Append(' ');  // 1 space
						}
					}
					s.Append("\r\n");
				}
			}
			return s.ToString();
		}
	}
}
