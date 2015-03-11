// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.IO;
using IcarusLobbyServer.Network.Packets.Processors;

namespace IcarusLobbyServer.Network.Packets.Send
{
    internal class SM_RESTORE_CONNECTION : ASendPacket
    {
        public override void Write(BinaryWriter writer)
        {
            WriteD(writer, 0);
            WriteD(writer, 5);
            WriteD(writer, 0x3fb60000);
            WriteD(writer, 1);
            WriteD(writer, 0);
        }
    }
}