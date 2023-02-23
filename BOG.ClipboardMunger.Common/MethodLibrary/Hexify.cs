using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Interface;
using System.Text;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class Hexify : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "Hexify"; }
		public override string GroupName { get => "Investigation"; }
		public override string Description { get => "Change string to hex values, e.g. \"Hi\" to \"4669\""; }

		public Hexify()
		{

		}

		public override string Munge(string clipboardSource)
		{
			StringBuilder s = new StringBuilder();
			int Index = 0;
			while (Index < clipboardSource.Length)
			{
				s.Append(string.Format("{0:x2}", (int)clipboardSource[Index++]));
			}
			return s.ToString();
		}
	}
}
