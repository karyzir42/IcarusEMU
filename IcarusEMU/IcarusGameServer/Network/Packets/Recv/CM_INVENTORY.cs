// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using IcarusGameServer.Network.Packets.Processors;
using IcarusGameServer.Network.Packets.Send;

namespace IcarusGameServer.Network.Packets.Recv
{
    internal class CM_INVENTORY : ARecvPacket
    {
        protected byte Unk;

        public override void Read()
        {
            Unk = (byte) ReadC();
        }

        public override void Process()
        {
            new SM_INVENTORY_RESPONSE(Unk, Connection.ActivePlayer.PlayerData).Send(Connection);
        }
    }
}