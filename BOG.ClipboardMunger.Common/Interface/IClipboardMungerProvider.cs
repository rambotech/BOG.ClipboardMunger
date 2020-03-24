using System.Collections.Generic;
using BOG.ClipboardMunger.Common.Entity;

namespace BOG.ClipboardMunger.Common.Interface
{
	public interface IClipboardMungerProvider
	{
		string MethodName { get; }
		string GroupName { get; }
		string Description { get; }
		string Munge(string clipboardSource);
	}
}
