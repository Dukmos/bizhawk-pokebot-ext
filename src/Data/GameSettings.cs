using bizhawk.pokebot.ext.Utils;
using BizHawk.Client.Common;

namespace bizhawk.pokebot.ext.Data
{
    internal static class GameSettings
    {
        public static int game = 0;
        public static string gamename = "";
        public static long gamecolor = 0;
        public static long rngseed = 0;
        public static long mapbank = 0;
        public static long mapid = 0;
        public static long encountertable = 0;
        public static long pstats = 0;
        public static long pcount = 0;
        public static long estats = 0;
        public static long rng = 0;
        public static long rng2 = 0;
        public static long wram = 0;
        public static VERSION version = 0;
        public static LANGUAGES language = 0;
        public static long trainerpointer = 0;
        public static long coords = 0;
        public static long roamerpokemonoffset = 0;
        public static long bag = 0;

        public static void Initialize(IMemoryApi memory)
        {
            memory.SetBigEndian(true);
            var gamecode = memory.ReadU32(0x0000AC, "ROM");
            memory.SetBigEndian(false);
            //TODO: replace missing 0x0 pointers with real values
            //RS(U)  EMER(U)    FRLG(U)    RS(J)      EMER(J)    FRLG(J)    RS(S)      EMER(S)    FRLG(S)
            long[] _pstats = new long[9] { 0x3004360, 0x20244EC, 0x2024284, 0x3004290, 0x2024190, 0x20241E4, 0x0, 0x20244EC, 0x0 }; //Trainer stats
            long[] _pcount = new long[9] { 0x3004350, 0x20244E9, 0x2024029, 0x0, 0x202418D, 0x0, 0x0, 0x20244E9, 0x0 }; //Party count
            long[] _estats = new long[9] { 0x30045C0, 0x2024744, 0x202402C, 0x30044F0, 0x20243E8, 0x2023F8C, 0x0, 0x2024744, 0x0 }; //Enemy stats
            long[] _rng = new long[9] { 0x3004818, 0x3005D80, 0x3005000, 0x3004748, 0x3005AE0, 0x3005040, 0x0, 0x3005D80, 0x0 }; //RNG address
            long[] _coords = new long[9] { 0x30048B0, 0x2037360, 0x2036E48, 0x30047E0, 0x2037000, 0x2036D7C, 0x0, 0x2037360, 0x0 }; //X / Y coords
            long[] _rng2 = new long[9] { 0x0, 0x0, 0x20386D0, 0x0, 0x0, 0x203861C, 0x0, 0x0, 0x0 }; //RNG encounter(FRLG only)
            long[] _wram = new long[9] { 0x0, 0x2020000, 0x2020000, 0x0, 0x2020000, 0x201FF4C, 0x0, 0x2020000, 0x0 }; //WRAM address
            long[] _mapbank = new long[9] { 0x20392FC, 0x203BC80, 0x203F3A8, 0x2038FF4, 0x203B94C, 0x203F31C, 0x0, 0x203BC80, 0x0 }; //Map Bank
            long[] _mapid = new long[9] { 0x202E83C, 0x203732C, 0x2036E10, 0x202E59C, 0x2036FCC, 0x2036D44, 0x0, 0x203732C, 0x0 }; //Map ID
            long[] _trainerpointer = new long[9] { 0x3001FB4, 0x3005D90, 0x300500C, 0x3001F28, 0x3005AF0, 0x300504C, 0x0, 0x3005D90, 0x0 }; //Trainer data
            long[] _roamerpokemonoffset = new long[9] { 0x39D4, 0x4188, 0x4074, 0x39D4, 0x4188, 0x4074, 0x39D4, 0x4188, 0x4074 }; //Roamer Pokemon
            long[] _bag = new long[9] { 0x0, 0x2039DD8, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 }; //Bag address

            switch (gamecode)
            {
                case 0x41585645:
                    game = 1;
                    gamename = "Pokemon Ruby (U)";
                    encountertable = 0x839D454;
                    version = VERSION.RS;
                    language = LANGUAGES.U;
                    break;

                case 0x41585045:
                    game = 1;
                    gamename = "Pokemon Sapphire (U)";
                    encountertable = 0x839D29C;
                    version = VERSION.RS;
                    language = LANGUAGES.U;
                    break;

                case 0x42504545:
                    game = 2;
                    gamename = "Pokemon Emerald (U)";
                    encountertable = 0x8552D48;
                    version = VERSION.E;
                    language = LANGUAGES.U;
                    break;

                case 0x42504546:
                    game = 2;
                    gamename = "Pokemon Emerald (F)";
                    encountertable = 0x8552D48;
                    version = VERSION.E;
                    language = LANGUAGES.F;
                    break;

                case 0x42505245:
                    game = 3;
                    gamename = "Pokemon FireRed (U)";
                    encountertable = 0x83C9CB8;
                    version = VERSION.FRLG;
                    language = LANGUAGES.U;
                    break;

                case 0x42504745:
                    game = 3;
                    gamename = "Pokemon LeafGreen (U)";
                    encountertable = 0x83C9AF4;
                    version = VERSION.FRLG;
                    language = LANGUAGES.U;
                    break;

                case 0x4158564A:
                    game = 4;
                    gamename = "Pokemon Ruby (J)";
                    encountertable = 0x8379304;
                    version = VERSION.RS;
                    language = LANGUAGES.J;
                    break;

                case 0x4158504A:
                    game = 4;
                    gamename = "Pokemon Sapphire (J)";
                    encountertable = 0x83792FC;
                    version = VERSION.RS;
                    language = LANGUAGES.J;
                    break;

                case 0x4250454A:
                    game = 5;
                    gamename = "Pokemon Emerald (J)";
                    encountertable = 0x852D9F4;
                    version = VERSION.E;
                    language = LANGUAGES.J;
                    break;

                case 0x4250524A:
                    game = 6;
                    gamename = "Pokemon FireRed (J)";
                    encountertable = 0x8390B34;
                    version = VERSION.FRLG;
                    language = LANGUAGES.J;
                    break;

                case 0x4250474A:
                    game = 6;
                    gamename = "Pokemon LeafGreen (J)";
                    encountertable = 0x83909A4;
                    version = VERSION.FRLG;
                    language = LANGUAGES.J;
                    break;

                case 0x41585653:
                    game = 7;
                    gamename = "Pokemon Ruby (S)";
                    encountertable = 0x0;
                    version = VERSION.RS;
                    language = LANGUAGES.S;
                    break;

                case 0x41585053:
                    game = 7;
                    gamename = "Pokemon Sapphire (S)";
                    encountertable = 0x0;
                    version = VERSION.RS;
                    language = LANGUAGES.S;
                    break;

                case 0x42504553:
                    game = 8;
                    gamename = "Pokemon Emerald (S)";
                    encountertable = 0x0;
                    version = VERSION.E;
                    language = LANGUAGES.S;
                    break;

                case 0x42505253:
                    game = 9;
                    gamename = "Pokemon FireRed (S)";
                    encountertable = 0x0;
                    version = VERSION.FRLG;
                    language = LANGUAGES.S;
                    break;

                case 0x42504753:
                    game = 9;
                    gamename = "Pokemon LeafGreen (S)";
                    encountertable = 0x0;
                    version = VERSION.FRLG;
                    language = LANGUAGES.S;
                    break;

                case 0x42504544:
                    game = 2;
                    gamename = "Pokemon Emerald (G)";
                    encountertable = 0x0;
                    version = VERSION.E;
                    language = LANGUAGES.G;
                    break;

                default:
                    game = 0;
                    gamename = "Unsupported game";
                    encountertable = 0;
                    break;
            }

            if (game > 0)
            {
                pstats = _pstats[game - 1];
                pcount = _pcount[game - 1];
                estats = _estats[game - 1];
                rng = _rng[game - 1];
                rng2 = _rng2[game - 1];
                wram = _wram[game - 1];
                mapbank = _mapbank[game - 1];
                mapid = _mapid[game - 1];
                trainerpointer = _trainerpointer[game - 1];
                coords = _coords[game - 1];
                roamerpokemonoffset = _roamerpokemonoffset[game - 1];
                bag = _bag[game - 1];
            }

            if (game % 3 == 1)
            {
                rngseed = 0x5A0;
            }
            else
            {
                rngseed = Memory.readword(wram);
            }
        }
    }

    public enum VERSION
    {
        RS = 1,
        E = 2,
        FRLG = 3
    }

    public enum LANGUAGES
    {
        U = 1,
        J = 2,
        F = 3,
        S = 4,
        G = 5,
        I = 6
    }
}