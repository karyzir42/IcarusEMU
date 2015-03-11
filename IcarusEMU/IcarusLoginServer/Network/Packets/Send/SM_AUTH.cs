// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.IO;
using IcarusLoginServer.Network.Packets.Processors;

namespace IcarusLoginServer.Network.Packets.Send
{
    internal class SM_AUTH : ASendPacket
    {
        protected string Name;

        public SM_AUTH(string name)
        {
            Name = name;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteH(writer, 0);
            WriteH(writer, 0);
            WriteH(writer, 0);
            WriteS(writer, Name);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0);
            WriteD(writer, 0x00010100);
        }
    }
}