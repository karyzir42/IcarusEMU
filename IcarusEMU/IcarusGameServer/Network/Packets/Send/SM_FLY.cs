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
    internal class SM_FLY : ASendPacket
    {
        protected Player Player;

        public SM_FLY(Player player)
        {
            Player = player;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteD(writer, Player.Id);
            WriteD(writer, Player.Position.MovementStatus & 0xff);
        }
    }
}