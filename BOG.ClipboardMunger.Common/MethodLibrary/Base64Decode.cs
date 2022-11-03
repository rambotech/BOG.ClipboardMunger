using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class Base64Decode : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "Base64Decode"; }
		public override string GroupName { get => "Encoding"; }
		public override string Description { get; }

		public Base64Decode()
		{

		}

		public override string Munge(string clipboardSource)
		{
			byte[] encodedDataAsBytes = System.Convert.FromBase64String(clipboardSource);
			return System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
		}
	}
}
