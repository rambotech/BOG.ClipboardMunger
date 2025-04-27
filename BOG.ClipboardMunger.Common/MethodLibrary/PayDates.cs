using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
    public class PayDates : ClipboardMungerProviderBase, IClipboardMungerProvider
    {
        public override string MethodName { get => "Pay Dates"; }
        public override string GroupName { get => "Time"; }
        public override string Description { get => "Offset a date/time with Format value"; }

        private StringBuilder Result = new StringBuilder();


        public PayDates()
        {
            base.SetArgument(new Argument
            {
                Name = "Year",
                Title = "The year which to generate",
                DefaultValue = DateTime.Now.ToString("YYYY"),
                Description = "",
                ValidatorRegex = @"[\d]{4}"
            });
            base.SetArgument(new Argument
            {
                Name = "PayFrequency",
                Title = "The interval between paydates",
                DefaultValue = "D",
                Description = "W=Weekly,B=BiWeekly,S=SemiMonthly(15th & EOM),M=Monthly",
                ValidatorRegex = "W|B|S|M|w|b|s|m"
            });
            base.SetArgument(new Argument
            {
                Name = "OutputFormat",
                Title = "format in .ToString({format}) for the output.",
                DefaultValue = "MM/dd/yyyy",
                Description = "Leave unchanged for MM/dd/yyyy",
                HelpUrl = "https://blog.stevex.net/string-formatting-in-csharp/",
                ValidatorRegex = ".+"
            });

            #region Examples
            base.Examples.Add("Weekly 2025", new Example
            {
                Name = "Weekly 2025",
                ArgumentValues = new Dictionary<string, string>
                {
                    {"Year", "2025" },
                    {"PayFrequency", "W" },
                    {"OutputFormat", "MM/dd/yyyy" }
                }
            });
            base.Examples.Add("BiWeekly 2025", new Example
            {
                Name = "BiWeekly 2025",
                ArgumentValues = new Dictionary<string, string>
                {
                    {"Year", "2025" },
                    {"PayFrequency", "B" },
                    {"OutputFormat", "MM/dd/yyyy" }
                }
            });
            base.Examples.Add("SemiMonthly 2025", new Example
            {
                Name = "SemiMonthly 2025",
                ArgumentValues = new Dictionary<string, string>
                {
                    {"Year", "2025" },
                    {"PayFrequency", "S" },
                    {"OutputFormat", "MM/dd/yyyy" }
                }
            });
            base.Examples.Add("Monthly 2025", new Example
            {
                Name = "Monthly 2025",
                ArgumentValues = new Dictionary<string, string>
                {
                    {"Year", "2025" },
                    {"PayFrequency", "M" },
                    {"OutputFormat", "MM/dd/yyyy" }
                }
            });
            #endregion
        }

        public override string Munge(string textToMunge)
        {
            var year = int.Parse(ArgumentValues["Year"]);
            var payFrequency = ArgumentValues["PayFrequency"].ToUpper();
            var outputFormat = "{0:" + ArgumentValues["OutputFormat"] + "}";

            if (payFrequency == "W") CalcWeekly(year, outputFormat);
            else if (payFrequency == "B") CalcBiWeekly(year, outputFormat);
            else if (payFrequency == "S") CalcSemiMonthly(year, outputFormat);
            else if (payFrequency == "M") CalcMonthly(year, outputFormat);
            return Result.ToString();
        }

        private void CalcWeekly(int year, string outputFormat)
        {
            var thisDate = DateTime.Parse(string.Format("01/01/{0}", year));
            var fridayOffset = (7 + (int)DayOfWeek.Friday - (int)thisDate.DayOfWeek) % 7;
            thisDate = thisDate.AddDays(fridayOffset);
            while (thisDate.Year == year)
            {
                var workDate = thisDate;
                if (workDate.Year == year)
                {
                    switch (thisDate.DayOfWeek)
                    {
                        case DayOfWeek.Saturday:
                            workDate.AddDays(-1);
                            break;
                        case DayOfWeek.Sunday:
                            workDate.AddDays(-2);
                            break;
                    }
                    Result.AppendLine(string.Format(outputFormat, workDate));
                    thisDate = thisDate.AddDays(7);
                }
            }
        }
        private void CalcBiWeekly(int year, string outputFormat)
        {
            var thisDate = DateTime.Parse(string.Format("01/01/{0}", year));
            var fridayOffset = (7 + (int)DayOfWeek.Friday - (int)thisDate.DayOfWeek) % 7;
            thisDate = thisDate.AddDays(fridayOffset);

            while (thisDate.Year == year)
            {
                var workDate = thisDate;
                if (workDate.Year == year)
                {
                    switch (thisDate.DayOfWeek)
                    {
                        case DayOfWeek.Saturday:
                            workDate.AddDays(-1);
                            break;
                        case DayOfWeek.Sunday:
                            workDate.AddDays(-2);
                            break;
                    }
                    Result.AppendLine(string.Format(outputFormat, workDate));
                    thisDate = thisDate.AddDays(14);
                }
            }
        }
        private void CalcSemiMonthly(int year, string outputFormat)
        {
            var thisDate = DateTime.Parse(string.Format("01/01/{0}", year));
            var fridayOffset = (7 + (int)DayOfWeek.Friday - (int)thisDate.DayOfWeek) % 7;
            thisDate = thisDate.AddDays(fridayOffset);

            while (thisDate.Year == year)
            {
                var workDate = thisDate;
                if (workDate.Year == year)
                {
                    switch (thisDate.DayOfWeek)
                    {
                        case DayOfWeek.Saturday:
                            workDate.AddDays(-1);
                            break;
                        case DayOfWeek.Sunday:
                            workDate.AddDays(-2);
                            break;
                    }
                    Result.AppendLine(string.Format(outputFormat, workDate));
                    if (thisDate.Day == 1)
                    {
                        thisDate = thisDate.AddDays(14);
                    }
                    else
                    {
                        thisDate = thisDate.AddDays(-14).AddMonths(1);
                    }
                }
            }
        }

        private void CalcMonthly(int year, string outputFormat)
        {
            var thisDate = DateTime.Parse(string.Format("01/01/{0}", year));
            var fridayOffset = (7 + (int)DayOfWeek.Friday - (int)thisDate.DayOfWeek) % 7;
            thisDate = thisDate.AddDays(fridayOffset);

            while (thisDate.Year == year)
            {
                var workDate = thisDate;
                if (workDate.Year == year)
                {
                    switch (thisDate.DayOfWeek)
                    {
                        case DayOfWeek.Saturday:
                            workDate.AddDays(-1);
                            break;
                        case DayOfWeek.Sunday:
                            workDate.AddDays(-2);
                            break;
                    }
                    Result.AppendLine(string.Format(outputFormat, workDate));
                    thisDate = thisDate.AddMonths(1).AddDays(-1);
                }
            }
        }
    }
}
