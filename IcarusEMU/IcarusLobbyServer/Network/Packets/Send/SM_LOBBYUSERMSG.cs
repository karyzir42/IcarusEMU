// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.IO;
using IcarusLobbyServer.Network.Packets.Processors;

namespace IcarusLobbyServer.Network.Packets.Send
{
    internal class SM_LOBBYUSERMSG : ASendPacket
    {
        protected int MsgId;

        public SM_LOBBYUSERMSG(int msgId)
        {
            MsgId = msgId;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteD(writer, MsgId);
        }
    }
}