// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.IO;
using IcarusCommons.Models.Player;
using IcarusCommons.Structures.Chat;
using IcarusCommons.Utils;
using IcarusGameServer.Network.Packets.Processors;

namespace IcarusGameServer.Network.Packets.Send
{
    internal class SM_CHAT_MESSAGE : ASendPacket
    {
        protected Player Player;
        protected string Message;
        protected string Header;
        protected ChatChannelsEnum ChannelType;

        protected bool IsSystem = false;

        public SM_CHAT_MESSAGE(Player player, string msg)
        {
            Player = player;
            Message = msg;
        }

        public SM_CHAT_MESSAGE(string header, string message, ChatChannelsEnum channelType)
        {
            Header = header;
            Message = message;
            ChannelType = channelType;
            IsSystem = true;
        }

        public override void Write(BinaryWriter writer)
        {
            if (!IsSystem)
            {
                var buffName = new byte[20];
                var bufferedName = Extensions.GetBytes(Player.PlayerData.Name);
                for (int i = 0; i < bufferedName.Length; i++)
                    buffName[i] = bufferedName[i];
                WriteH(writer, 0);
                WriteB(writer, buffName);
                WriteH(writer, 0);

                WriteD(writer, 0);
                WriteD(writer, 0);
                WriteD(writer, 0);
                WriteD(writer, 0);
                WriteD(writer, 5);
                WriteD(writer, Player.PlayerData.PlayerId);

                WriteD(writer, 0);
                WriteD(writer, 0);

                WriteD(writer, 0xFF);
                WriteD(writer, 0);
                WriteD(writer, 0);
                WriteD(writer, 0);
                WriteD(writer, 0);
                WriteD(writer, 0);
                WriteD(writer, 0);

                WriteH(writer, 0);
                WriteH(writer, (short) Message.Length);
                WriteSs(writer, Message);
                WriteD(writer, 0);
                return;
            }

            var buffHeader = new byte[28];
            var header = Extensions.GetBytes(Header);

            for (int h = 0; h < header.Length; h++) buffHeader[h] = header[h];

            WriteH(writer, (short) ChannelType);
            WriteB(writer, buffHeader);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteH(writer, 0);
            WriteH(writer, 1);
            WriteH(writer, 0);
            WriteD(writer, 0); // player id, Можно оставить пустым //B1C40880
            WriteB(writer, "0000803F");
            WriteB(writer, new byte[32]);
            WriteH(writer, 0);
            WriteH(writer, (short) Message.Length);
            WriteSs(writer, Message);
            WriteD(writer, 0);
        }
    }
}