using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BOG.ClipboardMunger.Common.Base;
using BOG.ClipboardMunger.Common.Entity;
using BOG.ClipboardMunger.Common.Interface;
using Figgle.Fonts;

namespace BOG.ClipboardMunger.Common.MethodLibrary
{
    public class MakeBannerFontsProofSheet : ClipboardMungerProvider, IClipboardMungerProvider
    {
        public override string MethodName { get => "Make Banner Fonts Proof Sheet"; }
        public override string GroupName { get => "String-Magic"; }
        public override string Description { get => "Shows all available fonts in the Figgle assembly available, with a sample"; }

        public MakeBannerFontsProofSheet()
        {
            base.SetArgument(new Argument
            {
                Name = "TextToGenerate",
                Title = "The text to use for the font generated",
                DefaultValue = "Hello World !!",
                Description = "",
                ValidatorRegex = ".+"
            });
        }

        public override string Munge(string textToMunge)
        {
            var textToEngrave = GetArgumentValue("TextToGenerate").Trim();

            if (textToEngrave.Length > 100) throw new ArgumentException("Text to generate must be 100 characters or less, to prevent excessive output");

            if (textToEngrave.Length > 30)
            {
                var dialogResult = MessageBox.Show (
                    "Text over 30 characters long may be too large for a proof sheet. Continue anyway?",
                    "Excessive size for samples",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning
                );

                if (dialogResult != DialogResult.No)
                {
                    return textToMunge; // if they don't want to continue, just return the original text un-munged
                }
            }

            var result = new StringBuilder();
            var fontsLibrary = LoadSelectionList();

            result.AppendLine(new string('=', 78));
            result.AppendLine($"Figgle Fonts Proof Sheet - {fontsLibrary.Count} fonts, Generated: {DateTime.Now:F}");
            result.AppendLine(new string('=', 78));
            result.AppendLine();

            foreach (var key in fontsLibrary.Keys.OrderBy(o => o))
            {
                result.AppendLine($"*** Font: {key} - {fontsLibrary[key]} ***");
                result.AppendLine();
                try
                {
                    var fontWriter = FiggleFonts.TryGetByName(fontsLibrary[key]);
                    if (fontWriter == null)
                    {
                        result.AppendLine($"  :-(   font not found in Figgle library   :-(");
                        result.AppendLine();
                        continue;
                    }
                    result.AppendLine(fontWriter.Render(textToEngrave));
                    result.AppendLine();
                }
                catch (Exception err)
                {
                    result.AppendLine($"  :-(   error rendering font: {err.Message}   :-(");
                }
            }
            return result.ToString();
        }

