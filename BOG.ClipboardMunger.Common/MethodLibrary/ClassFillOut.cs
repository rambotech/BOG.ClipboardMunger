using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	// Take a simple CSharp class sample, with only property values, and make a public
	// class form it with get/set methods for each property, where the get/set method
	// uses the property's original name, and stores in local variable named using a
	// prefix.
	//
	// E.g.
	// class MyClass
	// {
	//   int Sequence;
	//   string[] DisplayItems;
	//   OtherClass.Special x;
	// }
	public class ClassFillOut : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "CSharp Class Creation"; }
		public override string GroupName { get => "Wizards"; }
		public override string Description { get; }

		public ClassFillOut()
		{

		}

		public override string Munge(string clipboardSource)
		{
			StringBuilder output = new StringBuilder();
			Dictionary<int, string[]> Property = new Dictionary<int, string[]>();
			StringDictionary Attribute = new StringDictionary();
			List<string> AttributeHolder = new List<string>();
			bool InDefinitions = false;
			string ClassName = string.Empty;
			string ClassImplements = string.Empty;

			foreach (string l in clipboardSource.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
			{
				string s = l.Trim();
				while (s.Length > 0 && s[0] == '\t') s = s.Length == 1 ? string.Empty : s.Substring(1);
				while (s.Length > 0 && s[s.Length - 1] == '\t') s = s.Length == 1 ? string.Empty : s.Substring(0, s.Length - 2);
				s = l.Trim();

				if (!InDefinitions && l.IndexOf("class ") == 0)
				{
					output.AppendLine(string.Format("public {0}", s));
					ClassName = l.Substring(5).Trim();
					if (ClassName.IndexOf(':') >= 0)
					{
						if (ClassName.Length - 1 > ClassName.IndexOf(':'))
						{
							ClassImplements = ClassName.Substring(ClassName.IndexOf(':') + 1).Trim();
						}
						ClassName = ClassName.Substring(0, ClassName.IndexOf(':')).Trim();
					}
				}
				else if (l.Substring(0) == "{")
				{
					InDefinitions = true;
					output.AppendLine(s);
				}
				else if (l.Substring(0) == "}")
				{
					// Create the instantiation method
					output.AppendLine();
					output.AppendLine(string.Format("\tpublic {0} ()", ClassName));
					output.AppendLine("\t{");
					output.AppendLine("\t}");

					// Create an instantiation method with all the properties as parameters.
					output.AppendLine();
					output.Append(string.Format("\tpublic {0} (", ClassName));
					// Got all the definitions, so create the properties.
					string comma = string.Empty;
					for (int Index = 0; Index < Property.Keys.Count; Index++)
					{
						output.Append(string.Format("{0}{1} p_{2}", comma, Property[Index][0], Property[Index][1]));
						comma = ", ";
					}
					output.AppendLine(")");
					output.AppendLine("\t{");
					for (int Index = 0; Index < Property.Keys.Count; Index++)
					{
						output.AppendLine(string.Format("\t\tthis._{0} = p_{1};", Property[Index][1], Property[Index][1]));
					}
					output.AppendLine("\t}");

					// Create an instantiation method with an existing instance as the parameter.
					output.AppendLine();
					output.AppendLine(string.Format("\tpublic {0} ({0} p_obj)", ClassName));
					output.AppendLine("\t{");
					output.AppendLine(string.Format("\t\tLoad (p_obj);", ClassName));
					output.AppendLine("\t}");

					// Create an instantiation method with object[] as the parameter.
					output.AppendLine();
					output.AppendLine(string.Format("\tpublic {0} (object[] p_obj)", ClassName));
					output.AppendLine("\t{");
					int Index1 = 0;
					for (int Index = 0; Index < Property.Keys.Count; Index++)
					{
						output.AppendLine(string.Format("\t\tthis._{0} = ({1}) p_obj[{2}];", Property[Index][1], Property[Index][0], Index1++));
					}
					output.AppendLine("\t}");

					// Create a load method to populate from an existing instance as the parameter.
					output.AppendLine();
					output.AppendLine(string.Format("\tpublic void Load ({0} p_obj)", ClassName));
					output.AppendLine("\t{");
					for (int Index = 0; Index < Property.Keys.Count; Index++)
					{
						output.AppendLine(string.Format("\t\tthis._{0} = p_obj.{1};", Property[Index][1], Property[Index][1]));
					}
					output.AppendLine("\t}");

					// Got all the definitions, so create the properties.
					for (int Index = 0; Index < Property.Keys.Count; Index++)
					{
						output.AppendLine();

						if (Attribute.ContainsKey(Property[Index][1]))
						{
							foreach (string Line in Attribute[Property[Index][1]].Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
							{
								output.AppendLine(string.Format("\t{0}", Line));
							}
						}

						output.AppendLine(string.Format("\tpublic {0} {1}", Property[Index][0], Property[Index][1]));
						output.AppendLine("\t{");
						output.AppendLine(string.Format("\t\tget {{ return _{0}; }}", Property[Index][1]));
						output.AppendLine(string.Format("\t\tset {{ _{0} = value; }}", Property[Index][1]));
						output.AppendLine("\t}");
					}
					output.AppendLine("}");
					InDefinitions = false;
					break;
				}
				else if (InDefinitions && s.Length > 0 && s[s.Length - 1] == ';')
				{
					string[] a = s.Substring(0, s.Length - 1).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
					if (a.Length < 2)
					{
						output.AppendLine(l);
					}
					else
					{
						Property.Add(Property.Keys.Count, new string[] { a[0], a[1] });
						StringBuilder AttributeLines = new StringBuilder();
						foreach (string AttributeLine in AttributeHolder) AttributeLines.AppendLine(AttributeLine);
						Attribute.Add(a[1], AttributeLines.ToString());
						AttributeHolder.Clear();

						output.Append(string.Format("\tprivate {0} _{1}", a[0], a[1]));
						for (int i = 2; i < a.Length; i++)
						{
							output.Append(string.Format(" {0}", a[i]));
						}
						output.AppendLine(";");
					}
				}
				else if (InDefinitions && s.Length > 0 && s[0] == '[' && s[s.Length - 1] == ']')
				{
					AttributeHolder.Add(s);
				}
				else
				{
					output.AppendLine(s);
				}
			}
			return output.ToString();
		}
	}
}
