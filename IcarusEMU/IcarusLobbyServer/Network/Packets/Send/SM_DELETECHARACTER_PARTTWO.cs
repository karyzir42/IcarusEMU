// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.IO;
using IcarusLobbyServer.Network.Packets.Processors;

namespace IcarusLobbyServer.Network.Packets.Send
{
    internal class SM_DELETECHARACTER_PARTTWO : ASendPacket
    {
        protected int CharId;

        public SM_DELETECHARACTER_PARTTWO(int charId)
        {
            CharId = charId;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteC(writer, 0);
            WriteD(writer, CharId);
            WriteD(writer, 0x00013601);
        }
    }
}