        private Dictionary<string, string> LoadSelectionList()
        {
            var repository = new Dictionary<string, string>();

            repository.Add("OneRow", "1row");
            repository.Add("ThreeD", "3-d");
            repository.Add("ThreeDDiagonal", "3d_diagonal");
            repository.Add("ThreeByFive", "3x5");
            repository.Add("FourMax", "4max");
            repository.Add("FiveLineOblique", "5lineoblique");
            repository.Add("Acrobatic", "acrobatic");
            repository.Add("Alligator", "alligator");
            repository.Add("Alligator2", "alligator2");
            repository.Add("Alligator3", "alligator3");
            repository.Add("Alpha", "alpha");
            repository.Add("Alphabet", "alphabet");
            repository.Add("Amc3Line", "amc3line");
            repository.Add("Amc3Liv1", "amc3liv1");
            repository.Add("AmcAaa01", "amcaaa01");
            repository.Add("AmcNeko", "amcneko");
            repository.Add("AmcRazor2", "amcrazo2");
            repository.Add("AmcRazor", "amcrazor");
            repository.Add("AmcSlash", "amcslash");
            repository.Add("AmcSlder", "amcslder");
            repository.Add("AmcThin", "amcthin");
            repository.Add("AmcTubes", "amctubes");
            repository.Add("AmcUn1", "amcun1");
            repository.Add("Arrows", "arrows");
            repository.Add("AsciiNewRoman", "ascii_new_roman");
            repository.Add("Avatar", "avatar");
            repository.Add("B1FF", "B1FF");
            repository.Add("Banner", "banner");
            repository.Add("Banner3", "banner3");
            repository.Add("Banner3D", "banner3-D");
            repository.Add("Banner4", "banner4");
            repository.Add("BarbWire", "barbwire");
            repository.Add("Basic", "basic");
            repository.Add("Bear", "bear");
            repository.Add("Bell", "bell");
            repository.Add("Benjamin", "benjamin");
            repository.Add("Big", "big");
            repository.Add("BigChief", "bigchief");
            repository.Add("BigFig", "bigfig");
            repository.Add("Binary", "binary");
            repository.Add("Block", "block");
            repository.Add("Blocks", "blocks");
            repository.Add("Bolger", "bolger");
            repository.Add("Braced", "braced");
            repository.Add("Bright", "bright");
            repository.Add("Broadway", "broadway");
            repository.Add("BroadwayKB", "broadway_kb");
            repository.Add("Bubble", "bubble");
            repository.Add("Bulbhead", "bulbhead");
            repository.Add("Caligraphy2", "calgphy2");
            repository.Add("Caligraphy", "caligraphy");
            repository.Add("Cards", "cards");
            repository.Add("CatWalk", "catwalk");
            repository.Add("Chiseled", "chiseled");
            repository.Add("Chunky", "chunky");
            repository.Add("Coinstak", "coinstak");
            repository.Add("Cola", "cola");
            repository.Add("Colossal", "colossal");
            repository.Add("Computer", "computer");
            repository.Add("Contessa", "contessa");
            repository.Add("Contrast", "contrast");
            repository.Add("Cosmic", "cosmic");
            repository.Add("Cosmike", "cosmike");
            repository.Add("Crawford", "crawford");
            repository.Add("Crazy", "crazy");
            repository.Add("Cricket", "cricket");
            repository.Add("Cursive", "cursive");
            repository.Add("CyberLarge", "cyberlarge");
            repository.Add("CyberMedium", "cybermedium");
            repository.Add("CyberSmall", "cybersmall");
            repository.Add("Cygnet", "cygnet");
            repository.Add("DANC4", "DANC4");
            repository.Add("DancingFont", "dancingfont");
            repository.Add("Decimal", "decimal");
            repository.Add("DefLeppard", "defleppard");
            repository.Add("Diamond", "diamond");
            repository.Add("DietCola", "dietcola");
            repository.Add("Digital", "digital");
            repository.Add("Doh", "doh");
            repository.Add("Doom", "doom");
            repository.Add("DosRebel", "dosrebel");
            repository.Add("DotMatrix", "dotmatrix");
            repository.Add("Double", "double");
            repository.Add("DoubleShorts", "doubleshorts");
            repository.Add("DRPepper", "drpepper");
            repository.Add("DWhistled", "dwhistled");
            repository.Add("EftiChess", "eftichess");
            repository.Add("EftiFont", "eftifont");
            repository.Add("EftiPiti", "eftipiti");
            repository.Add("EftiRobot", "eftirobot");
            repository.Add("EftiItalic", "eftitalic");
            repository.Add("EftiWall", "eftiwall");
            repository.Add("EftiWater", "eftiwater");
            repository.Add("Epic", "epic");
            repository.Add("Fender", "fender");
            repository.Add("Filter", "filter");
            repository.Add("FireFontK", "fire_font-k");
            repository.Add("FireFontS", "fire_font-s");
            repository.Add("Flipped", "flipped");
            repository.Add("FlowerPower", "flowerpower");
            repository.Add("FourTops", "fourtops");
            repository.Add("Fraktur", "fraktur");
            repository.Add("FunFace", "funface");
            repository.Add("FunFaces", "funfaces");
            repository.Add("Fuzzy", "fuzzy");
            repository.Add("Georgia16", "georgi16");
            repository.Add("Georgia11", "Georgia11");
            repository.Add("Ghost", "ghost");
            repository.Add("Ghoulish", "ghoulish");
            repository.Add("Glenyn", "glenyn");
            repository.Add("Goofy", "goofy");
            repository.Add("Gothic", "gothic");
            repository.Add("Graceful", "graceful");
            repository.Add("Gradient", "gradient");
            repository.Add("Graffiti", "graffiti");
            repository.Add("Greek", "greek");
            repository.Add("HeartLeft", "heart_left");
            repository.Add("HeartRight", "heart_right");
            repository.Add("Henry3d", "henry3d");
            repository.Add("Hex", "hex");
            repository.Add("Hieroglyphs", "hieroglyphs");
            repository.Add("Hollywood", "hollywood");
            repository.Add("HorizontalLeft", "horizontalleft");
            repository.Add("HorizontalRight", "horizontalright");
            repository.Add("ICL1900", "ICL-1900");
            repository.Add("Impossible", "impossible");
            repository.Add("Invita", "invita");
            repository.Add("Isometric1", "isometric1");
            repository.Add("Isometric2", "isometric2");
            repository.Add("Isometric3", "isometric3");
            repository.Add("Isometric4", "isometric4");
            repository.Add("Italic", "italic");
            repository.Add("Ivrit", "ivrit");
            repository.Add("Jacky", "jacky");
            repository.Add("Jazmine", "jazmine");
            repository.Add("Jerusalem", "jerusalem");
            repository.Add("Katakana", "katakana");
            repository.Add("Kban", "kban");
            repository.Add("Keyboard", "keyboard");
            repository.Add("Knob", "knob");
            repository.Add("Konto", "konto");
            repository.Add("KontoSlant", "kontoslant");
            repository.Add("Larry3d", "larry3d");
            repository.Add("Lcd", "lcd");
            repository.Add("Lean", "lean");
            repository.Add("Letters", "letters");
            repository.Add("LilDevil", "lildevil");
            repository.Add("LineBlocks", "lineblocks");
            repository.Add("Linux", "linux");
            repository.Add("LockerGnome", "lockergnome");
            repository.Add("Madrid", "madrid");
            repository.Add("Marquee", "marquee");
            repository.Add("MaxFour", "maxfour");
            repository.Add("Merlin1", "merlin1");
            repository.Add("Merlin2", "merlin2");
            repository.Add("Mike", "mike");
            repository.Add("Mini", "mini");
            repository.Add("Mirror", "mirror");
            repository.Add("Mnemonic", "mnemonic");
            repository.Add("Modular", "modular");
            repository.Add("Morse", "morse");
            repository.Add("Morse2", "morse2");
            repository.Add("Moscow", "moscow");
            repository.Add("Mshebrew210", "mshebrew210");
            repository.Add("Muzzle", "muzzle");
            repository.Add("NancyJ", "nancyj");
            repository.Add("NancyJFancy", "nancyj-fancy");
            repository.Add("NancyJImproved", "nancyj-improved");
            repository.Add("NancyJUnderlined", "nancyj-underlined");
            repository.Add("Nipples", "nipples");
            repository.Add("NScript", "nscript");
            repository.Add("NTGreek", "ntgreek");
            repository.Add("NVScript", "nvscript");
            repository.Add("O8", "o8");
            repository.Add("Octal", "octal");
            repository.Add("Ogre", "ogre");
            repository.Add("OldBanner", "oldbanner");
            repository.Add("OS2", "os2");
            repository.Add("Pawp", "pawp");
            repository.Add("Peaks", "peaks");
            repository.Add("PeaksSlant", "peaksslant");
            repository.Add("Pebbles", "pebbles");
            repository.Add("Pepper", "pepper");
            repository.Add("Poison", "poison");
            repository.Add("Puffy", "puffy");
            repository.Add("Puzzle", "puzzle");
            repository.Add("Pyramid", "pyramid");
            repository.Add("Rammstein", "rammstein");
            repository.Add("Rectangles", "rectangles");
            repository.Add("RedPhoenix", "red_phoenix");
            repository.Add("Relief", "relief");
            repository.Add("Relief2", "relief2");
            repository.Add("Rev", "rev");
            repository.Add("Reverse", "reverse");
            repository.Add("Roman", "roman");
            repository.Add("Rot13", "rot13");
            repository.Add("Rotated", "rotated");
            repository.Add("Rounded", "rounded");
            repository.Add("RowanCap", "rowancap");
            repository.Add("Rozzo", "rozzo");
            repository.Add("Runic", "runic");
            repository.Add("Runyc", "runyc");
            repository.Add("SRelief", "s-relief");
            repository.Add("SantaClara", "santaclara");
            repository.Add("SBlood", "sblood");
            repository.Add("Script", "script");
            repository.Add("SerifCap", "serifcap");
            repository.Add("Shadow", "shadow");
            repository.Add("Shimrod", "shimrod");
            repository.Add("Short", "short");
            repository.Add("Slant", "slant");
            repository.Add("Slide", "slide");
            repository.Add("ScriptSlant", "slscript");
            repository.Add("Small", "small");
            repository.Add("SmallCaps", "smallcaps");
            repository.Add("IsometricSmall", "smisome1");
            repository.Add("KeyboardSmall", "smkeyboard");
            repository.Add("PoisonSmall", "smpoison");
            repository.Add("ScriptSmall", "smscript");
            repository.Add("ShadowSmall", "smshadow");
            repository.Add("SlantSmall", "smslant");
            repository.Add("TengwarSmall", "smtengwar");
            repository.Add("Soft", "soft");
            repository.Add("Speed", "speed");
            repository.Add("Spliff", "spliff");
            repository.Add("Stacey", "stacey");
            repository.Add("Stampate", "stampate");
            repository.Add("Stampatello", "stampatello");
            repository.Add("Standard", "standard");
            repository.Add("Starstrips", "starstrips");
            repository.Add("Starwars", "starwars");
            repository.Add("Stellar", "stellar");
            repository.Add("Stforek", "stforek");
            repository.Add("Stop", "stop");
            repository.Add("Straight", "straight");
            repository.Add("SubZero", "sub-zero");
            repository.Add("Swampland", "swampland");
            repository.Add("Swan", "swan");
            repository.Add("Sweet", "sweet");
            repository.Add("Tanja", "tanja");
            repository.Add("Tengwar", "tengwar");
            repository.Add("Term", "term");
            repository.Add("Test1", "test1");
            repository.Add("Thick", "thick");
            repository.Add("Thin", "thin");
            repository.Add("ThreePoint", "threepoint");
            repository.Add("Ticks", "ticks");
            repository.Add("TicksSlant", "ticksslant");
            repository.Add("Tiles", "tiles");
            repository.Add("TinkerToy", "tinker-toy");
            repository.Add("Tombstone", "tombstone");
            repository.Add("Train", "train");
            repository.Add("Trek", "trek");
            repository.Add("Tsalagi", "tsalagi");
            repository.Add("Tubular", "tubular");
            repository.Add("Twisted", "twisted");
            repository.Add("TwoPoint", "twopoint");
            repository.Add("Univers", "univers");
            repository.Add("UsaFlag", "usaflag");
            repository.Add("Varsity", "varsity");
            repository.Add("Wavy", "wavy");
            repository.Add("Weird", "weird");
            repository.Add("WetLetter", "wetletter");
            repository.Add("Whimsy", "whimsy");
            repository.Add("Wow", "wow");

            return repository;
        }
    }
}
