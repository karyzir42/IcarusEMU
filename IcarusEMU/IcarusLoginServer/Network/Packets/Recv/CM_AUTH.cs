// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using IcarusCommons.Utils;
using IcarusLoginServer.Network.Packets.Processors;
using IcarusLoginServer.Services;

namespace IcarusLoginServer.Network.Packets.Recv
{
    internal class CM_AUTH : ARecvPacket
    {
        protected string Hash;

        public override void Read()
        {
            ReadC();
            Hash = ReadSs();
        }

        public override void Process()
        {
            Log.Info("HASH READED: {0}", Hash);

            AuthorizationService.CheckToken(Connection, Hash);
        }
    }
}