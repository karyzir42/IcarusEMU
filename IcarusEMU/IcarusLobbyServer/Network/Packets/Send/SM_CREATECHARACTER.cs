// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System;
using System.IO;
using IcarusCommons.Models.Player;
using IcarusLobbyServer.Network.Packets.Processors;

namespace IcarusLobbyServer.Network.Packets.Send
{
    internal class SM_CREATECHARACTER : ASendPacket
    {
        protected PlayerData Player;
        protected byte[] CharName = new byte[20];
        protected int MaxCharacter = 6; //TODO move on config

        public SM_CREATECHARACTER(PlayerData player)
        {
            Player = player;

            for (int i = 0; i < GetBytes(Player.Name).Length; i++)
                CharName[i] = GetBytes(Player.Name)[i];
        }

        public override void Write(BinaryWriter writer)
        {
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, Player.PlayerId);
            WriteB(writer, CharName);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteH(writer, 0);
            WriteH(writer, 0);
            WriteH(writer, (short) Player.ClassType);
            WriteH(writer, 0);
            WriteH(writer, 1);
            WriteH(writer, (short) Player.Sex);

            WriteD(writer, 0xffffffff);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0xb1);

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

            WriteD(writer, 0xffffffff);
            WriteD(writer, 0);
            WriteD(writer, 0xffffffff);
            WriteD(writer, 0xffffffff);
            WriteD(writer, 0xffffffff);

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

            WriteD(writer, 0x01000000);
            WriteD(writer, 1);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0x00010064); //Armor
            WriteD(writer, 0);
            WriteD(writer, 0xffffffff);
            WriteD(writer, 0xffffffff);
            WriteD(writer, 0xffffffff);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0xffffffff);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0x00010065); //Foot
            WriteD(writer, 0);
            WriteD(writer, 0xffffffff);
            WriteD(writer, 0xffffffff);
            WriteD(writer, 0xffffffff);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0xffffffff);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0xffffffff);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0xffffffff);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0xffffffff);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0xffffffff);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0xffffffff);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteH(writer, (short) Player.Style.unk0);
            WriteD(writer, Player.Style.unk1);
            WriteC(writer, (byte) Player.Style.eye);
            WriteC(writer, (byte) Player.Style.unk2);
            WriteC(writer, (byte) Player.Style.eyebrows);
            WriteC(writer, (byte) Player.Style.unk3);
            WriteC(writer, (byte) Player.Style.iris);
            WriteH(writer, (short) Player.Style.unk4);
            WriteC(writer, (byte) Player.Style.unk5);
            WriteC(writer, (byte) Player.Style.tatoo);
            WriteH(writer, (short) Player.Style.unk6);
            WriteC(writer, (byte) Player.Style.unk7);
            WriteH(writer, (short) Player.Style.unk81);
            WriteH(writer, (short) Player.Style.unk8);
            WriteD(writer, Player.Style.unk9);
            WriteC(writer, (byte) Player.Style.hair);
            WriteH(writer, (short) Player.Style.unk10);
            WriteC(writer, (byte) Player.Style.unk11);
            WriteD(writer, Player.Style.unk12);
            WriteD(writer, Player.Style.unk13);
            WriteD(writer, Player.Style.unk14);
            WriteD(writer, Player.Style.color_lips);
            WriteD(writer, Player.Style.color_eyeb);
            WriteD(writer, Player.Style.color_iris);
            WriteD(writer, Player.Style.color_eyebrows);
            WriteD(writer, Player.Style.color_eye);
            WriteD(writer, Player.Style.unk15);
            WriteD(writer, Player.Style.unk16);
            WriteD(writer, Player.Style.unk17);
            WriteD(writer, Player.Style.unk18);
            WriteD(writer, Player.Style.unk19);
            WriteD(writer, Player.Style.unk20);
            WriteD(writer, Player.Style.unk21);
            WriteD(writer, Player.Style.unk22);
            WriteD(writer, Player.Style.unk23);
            WriteD(writer, Player.Style.unk24);
            WriteD(writer, Player.Style.unk25);
            WriteD(writer, Player.Style.unk26);
            WriteD(writer, Player.Style.unk27);
            WriteD(writer, Player.Style.unk28);
            WriteD(writer, Player.Style.unk29);
            WriteD(writer, Player.Style.unk30);
            WriteD(writer, Player.Style.unk31);
            WriteD(writer, Player.Style.unk32);
            WriteD(writer, Player.Style.unk33);
            WriteC(writer, (byte) Player.Style.unk34);
            WriteC(writer, (byte) Player.Style.unk35);
            WriteC(writer, (byte) Player.Style.unk36);
            WriteH(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);

            WriteD(writer, 1); // Can or not
        }

        private static byte[] GetBytes(string str)
        {
            var bytes = new byte[str.Length*sizeof (char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
    }
}