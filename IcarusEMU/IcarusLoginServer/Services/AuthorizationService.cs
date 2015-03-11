// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using IcarusLoginServer.Managers;
using IcarusLoginServer.Network;
using IcarusLoginServer.Network.Packets.Send;

namespace IcarusLoginServer.Services
{
    public static class AuthorizationService
    {
        public static void CheckToken(Connection connection, string token)
        {
            var data = LoginManager.LoginServiceClient.ServiceProxy.CheckedAccountData(token);
            if (data != null)
            {
                connection.AccountData = data;

                new SM_AUTH(connection.AccountData.Login).Send(connection);
            }
            else
                connection.Client.Disconnect();
        }

        public static void SendServerList(Connection connection)
        {
            new SM_SERVER_LIST(LoginManager.LoginServiceClient.ServiceProxy.GetLobbyServers()).Send(connection);
        }
    }
}