// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using IcarusCommons.Models.Creature;
using IcarusCommons.Models.Player;
using IcarusCommons.Structures.Global;
using IcarusCommons.Utils;
using IcarusGameServer.Network.Packets.Processors;
using IcarusGameServer.Network.Packets.Send;
using IcarusGameServer.Services;

namespace IcarusGameServer.Network.Packets.Recv
{
    internal class CM_CHAT : ARecvPacket
    {
        protected string Message;
        protected static Position Pos;
        protected static Mob Mob;

        public override void Read()
        {
            ReadD();
            ReadD();
            ReadD();
            ReadD();
            ReadD();
            ReadD();
            ReadD();
            ReadD();
            ReadD();
            ReadD();
            ReadH();
            int messageLen = ReadH();

            Message = ReadSs();
        }

        public override void Process()
        {
            Log.Debug(Message);

            var bot = new Player(new PlayerData()) {PlayerData = {Sex = SexEnum.Female}};
            bot.PlayerData.Storage = Connection.ActivePlayer.PlayerData.Storage;

            if (Message == "test")
            {
                Pos = Connection.ActivePlayer.PlayerData.Position;
                Mob = new Mob
                {
                    Animation = 0,
                    Unk1 = 1,
                    Id = 0x00030007,
                    Level = 3,
                    Position = Pos,
                    Unk9 = 0x00020007,
                    Unk3 = 0,
                    Type1 = 2,
                    Unk11 = 1,
                    Unk13 = 0xffffffff,
                    Unk14 = 0x6ff400ff,
                    Unk15 = 2,
                    Unk16 = 0xffffffff,
                    Unk17 = 0xffffffff,
                    Unk18 = 0xffffffff,
                    Unk19 = 0xffffffff,
                    Unk20 = 0x60ad3400,
                    Unk21 = 0,
                    Unk22 = 0,
                    Unk2 = 0,
                    Unk10 = 0x01000000,
                    Unk12 = 0,
                    Unk4 = 0,
                    Unk5 = 0,
                    Unk6 = 0,
                    Unk7 = 0,
                    Unk8 = 0,
                    MovementType = 2 //2 stand, 5 move
                };
                new SM_GAMEOBJECT_SPAWN(Mob).Send(Connection);
                return;
            }
            if (Message == "Move")
            {
                new SM_GAMEOBJECT_MOVE(Mob, Connection.ActivePlayer.Position).Send(Connection);
            }
            if (Message == "inv")
            {
                StorageService.AddItem(718, 3, 1, 1, Connection.ActivePlayer);


                new SM_INVENTORY_RESPONSE(3, Connection.ActivePlayer.PlayerData).Send(Connection);
            }
            if (Message == "spawn")
            {
                Pos = Connection.ActivePlayer.PlayerData.Position;
                new SM_PLAYER_INFO(bot, Pos).Send(Connection);
                //new SM_CHARACTER_STYLE(Connection.ActivePlayer.PlayerData).Send(Connection);
            }
            if (Message == "come")
            {
                Pos = Connection.ActivePlayer.PlayerData.Position;
                new SM_MOVE(bot, Connection.ActivePlayer.Position, Pos).Send(Connection);
            }
            if (Message == "tp")
            {
                new SM_SETAREA(Connection.ActivePlayer).Send(Connection);
            }
            new SM_CHAT_MESSAGE(Connection.ActivePlayer, Message).Send(Connection);
        }
    }
}