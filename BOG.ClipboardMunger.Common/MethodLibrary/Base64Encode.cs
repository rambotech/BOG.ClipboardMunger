using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class Base64Encode : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "Base64Encode"; }
		public override string GroupName { get => "Encoding"; }
		public override string Description { get; }

		public Base64Encode()
		{

		}

		public override string Munge(string clipboardSource)
		{
			byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(clipboardSource);
			return System.Convert.ToBase64String(toEncodeAsBytes);
		}
	}
}
