using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class UrlDecode : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "Url Decode"; }
		public override string GroupName { get => "Encoding"; }
		public override string Description { get; }

		public UrlDecode()
		{

		}

		public override string Munge()
		{
			return System.Web.HttpUtility.UrlDecode(base.ClipboardSource);
		}
	}
}
