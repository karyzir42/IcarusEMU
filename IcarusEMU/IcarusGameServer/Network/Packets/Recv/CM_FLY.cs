﻿// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using IcarusGameServer.Network.Packets.Processors;
using IcarusGameServer.Network.Packets.Send;

namespace IcarusGameServer.Network.Packets.Recv
{
    internal class CM_FLY : ARecvPacket
    {
        protected int Status;

        public override void Read()
        {
            Status = ReadC();
        }

        public override void Process()
        {
            bool FlyModeIsActive = Status > 0;
            Connection.ActivePlayer.Position.MovementStatus = Status;

            new SM_FLY(Connection.ActivePlayer).Send(Connection);
        }
    }
}