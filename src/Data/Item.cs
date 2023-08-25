using bizhawk.pokebot.ext.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bizhawk.pokebot.ext.Data
{
    internal class Item
    {
        public uint type { get; set; }
        public string name { get; set; }
        public uint quantity { get; set; }

        public Item(uint address, uint securityKey)
        {
            type = Memory.readword(address + 0);
            name = ItemNames.List.ElementAtOrDefault(Convert.ToInt32(type) + 1);
            quantity = Memory.readword(address + 2) ^ securityKey;
        }
    }
}