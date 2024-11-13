using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

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
		public override string GroupName { get => "Wizardry"; }
		public override string Description { get; }

		public ClassFillOut() : base()
        {
			base.Examples.Add("Simple", new Example
			{
				Input = BOG.Framework.Extensions.StringEx.Base64DecodeString(
@"W0lTZXJpYWxpemFibGVdDQpjbGFzcyBNeUNsYXNzIDogSUNsb25hYmxlDQp7DQoJW0NhdGVnb3J5
QXR0cmlidXRlKCJBZG1pbiIpLCBEaXNwbGF5TmFtZUF0dHJpYnV0ZSgiU2VxdWVuY2UiKSwgRGVz
Y3JpcHRpb25BdHRyaWJ1dGUoIkNvbnRyb2xzIHRoZSBzZXF1ZW5jZSBvZiBwbGFjZW1lbnQuIiks
IFJlYWRPbmx5KGZhbHNlKV0NCglpbnQgU2VxdWVuY2UgPSAyNzsNCgkNCglbQ2F0ZWdvcnlBdHRy
aWJ1dGUoIkFkbWluIiksIERpc3BsYXlOYW1lQXR0cmlidXRlKCJEaXNwbGF5IEl0ZW1zIiksIERl
c2NyaXB0aW9uQXR0cmlidXRlKCJUaGUgY29sbGVjdGlvbiBvZiBpdGVtcyB0byBkaXNwbGF5LiIp
LCBSZWFkT25seShmYWxzZSldDQoJW1htbEF0dHJpYnV0ZSAoInNlY29uZCIpXQ0KCXN0cmluZ1td
IERpc3BsYXlJdGVtcyA9IG5ldyBzdHJpbmdbXSB7IlRlc3QiLCAiU3RhZ2UiLCAiUHJvZHVjdGlv
biJ9Ow0KCQ0KCVtDYXRlZ29yeUF0dHJpYnV0ZSgiQWRtaW4iKSwgRGlzcGxheU5hbWVBdHRyaWJ1
dGUoIkNvbm5lY3Rpb24gU3RyaW5nIiksIERlc2NyaXB0aW9uQXR0cmlidXRlKCJUaGUgbG9jYXRp
b24gb2YgU1FMLCBBY2Nlc3MsIE15U1FMIG9yIFBvc3RHcmUgU1FMLiIpLCBSZWFkT25seShmYWxz
ZSldDQoJc3RyaW5nIENvbm5lY3Rpb25TdHJpbmcgPSAiU2VydmVyPWxvY2FsaG9zdDtJbnRlZ3Jh
dGVkIFNlY3VyaXR5PVRydWU7SW5pdGlhbCBDYXRhbG9nPW1hc3RlciI7DQoJDQoJT3RoZXJDbGFz
cy5TcGVjaWFsIHg7DQp9"),
				Name = "Simple Class"
			}); ;
			base.Examples.Add("Complex", new Example
			{
				Input = BOG.Framework.Extensions.StringEx.Base64DecodeString(
@"W0lTZXJpYWxpemFibGVdDQpbRGVmYXVsdFByb3BlcnR5QXR0cmlidXRlKCJEZXNjcmlwdGlvbiIp
XQ0KDQpjbGFzcyBNeUNsYXNzIDogSUNsb25lYWJsZSwgSVNlcmlhbGl6YWJsZQ0Kew0KICAgICAg
ICAvLy8gPHN1bW1hcnk+DQogICAgICAgIC8vLyBQdWJsaWMgZG9jdW1lbnRhdGlvbiBmb3IgZW51
bSBXYXRjaGVyRm9ybS4NCiAgICAgICAgLy8vIDxzdW1tYXJ5Pg0KCXB1YmxpYyBlbnVtIFdhdGNo
ZXJGb3JtIDogYnl0ZSB7IG1pbGQsIG1lZGl1bSwgcGFyYW5vaWQgfQ0KDQogICAgICAgIC8vLyA8
c3VtbWFyeT4NCiAgICAgICAgLy8vIFB1YmxpYyBkb2N1bWVudGF0aW9uIGZvciBTZXF1ZW5jZS4N
CiAgICAgICAgLy8vIDxzdW1tYXJ5Pg0KCVtDYXRlZ29yeUF0dHJpYnV0ZSgiQWRtaW4iKSwgRGlz
cGxheU5hbWVBdHRyaWJ1dGUoIlNlcXVlbmNlIiksIERlc2NyaXB0aW9uQXR0cmlidXRlKCJDb250
cm9scyB0aGUgc2VxdWVuY2Ugb2YgcGxhY2VtZW50LiIpLCBSZWFkT25seShmYWxzZSldDQoJaW50
IFNlcXVlbmNlID0gMjc7DQoJDQoJLy8gQSBjb21tZW50IHdoaWNoIHNob3VsZCBiZSB0YWJiZWQg
aW4gd2l0aCB0aGUgb3V0cHV0Lg0KICAgICAgICBbQ2F0ZWdvcnlBdHRyaWJ1dGUoIkFkbWluIiks
IERpc3BsYXlOYW1lQXR0cmlidXRlKCJEaXNwbGF5IEl0ZW1zIiksIERlc2NyaXB0aW9uQXR0cmli
dXRlKCJUaGUgY29sbGVjdGlvbiBvZiBpdGVtcyB0byBkaXNwbGF5LiIpLCBSZWFkT25seShmYWxz
ZSldDQoJW1htbEF0dHJpYnV0ZSAoInNlY29uZCIpXQ0KCXN0cmluZ1tdIERpc3BsYXlJdGVtcyA9
IG5ldyBzdHJpbmdbXSB7ICJUZXN0IiwgIlN0YWdlIiwgIlByb2R1Y3Rpb24iIH07DQoJDQoJW0Nh
dGVnb3J5QXR0cmlidXRlKCJBZG1pbiIpLCBEaXNwbGF5TmFtZUF0dHJpYnV0ZSgiQ29ubmVjdGlv
biBTdHJpbmciKSwgRGVzY3JpcHRpb25BdHRyaWJ1dGUoIlRoZSBsb2NhdGlvbiBvZiBTUUwsIEFj
Y2VzcywgTXlTUUwgb3IgUG9zdEdyZSBTUUwuIiksIFJlYWRPbmx5KGZhbHNlKV0NCglzdHJpbmcg
Q29ubmVjdGlvblN0cmluZyA9ICJTZXJ2ZXI9bG9jYWxob3N0O0ludGVncmF0ZWQgU2VjdXJpdHk9
VHJ1ZTtJbml0aWFsIENhdGFsb2c9bWFzdGVyIjsNCgkNCglEaWN0aW9uYXJ5PGludCwgRGljdGlv
bmFyeTxpbnQsIHN0cmluZz4+IFNwZWNpYWwxOw0KCURpY3Rpb25hcnk8aW50LCBEaWN0aW9uYXJ5
PGludCwgc3RyaW5nPj4gU3BlY2lhbDIgPSBuZXcgRGljdGlvbmFyeTxpbnQsIERpY3Rpb25hcnk8
aW50LCBzdHJpbmc+PigpOw0KDQoJT3RoZXJDbGFzcy5TcGVjaWFsIFg7DQoNCgkvLyBbW05PIENM
T05FXV0NCgkvLyBbW05PIFNFUklBTElaRV1dDQoJT3RoZXJDbGFzcy5TcGVjaWFsIFk7DQoJDQog
ICAgICAgIFdhdGNoZXJGb3JtIHcgPSBXYXRjaGVyRm9ybS5wYXJhbm9pZDsNCn0NCg0KcHVibGlj
IHZvaWQgVmFsaWRhdGUoKQ0Kew0KICAgLy8gVGhpcyBpcyBhIG1ldGhvZCBJIGRvIHdhbnQgYWRk
ZWQgYWZ0ZXIgdGhlIGF1dG8gZ2VuZXJhdGVkIGNvZGUuDQp9DQoNCnByaXZhdGUgc3RyaW5nIFZh
bGlkYXRlQWxsKCkNCnsNCiAgIC8vIFRoaXMgaXMgYW5vdGhlciBtZXRob2QgSSBkbyB3YW50IGFk
ZGVkIGFmdGVyIHRoZSBhdXRvIGdlbmVyYXRlZCBjb2RlLg0KICAgcmV0dXJuICJUZXN0IjsNCn0=
"),
				Name = "Complex Class"
			});
		}

		public override string Munge(string textToMunge)
		{
			StringBuilder output = new StringBuilder();
			Dictionary<int, string[]> Property = new Dictionary<int, string[]>();
			StringDictionary Attribute = new StringDictionary();
			List<string> AttributeHolder = new List<string>();
			bool InDefinitions = false;
			string ClassName = string.Empty;
			string ClassImplements = string.Empty;

			foreach (string l in textToMunge.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
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
