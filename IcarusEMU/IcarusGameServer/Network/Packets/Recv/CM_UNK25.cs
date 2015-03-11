// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using IcarusGameServer.Network.Packets.Processors;
using IcarusGameServer.Network.Packets.Send;

namespace IcarusGameServer.Network.Packets.Recv
{
    internal class CM_UNK25 : ARecvPacket
    {
        protected int Val;

        public override void Read()
        {
            Val = ReadC();
        }

        public override void Process()
        {
            new SM_UNK1506(Val).Send(Connection);
            new SM_UNK1606(Val).Send(Connection);
        }
    }
}