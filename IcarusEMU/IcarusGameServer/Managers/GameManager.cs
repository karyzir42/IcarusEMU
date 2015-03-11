// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System;
using Hik.Communication.Scs.Communication;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.ScsServices.Client;
using Hik.Communication.ScsServices.Service;
using IcarusCommons.Configurations;
using IcarusCommons.Models.Account;
using IcarusCommons.Models.Bridge;
using IcarusCommons.Models.Player;
using IcarusCommons.Utils;

namespace IcarusGameServer.Managers
{
    public static class GameManager
    {
        public static IScsServiceClient<IGameServerBridge> GameServiceClient { get; set; }

        public static void Init()
        {
            GameServiceClient =
                ScsServiceClientBuilder.CreateClient<IGameServerBridge>(new ScsTcpEndPoint("127.0.0.1",
                    Convert.ToInt32(CIniLoader.ReadValue("NETWORK", "GAMESERVER_BRIDGE_PORT"))));
            GameServiceClient.ConnectTimeout = 3000;
            GameServiceClient.Timeout = 500000;

            while (true)
            {
                if (GameServiceClient.CommunicationState != CommunicationStates.Connected)
                {
                    try
                    {
                        GameServiceClient.Connect();
                        GameServiceClient.ServiceProxy.RegisterGameServer(new GameServerData
                        {
                            Id = 1,
                            Name = "NecroWorldOne",
                            Ip = CIniLoader.ReadValue("NETWORK", "GAMESERVER_IP").ToInt().ReverseIp(),
                            Port = int.Parse(CIniLoader.ReadValue("NETWORK", "GAMESERVER_PORT")),
                            MaxPlayers = 100,
                            PlayersOnline = 0,
                            Securitykey = "[NP]",
                            Status = 0
                        });
                        if (GameServiceClient.CommunicationState == CommunicationStates.Connected)
                        {
                            Log.Info("GameServer authed by validation server");
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
        public interface IGameServerBridge
        {
            void RegisterGameServer(GameServerData info);
            AccountData SetAccountData(int charId);
            void SaveCharacter(AccountData accData, PlayerData playerData);
            void SetTotalPlayersOnline(int serverId, int total);
        }
    }
}