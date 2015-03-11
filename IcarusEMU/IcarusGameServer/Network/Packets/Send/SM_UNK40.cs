// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.IO;
using IcarusGameServer.Network.Packets.Processors;

namespace IcarusGameServer.Network.Packets.Send
{
    internal class SM_UNK40 : ASendPacket
    {
        public override void Write(BinaryWriter writer)
        {
            WriteB(writer, "04D7D1330000000" +
                           "0DF0000000000000" +
                           "0DF0000000000000" +
                           "0DF0000000000000" +
                           "0DF0000000000000" +
                           "0DF000000000000000100000" +
                           "0DF0000000000000" +
                           "0DF0000000000000" +
                           "0DF0000000000000" +
                           "0DF0000000000000" +
                           "0DF000000000000000200000" +
                           "0DF0000000000000" +
                           "0DF0000000000000" +
                           "0DF0000000000000" +
                           "0DF0000000000000" +
                           "0DF000000000000000300000" +
                           "0DF0000000000000" +
                           "0DF0000000000000" +
                           "0DF0000000000000" +
                           "0DF0000000000000" +
                           "0DF00000000000000");
        }
    }
}