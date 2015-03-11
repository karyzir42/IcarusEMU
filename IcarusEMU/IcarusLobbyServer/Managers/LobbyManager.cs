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
using IcarusCommons.Models.Player;
using IcarusCommons.Utils;

namespace IcarusLobbyServer.Managers
{
    public static class LobbyManager
    {
        public static IScsServiceClient<ILobbyServerBridge> LobbyServiceClient { get; set; }

        public static void Init()
        {
            LobbyServiceClient =
                ScsServiceClientBuilder.CreateClient<ILobbyServerBridge>(new ScsTcpEndPoint("127.0.0.1",
                    Convert.ToInt32(CIniLoader.ReadValue("NETWORK", "LOBBYSERVER_BRIDGE_PORT"))));
            LobbyServiceClient.ConnectTimeout = 3000;
            LobbyServiceClient.Timeout = 1000;

            while (true)
            {
                if (LobbyServiceClient.CommunicationState != CommunicationStates.Connected)
                {
                    try
                    {
                        LobbyServiceClient.Connect();
                        string ip = Extensions.ReverseIp(Extensions.ToInt("127.0.0.1"));
                        LobbyServiceClient.ServiceProxy.RegisterLobbyServer(new LobbyServerData
                        {
                            Id = 1,
                            Name = "NecroSiberian",
                            Ip = ip,
                            Port = 5695,
                            MaxPlayers = 100,
                            PlayersOnline = 0,
                            Securitykey = "[NP]",
                            Status = 0
                        });
                        if (LobbyServiceClient.CommunicationState == CommunicationStates.Connected)
                        {
                            Log.Info("LobbyServer authed by validation server");
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
        public interface ILobbyServerBridge
        {
            void RegisterLobbyServer(LobbyServerData info);
            List<PlayerData> GetCharacterList(int accId);
            AccountData GetAccountData(int accId);
            void CreateCharacter(AccountData data, PlayerData plData);
            void DeleteCharacter(AccountData data, int charId);
            void UpdateAccountData(AccountData data);
            bool IsUsedName(string name);
        }
    }
}