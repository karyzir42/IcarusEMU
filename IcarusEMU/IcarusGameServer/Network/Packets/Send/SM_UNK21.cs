// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.IO;
using IcarusGameServer.Network.Packets.Processors;

namespace IcarusGameServer.Network.Packets.Send
{
    internal class SM_UNK21 : ASendPacket
    {
        public override void Write(BinaryWriter writer)
        {
            WriteC(writer, 5);
            WriteH(writer, 0x0100);
            WriteH(writer, 0x010b);
            WriteH(writer, 0x010c);
            WriteH(writer, 0x010f);
            WriteH(writer, 0x0113);
        }
    }
}