using System;
using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Web.Caching;
using System.Collections.Generic;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
    public class GoogleMapsLatLong : ClipboardMungerProviderBase, IClipboardMungerProvider
    {
        public override string MethodName { get => "Google Maps Lat Long Extract"; }
        public override string GroupName { get => "Math Extract"; }
        public override string Description { get => "Returns the lat long as dddDmmss, e.g. 117N5051 12E3407"; }

        // https://www.google.com/maps/place/Spokane,+WA/@47.6726729,-117.6187703,36914m/data=!3m2!1e3!4b1!4m6!3m5!1s0x549e185c30bbe7e5:0xddfcc9d60b84d9b1!8m2!3d47.6579711!4d-117.4235318!16zL20vMDEwdjhr?entry=ttu&g_ep=EgoyMDI0MDgyNi4wIKXMDSoASAFQAw%3D%3D
        private const string GoogleMapsUrlValidateRegex = @"https://www\.google\.com/maps/place/.+@[\-]?[\d]{1,3}\.[\d]+,[\-]?[\d]{1,3}\.[\d]+,.+";

        public GoogleMapsLatLong() : base()
        {
            base.Examples.Add("Seattle", new Example
            {
                Name = "Seattle, WA",
                Input = "https://www.google.com/maps/place/Seattle,+WA/@28.5568517,-81.8552188,3009m/data=!3m1!1e3!4m6!3m5!1s0x5490102c93e83355:0x102565466944d59a!8m2!3d47.6061389!4d-122.3328481!16zL20vMGQ5anI?entry=ttu&g_ep=EgoyMDI0MDgyNi4wIKXMDSoASAFQAw%3D%3D"
            });
        }

        public override string Munge(string textToMunge)
        {
            var r = new Regex(GoogleMapsUrlValidateRegex, RegexOptions.Singleline);
            if (!r.IsMatch(textToMunge))
            {
                throw new ArgumentException("The clipboard does not contains a valid URL");
            }

            var workline = textToMunge.Substring(textToMunge.IndexOf("@"));
            workline = workline.Substring(0, workline.IndexOf("@m"));
            workline = workline.Substring(0, workline.Length - 2);

            return FormatLatLong(workline);
        }

        private string FormatLatLong(string input)
        {
            var output = new StringBuilder();
            var tudes = input.Split(new char[',']);

            var num = double.Parse(input);
            var absnum = Math.Abs(num);
            var degrees = (int)(absnum - (absnum % 1.0d));
            output.Append(string.Format("{0:00#}", num));
            output.Append((num < 0) ? "S" : "N");
            var minutes = (int)(60.0d * (absnum % 1.0d));
            output.Append((minutes.ToString()));
            return output.ToString();
        }
    }
}
