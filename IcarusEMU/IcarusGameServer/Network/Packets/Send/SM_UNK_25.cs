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
    internal class SM_UNK_25 : ASendPacket
    {
        protected int Unk;
        protected int Unk1;
        protected int Unk2;
        protected int Unk3;
        protected int Unk4;
        protected Player Player;

        public SM_UNK_25(Player player, int unk, int unk1, int unk2, int unk3, int unk4)
        {
            Player = player;

            Unk = unk;
            Unk1 = unk1;
            Unk2 = unk2;
            Unk3 = unk3;
            Unk4 = unk4;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteD(writer, Player.Id);
            WriteD(writer, 0x1019);
            WriteD(writer, Unk1);
            WriteD(writer, Unk2);
            WriteD(writer, Unk3);
            WriteD(writer, 0x439d8000);
            WriteD(writer, Unk4);
            WriteD(writer, 0);
            WriteD(writer, Player.Id);
            WriteD(writer, 0x00000c3c);
            WriteD(writer, 0);

            WriteD(writer, 0x3ff20c55);
            WriteD(writer, 0x4050aeff);
            WriteD(writer, Unk);
            WriteD(writer, 1);

            WriteD(writer, 0xffffffff);
            WriteD(writer, 0x40500000);
            WriteD(writer, 1);
            WriteD(writer, 0x3ff29cfe);
            WriteD(writer, 1);
            WriteD(writer, 0x3517f400);
            WriteD(writer, 1);
            WriteD(writer, 0x3c3939ff);
            WriteD(writer, 1);
        }
    }
}