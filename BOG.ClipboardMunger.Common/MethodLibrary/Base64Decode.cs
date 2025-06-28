using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;
using System.Collections.Generic;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class Base64Decode : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "Base64Decode"; }
		public override string GroupName { get => "Encoding"; }
		public override string Description { get; }

		public Base64Decode() 
        {
			base.Examples.Add("Example 1",
				new Example
				{
					Name = "Simple",
					Input = "aHR0cDovL3d3dy5teXNlcnZlci5jb20/QXJndW1lbnQ9NDQmTGlzdD0yDQoNCk5ldyBMaW5lK0Fub3RoZXIgTmV3IExpbmU="
				}
			);
		}

		public override string Munge(string textToMunge)
		{
			byte[] encodedDataAsBytes = System.Convert.FromBase64String(textToMunge);
			return System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
		}
	}
}
