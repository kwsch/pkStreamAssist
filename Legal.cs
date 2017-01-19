using System.Linq;

namespace pkStreamAssist
{
    class Legal
    {
        public static readonly int[] BattleForms =
        {
            351, // Castform
            421, // Cherrim
            555, // Darmanitan
            648, // Meloetta
            681, // Aegislash
            719, // Xerneas
            746, // Wishiwashi
            778, // Mimikyu
        };
        public static readonly int[] BattleMegas =
        {
            // XY
            3,6,9,65,80,
            115,127,130,142,150,181,
            212,214,229,248,282,
            303,306,308,310,354,359,380,381,
            445,448,460,

            // AO
            15,18,94,
            208,254,257,260,
            302,319,323,334,362,373,376,384,
            428,475,
            531,
            719
        };
        public static readonly int[] BattlePrimals = { 382, 383 };

        public static bool getIsBattleForm(int Species)
        {
            if (BattleForms.Contains(Species))
                return true;
            if (BattleMegas.Contains(Species))
                return true;
            if (BattlePrimals.Contains(Species))
                return true;
            return false;
        }
    }
}
