using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class WindowsSlashes : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "Use Windows back slash"; }
		public override string GroupName { get => "Refactor"; }
		public override string Description { get; }

		public WindowsSlashes()
		{

		}

		public override string Munge(string clipboardSource)
		{
			return clipboardSource.Replace(@"/", @"\");
		}
	}
}

