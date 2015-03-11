// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.IO;
using IcarusLobbyServer.Network.Packets.Processors;

namespace IcarusLobbyServer.Network.Packets.Send
{
    internal class SM_DELETECHARACTER : ASendPacket
    {
        protected int CharId;
        protected Connection ConnnConnection;

        public SM_DELETECHARACTER(Connection connection, int charId)
        {
            CharId = charId;
            ConnnConnection = connection;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteD(writer, 0);
            WriteD(writer, CharId);
            WriteD(writer, 10);
            WriteD(writer, 0);
            WriteC(writer, 0x66);
            WriteC(writer, 0);
            WriteD(writer, 0);

            new SM_DELETECHARACTER_PARTTWO(CharId).Send(ConnnConnection);
        }
    }
}