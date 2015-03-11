// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.IO;
using IcarusGameServer.Network.Packets.Processors;

namespace IcarusGameServer.Network.Packets.Send
{
    internal class SM_REMOVE_ITEM : ASendPacket
    {
        protected int SId;

        public SM_REMOVE_ITEM(int sid)
        {
            SId = sid;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteH(writer, (short) SId);
            WriteH(writer, 0);
            WriteD(writer, 5);
        }
    }
}