// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.IO;
using IcarusLobbyServer.Network.Packets.Processors;

namespace IcarusLobbyServer.Network.Packets.Send
{
    internal class SM_PIN : ASendPacket
    {
        protected int CharId;
        protected bool Successful;

        public SM_PIN(int charId, bool successful)
        {
            CharId = charId;
            Successful = successful;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteD(writer, 0);
            WriteC(writer, 0);

            WriteC(writer, 2);
            WriteD(writer, 0);
        }
    }
}