using BOG.ClipboardMunger.Common.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using BOG.ClipboardMunger.Common.Base;

namespace BOG.ClipboardMunger.Common.Helper
{
	public class MethodRetriever
	{
		public Dictionary<string, ClipboardMungerProviderBase> mungers = new Dictionary<string, ClipboardMungerProviderBase>();

		public MethodRetriever()
		{
			var dllFile = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "BOG.ClipboardMunger.Common.dll");
			var mungerAssembly = Assembly.LoadFile(dllFile);
			foreach (var t in mungerAssembly.GetTypes())
			{
				if (t.BaseType != null)
				{
					var interfaceType = t.BaseType.GetInterface("BOG.ClipboardMunger.Common.Interface.IClipboardMungerProvider");
					//var interfaceTypes = t.BaseType.GetInterfaces();

					if (interfaceType != null)
					{
						ClipboardMungerProviderBase o = (ClipboardMungerProviderBase)Activator.CreateInstance(t);
						if (o.GroupName.Contains(":") || o.MethodName.Contains(":"))
						{
							throw new Exception($"Group name ({o.GroupName}) or Method name ({o.MethodName}) can not contain a colon: {t.FullName}");
						}
						mungers.Add(o.GroupName + "::" + o.MethodName, o);
					}
				}
			}
			if (mungers.Keys.Count == 0)
			{
				throw new Exception("No clipboard mungers were found in the expected namespace: BOG.ClipboardMunger.Common; ensure the methods implement the interface IClipboardMungerProvider");
			}
		}

		public ClipboardMungerProviderBase GetProviderInstance(string groupName, string methodName)
		{
			return GetProviderInstance(groupName + "::" + methodName);
		}

		public ClipboardMungerProviderBase GetProviderInstance(string key)
		{
			if (!mungers.Keys.Contains(key))
			{
				throw new Exception($"The munger method index {key} was not found");
			}
			return mungers[key];
		}
	}
}
