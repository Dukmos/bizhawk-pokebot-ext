using bizhawk.pokebot.ext.Utils;
using System;
using System.Linq;

namespace bizhawk.pokebot.ext.Data
{
    internal class Pokemon
    {
        public uint personality { get; private set; }
        public uint magicWord { get; private set; }
        public uint otId { get; private set; }
        public uint language { get; private set; }
        public uint isBadEgg { get; private set; }
        public uint hasSpecies { get; private set; }
        public uint isEgg { get; private set; }
        public uint markings { get; private set; }
        public uint status { get; private set; }
        public uint level { get; private set; }
        public uint mail { get; private set; }
        public uint hp { get; private set; }
        public uint maxHP { get; private set; }
        public uint attack { get; private set; }
        public uint defense { get; private set; }
        public uint speed { get; private set; }
        public uint spAttack { get; private set; }
        public uint spDefense { get; private set; }

        public uint spicies { get; private set; }
        public string name { get; private set; }
        public uint heldItem { get; private set; }
        public uint experience { get; private set; }
        public uint ppBonuses { get; private set; }
        public uint friendship { get; private set; }
        public uint[] moves { get; private set; }
        public uint[] pp { get; private set; }
        public uint hpEV { get; private set; }
        public uint attackEV { get; private set; }
        public uint defenseEV { get; private set; }
        public uint speedEV { get; private set; }
        public uint spAttackEV { get; private set; }
        public uint spDefenseEV { get; private set; }

        public uint pokerus { get; private set; }
        public uint metLocation { get; private set; }
        public uint metLevel { get; private set; }
        public uint metGame { get; private set; }
        public uint pokeball { get; private set; }
        public uint otGender { get; private set; }

        public uint hpIV { get; private set; }
        public uint attackIV { get; private set; }
        public uint defenseIV { get; private set; }
        public uint speedIV { get; private set; }
        public uint spAttackIV { get; private set; }
        public uint spDefenseIV { get; private set; }
        public uint altAbility { get; private set; }

        private static uint[][] substrucSelector = {
                new uint[]{ 0, 1, 2, 3 },
                new uint[]{ 0, 1, 3, 2 },
                new uint[]{ 0, 2, 1, 3 },
                new uint[]{ 0, 3, 1, 2 },
                new uint[]{ 0, 2, 3, 1 },
                new uint[]{ 0, 3, 2, 1 },
                new uint[]{ 1, 0, 2, 3 },
                new uint[]{ 1, 0, 3, 2 },
                new uint[]{ 2, 0, 1, 3 },
                new uint[]{ 3, 0, 1, 2 },
                new uint[]{ 2, 0, 3, 1 },
                new uint[]{ 3, 0, 2, 1 },
                new uint[]{ 1, 2, 0, 3 },
                new uint[]{ 1, 3, 0, 2 },
                new uint[]{ 2, 1, 0, 3 },
                new uint[]{ 3, 1, 0, 2 },
                new uint[]{ 2, 3, 0, 1 },
                new uint[]{ 3, 2, 0, 1 },
                new uint[]{ 1, 2, 3, 0 },
                new uint[]{ 1, 3, 2, 0 },
                new uint[]{ 2, 1, 3, 0 },
                new uint[]{ 3, 1, 2, 0 },
                new uint[]{ 2, 3, 1, 0 },
                new uint[]{ 3, 2, 1, 0 },
            };

        public Pokemon(long address)
        {
            personality = Memory.readdword(address + 0);
            magicWord = personality ^ Memory.readdword(address + 4);
            otId = Memory.readdword(address + 4);
            language = Memory.readbyte(address + 18);
            var flags = Memory.readbyte(address + 19);
            isBadEgg = flags & 1;
            hasSpecies = (flags >> 1) & 1;
            isEgg = (flags >> 2) & 1;
            markings = Memory.readbyte(address + 27);
            status = Memory.readdword(address + 80);
            level = Memory.readbyte(address + 84);
            mail = Memory.readdword(address + 85);
            hp = Memory.readword(address + 86);
            maxHP = Memory.readword(address + 88);
            attack = Memory.readword(address + 90);
            defense = Memory.readword(address + 92);
            speed = Memory.readword(address + 94);
            spAttack = Memory.readword(address + 96);
            spDefense = Memory.readword(address + 98);

            uint key = otId ^ personality;

            uint[] pSel = substrucSelector[personality % 24];
            uint[] ss0 = new uint[3];
            uint[] ss1 = new uint[3];
            uint[] ss2 = new uint[3];
            uint[] ss3 = new uint[3];

            for (int i = 0; i <= 2; i++)
            {
                ss0[i] = Memory.readdword(address + 32 + pSel[0] * 12 + i * 4) ^ key;
                ss1[i] = Memory.readdword(address + 32 + pSel[1] * 12 + i * 4) ^ key;
                ss2[i] = Memory.readdword(address + 32 + pSel[2] * 12 + i * 4) ^ key;
                ss3[i] = Memory.readdword(address + 32 + pSel[3] * 12 + i * 4) ^ key;
            }

            spicies = (ss0[0] & 0xFFFF) + 1;
            name = PokemonNames.List.ElementAtOrDefault(Convert.ToInt32(spicies));
            heldItem = ss0[0] >> 16;
            experience = ss0[1];
            ppBonuses = ss0[2] & 0xFF;
            friendship = (ss0[2] >> 8) & 0xFF;

            moves = new uint[]{
                ss1[0] & 0xFFFF,
                ss1[0] >> 16,
                ss1[1] & 0xFFFF,
                ss1[1] >> 16
            };
            pp = new uint[]{
                ss1[2] & 0xFF,
                (ss1[2] >> 8) & 0xFF,
                (ss1[2] >> 16) & 0xFF,
                ss1[2] >> 24
            };

            hpEV = ss2[0] & 0xFF;
            attackEV = (ss2[0] >> 8) & 0xFF;
            defenseEV = (ss2[0] >> 16) & 0xFF;
            speedEV = ss2[0] >> 24;
            spAttackEV = ss2[1] & 0xFF;
            spDefenseEV = (ss2[1] >> 8) & 0xFF;

            pokerus = ss3[0] & 0xFF;
            metLocation = (ss3[0] >> 8) & 0xFF;
            flags = ss3[0] >> 16;
            metLevel = flags & 0x7F;
            metGame = (flags >> 7) & 0xF;
            pokeball = (flags >> 11) & 0xF;
            otGender = (flags >> 15) & 0x1;
            flags = ss3[1];
            hpIV = flags & 0x1F;
            attackIV = (flags >> 5) & 0x1F;
            defenseIV = (flags >> 10) & 0x1F;
            speedIV = (flags >> 15) & 0x1F;
            spAttackIV = (flags >> 20) & 0x1F;
            spDefenseIV = (flags >> 25) & 0x1F;
            altAbility = (flags >> 31) & 1;
            flags = ss3[2];
        }
    }
}