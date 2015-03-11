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
    internal class CM_RESTORE_CONNECTION : ARecvPacket
    {
        protected int AccountId;

        public override void Read()
        {
            AccountId = ReadD();
        }

        public override void Process()
        {
            Connection.AccountData = LobbyManager.LobbyServiceClient.ServiceProxy.GetAccountData(AccountId);
            Connection.AccountData.Players = LobbyManager.LobbyServiceClient.ServiceProxy.GetCharacterList(AccountId);

            new SM_RESTORE_CONNECTION().Send(Connection);
        }
    }
}