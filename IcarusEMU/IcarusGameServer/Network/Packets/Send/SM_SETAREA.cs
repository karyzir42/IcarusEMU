// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.IO;
using IcarusCommons.Models.Player;
using IcarusCommons.Utils;
using IcarusGameServer.Network.Packets.Processors;

namespace IcarusGameServer.Network.Packets.Send
{
    internal class SM_SETAREA : ASendPacket
    {
        protected Player Player;
        protected byte[] AreaTemp = new byte[24];
        protected byte[] CharName = new byte[20];

        public SM_SETAREA(Player player)
        {
            Player = player;
            for (int i = 0; i < 24; i++)
                AreaTemp[i] = Extensions.GetBytes(Player.Instance.MapName)[i];
            for (int i = 0; i < Extensions.GetBytes(player.PlayerData.Name).Length; i++)
                CharName[i] = Extensions.GetBytes(Player.PlayerData.Name)[i];
        }

        public override void Write(BinaryWriter writer)
        {
            WriteD(writer, 0);
            WriteD(writer, Player.PlayerData.PlayerId & 0xfffffff);
            WriteB(writer, CharName);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteC(writer, 0);
            WriteC(writer, 0);
            WriteC(writer, (byte) Player.PlayerData.ClassType);
            WriteC(writer, (byte) Player.PlayerData.Sex);
            WriteD(writer, 0x3bd70900);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteH(writer, (short) Player.PlayerData.Level);
            WriteH(writer, 0);
            WriteD(writer, 0);
            WriteH(writer, 0);
            WriteC(writer, 0);
            WriteC(writer, 0);
            WriteC(writer, 13);
            WriteC(writer, 0);
            WriteH(writer, 0);
            WriteD(writer, 0);
            WriteF(writer, Player.PlayerData.Position.X);
            WriteF(writer, Player.PlayerData.Position.Y);
            WriteF(writer, Player.PlayerData.Position.Z);
            WriteD(writer, Player.PlayerData.Position.Heading);
            WriteD(writer, 0x18);
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
            WriteD(writer, 0xffffffff);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0x3fcbd404);
            WriteD(writer, 1);
            WriteD(writer, 0x44cbaf54);
            WriteD(writer, 1);
            WriteD(writer, 0x3344f438);
            WriteD(writer, 1);
            WriteD(writer, 0x059a71d8);
            WriteD(writer, 1);
            WriteB(writer, AreaTemp);
            WriteD(writer, 0x3f000000);
            WriteD(writer, 1);
        }
    }
}