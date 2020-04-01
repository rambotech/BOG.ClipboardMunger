﻿using System.Collections.Generic;
using BOG.ClipboardMunger.Common.Entity;

namespace BOG.ClipboardMunger.Common.Interface
{
	public interface IClipboardMungerProvider
	{
		string MethodName { get; }
		string GroupName { get; }
		string Description { get; }

		List<string> GetExampleNames();
		Example GetExample(string exampleName);
		string Munge(string clipboardSource);
	}
}
