// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using IcarusLoginServer.Network.Packets.Processors;
using IcarusLoginServer.Services;

namespace IcarusLoginServer.Network.Packets.Recv
{
    internal class CM_SERVER_LIST : ARecvPacket
    {
        public override void Read()
        {
            //nothing, that just trigger
        }

        public override void Process()
        {
            AuthorizationService.SendServerList(Connection);
        }
    }
}