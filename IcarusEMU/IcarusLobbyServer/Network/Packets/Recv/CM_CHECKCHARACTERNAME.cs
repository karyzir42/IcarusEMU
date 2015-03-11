// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using IcarusLobbyServer.Managers;
using IcarusLobbyServer.Network.Packets.Processors;
using IcarusLobbyServer.Network.Packets.Send;

namespace IcarusLobbyServer.Network.Packets.Recv
{
    internal class CM_CHECKCHARACTERNAME : ARecvPacket
    {
        protected string CharacterName;

        public override void Read()
        {
            CharacterName = ReadS();
        }

        public override void Process()
        {
            if (CharacterName.Length < 4 || CharacterName.Length > 16)
            {
                new SM_LOBBYUSERMSG(14).Send(Connection);
                return;
            }
            if (LobbyManager.LobbyServiceClient.ServiceProxy.IsUsedName(CharacterName))
            {
                new SM_LOBBYUSERMSG(14).Send(Connection);
                return;
            }
            new SM_LOBBYUSERMSG(0).Send(Connection);
        }
    }
}