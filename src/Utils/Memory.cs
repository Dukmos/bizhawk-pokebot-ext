using BizHawk.Client.Common;

namespace bizhawk.pokebot.ext.Utils
{
    internal static class Memory
    {
        private static IMemoryApi? _maybeMemory { get; set; }
        private static IMemoryApi MemoryAPI => _maybeMemory!;

        public static void Initialize(IMemoryApi memory)
        {
            _maybeMemory = memory;
        }

        public static uint read(long addr, int size)
        {
            string mem = "";
            long memdomain = addr >> 24;
            switch (memdomain)
            {
                case 0:
                    mem = "BIOS";
                    break;

                case 2:
                    mem = "EWRAM";
                    break;

                case 3:
                    mem = "IWRAM";
                    break;

                case 8:
                    mem = "ROM";
                    break;
            }
            addr = addr & 0xFFFFFF;
            MemoryAPI.SetBigEndian(false);

            switch (size)
            {
                case 1:
                    return MemoryAPI.ReadU8(addr, mem);

                case 2:
                    return MemoryAPI.ReadU16(addr, mem);

                case 3:
                    return MemoryAPI.ReadU24(addr, mem);

                default:
                    return MemoryAPI.ReadU32(addr, mem);
            }
        }

        public static uint readdword(long addr) => read(addr, 4);

        public static uint readword(long addr) => read(addr, 2);

        public static uint readbyte(long addr) => read(addr, 1);
    }
}