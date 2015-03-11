// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.IO;
using IcarusCommons.Models.Player;
using IcarusCommons.Structures.Global;
using IcarusGameServer.Network.Packets.Processors;

namespace IcarusGameServer.Network.Packets.Send
{
    internal class SM_MOUSE_MOVE : ASendPacket
    {
        public Position OldPosition;
        public Position NewPosition;
        protected Player Player;

        public SM_MOUSE_MOVE(Player player, Position oldPos, Position newPos)
        {
            OldPosition = oldPos;
            NewPosition = newPos;
            Player = player;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteC(writer, 1); //action
            WriteC(writer, 0); //status

            WriteC(writer, 0);
            WriteC(writer, 0x88);
            WriteD(writer, Player.Id);

            WriteF(writer, OldPosition.X);
            WriteF(writer, OldPosition.Y);
            WriteF(writer, OldPosition.Z);

            WriteF(writer, NewPosition.X);
            WriteF(writer, NewPosition.Y);
            WriteF(writer, NewPosition.Z);
        }
    }
}