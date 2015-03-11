// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.IO;
using IcarusLobbyServer.Network.Packets.Processors;

namespace IcarusLobbyServer.Network.Packets.Send
{
    internal class SM_STARTGAME : ASendPacket
    {
        protected int CharacterId;

        public SM_STARTGAME(int charId)
        {
            CharacterId = charId;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteD(writer, 0);
            WriteD(writer, CharacterId);
            WriteB(writer, "7F000001");
            WriteH(writer, 6001);
            WriteH(writer, 1);
            WriteD(writer, CharacterId);
            WriteD(writer, 0xa146a5a6);
        }
    }
}