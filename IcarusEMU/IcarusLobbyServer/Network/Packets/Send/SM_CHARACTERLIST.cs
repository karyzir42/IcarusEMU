// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using IcarusCommons.Models.Player;
using IcarusCommons.Utils;
using IcarusLobbyServer.Network.Packets.Processors;
using IcarusLobbyServer.Services;

namespace IcarusLobbyServer.Network.Packets.Send
{
    internal class SM_CHARACTERLIST : ASendPacket
    {
        protected List<PlayerData> Players;
        protected int MaxPlayers = CharacterService.MaxPlayersOnAccount;

        public SM_CHARACTERLIST(List<PlayerData> players)
        {
            Players = players;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteD(writer, 0);
            WriteC(writer, 1);

            if (Players.Count == 0)
            {
                WriteC(writer, 1);
                WriteH(writer, 0);
                WriteD(writer, 0);
                WriteD(writer, 0);
                return;
            }

            WriteC(writer, (byte) (Players.Count >= MaxPlayers ? 0 : 1));
            WriteH(writer, (short) Players.Count);
            WriteD(writer, Players.First().PlayerId);
            WriteD(writer, 0);
            var name = new byte[20];

            foreach (var playerData in Players)
            {
                WriteD(writer, playerData.PlayerId);
                for (int i = 0; i < Extensions.GetBytes(playerData.Name).Length; i++)
                    name[i] = Extensions.GetBytes(playerData.Name)[i];

                WriteB(writer, name);
                WriteD(writer, 0);
                WriteD(writer, 0);
                WriteH(writer, 0);
                WriteH(writer, 0);
                WriteH(writer, 0);
                WriteH(writer, 0);
                WriteH(writer, 0);
                WriteH(writer, 0);
                WriteD(writer, (int) playerData.ClassType);
                WriteH(writer, (short) playerData.Level);
                WriteC(writer, (byte) playerData.Sex);
                WriteC(writer, 0);
                WriteC(writer, 12);
                WriteC(writer, 0);
                WriteC(writer, 0);
                WriteC(writer, 0);
                WriteD(writer, 0x433f0000); //unk 00003f43
                WriteD(writer, 0x44580800); //unk 00085844
                WriteD(writer, 0x42c40000); //unk 0000c442
                WriteD(writer, 0);
                WriteD(writer, 0);
                WriteD(writer, 0xb1000000);
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
                WriteD(writer, 0xffffffff);
                WriteD(writer, 0);
                WriteD(writer, 0xffffffff);
                WriteD(writer, 0xffffffff);
                WriteD(writer, 0xffffffff);
                WriteD(writer, 0);
                WriteD(writer, 0xd9efa578); //unk
                WriteD(writer, 2);
                WriteD(writer, 0);
                WriteD(writer, 0);
                WriteD(writer, 0);
                WriteD(writer, 0);
                WriteD(writer, 0);
                WriteD(writer, 0);
                WriteD(writer, 0);
                WriteD(writer, 0);
                WriteD(writer, 0);
                WriteD(writer, 0x02800000);

                WriteC(writer, 0x32);
                WriteC(writer, 23);
                WriteC(writer, 0x2);
                WriteC(writer, 0x3);

                WriteC(writer, 0x0e);
                WriteC(writer, 0x0b);

                WriteH(writer, 0x0000);
                WriteD(writer, 0x0145);
                WriteD(writer, 0);

                WriteD(writer, playerData.Storage.Storage.First(s => s.Slot == 1).ItemId); //0x00010064

                WriteD(writer, 0xffff0000);
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

                WriteD(writer, playerData.Storage.Storage.First(s => s.Slot == 7).ItemId); //0x0001006d

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
                WriteD(writer, 0xffffffff);
                WriteD(writer, 0);
                WriteD(writer, 0);
                WriteD(writer, 0);

                #region Style 

                WriteH(writer, (short) playerData.Style.unk0);
                WriteD(writer, playerData.Style.unk1);
                WriteC(writer, (byte) playerData.Style.eye);
                WriteC(writer, (byte) playerData.Style.unk2);
                WriteC(writer, (byte) playerData.Style.eyebrows);
                WriteC(writer, (byte) playerData.Style.unk3);
                WriteC(writer, (byte) playerData.Style.iris);
                WriteH(writer, (short) playerData.Style.unk4);
                WriteC(writer, (byte) playerData.Style.unk5);
                WriteC(writer, (byte) playerData.Style.tatoo);
                WriteH(writer, (short) playerData.Style.unk6);
                WriteC(writer, (byte) playerData.Style.unk7);
                WriteH(writer, (short) playerData.Style.unk81);
                WriteH(writer, (short) playerData.Style.unk8);
                WriteD(writer, playerData.Style.unk9);
                WriteC(writer, (byte) playerData.Style.hair);
                WriteH(writer, (short) playerData.Style.unk10);
                WriteC(writer, (byte) playerData.Style.unk11);
                WriteD(writer, playerData.Style.unk12);
                WriteD(writer, playerData.Style.unk13);
                WriteD(writer, playerData.Style.unk14);
                WriteD(writer, playerData.Style.color_lips);
                WriteD(writer, playerData.Style.color_eyeb);
                WriteD(writer, playerData.Style.color_iris);
                WriteD(writer, playerData.Style.color_eyebrows);
                WriteD(writer, playerData.Style.color_eye);
                WriteD(writer, playerData.Style.unk15);
                WriteD(writer, playerData.Style.unk16);
                WriteD(writer, playerData.Style.unk17);
                WriteD(writer, playerData.Style.unk18);
                WriteD(writer, playerData.Style.unk19);
                WriteD(writer, playerData.Style.unk20);
                WriteD(writer, playerData.Style.unk21);
                WriteD(writer, playerData.Style.unk22);
                WriteD(writer, playerData.Style.unk23);
                WriteD(writer, playerData.Style.unk24);
                WriteD(writer, playerData.Style.unk25);
                WriteD(writer, playerData.Style.unk26);
                WriteD(writer, playerData.Style.unk27);
                WriteD(writer, playerData.Style.unk28);
                WriteD(writer, playerData.Style.unk29);
                WriteD(writer, playerData.Style.unk30);
                WriteD(writer, playerData.Style.unk31);
                WriteD(writer, playerData.Style.unk32);
                WriteD(writer, playerData.Style.unk33);
                WriteC(writer, (byte) playerData.Style.unk34);
                WriteC(writer, (byte) playerData.Style.unk35);
                WriteC(writer, (byte) playerData.Style.unk36);
                WriteC(writer, 0);
                WriteH(writer, 0);
                WriteD(writer, 0);
                WriteD(writer, 0);
                WriteD(writer, 0);
                WriteD(writer, 0);
                WriteD(writer, 0);
                WriteD(writer, 0);
                WriteD(writer, 0);
                WriteD(writer, 0);

                #endregion
            }
        }
    }
}