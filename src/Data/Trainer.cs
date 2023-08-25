using bizhawk.pokebot.ext.Utils;

namespace bizhawk.pokebot.ext.Data
{
    internal class Trainer
    {
        public string facing { get; set; }
        public uint mapBank { get; set; }
        public uint mapId { get; set; }
        public uint posX { get; set; }
        public uint posY { get; set; }
        public uint roamerMapId { get; set; }
        public uint sid { get; set; }
        public uint state { get; set; }
        public uint tid { get; set; }

        public Trainer()
        {
            uint trainer = Memory.readdword(GameSettings.trainerpointer);

            facing = trainerFacing(Memory.readbyte(GameSettings.coords + 8) - 7);
            mapBank = Memory.readbyte(GameSettings.trainerpointer + 201);
            mapId = Memory.readbyte(GameSettings.trainerpointer + 200);
            posX = Memory.readbyte(GameSettings.coords + 0) - 7;
            posY = Memory.readbyte(GameSettings.coords + 2) - 7;
            roamerMapId = Memory.readbyte(GameSettings.mapbank + 7);
            sid = Memory.readword(trainer + 12);
            state = Memory.readbyte(GameSettings.trainerpointer + 199);
            tid = Memory.readword(trainer + 10);
        }

        private static string trainerFacing(uint facing)
        {
            switch (facing)
            {
                case 27:
                    return "Up";

                case 61:
                    return "Right";

                case 10:
                    return "Down";

                case 44:
                    return "Left";

                default:
                    return "";
            }
        }
    }
}