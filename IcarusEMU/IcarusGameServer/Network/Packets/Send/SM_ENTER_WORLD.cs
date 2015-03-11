// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.IO;
using IcarusCommons.Models.Player;
using IcarusGameServer.Network.Packets.Processors;

namespace IcarusGameServer.Network.Packets.Send
{
    internal class SM_ENTER_WORLD : ASendPacket
    {
        protected PlayerData PlayerData;

        public SM_ENTER_WORLD(PlayerData playerData)
        {
            PlayerData = playerData;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteD(writer, PlayerData.PlayerId);

            WriteF(writer, 888);
            WriteF(writer, 889);
            WriteF(writer, 890);

            WriteF(writer, 0x42893333 /*PlayerData.Abilities.PhysicalDefence*/);
            WriteF(writer, 0x42426666 /*PlayerData.Abilities.MagicalDefence*/);

            WriteF(writer, 3);
            WriteF(writer, 4);

            WriteF(writer, 5);
            WriteF(writer, 6);
            WriteF(writer, PlayerData.Abilities.PhysicalDefence); //phys def
            WriteF(writer, PlayerData.Abilities.MagicalDefence); //mag def

            WriteF(writer, 9);
            WriteF(writer, 10);
            WriteF(writer, PlayerData.Abilities.Evasion); //evasion
            WriteF(writer, 12);

            WriteF(writer, PlayerData.Abilities.Accuracy); //accuracy (прицел)

            WriteF(writer, 14);
            WriteF(writer, PlayerData.Abilities.PhysicalCritRate); //Phys crit rate
            WriteF(writer, 16);
            WriteF(writer, PlayerData.Abilities.PhysicalCritTwo); //phys crit rate
            WriteF(writer, 18);

            WriteF(writer, 19);
            WriteF(writer, PlayerData.Abilities.Power); //power
            WriteF(writer, PlayerData.Abilities.Agility); //agility
            WriteF(writer, PlayerData.Abilities.Intelligence); //intelligence
            WriteF(writer, PlayerData.Abilities.Fitness); //fitness
            WriteF(writer, PlayerData.Abilities.Mentality); //mentality
            WriteF(writer, 25);
            WriteF(writer, 26);
            WriteF(writer, PlayerData.Abilities.MaxHp); //max_hp
            WriteF(writer, PlayerData.Abilities.MaxMp); //max_mp

            WriteF(writer, 29);
            WriteF(writer, 30);
            WriteF(writer, 31);
            WriteF(writer, 32);
            WriteF(writer, 33);

            WriteF(writer, PlayerData.Abilities.AbilityPointsMax); //хер значет (де плюсик стоит) max
            WriteF(writer, 35); //31300 abylity points
            WriteF(writer, 36); //31300 abylity points
            WriteF(writer, 37); //31300 abylity points
            WriteF(writer, 38); //31300 abylity points

            WriteF(writer, PlayerData.Abilities.CharacterSpeed); //move speed
            WriteF(writer, 5); //animation speed
            WriteF(writer, 41); //32831 
            WriteF(writer, 42); //32831 

            WriteF(writer, 43);

            WriteF(writer, 44);
            WriteF(writer, 45);
            WriteF(writer, PlayerData.Abilities.Blade); // blade
            WriteF(writer, 47);
            WriteF(writer, 48);
            WriteF(writer, PlayerData.Abilities.DualBlades); // dual blade
            WriteF(writer, PlayerData.Abilities.PhysicalCrit); //phys crit
            WriteF(writer, PlayerData.Abilities.PhysicalCritTwo); //phys crit
            WriteF(writer, 52);
            WriteF(writer, PlayerData.Abilities.ShieldWithThreePupirishki); //щит с тремя пупурышками

            WriteF(writer, 54);
            WriteF(writer, 55);
            WriteF(writer, 56);
            WriteF(writer, 57);
            WriteF(writer, 58);
            WriteF(writer, 59);
            WriteF(writer, 60);
            WriteF(writer, 61);
            WriteF(writer, 62);
            WriteF(writer, 63);

            WriteF(writer, PlayerData.Abilities.CurrentHp); //	hp
            WriteF(writer, PlayerData.Abilities.CurrentMp); //	mp

            WriteF(writer, 66);
            WriteF(writer, 67);
            WriteF(writer, 68);

            WriteF(writer, PlayerData.Abilities.TimeRestorePoints); //Taming restore point
            WriteF(writer, 70);
            WriteF(writer, PlayerData.Abilities.AbilityPoints); //хер значет (де плюсик стоит) 
            WriteF(writer, 72);
            WriteF(writer, 73);
            WriteF(writer, 74);
            WriteF(writer, 75);
            WriteF(writer, 76);
            WriteF(writer, 111);
            WriteF(writer, 112);
            WriteF(writer, 113);
            WriteF(writer, 114);
            WriteF(writer, 115);
            WriteF(writer, 116);
            WriteF(writer, 117);
            WriteF(writer, 118);
            WriteF(writer, 119);
            WriteF(writer, 120);
            WriteF(writer, 121);
            WriteF(writer, 122);

            WriteD(writer, 0xb);
            WriteD(writer, 0xffffffff);
            WriteD(writer, 1);
            WriteD(writer, 0);
        }
    }
}