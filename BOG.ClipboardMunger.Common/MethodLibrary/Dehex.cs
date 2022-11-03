using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;
using BOG.Framework;
using BOG.Framework.Extensions;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class Dehex : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "Dehex (returns answers as base64 encoded, to work with binary"; }
		public override string GroupName { get => "Investigation"; }
		public override string Description { get; }

		public Dehex()
		{

		}

		public override string Munge(string clipboardSource)
		{
			var s = new StringBuilder();
			if (clipboardSource.Length > 0)
			{
				var value = 0;
				var offset = 0;
				for (int colIndex = 0; colIndex < clipboardSource.Length; colIndex++)
				{
					var digitValue = "0123456789ABCDEFabcdef".IndexOf(clipboardSource[colIndex]);
					if (digitValue >= 0)
					{
						if (digitValue > 15) digitValue -= 6;
						value *= 16;
						value += digitValue;
						if (++offset == 2)
						{
							s.Append((char) value);
							value = 0;
							offset = 0;
						}
					}
				}
				if (offset == 1)
				{
					throw new ArgumentException("Odd count of hex digits: must be paired");
				}
			}
			return s.ToString().Base64EncodeString(true);
		}
	}
}
