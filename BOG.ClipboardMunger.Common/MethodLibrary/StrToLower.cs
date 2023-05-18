
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class StrToLower : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "To lower Case"; }
		public override string GroupName { get => "String-like"; }
		public override string Description { get; }

		public StrToLower()
		{

		}

		public override string Munge(string clipboardSource)
		{
			return clipboardSource.ToLower();
		}
	}
}
