// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using IcarusGameServer.Network.Packets.Processors;
using IcarusGameServer.Network.Packets.Send;

namespace IcarusGameServer.Network.Packets.Recv
{
    internal class CM_UNK11 : ARecvPacket
    {
        public override void Read()
        {
        }

        public override void Process()
        {
            new SM_UNK17().Send(Connection);
            new SM_ENTER_WORLD(Connection.ActivePlayer.PlayerData).Send(Connection);
            new SM_UNK20().Send(Connection);
            new SM_UNK21().Send(Connection);
            new SM_UNK40().Send(Connection);
        }
    }
}