using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;
using System;
using System.Collections.Generic;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class Base64Encode : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "Base64Encode"; }
		public override string GroupName { get => "Encoding"; }
		public override string Description { get; }

		public Base64Encode() 
        {
			base.SetArgument(new Argument
			{
				Name = "LineBreaks",
				Title = "Use Line Breaks?",
				DefaultValue = "false",
				Description = "Leave as false for single line output",
				ValidatorRegex = @"true|TRUE|false|FALSE"
			});

			base.Examples.Add("Single Line", new Example
			{
				Input = "The quick brown fox jumped over the lazy dogs back ... into the road where a car was coming... and ... squish!",
				ArgumentValues = new Dictionary<string, string> { { "LineBreaks", "false" } },
				Name = "One Continuous Line"
			});

			base.Examples.Add("Multiple Lines", new Example
			{
				Input = "The quick brown fox jumped over the lazy dogs back ... into the road where a car was coming... and ... squish!",
				ArgumentValues = new Dictionary<string, string> { { "LineBreaks", "true" } },
				Name = "With Line Breaks"
			});
		}

		public override string Munge(string textToMunge)
		{
			var useLineBreaks = bool.Parse(this.ArgumentValues["LineBreaks"]);
			byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(textToMunge);
			return System.Convert.ToBase64String(
				toEncodeAsBytes,
				useLineBreaks ? Base64FormattingOptions.InsertLineBreaks : Base64FormattingOptions.None);
		}
	}
}
