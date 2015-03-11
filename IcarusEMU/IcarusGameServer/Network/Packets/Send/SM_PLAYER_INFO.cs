// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.IO;
using System.Linq;
using IcarusCommons.Models.Player;
using IcarusCommons.Structures.Global;
using IcarusCommons.Utils;
using IcarusGameServer.Network.Packets.Processors;

namespace IcarusGameServer.Network.Packets.Send
{
    internal class SM_PLAYER_INFO : ASendPacket
    {
        protected Position SpawnedPos;
        protected Player Player;

        protected byte[] CharName = new byte[24];

        public SM_PLAYER_INFO(Player player, Position spawnedPos)
        {
            SpawnedPos = spawnedPos;
            Player = player;
            for (int i = 0; i < Extensions.GetBytes(player.PlayerData.Name).Length; i++)
                CharName[i] = Extensions.GetBytes(Player.PlayerData.Name)[i];
        }

        public override void Write(BinaryWriter writer)
        {
            const string femaleObjectStringId = "01";
            const string maleObjectStringId = "0A";

            string sex = "";

            switch (Player.PlayerData.Sex)
            {
                case SexEnum.Male:
                    sex = maleObjectStringId;
                    break;
                case SexEnum.Female:
                    sex = femaleObjectStringId;
                    break;
            }

            WriteH(writer, 1); //unk 01 spawn 00 no spawn
            WriteH(writer, 0);
            WriteD(writer, Player.UID);
            WriteD(writer, 0);

            WriteF(writer, SpawnedPos.X);
            WriteF(writer, SpawnedPos.Y);
            WriteF(writer, SpawnedPos.Z);
            WriteF(writer, SpawnedPos.Heading);

            WriteH(writer, 0); // 1 sitdown, 2 angry, 0 stay
            WriteB(writer, "000010000000100000000000000");
            WriteH(writer, (short) Player.PlayerData.Level);
            WriteB(writer, new byte[16]); //B840333313403333933F

            WriteB(writer, "61430000");
            WriteB(writer, "61430000");
            WriteB(writer, "E1430000");
            WriteB(writer, "E1430000");
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteB(writer, new byte[16]);
            WriteD(writer, 0);
            WriteB(writer, new byte[264]);
            WriteH(writer, 0);
            WriteH(writer, 3);
            WriteD(writer, 0);

            WriteH(writer, 0);
            WriteH(writer, 1);
            WriteH(writer, 0);
            WriteH(writer, 5);
            WriteH(writer, 0);

            WriteB(writer, "FC040580");
            WriteB(writer, "FF000000");
            WriteD(writer, 0);

            WriteB(writer, "00000000");
            WriteB(writer, "05001400");
            WriteB(writer, "000000FF");
            WriteC(writer, 1);

            WriteC(writer, Player.PlayerData.ClassType);
            WriteB(writer, sex);
            WriteC(writer, 0);
            WriteB(writer, CharName);
            WriteB(writer, new byte[24]);

            #region Armor

            if (Player.PlayerData.Storage != null)
                WriteD(writer,
                    Player.PlayerData.Storage.Storage != null
                        ? Player.PlayerData.Storage.Storage.First(s => s.Slot == 1).ItemId
                        : 0x00010064);
            else if (Player.PlayerData.Storage == null)
                WriteD(writer, 0x00010064);

            WriteD(writer, 0);

            #endregion

            WriteD(writer, 0xFFFFFFFF); //Unk
            WriteD(writer, 0);
            WriteD(writer, 0xFFFFFFFF); //Unk 
            WriteD(writer, 0);

            #region Foot

            if (Player.PlayerData.Storage != null)
                WriteD(writer,
                    Player.PlayerData.Storage.Storage != null ? Player.PlayerData.Storage.Storage.First(s => s.Slot == 7).ItemId : 65643);
            else if (Player.PlayerData.Storage == null)
                WriteD(writer, 65643);

            WriteD(writer, 0);

            #endregion

            WriteD(writer, 0xFFFFFFFF); //Unk
            WriteD(writer, 0);
            WriteD(writer, 0xFFFFFFFF); //Unk
            WriteD(writer, 0);
            WriteD(writer, 0xFFFFFFFF); //Unk
            WriteD(writer, 0);
            WriteD(writer, 0xFFFFFFFF); //Unk
            WriteD(writer, 0);
            WriteD(writer, 0xFFFFFFFF); //Unk
            WriteD(writer, 0);

            #region Weapon

            if (Player.PlayerData.Storage != null)
                WriteD(writer,
                    Player.PlayerData.Storage.Storage != null
                        ? Player.PlayerData.Storage.Storage.First(s => s.Slot == 3).ItemId
                        : 718);
            else if (Player.PlayerData.Storage == null)
                WriteD(writer, 718);

            WriteD(writer, 0);

            #endregion

            WriteD(writer, 0xFFFFFFFF); //??
            WriteD(writer, 0);
            WriteD(writer, 0xFFFFFFFF); //??
            WriteD(writer, 0);
            WriteD(writer, 0xFFFFFFFF); //??
            WriteD(writer, 0);
            WriteD(writer, 0xFFFFFFFF); //??
            WriteD(writer, 0);

            WriteB(writer, new byte[32]);
            WriteD(writer, 0);
            WriteD(writer, 2);

            WriteD(writer, 0xFFFFFFFF); //??
            WriteD(writer, 0);

            WriteD(writer, 0);
            WriteD(writer, 0);

            WriteD(writer, 3);
            WriteD(writer, 0);
            WriteD(writer, 0);
        }
    }
}