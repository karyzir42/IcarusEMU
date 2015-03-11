// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.IO;
using IcarusCommons.Models.Creature;
using IcarusGameServer.Network.Packets.Processors;

namespace IcarusGameServer.Network.Packets.Send
{
    internal class SM_GAMEOBJECT_SPAWN : ASendPacket
    {
        protected Mob Mob;

        public SM_GAMEOBJECT_SPAWN(Mob mob)
        {
            Mob = mob;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteD(writer, 1);
            WriteD(writer, Mob.UID);
            WriteD(writer, 2);
            WriteF(writer, Mob.Position.X);
            WriteF(writer, Mob.Position.Y);
            WriteF(writer, Mob.Position.Z);
            WriteF(writer, Mob.Position.Heading);
            WriteD(writer, 1); // animation ?
            WriteD(writer, 0);
            WriteD(writer, Mob.Type1); // 0 - npc yellow, 1 - npc blue, 6 - mob
            WriteD(writer, 0);
            WriteD(writer, Mob.Level);
            WriteD(writer, 0x40b00000);
            WriteF(writer, 666); //curr hp
            WriteF(writer, 666); //curr hp base
            WriteD(writer, 0x43280000);
            WriteD(writer, 0x43280000);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);

            WriteD(writer, 0);
            WriteH(writer, 0);
            WriteD(writer, 0);
            WriteH(writer, 0);

            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, Mob.Id);
            WriteD(writer, 0x02000000);
            WriteD(writer, 1);
            WriteD(writer, 0);
            WriteD(writer, 0xffffffff);
            WriteD(writer, 0x6ff400ff);
            WriteD(writer, 2);
            WriteD(writer, 0xffffffff);
            WriteD(writer, 0xffffffff);
            WriteD(writer, 0xffffffff);
            WriteD(writer, 0xffffffff);
            WriteD(writer, 0x50ad3400);
        }
    }
}