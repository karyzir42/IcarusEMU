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
    internal class SM_ANIMATION : ASendPacket
    {
        protected Player Player;
        protected int Unk1;
        protected int Unk2;

        public SM_ANIMATION(Player player, int unk1, int unk2)
        {
            Player = player;
            Unk1 = unk1;
            Unk2 = unk2;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteD(writer, Player.Id);
            WriteD(writer, 0xbe76);
            WriteD(writer, Unk1);
            WriteD(writer, Unk2);
            WriteD(writer, 0x41c1e768);
            WriteD(writer, 1);
            WriteD(writer, Player.BattleModel.TargetId);
            WriteD(writer, 0x0c3d);
            WriteD(writer, 0);

            WriteD(writer, 0x3ff20c55);
            WriteD(writer, 0x752a20ff);
            WriteD(writer, 0);
            WriteD(writer, 0x3a484bf0);
            WriteD(writer, 0xff);
            WriteD(writer, 0);
        }
    }
}