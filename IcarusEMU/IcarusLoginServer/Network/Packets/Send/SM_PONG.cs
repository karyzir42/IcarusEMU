﻿// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.IO;
using IcarusLoginServer.Network.Packets.Processors;

namespace IcarusLoginServer.Network.Packets.Send
{
    internal class SM_PONG : ASendPacket
    {
        public override void Write(BinaryWriter writer)
        {
            WriteC(writer, 0); //Zero
        }
    }
}