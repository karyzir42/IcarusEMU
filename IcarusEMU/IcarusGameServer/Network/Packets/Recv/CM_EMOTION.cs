﻿// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using IcarusGameServer.Network.Packets.Processors;
using IcarusGameServer.Network.Packets.Send;

namespace IcarusGameServer.Network.Packets.Recv
{
    internal class CM_EMOTION : ARecvPacket
    {
        protected string Emotion;

        public override void Read()
        {
            ReadD();
            Emotion = ReadSs();
        }

        public override void Process()
        {
            new SM_EMOTION(Connection.ActivePlayer, Emotion).Send(Connection);
        }
    }
}