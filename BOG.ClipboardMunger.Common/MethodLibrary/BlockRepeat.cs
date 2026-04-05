using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
    public class BlockRepeat : ClipboardMungerProvider, IClipboardMungerProvider
    {
        public override string MethodName { get => "Iterate replacement of placeholder values"; }
        public override string GroupName { get => "Bulk Actions"; }
        public override string Description { get =>
            @"
            Clipboard content is single or multi-line text as a template, containing one or more occurences 
            of a unique placeholder. The Placeholder argument is the value of the place holder embedded in the 
            clipboard text. The ValueSet argument is a multi-line set of values for the palceholder, to replace 
            the placeholder with. The SplitDelimiter argument is the string sequence (URL encoded) used to split 
            the ValueSet into individual values, to iterate against the clipboard content. (eg. Windows line 
            break is %0D%0A).";
        }

        public BlockRepeat()
        {
            base.SetArgument(new Argument
            {
                Name = "Placeholder",
                Title = "Placeholder to replace",
                DefaultValue = ".+",
                Description = "Enter a value for the placeholder",
                ValidatorRegex = ".+"
            });
            base.SetArgument(new Argument
            {
                Name = "IgnoreCase",
                Title = "Ignore Case",
                DefaultValue = @"true",
                Description = @"true for case insensitive match, or false to honor case",
                ValidatorRegex = @"true|TRUE|false|FALSE"
            });
            base.SetArgument(new Argument
            {
                Name = "ValueSet",
                Title = "Values",
                DefaultValue = @"T1\r\nT4\r\nT5",
                Description = @"Enter the values to be used for each iteration of the block repeat",
                ValidatorRegex = @".+"
            });
            base.SetArgument(new Argument
            {
                Name = "SplitDelimiter",
                Title = "Split Delimiter",
                DefaultValue = @"%0D%0A",
                Description = @"Enter the string line break sequence (URL escaped, eg. Windows CR+LF \r\n as %0d%0a)",
                ValidatorRegex = @"(%[0-9a-fA-F]{2})+"
            });

            #region Examples
            base.Examples.Add("Simple Demo", new Example
            {
                Input =
                    "{THIS}.ConsoleDebug|Any CPU.ActiveCfg = Debug|Any CPU " + "\r\n" +
                    "{THIS}.ConsoleDebug|Any CPU.Build.0 = Debug|Any CPU " + "\r\n" +
                    "{THIS}.Debug|Any CPU.ActiveCfg = Debug|Any CPU " + "\r\n" +
                    "{THIS}.Debug|Any CPU.Build.0 = Debug|Any CPU " + "\r\n" +
                    "{THIS}.Release|Any CPU.ActiveCfg = Release|Any CPU " + "\r\n" +
                    "{THIS}.Release|Any CPU.Build.0 = Release|Any CPU " + "\r\n" +
                    "{THIS}.DEV|Any CPU.ActiveCfg = Debug|Any CPU " + "\r\n" +
                    "{THIS}.DEV|Any CPU.Build.0 = Debug|Any CPU " + "\r\n" +
                    "{THIS}.QA|Any CPU.ActiveCfg = Debug|Any CPU " + "\r\n" +
                    "{THIS}.QA|Any CPU.Build.0 = Debug|Any CPU " + "\r\n" +
                    "{THIS}.UAT|Any CPU.ActiveCfg = Release|Any CPU " + "\r\n" +
                    "{THIS}.UAT|Any CPU.Build.0 = Release|Any CPU " + "\r\n" +
                    "{THIS}.PRD|Any CPU.ActiveCfg = Release|Any CPU " + "\r\n" +
                    "{THIS}.PRD|Any CPU.Build.0 = Release|Any CPU " + "\r\n",
                ArgumentValues = new Dictionary<string, string>
                {
                    {"Placeholder", "{THIS}" },
                    {"IgnoreCase", "true" },
                    {"ValueSet",
                        "{a861a3fe-86d2-4931-8a47-8b8fd14f3464} " + "\r\n" +
                        "{3bd05941-aeeb-4ccc-bb23-9f0ca47da8b8} " + "\r\n" +
                        "{6cbd0d4f-d93e-4006-9ff7-507b82c600f9} " + "\r\n" +
                        "{81bcbf81-7879-4d68-b89b-8d09158ea8a0} " + "\r\n" +
                        "{cba7a560-5b19-4564-89d7-ebfdc3a68e15} " + "\r\n" +
                        "{077fca02-e1b3-48cb-9c3b-358b065dbfc4} " + "\r\n" +
                        "{3cdcdad0-2e3b-402b-b32e-f9be8567d410} " + "\r\n" +
                        "{edc88502-211f-448f-9ac0-3332d8e9b004} " + "\r\n" +
                        "{1d5112cf-3e00-4b9f-a44a-83dd01e640e6} " + "\r\n" +
                        "{41a97be0-27c1-490a-a407-b820add2db9e} " + "\r\n" +
                        "{7a62a123-aabc-4e26-a6d3-9e8f0adc70d6} " + "\r\n" +
                        "{17e1b926-ef79-45bf-85fe-64b51fb1e8a7} " + "\r\n" },
                    {"SplitDelimiter", "%0D%0A" }
                }
            });
            #endregion
        }

        public override string Munge(string textToMunge)
        {
            var placeholder = ArgumentValues["Placeholder"];
            var ignoreCase = bool.Parse(ArgumentValues["IgnoreCase"]);
            var valueSet = ArgumentValues["ValueSet"];
            var splitDelimiter = System.Web.HttpUtility.UrlDecode(ArgumentValues["SplitDelimiter"]);

            var result = new StringBuilder();

            foreach (var line in valueSet.Split(new string[] { splitDelimiter }, StringSplitOptions.RemoveEmptyEntries))
            {
                result.AppendLine(textToMunge.Replace(placeholder, line));
            }
            return result.ToString();
        }
    }
}
