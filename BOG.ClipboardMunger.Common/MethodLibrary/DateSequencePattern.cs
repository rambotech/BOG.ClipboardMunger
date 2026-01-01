using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
    public class DateSequencePattern : ClipboardMungerProvider, IClipboardMungerProvider
    {
        public override string MethodName { get => "Date Sequence Pattern"; }
        public override string GroupName { get => "Time"; }
        public override string Description { get => "Make a list of dates per pattern (see examples)"; }

        private StringBuilder Result = new StringBuilder();

        public DateSequencePattern()
        {
            // Pattern is...
            //  StartDate;Step;Milestones;Rectify;EndDate;date output format
            //      Step is #D = # of day, #M = # of month, #W = # of weeks, #D = # of days
            //      Milestones: comma delimited list of day of week values, or day of month values.  Empty for everything
            //          Day or Week milestone: 0 = Sun, ..., 6 = Sat.  0,6 = Sunday and Saturday.
            //          Month milestone: 

            base.SetArgument(new Argument
            {
                Name = "Pattern",
                Title = "Pattern of Date repitiion",
                DefaultValue = "MM/DD/YYYY;M;15,EOM;SAT,SUN;MM/DD/YYYY;yyyy-MM-dd",
                Description = "The first date to generate",
                ValidatorRegex = @"[\d]{2}/[\d]{2}/[\d]{4}"
            });

            base.SetArgument(new Argument
            {
                Name = "Pattern",
                Title = "Pattern of Date repitiion",
                DefaultValue = "MM/DD/YYYY;M;15,EOM;SAT,SUN;MM/DD/YYYY;yyyy-MM-dd",
                Description = "The first date to generate",
                ValidatorRegex = @"[\d]{2}/[\d]{2}/[\d]{4}"
            });

            #region Examples
            base.Examples.Add("Weekly, all days, March 1, 2025 to December 31, 2025", new Example
            {
                Name = "Weekly All Days, 2025",
                ArgumentValues = new Dictionary<string, string>
                {
                    {"Pattern", "3/1/2025;W;*;12/31/2025" }
                }
            });
            base.Examples.Add("Weekly, some days, March 1, 2025 to December 31, 2025", new Example
            {
                Name = "Weekly Some Days (Tue Thu Sat), 2025",
                ArgumentValues = new Dictionary<string, string>
                {
                    {"Pattern", "3/1/2025;W;Tue,Thu,Sat;12/31/2025" }
                }
            });
            base.Examples.Add("Bi-Weekly, some days, March 1, 2025 to December 31, 2025", new Example
            {
                Name = "Weekly Some Days (Fri), 2025",
                ArgumentValues = new Dictionary<string, string>
                {
                    {"Pattern", "3/1/2025;W;Fri;12/31/2025" }
                }
            });
            base.Examples.Add("Monthly, specific days, March 1, 2025 to December 31, 2025", new Example
            {
                Name = "Monthly Specific Days (15,EOM), 2025",
                ArgumentValues = new Dictionary<string, string>
                {
                    {"Pattern", "3/1/2025;M;15,EOM;12/31/2025" }
                }
            });
            #endregion
        }

        public override string Munge(string textToMunge)
        {
            var pattern = base.GetArgumentValue("Pattern");

            var arguments = pattern.Split(new char[] { ';'}, StringSplitOptions.None);
            if (arguments.Length != 6)
            {
                throw new Exception($"Pattern requires 6 arguments, but has {arguments.Length}");
            }
            var startDate = DateTime.Parse(arguments[0]);
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
