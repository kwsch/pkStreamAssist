using System;
using System.Drawing;
using System.Linq;

namespace pkStreamAssist
{
    public static class PKX
    {
        public static string[] getFormList(int species, string[] t, string[] f, string[] g, int generation)
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
                        f[804], // Mega
                    };
            }
            switch (species)
            {
                case 6:
                case 150:
                    return new[]
                    {
                        t[000], // Normal
                        f[805], // Mega X
                        f[806], // Mega Y
                    };
                case 025:
                    if (generation == 6)
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
                    if (generation == 7)
                        return new[]
                        {
                        t[000], // Normal
                        f[813], // Original
                        f[814], // Hoenn
                        f[815], // Sinnoh
                        f[816], // Unova
                        f[817], // Kalos
                        f[818], // Alola
                    };
                    break;
                case 172:
                    if (generation != 4)
                        break;
                    return new[]
                    {
                        t[000], // Normal
                        f[000], // Spiky
                    };
                case 201:
                    if (generation == 2)
                        return new[]
                        {
                        "A", "B", "C", "D", "E",
                        "F", "G", "H", "I", "J",
                        "K", "L", "M", "N", "O",
                        "P", "Q", "R", "S", "T",
                        "U", "V", "W", "X", "Y",
                        "Z",
                        // "!", "?", not in Gen II
                    };
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
                        f[889], // Sunny
                        f[890], // Rainy
                        f[891], // Snowy
                    };
                case 382:
                case 383:
                    return new[]
                    {
                        t[000], // Normal
                        f[899], // Primal
                    };
                case 386:
                    return new[]
                    {
                        t[000], // Normal
                        f[902], // Attack
                        f[903], // Defense
                        f[904], // Speed
                    };

                case 412:
                case 413:
                case 414:
                    return new[]
                    {
                        f[412], // Plant
                        f[905], // Sandy
                        f[904], // Trash
                    };

                case 421:
                    return new[]
                    {
                        f[421], // Overcast
                        f[909], // Sunshine
                    };

                case 422:
                case 423:
                    return new[]
                    {
                        f[422], // West
                        f[911], // East
                    };

                case 479:
                    return new[]
                    {
                        t[000], // Normal
                        f[917], // Heat
                        f[918], // Wash
                        f[919], // Frost
                        f[920], // Fan
                        f[921], // Mow
                    };

                case 487:
                    return new[]
                    {
                        f[487], // Altered
                        f[922], // Origin
                    };

                case 492:
                    return new[]
                    {
                        f[492], // Land
                        f[923], // Sky
                    };

                case 493: // Arceus
                case 773: // Silvally
                    if (generation == 4)
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
                            "???", // ???-type arceus
                            t[09],
                            t[10],
                            t[11],
                            t[12],
                            t[13],
                            t[14],
                            t[15],
                            t[16] // No Fairy Type
                        };
                    if (generation == 5)
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
                            t[16] // No Fairy type
                        };
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
                        f[942], // Blue
                    };

                case 555:
                    return new[]
                    {
                        f[555], // Standard
                        f[943], // Zen
                    };

                case 585:
                case 586:
                    return new[]
                    {
                        f[585], // Spring
                        f[947], // Summer
                        f[948], // Autumn
                        f[949], // Winter
                    };

                case 641:
                case 642:
                case 645:
                    return new[]
                    {
                        f[641], // Incarnate
                        f[952], // Therian
                    };

                case 646:
                    return new[]
                    {
                        t[000], // Normal
                        f[953], // White
                        f[954], // Black
                    };

                case 647:
                    return new[]
                    {
                        f[647], // Ordinary
                        f[955], // Resolute
                    };

                case 648:
                    return new[]
                    {
                        f[648], // Aria
                        f[956], // Pirouette
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

                case 658:
                    return new[]
                    {
                        t[000], // Normal
                        f[962], // "Ash",
                        f[1012], // "Bonded" - Active
                    };

                case 664:
                case 665:
                case 666:
                    return new[]
                    {
                        f[666], // Icy Snow
                        f[963], // Polar
                        f[964], // Tundra
                        f[965], // Continental 
                        f[966], // Garden
                        f[967], // Elegant
                        f[968], // Meadow
                        f[969], // Modern 
                        f[970], // Marine
                        f[971], // Archipelago
                        f[972], // High-Plains
                        f[973], // Sandstorm
                        f[974], // River
                        f[975], // Monsoon
                        f[976], // Savannah 
                        f[977], // Sun
                        f[978], // Ocean
                        f[989], // Jungle
                        f[980], // Fancy
                        f[981], // Poké Ball
                    };

                case 669:
                case 671:
                    return new[]
                    {
                        f[669], // Red
                        f[986], // Yellow
                        f[987], // Orange
                        f[988], // Blue
                        f[989], // White
                    };

                case 670:
                    return new[]
                    {
                        f[669], // Red
                        f[986], // Yellow
                        f[987], // Orange
                        f[988], // Blue
                        f[989], // White
                        f[990], // Eternal
                    };

                case 676:
                    return new[]
                    {
                        f[994], // Natural
                        f[995], // Heart
                        f[996], // Star
                        f[997], // Diamond
                        f[998], // Deputante
                        f[999], // Matron
                        f[1000], // Dandy
                        f[1001], // La Reine
                        f[1002], // Kabuki 
                        f[1003], // Pharaoh
                    };

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
                        f[1005], // Blade
                    };

                case 710:
                case 711:
                    return new[]
                    {
                        f[710], // Average
                        f[1006], // Small
                        f[1007], // Large
                        f[1008], // Super
                    };

                case 716:
                    return new[]
                    {
                        t[000], // Normal
                        f[1012], // Active
                    };

                case 720:
                    return new[]
                    {
                        t[000], // Normal
                        f[1018], // Unbound
                    };

                case 718: // Zygarde
                    return new[]
                    {
                        t[000], // Normal (Aura Break)
                        "10%", // (Aura Break)
                        "10%-C", // Cell (Power Construct)
                        "50%-C", // Cell (Power Construct)
                        "100%-C" // Cell (Power Construct)
                    };

                case 741: // Oricorio
                    return new[]
                    {
                        f[741], // "RED" - Baile
                        f[1021], // "YLW" - Pom-Pom
                        f[1022], // "PNK" - Pa'u
                        f[1023], // "BLU" - Sensu
                    };

                case 745: // Lycanroc
                    return new[]
                    {
                        f[745], // Midday
                        f[1024], // Midnight
                    };

                case 746: // Wishiwashi
                    return new[]
                    {
                        f[746],
                        f[1025], // School
                    };

                case 774: // Minior
                    return new[]
                    {
                        f[774], // "R-Meteor", // Meteor Red
                        f[1045], // "O-Meteor", // Meteor Orange
                        f[1046], // "Y-Meteor", // Meteor Yellow
                        f[1047], // "G-Meteor", // Meteor Green
                        f[1048], // "B-Meteor", // Meteor Blue
                        f[1049], // "I-Meteor", // Meteor Indigo
                        f[1050], // "V-Meteor", // Meteor Violet
                        f[1051], // "R-Core", // Core Red
                        f[1052], // "O-Core", // Core Orange
                        f[1053], // "Y-Core", // Core Yellow
                        f[1054], // "G-Core", // Core Green
                        f[1055], // "B-Core", // Core Blue
                        f[1056], // "I-Core", // Core Indigo
                        f[1057], // "V-Core", // Core Violet
                    };

                case 778: // Mimikyu
                    return new[]
                    {
                        t[000],
                        f[1058], // Busted
                    };

                case 19: // Rattata
                case 20: // Raticate
                case 26: // Raichu
                case 27: // Sandshrew
                case 28: // Sandslash
                case 37: // Vulpix
                case 38: // Ninetails
                case 50: // Diglett
                case 51: // Dugtrio
                case 52: // Meowth
                case 53: // Persian
                case 74: // Geodude
                case 75: // Graveler
                case 76: // Golem
                case 88: // Grimer
                case 89: // Muk
                case 105: // Marowak
                case 103: // Exeggutor
                    if (generation < 7)
                        break;
                    return new[]
                    {
                        t[000],
                        f[810] // Alolan
                    };

                case 801: // Magearna
                    return new[]
                    {
                        t[000],
                        f[1062], // Original
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
