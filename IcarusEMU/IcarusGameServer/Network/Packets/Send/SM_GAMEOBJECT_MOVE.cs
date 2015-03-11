// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.IO;
using IcarusCommons.Abstractions.Creature;
using IcarusCommons.Models.Creature;
using IcarusCommons.Structures.Global;
using IcarusGameServer.Network.Packets.Processors;

namespace IcarusGameServer.Network.Packets.Send
{
    internal class SM_GAMEOBJECT_MOVE : ASendPacket
    {
        protected Mob Mob;
        protected Position NewPos;

        public SM_GAMEOBJECT_MOVE(ACreature creature, Position newPos)
        {
            Mob = (Mob) creature;
            NewPos = newPos;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteD(writer, 0);
            WriteD(writer, Mob.UID);
            WriteD(writer, Mob.MovementType);
            WriteF(writer, Mob.Position.X);
            WriteF(writer, Mob.Position.Y);
            WriteF(writer, Mob.Position.Z);
            WriteF(writer, NewPos.X);
            WriteF(writer, NewPos.Y);
            WriteF(writer, NewPos.Z);
        }
    }
}