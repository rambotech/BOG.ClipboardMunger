using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Interface;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class StrToUpper : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "To UPPER Case"; }
		public override string GroupName { get => "String-like"; }
		public override string Description { get; }

		public StrToUpper() 
        {

		}

		public override string Munge(string textToMunge)
		{
			return textToMunge.ToUpper();
		}
	}
}
