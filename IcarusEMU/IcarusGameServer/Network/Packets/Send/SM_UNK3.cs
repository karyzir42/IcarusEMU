// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.IO;
using IcarusGameServer.Network.Packets.Processors;

namespace IcarusGameServer.Network.Packets.Send
{
    internal class SM_UNK3 : ASendPacket
    {
        protected byte Val;

        public SM_UNK3(byte val)
        {
            Val = val;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteC(writer, Val);
            WriteC(writer, 1);
        }
    }
}