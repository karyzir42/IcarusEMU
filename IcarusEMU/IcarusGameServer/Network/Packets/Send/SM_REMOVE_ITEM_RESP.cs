// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.IO;
using IcarusGameServer.Network.Packets.Processors;

namespace IcarusGameServer.Network.Packets.Send
{
    internal class SM_REMOVE_ITEM_RESP : ASendPacket
    {
        protected bool IsItem;

        public SM_REMOVE_ITEM_RESP(bool isItem)
        {
            IsItem = isItem;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteD(writer, IsItem ? 0 : 1);
        }
    }
}