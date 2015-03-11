﻿// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using IcarusGameServer.Network.Packets.Processors;
using IcarusGameServer.Services;

namespace IcarusGameServer.Network.Packets.Recv
{
    internal class CM_CONNECT : ARecvPacket
    {
        protected int CharacterId;

        public override void Read()
        {
            int CharId = ReadD();

            CharacterId = CharId;
            ReadD();
            ReadH();
            ReadH();
        }

        public override void Process()
        {
            GameService.SetAccountDataProcess(Connection, CharacterId);
        }
    }
}