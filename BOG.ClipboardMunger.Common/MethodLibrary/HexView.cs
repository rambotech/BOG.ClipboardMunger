using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class HexView : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "Hex View"; }
		public override string GroupName { get => "Investigation"; }
		public override string Description { get; }

		public HexView()
		{

		}

		public override string Munge(string clipboardSource)
		{
			StringBuilder s = new StringBuilder();
			if (clipboardSource.Length > 0)
			{
				for (int iRow = 0; iRow <= ((clipboardSource.Length - 1) / 16); iRow++)
				{
					s.Append(String.Format("{0:x4}: ", iRow * 16));
					for (int iCol = 0; iCol < 16; iCol++)
					{
						if (clipboardSource.Length > iRow * 16 + iCol)
						{
							byte b = (byte)clipboardSource[iRow * 16 + iCol];
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
						if (clipboardSource.Length > iRow * 16 + iCol)
						{
							byte b = (byte)clipboardSource[iRow * 16 + iCol];
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
