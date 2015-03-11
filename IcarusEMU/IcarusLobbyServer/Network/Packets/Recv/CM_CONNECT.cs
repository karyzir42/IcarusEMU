// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using IcarusCommons.Utils;
using IcarusLobbyServer.Network.Packets.Processors;
using IcarusLobbyServer.Services;

namespace IcarusLobbyServer.Network.Packets.Recv
{
    internal class CM_CONNECT : ARecvPacket
    {
        protected int LoginId;

        public override void Read()
        {
            LoginId = ReadD();
        }

        public override void Process()
        {
            Log.Info("AUTHED:{0}", LoginId);

            CharacterService.GetBaseInfoProceed(Connection, LoginId);
        }
    }
}