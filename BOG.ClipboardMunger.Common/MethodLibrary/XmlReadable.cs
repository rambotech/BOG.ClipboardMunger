﻿using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Interface;
using BOG.ClipboardMunger.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

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

		public override string Munge()
		{
			StringBuilder s = new StringBuilder();
			bool InNode = false;
			bool InText = true;
			bool IsEndNode = false;

			if (base.ClipboardSource.Length > 0)
			{
				for (int Index = 0; Index < base.ClipboardSource.Length; Index++)
				{
					switch (base.ClipboardSource[Index])
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
					s.Append(base.ClipboardSource[Index]);
				}
			}
			return s.ToString();
		}
	}
}
