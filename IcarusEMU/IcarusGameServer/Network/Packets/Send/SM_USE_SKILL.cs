// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.IO;
using IcarusGameServer.Network.Packets.Processors;

namespace IcarusGameServer.Network.Packets.Send
{
    internal class SM_USE_SKILL : ASendPacket
    {
        protected int Unk;

        public SM_USE_SKILL(int unk)
        {
            Unk = unk;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteD(writer, 0);
            WriteD(writer, Unk);
            WriteD(writer, 0x00000c01);
            WriteD(writer, 0xffffffff);
            WriteD(writer, 0x40500000);
        }
    }
}