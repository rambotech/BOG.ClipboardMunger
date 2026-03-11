using System;
using System.Collections.Generic;
using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;
using Figgle.Fonts;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
    public class MakeBanner : ClipboardMungerProvider, IClipboardMungerProvider
    {
        public override string MethodName { get => "Make Banner"; }
        public override string GroupName { get => "String-Magic"; }
        public override string Description { get => 
                "Uses Figgle to turn the clipboard text into a Banner"; 
        }

        public MakeBanner()
        {
            base.SetArgument(new Argument
            {
                Name = "FontName",
                Title = "The Figgle font to use for the banner",
                DefaultValue = "Standard",
                Description = "",
                ValidatorRegex = ".+"
            });
        }

        public override string Munge(string textToMunge)
        {
            var fontName = ArgumentValues["FontName"];
            var fontWriter = FiggleFonts.TryGetByName(fontName);
            var result = string.Empty;
            if (fontWriter == null)
            {
                result = FiggleFonts.Banner3.Render(textToMunge);
            }
            else
            {
                result = fontWriter.Render(textToMunge);
            }
            return result;
        }
    }
}
