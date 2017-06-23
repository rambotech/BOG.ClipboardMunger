using System;

namespace BOG.ClipboardMunger.Interfaces
{
	public interface IClipboard
	{
		string Munge(string clipboardSource);
	}
}
