using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Interface;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class LinuxSlashes : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "Use Linux forward slash"; }
		public override string GroupName { get => "Cross-Platform"; }
		public override string Description { get; }

		public LinuxSlashes() : base()
        {

		}

		public override string Munge(string textToMunge)
		{
			return textToMunge.Replace(@"\", @"/");
		}
	}
}

