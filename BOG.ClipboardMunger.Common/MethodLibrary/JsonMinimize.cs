﻿using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;
using System;
using System.Text;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class JsonMinimize : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "JSON minimize"; }
		public override string GroupName { get => "Rectify"; }
		public override string Description { get; }

		public JsonMinimize()
		{
		}

		public override string Munge()
		{
			StringBuilder result = new StringBuilder();

			if (base.ClipboardSource.Length > 0)
			{
				bool inQuote = false;
				int EscapeBypass = 0;
				for (int index = 0; index < base.ClipboardSource.Length; index++)
				{
					if (EscapeBypass > 0) EscapeBypass--;

					char thisChar = base.ClipboardSource[index];
					if (!inQuote)
					{
						// drop the whitespace characters
						if (thisChar == ' ') continue;
						if (thisChar == '\t') continue;
						if (thisChar == '\r') continue;
						if (thisChar == '\n') continue;
					}
					if (inQuote)
					{
						if (thisChar == '\\' && EscapeBypass == 0) EscapeBypass = 2;
					}
					if (thisChar == '"')
					{
						if (EscapeBypass == 0)
						{
							inQuote = !inQuote;
						}
						result.Append(thisChar);
						continue;
					}
					result.Append(thisChar);
				}
			}
			return result.ToString();
		}
	}
}
