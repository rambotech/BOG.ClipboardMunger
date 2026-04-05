using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;
using BOG.Framework.Extensions;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
    public class WeeklyEvents : ClipboardMungerProvider, IClipboardMungerProvider
    {
        public override string MethodName { get => "Weekly Events "; }
        public override string GroupName { get => "Time and Calendar"; }
        public override string Description { get => "Build weeks of events for a todo list from a template (see examples)"; }

        public WeeklyEvents()
        {
            base.SetArgument(new Argument
            {
                Name = "Starting Date",
                Title = "Day in first week of generation",
                DefaultValue = DateTime.Now.ToString("s"),
                Description = "",
                ValidatorRegex = ".+"
            });
            base.SetArgument(new Argument
            {
                Name = "Ending Date",
                Title = "Day in final week of generation",
                DefaultValue = DateTime.Now.ToString("s"),
                Description = "",
                ValidatorRegex = ".+"
            });

            #region Examples
            base.Examples.Add("First Quarter 2026", new Example
            {
                ArgumentValues = new Dictionary<string, string>
                {
                    {"Starting Date", "1/1/2026" },
                    {"Ending Date",  "3/31/2026"}
                },
                Input = TestTemplateText
            });
            base.Examples.Add("This week", new Example
            {
                ArgumentValues = new Dictionary<string, string>
                {
                    {"Starting Date", DateTime.Now.Date.ToString("d") },
                    { "Ending Date", DateTime.Now.Date.ToString("d") }
                },
                Input = TestTemplateText
            });
            #endregion
        }

        public override string Munge(string textToMunge)
        {
            var result = new StringBuilder();
            var ThisDate = DateTime.Parse(base.GetArgumentValue("Starting Date")).Date;
            var EndDate = DateTime.Parse(base.GetArgumentValue("Ending Date")).Date;
            if (ThisDate > EndDate) throw new ArgumentException("Starting Date must be before Ending Date");

            while (ThisDate.DayOfWeek != DayOfWeek.Monday) ThisDate = ThisDate.AddDays(-1);
            while (EndDate.DayOfWeek != DayOfWeek.Sunday) EndDate = EndDate.AddDays(1);

            var Output = textToMunge;

            while (ThisDate <= EndDate)
            {
                var changedDay = "{" + ThisDate.DayOfWeek.ToString().ToUpper().Substring(0, 3) + "_DATE}";
                Output = Output.Replace(changedDay, string.Format("{0:MMM dd, yyyy}", ThisDate));
                if (ThisDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    result.AppendLine(Output);
                    // reset template for the next week.
                    if (ThisDate != EndDate)
                    {
                        Output = textToMunge;
                    }
                }
                ThisDate = ThisDate.AddDays(1);
            }
            return result.ToString();
        }

        private static readonly string TestTemplateText = @"
                    PT09PT09PT09PT09PT09PT09PT09PT09PT09PT09PT09PT09PT09PT09PT09PT09DQoNCihNb24p
                    OiB7TU9OX0RBVEV9DQo5OjMwIEFNOiBnbyB0byB3b3JrDQo1OjMwIFBNOiBsZWF2ZSBmb3IgSG9t
                    ZQ0KNzowMCBQTTogR2F0aGVyaW5nDQoNCihUdWUpOiB7VFVFX0RBVEV9DQo5OjMwIEFNOiBnbyB0
                    byB3b3JrDQo1OjMwIFBNOiBsZWF2ZSBmb3IgSG9tZQ0KDQooV2VkKToge1dFRF9EQVRFfQ0KOToz
                    MCBBTTogZ28gdG8gd29yaw0KNTozMCBQTTogbGVhdmUgZm9yIEhvbWUNCg0KKFRodSk6IHtUSFVf
                    REFURX0NCjk6MzAgQU06IGdvIHRvIHdvcmsNCjEyOjAwIFBNOiBMdW5jaCB3aXRoIGZyaWVuZHMN
                    CjQ6MzAgUE06IGxlYXZlIGZvciBIb21lDQo1OjAwIFBNOiBMZWF2ZSBmb3IgR2FtZSBuaWdodA0K
                    DQooRnJpKToge0ZSSV9EQVRFfQ0KVEJEDQoNCihTYXQpOiB7U0FUX0RBVEV9DQpUQkQNCg0KKFN1
                    bik6IHtTVU5fREFURX0NCjEwOjE1QU06IEAgQ2h1cmNoIHRoaW5nDQoNCg==
                ".Base64DecodeString();
    }
}
