using bizhawk.pokebot.ext.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bizhawk.pokebot.ext.Data
{
    internal static class Party
    {
        public static Pokemon[] getParty()
        {
            long start = GameSettings.pstats;
            uint partyCount = Memory.readbyte(GameSettings.pcount);

            Pokemon[] party = new Pokemon[partyCount];

            for (int i = 0; i < partyCount; i++)
            {
                party[i] = new Pokemon(start);
                start += 100;
            }

            return party;
        }
    }
}