// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.IO;
using IcarusGameServer.Network.Packets.Processors;

namespace IcarusGameServer.Network.Packets.Send
{
    internal class SM_UNK4 : ASendPacket
    {
        public override void Write(BinaryWriter writer)
        {
            WriteD(writer, 0);
            WriteD(writer, 0x01000101);
            WriteD(writer, 0x01000101);
            WriteD(writer, 0x01010101);
            WriteD(writer, 0x01000001);
            WriteD(writer, 0x00000101);
            WriteD(writer, 0x01000101);
        }
    }
}