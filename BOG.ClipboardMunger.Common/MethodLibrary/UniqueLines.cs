using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	internal class UniqueLines : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "Unique Lines"; }
		public override string GroupName { get => "String-like"; }
		public override string Description { get; }

		public UniqueLines()
		{
			base.SetArgument(new Argument
			{
				Name = "ShowCounts",
				Title = "Show counts",
				DefaultValue = "false",
				Description = "Whether value and total counts should be appended at the bottom",
				ValidatorRegex = @"true|TRUE|false|FALSE"
			});
		}

		public override string Munge()
		{
			var showCounts = bool.Parse(ArgumentValues["ShowCounts"]);

			StringBuilder output = new StringBuilder();
			Dictionary<string, int> Unique = new Dictionary<string, int>();
			var total = 0;
			foreach (string s in base.ClipboardSource.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
			{
				var key = s.ToLower();
				if (!Unique.ContainsKey(key))
				{
					Unique.Add(key, 0);
				}
				Unique[key]++;
			}
			foreach (var s in Unique.Keys)
			{
				if (showCounts)
				{
					total += Unique[s];
					output.AppendLine(string.Format("{0,8:#,0}: {1}", Unique[s], s));
				}
				else
				{
					output.AppendLine(string.Format("{0}", s));
				}
			}
			if (showCounts)
			{
				output.AppendLine(string.Format("Total: {0}", total));
			}
			return output.ToString();
		}
	}
}
