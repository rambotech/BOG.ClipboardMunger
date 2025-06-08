using System;
using System.Text;
using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
    public class VEventFilter : ClipboardMungerProviderBase, IClipboardMungerProvider
    {
        public override string MethodName { get => "ICS Filter Events"; }
        public override string GroupName { get => "Encoding"; }
        public override string Description { get => "Filter events to keep by key phrase"; }

        public VEventFilter()
        {
            base.SetArgument(new Argument
            {
                Name = "Key Phrase",
                Title = "Enter a value which appears in all the events",
                DefaultValue = string.Empty,
                Description = "Each BEGIN:VEVENT ... END:VEVENT block which matches survives",
                ValidatorRegex = @".+"
            });
            base.SetArgument(new Argument
            {
                Name = "Case Insensitive",
                Title = "Case insensitive?",
                DefaultValue = "true",
                Description = "Enter true to ignore case when comparing",
                ValidatorRegex = @"TRUE|FALSE|true|false"
            });
        }

        public override string Munge(string textToMunge)
        {
            var keyPhrase = this.ArgumentValues["Key Phrase"];
            var caseIgnored = bool.Parse(this.ArgumentValues["Case Insensitive"]);
            var survivors = new StringBuilder();
            var thisEvent = new StringBuilder();

            var inBody = false;
            var inEvent = false;
            var capture = false;
            foreach (var line in textToMunge.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None))
            {
                if (!inBody && line == "BEGIN:VCALENDAR") inBody = true;
                if (!inBody) continue;
                inEvent |= line == "BEGIN:VEVENT";
                if (inEvent)
                {
                    capture |= -1 < line.IndexOf(
                        keyPhrase,
                        caseIgnored ?
                            StringComparison.CurrentCultureIgnoreCase :
                            StringComparison.CurrentCultureIgnoreCase);
                    thisEvent.AppendLine(line);
                }
                else
                {
                    survivors.AppendLine(line);
                }
                if (line == "END:VEVENT" || (line == "END:VCALENDAR" && capture))
                {
                    if (capture)
                    {
                        survivors.Append(thisEvent.ToString());
                        inEvent = false;
                        capture = false;
                    }
                    thisEvent.Clear();
                }
            }
            return survivors.ToString();
        }
    }
}
