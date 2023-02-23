using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Interface;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class HtmlEncode : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "Html Encode"; }
		public override string GroupName { get => "Encoding"; }
		public override string Description { get; }

		public HtmlEncode()
		{

		}

		public override string Munge(string clipboardSource)
		{
			return System.Web.HttpUtility.HtmlDecode(clipboardSource);
		}
	}
}
