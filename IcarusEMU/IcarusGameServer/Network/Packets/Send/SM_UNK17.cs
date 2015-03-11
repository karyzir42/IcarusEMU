// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.IO;
using IcarusGameServer.Network.Packets.Processors;

namespace IcarusGameServer.Network.Packets.Send
{
    internal class SM_UNK17 : ASendPacket
    {
        public override void Write(BinaryWriter writer)
        {
            WriteD(writer, 0);
            WriteD(writer, 0x41180003); //quest id and quest stage??
            WriteD(writer, 0x00040183); //x??
            WriteD(writer, 0x00040184); //y??
            WriteD(writer, 0x00040188); //z??
        }
    }
}