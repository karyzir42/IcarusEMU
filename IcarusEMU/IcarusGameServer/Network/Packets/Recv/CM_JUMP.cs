﻿// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using IcarusGameServer.Network.Packets.Processors;

namespace IcarusGameServer.Network.Packets.Recv
{
    internal class CM_JUMP : ARecvPacket
    {
        protected float NowX;
        protected float NowY;
        protected float NowZ;
        protected float NewZ;

        public override void Read()
        {
            ReadD();
            ReadD();
            NowX = ReadF();
            NowY = ReadF();
            NowZ = ReadF();
            NewZ = ReadF();
            ReadD();
        }
    }
}