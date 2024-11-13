using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Interface;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class HtmlDecode : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "Html Decode"; }
		public override string GroupName { get => "Encoding"; }
		public override string Description { get; }

		public HtmlDecode() 
        {

		}

		public override string Munge(string textToMunge)
		{
			return System.Web.HttpUtility.HtmlDecode(textToMunge);
		}
	}
}
