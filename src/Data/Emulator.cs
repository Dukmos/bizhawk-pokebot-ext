using bizhawk.pokebot.ext.Utils;
using BizHawk.Client.Common;

namespace bizhawk.pokebot.ext.Data
{
    internal class Emulator
    {
        private static ApiContainer? _maybeAPIContainer { get; set; }
        private static ApiContainer APIs => _maybeAPIContainer!;
        public int frameCount => APIs.Emulation.FrameCount();
        public int fps => APIs.EmuClient.GetApproxFramerate();
        public string detectedGame => GameSettings.gamename;
        public uint rngState => Memory.readdword(GameSettings.rng);
        public LANGUAGES language => GameSettings.language;

        public static void Initialize(ApiContainer APIs)
        {
            _maybeAPIContainer = APIs;
        }
    }
}