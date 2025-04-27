using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
    public class DateSequencePattern : ClipboardMungerProviderBase, IClipboardMungerProvider
    {
        public override string MethodName { get => "Date Sequence Pattern"; }
        public override string GroupName { get => "Time"; }
        public override string Description { get => "Make a list of dates per rules"; }

        private StringBuilder Result = new StringBuilder();

        public DateSequencePattern()
        {
            //base.SetArgument(new Argument
            //{
            //    Name = "StartDate",
            //    Title = "Starting Date",
            //    DefaultValue = DateTime.Now.ToString("MM/dd/yyyy"),
            //    Description = "",
            //    ValidatorRegex = @"[\d]{2}-[\d]{2}-[\d]{4}"
            //});

            #region Examples
#if FALSE
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
#endif
#endregion
        }

        public override string Munge(string textToMunge)
        {
            var startDate = DateTime.Parse("01/01/2025");
            var endDate = DateTime.Parse("12/01/2025");

            while (startDate <= endDate)
            {
                Result.AppendLine(string.Format("{0:yyyy-MM-dd}", startDate.AddMonths(1).AddDays(-1)));
                startDate = startDate.AddMonths(1);
            }
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
