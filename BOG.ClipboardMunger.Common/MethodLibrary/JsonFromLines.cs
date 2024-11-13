using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;
using BOG.Framework;
using BOG.Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
    public class JsonFromLines : ClipboardMungerProviderBase, IClipboardMungerProvider
    {
        public override string MethodName { get => "JSON from Lines"; }
        public override string GroupName { get => "Wizardry"; }
        public override string Description { get => "One property name per line, blank separator, propertiy value per line, blank separater, ... lines+++ ..."; }

        public JsonFromLines() : base()
        {
            base.SetArgument(new Argument
            {
                Name = "beautify",
                Title = "Output Format",
                DefaultValue = "true",
                Description = @"true to beutify, or false to minify",
                ValidatorRegex = @"true|TRUE|false|FALSE"
            });
            this.Examples.Add("Varying object counts (beautify)", new Entity.Example
            {
                Name = "???",
                ArgumentValues = new Dictionary<string, string>
                {
                    {"beautify", "true" }
                },
                Input = @"
                    SUQNCkJyYW5kDQpOdW1iZXINCkV4cGlyeQ0KQ1ZWDQpFeHBlY3RlZA0KTm90ZTENCk5vdGUyDQpO
                    b3RlMw0KDQoxDQpNQw0KNDU2NzAwMDgNCjIwMjQtMDQtMzRUMDA6MDA6MDANCg0KMg0KTUMNCjQ1
                    NjcwMDA4DQoyMDI0LTA0LTM0VDAwOjAwOjAwDQoxMjMNCjEwMDogQXBwcm92ZWQNCk5vIGludGVu
                    dCB0byBmYWlsDQoNCjMNCk1DDQo0NTY3MDAwOA0KMjAyNC0wNC0zNFQwMDowMDowMA0KNDU2DQox
                    MDA6IEFwcHJvdmVkDQpObyBpbnRlbnQgdG8gZmFpbA0KTm90ZTIgaGVyZQ0KTm90ZTMgaGVyZQ0K
                    Tm90ZTQgYW5kIGJleW9uZCBpcyBpZ25vcmVkDQpOb3RlNSBpcyBpZ25vcmVkDQoNCg0KDQoNCg0K
                    NA0KDQoNCg0K
                ".Base64DecodeString()
            });
            this.Examples.Add("Varying object counts (minify)", new Entity.Example
            {
                Name = "!!!",
                ArgumentValues = new Dictionary<string, string>
                {
                    {"beautify", "false" }
                },
                Input = @"
                    SUQNCkJyYW5kDQpOdW1iZXINCkV4cGlyeQ0KQ1ZWDQpFeHBlY3RlZA0KTm90ZTENCk5vdGUyDQpO
                    b3RlMw0KDQoxDQpNQw0KNDU2NzAwMDgNCjIwMjQtMDQtMzRUMDA6MDA6MDANCg0KMg0KTUMNCjQ1
                    NjcwMDA4DQoyMDI0LTA0LTM0VDAwOjAwOjAwDQoxMjMNCjEwMDogQXBwcm92ZWQNCk5vIGludGVu
                    dCB0byBmYWlsDQoNCjMNCk1DDQo0NTY3MDAwOA0KMjAyNC0wNC0zNFQwMDowMDowMA0KNDU2DQox
                    MDA6IEFwcHJvdmVkDQpObyBpbnRlbnQgdG8gZmFpbA0KTm90ZTIgaGVyZQ0KTm90ZTMgaGVyZQ0K
                    Tm90ZTQgYW5kIGJleW9uZCBpcyBpZ25vcmVkDQpOb3RlNSBpcyBpZ25vcmVkDQoNCg0KDQoNCg0K
                    NA0KDQoNCg0K
                ".Base64DecodeString()
            });
        }

        public override string Munge(string textToMunge)
        {
            var beautify = bool.Parse(System.Web.HttpUtility.UrlDecode(ArgumentValues["beautify"]));

            StringBuilder result = new StringBuilder();
            var HaveHeader = false;
            var useComma = false;

            Dictionary<int, string> PropertyNames = new Dictionary<int, string>();
            Dictionary<string, string> PropertyValues = new Dictionary<string, string>();
            result.Append("[");

            if (textToMunge.Length > 0)
            {
                var inputLines = textToMunge.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                var inputLineIndex = 0;
                var propertyIndex = 0;
                while (inputLineIndex <= inputLines.Length)
                {
                    if (inputLineIndex < inputLines.Length)  // gather info
                    {
                        var thisLine = inputLines[inputLineIndex++];
                        if (!HaveHeader)
                        {
                            if (string.IsNullOrWhiteSpace(thisLine))
                            {
                                if (PropertyNames.Keys.Count == 0) continue;
                                HaveHeader = true;
                                continue;
                            }
                            var propertyName = thisLine.Trim();
                            if (PropertyNames.Values.Contains(thisLine))
                            {
                                throw new Exception($"Duplicate property name detected: {propertyName}");
                            }
                            PropertyNames.Add(PropertyNames.Keys.Count, propertyName);
                            continue;
                        }
                        // property values
                        if (string.IsNullOrWhiteSpace(thisLine))
                        {
                            if (PropertyValues.Keys.Count == 0) continue;
                            if (useComma) result.Append(",");
                            result.Append(
                                ObjectJsonSerializer<Dictionary<string, string>>.CreateDocumentFormat(
                                    PropertyValues));
                            PropertyValues.Clear();
                            propertyIndex = 0;
                            useComma = true;
                            continue;
                        }
                        if (propertyIndex < PropertyNames.Keys.Count)
                        {
                            PropertyValues.Add(PropertyNames[propertyIndex++], thisLine);
                        }
                    }
                    else  // final pass only: write any stranded object to the output.
                    {
                        if (PropertyValues.Keys.Count > 0)
                        {
                            if (useComma) result.Append(",");
                            result.Append(
                                ObjectJsonSerializer<Dictionary<string, string>>.CreateDocumentFormat(
                                    PropertyValues));
                        }
                        break;
                    }
                }
            }
            result.Append("]");
            return beautify ? ObjectJsonUtility.BeautifyJSON(result.ToString()) : result.ToString();
        }
    }
}
