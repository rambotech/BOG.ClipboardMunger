using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Interface;
using System.Text;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class XmlReadable : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "XML readable"; }
		public override string GroupName { get => "Rectify"; }
		public override string Description { get; }

		public XmlReadable()
		{
		}

		public override string Munge(string textToMunge)
		{
			StringBuilder s = new StringBuilder();
			bool InNode = false;
			bool InText = true;
			bool IsEndNode = false;

			if (textToMunge.Length > 0)
			{
				for (int Index = 0; Index < textToMunge.Length; Index++)
				{
					switch (textToMunge[Index])
					{
						case ' ':
							break;
						case '<':
							if (InText == false || IsEndNode == true)
							{
								s.AppendLine();
							}
							InNode = true;
							IsEndNode = false;
							InText = false;
							break;
						case '/':
							if (InText == false && InNode == true)
							{
								IsEndNode = true;
							}
							break;
						case '>':
							InNode = false;
							InText = false;
							break;
						default:
							InText = !InNode;
							break;
					}
					s.Append(textToMunge[Index]);
				}
			}
			return s.ToString();
		}
	}
}
