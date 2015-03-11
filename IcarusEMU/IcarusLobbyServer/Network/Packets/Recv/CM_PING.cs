// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using IcarusLobbyServer.Network.Packets.Processors;
using IcarusLobbyServer.Network.Packets.Send;

namespace IcarusLobbyServer.Network.Packets.Recv
{
    internal class CM_PING : ARecvPacket
    {
        public override void Read()
        {
        }

        public override void Process()
        {
            new SM_PONG().Send(Connection);
        }
    }
}