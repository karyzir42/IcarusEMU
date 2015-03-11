// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using IcarusCommons.Abstractions.Creature;
using IcarusCommons.Structures.Global;

namespace IcarusCommons.Models.Creature
{
    public class Mob : ACreature
    {
        public override int UID { get; set; }

        public int Id;
        public int Zone;

        public Position Position;
        public int Animation;
        public int resp_time;
        public int Type1;
        public int MovementType;
        public int Level;
        public int Unk1;
        public int Unk2;
        public int Unk3;
        public int Unk4;
        public int Unk5;
        public int Unk6;
        public int Unk7;
        public int Unk8;
        public int Unk9;
        public int Unk10;
        public int Unk11;
        public int Unk12;
        public uint Unk13;
        public int Unk14;
        public int Unk15;
        public uint Unk16;
        public uint Unk17;
        public uint Unk18;
        public uint Unk19;
        public int Unk20;
        public int Unk21;
        public int Unk22;
        public bool[] PlayerhasVisibleMy;

        ~Mob()
        {
            base.Release(this);
        }

        public void Release()
        {
            base.Release(this);
        }
    }
}