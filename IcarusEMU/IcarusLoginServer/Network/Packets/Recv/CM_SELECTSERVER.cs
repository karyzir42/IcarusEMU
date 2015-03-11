// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using IcarusLoginServer.Network.Packets.Processors;
using IcarusLoginServer.Network.Packets.Send;

namespace IcarusLoginServer.Network.Packets.Recv
{
    internal class CM_SELECTSERVER : ARecvPacket
    {
        protected int SelectedServer;

        public override void Read()
        {
            SelectedServer = ReadD();
        }

        public override void Process()
        {
            new SM_SELECTSERVER(Connection, SelectedServer).Send(Connection);
        }
    }
}