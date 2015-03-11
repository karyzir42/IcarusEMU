// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System;
using System.Collections.Generic;
using Hik.Communication.Scs.Communication;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.ScsServices.Client;
using Hik.Communication.ScsServices.Service;
using IcarusCommons.Configurations;
using IcarusCommons.Models.Account;
using IcarusCommons.Models.Bridge;
using IcarusCommons.Utils;

namespace IcarusLoginServer.Managers
{
    public static class LoginManager
    {
        public static IScsServiceClient<ILoginContract> LoginServiceClient { get; set; }

        public static void Init()
        {
            LoginServiceClient = ScsServiceClientBuilder.CreateClient<ILoginContract>(new ScsTcpEndPoint
                ("127.0.0.1", Convert.ToInt32(CIniLoader.ReadValue("NETWORK", "LOGIN_BRIDGE_PORT"))));

            LoginServiceClient.ConnectTimeout = 3000;
            LoginServiceClient.Timeout = 1000;

            while (true)
            {
                if (LoginServiceClient.CommunicationState != CommunicationStates.Connected)
                {
                    try
                    {
                        LoginServiceClient.Connect();

                        if (LoginServiceClient.CommunicationState == CommunicationStates.Connected)
                        {
                            Log.Info("LoginServer authed by validation server");
                            break;
                        }
                    }
                    catch
                    {
                        Log.Error("Connection was not stabilished, reconnecting..");
                    }
                }
            }
        }

        [ScsService]
        public interface ILoginContract
        {
            AccountData CheckedAccountData(string token);
            List<LobbyServerData> GetLobbyServers();
        }
    }
}