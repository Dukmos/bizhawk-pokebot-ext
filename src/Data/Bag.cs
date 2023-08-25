using bizhawk.pokebot.ext.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bizhawk.pokebot.ext.Data
{
    internal static class Bag
    {
        public static Dictionary<string, Item[]> getBag()
        {
            long trainer = Memory.readdword(GameSettings.trainerpointer);
            uint securityKey = Memory.readword(trainer + 172);

            Dictionary<string, Item[]> bag = new();
            string[] bagType = { "Items", "Poké Balls", "TMs & HMs", "Berries", "Key Items" };

            for (int i = 0; i < bagType.Length; i++)
            {
                uint startBag = Memory.readdword(GameSettings.bag + i * 8);
                uint numberOfBytes = Memory.readbyte(GameSettings.bag + i * 8 + 4);
                bag[bagType[i]] = new Item[numberOfBytes];
                for (uint j = 0; j < numberOfBytes; j++)
                {
                    bag[bagType[i]][j] = new Item(startBag, securityKey);
                    startBag += 4;
                }
            }
            return bag;
        }
    }
}