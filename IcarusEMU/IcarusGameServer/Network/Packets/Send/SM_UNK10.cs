// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.IO;
using IcarusGameServer.Network.Packets.Processors;

namespace IcarusGameServer.Network.Packets.Send
{
    internal class SM_UNK10 : ASendPacket
    {
        public override void Write(BinaryWriter writer)
        {
            WriteB(writer, "000000000B003300350037013800340068006A002F01660034010C000000");
        }
    }
}