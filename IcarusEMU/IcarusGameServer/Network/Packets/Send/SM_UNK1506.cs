﻿// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.IO;
using IcarusGameServer.Network.Packets.Processors;

namespace IcarusGameServer.Network.Packets.Send
{
    internal class SM_UNK1506 : ASendPacket
    {
        public int Val;

        public SM_UNK1506(int val)
        {
            Val = val;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteD(writer, 0);
        }
    }
}