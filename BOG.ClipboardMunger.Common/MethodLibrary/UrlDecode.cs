using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class UrlEncode : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "Url Encode"; }
		public override string GroupName { get => "Encoding"; }
		public override string Description { get; }

		public UrlEncode() : base()
        {

		}

		public override string Munge(string textToMunge)
		{
			return System.Web.HttpUtility.UrlEncode(textToMunge);
		}
	}
}
