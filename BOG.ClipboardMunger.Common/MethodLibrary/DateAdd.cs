using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Windows.Forms;
using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;
using BOG.Framework;
using BOG.Framework.Extensions;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
	public class DateAdd : ClipboardMungerProviderBase, IClipboardMungerProvider
	{
		public override string MethodName { get => "Date/Time Offset"; }
		public override string GroupName { get => "Time"; }
		public override string Description { get => "Offset a date/time"; }

		public DateAdd()
		{
			base.SetArgument(new Argument
			{
				Name = "DateTimeBase",
				Title = "The date/time to offest",
				DefaultValue = "{now}",
				Description = "Leave as {now} for the current date time",
				ValidatorRegex = ".+"
			});
			base.SetArgument(new Argument
			{
				Name = "OffsetBy",
				Title = "The measurement for the offset",
				DefaultValue = "D",
				Description = "N = Minutes, H = Hours, D = Days, M = Months, Y = Years",
				ValidatorRegex = "N|H|D|M|Y|n|h|d|m|y"
			});
			base.SetArgument(new Argument
			{
				Name = "OffsetAmount",
				Title = "The numeric value for the offset",
				DefaultValue = "1",
				Description = "Use negative numbers fof the past",
				ValidatorRegex = @"\d+"
			});
			base.SetArgument(new Argument
			{
				Name = "OutputFormat",
				Title = "format in .ToString({format}) for the output.",
				DefaultValue = "s",
				Description = "Leave unchange for ISO-8166 (YYYY-MM-DDTHH:NN:SS)",
				HelpUrl = "https://blog.stevex.net/string-formatting-in-csharp/",
				ValidatorRegex = ".+"
			});
		}

		public override string Munge(string clipboardSource)
		{
			DateTime dateTimeBase;

			var offsetBy = ArgumentValues["OffsetBy"];
			var offsetAmount = int.Parse(ArgumentValues["OffsetAmount"]);
			var outputFormat = ArgumentValues["OutputFormat"];
			try
			{
				dateTimeBase = DateTime.Parse(ArgumentValues["DateTimeBase"]);
			}
			catch
			{
				dateTimeBase = DateTime.Now;
				//MessageBox.Show("Using current date/time", "Date/Time Parsing error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			DateTime result;
			switch (offsetBy)
			{
				case "N":
					result = dateTimeBase.AddMinutes(offsetAmount);
					break;
				case "H":
					result = dateTimeBase.AddHours(offsetAmount);
					break;
				case "D":
					result = dateTimeBase.AddDays(offsetAmount);
					break;
				case "M":
					result = dateTimeBase.AddMonths(offsetAmount);
					break;
				case "Y":
					result = dateTimeBase.AddYears(offsetAmount);
					break;
				default:
					throw new ArgumentException($"Unknown offset by: {offsetBy}");
			}
			try
			{
				return result.ToString(outputFormat);
			}
			catch (Exception err) 
			{
				throw new FormatException($"The value for argument OutputFormat specified is not valid for a date/time value: {outputFormat}", err);
			}
		}
	}
}
