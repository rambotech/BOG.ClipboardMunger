﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class ReformattedLine : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "Reformatted Line"; }
		public override string GroupName { get => "String-like"; }
		public override string Description { get => "takes each line as {0} input for string.Format method call."; }

		public ReformattedLine()
		{
			base.SetArgument(new Argument
			{
				Name = "FormatString",
				Title = "Enter the format string for string.Format()",
				DefaultValue = "{0}",
				Description = "E.g.  \"Col1\": \"{0}\", \"MapCol1\": \"target.{0}\" \"",
				ValidatorRegex = @".+"
			});
			base.Examples.Add("Data Mapping", new Example
			{
				Input = "Bob\r\nTed\r\nAlice\r\nKaren",
				ArgumentValues = new Dictionary<string, string> { { "FormatString", "this.{0} = that.{0}" } },
				Name = "Data Mapping Use"
			});
		}

		public override string Munge(string clipboardSource)
		{
			var FormattedAs= HttpUtility.UrlDecode(this.ArgumentValues["FormatString"]);

			StringBuilder output = new StringBuilder();
			foreach (string s in clipboardSource.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
			{
				output.AppendLine(string.Format(FormattedAs, s));
			}
			return output.ToString();
		}
	}
}
