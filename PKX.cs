using System;
using System.Drawing;
using System.Linq;

namespace pkStreamAssist
{
    public static class PKX
    {
        internal static string[] getFormList(int species, string[] t, string[] f, string[] g)
        {
            // Mega List            
            if (Array.IndexOf(new[]
                { // XY
                  003, 009, 065, 094, 115, 127, 130, 142, 181, 212, 214, 229, 248, 257, 282, 303, 306, 308, 310, 354, 359, 380, 381, 445, 448, 460, 
                  // ORAS
                  015, 018, 080, 208, 254, 260, 302, 319, 323, 334, 362, 373, 376, 384, 428, 475, 531, 719,
                }, species) > -1)
            { // ...
                return new[]
                {
                        t[000], // Normal
                        f[723], // Mega
                    };
            }
            // MegaXY List
            switch (species)
            {
                case 6:
                case 150:
                    return new[]
                    {
                        t[000], // Normal
                        f[724], // Mega X
                        f[725], // Mega Y
                    };
                case 025:
                    return new[]
                    {
                        t[000], // Normal
                        f[729], // Rockstar
                        f[730], // Belle
                        f[731], // Pop
                        f[732], // PhD
                        f[733], // Libre
                        f[734], // Cosplay
                    };
                case 201:
                    return new[]
                    {
                        "A", "B", "C", "D", "E",
                        "F", "G", "H", "I", "J",
                        "K", "L", "M", "N", "O",
                        "P", "Q", "R", "S", "T",
                        "U", "V", "W", "X", "Y",
                        "Z",
                        "!", "?",
                    };
                case 351:
                    return new[]
                    {
                        t[000], // Normal
                        f[789], // Sunny
                        f[790], // Rainy
                        f[791], // Snowy
                    };
                case 382:
                case 383:
                    return new[]
                    {
                        t[000], // Normal
                        f[800], // Primal
                    };
                case 386:
                    return new[]
                    {
                        t[000], // Normal
                        f[802], // Attack
                        f[803], // Defense
                        f[804], // Speed
                    };

                case 412:
                case 413:
                case 414:
                    return new[]
                    {
                        f[412], // Plant
                        f[805], // Sandy
                        f[806], // Trash
                    };

                case 421:
                    return new[]
                    {
                        f[421], // Overcast
                        f[809], // Sunshine
                    };

                case 422:
                case 423:
                    return new[]
                    {
                        f[422], // West
                        f[811], // East
                    };

                case 479:
                    return new[]
                    {
                        t[000], // Normal
                        f[817], // Heat
                        f[818], // Wash
                        f[819], // Frost
                        f[820], // Fan
                        f[821], // Mow
                    };

                case 487:
                    return new[]
                    {
                        f[487], // Altered
                        f[822], // Origin
                    };

                case 492:
                    return new[]
                    {
                        f[492], // Land
                        f[823], // Sky
                    };

                case 493:
                    return new[]
                    {
                        t[00], // Normal
                        t[01], // Fighting
                        t[02], // Flying
                        t[03], // Poison
                        t[04], // etc
                        t[05],
                        t[06],
                        t[07],
                        t[08],
                        t[09],
                        t[10],
                        t[11],
                        t[12],
                        t[13],
                        t[14],
                        t[15],
                        t[16],
                        t[17],
                    };

                case 550:
                    return new[]
                    {
                        f[550], // Red
                        f[842], // Blue
                    };

                case 555:
                    return new[]
                    {
                        f[555], // Standard
                        f[843], // Zen
                    };

                case 585:
                case 586:
                    return new[]
                    {
                        f[585], // Spring
                        f[844], // Summer
                        f[845], // Autumn
                        f[846], // Winter
                    };

                case 641:
                case 642:
                case 645:
                    return new[]
                    {
                        f[641], // Incarnate
                        f[852], // Therian
                    };

                case 646:
                    return new[]
                    {
                        t[000], // Normal
                        f[853], // White
                        f[854], // Black
                    };

                case 647:
                    return new[]
                    {
                        f[647], // Ordinary
                        f[855], // Resolute
                    };

                case 648:
                    return new[]
                    {
                        f[648], // Aria
                        f[856], // Pirouette
                    };

                case 649:
                    return new[]
                    {
                        t[000], // Normal
                        t[010], // Douse
                        t[012], // Shock
                        t[009], // Burn
                        t[014], // Chill
                    };

                case 664:
                case 665:
                case 666:
                    return new[]
                    {
                        f[666], // Icy Snow
                        f[861], // Polar
                        f[862], // Tundra
                        f[863], // Continental 
                        f[864], // Garden
                        f[865], // Elegant
                        f[866], // Meadow
                        f[867], // Modern 
                        f[868], // Marine
                        f[869], // Archipelago
                        f[870], // High-Plains
                        f[871], // Sandstorm
                        f[872], // River
                        f[873], // Monsoon
                        f[874], // Savannah 
                        f[875], // Sun
                        f[876], // Ocean
                        f[877], // Jungle
                        f[878], // Fancy
                        f[879], // Poké Ball
                    };

                case 669:
                case 671:
                    return new[]
                    {
                        f[669], // Red
                        f[884], // Yellow
                        f[885], // Orange
                        f[886], // Blue
                        f[887], // White
                    };

                case 670:
                    return new[]
                    {
                        f[669], // Red
                        f[884], // Yellow
                        f[885], // Orange
                        f[886], // Blue
                        f[887], // White
                        f[888], // Eternal
                    };

                case 676:
                    return new[]
                    {
                        f[676], // Natural
                        f[893], // Heart
                        f[894], // Star
                        f[895], // Diamond
                        f[896], // Deputante
                        f[897], // Matron
                        f[898], // Dandy
                        f[899], // La Reine
                        f[900], // Kabuki 
                        f[901], // Pharaoh
                    };

                // CUSTOM
                case 592: case 593: case 521: case 668:
                // ENDCUSTOM
                case 678:
                    return new[]
                    {
                        g[000], // Male
                        g[001], // Female
                    };

                case 681:
                    return new[]
                    {
                        f[681], // Shield
                        f[903], // Blade
                    };

                case 710:
                case 711:
                    return new[]
                    {
                        f[904], // Small
                        f[710], // Average
                        f[905], // Large
                        f[906], // Super
                    };

                case 716:
                    return new[]
                    {
                        t[000], // Normal
                        f[910], // Active
                    };

                case 720:
                    return new[]
                    {
                        t[000], // Normal
                        f[912], // Unbound
                    };
            }
            return new[] { "" };
        }
        internal static Bitmap getSprite(int species, int form)
        {
            if (species == 0)
                return (Bitmap)Properties.Resources.ResourceManager.GetObject("_0");
            if (new[] { 664, 665, 414, 493 }.Contains(species)) // Species who show their default sprite regardless of Form
                form = 0;

            string file = "_" + species;
            if (form > 0) // Alt Form Handling
                file = file + "_" + form;
            else if (form == 1 && new[] { 592, 593, 521, 668 }.Contains(species)) // Frillish & Jellicent, Unfezant & Pyroar
                file = file + "_" + form;

            // Redrawing logic
            Bitmap baseImage = (Bitmap)Properties.Resources.ResourceManager.GetObject(file);
            if (baseImage == null)
            {
                if (species < 722)
                {
                    baseImage = Util.LayerImage(
                        (Image)Properties.Resources.ResourceManager.GetObject("_" + species),
                        Properties.Resources.unknown,
                        0, 0, .5);
                }
                else
                    baseImage = Properties.Resources.unknown;
            }
            return baseImage;
        }
    }
}
