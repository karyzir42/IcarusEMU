// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.ScsServices.Service;
using IcarusCommons.Configurations;
using IcarusCommons.Models.Account;
using IcarusCommons.Models.Bridge;
using IcarusCommons.Models.Player;
using IcarusCommons.Utils;
using IcarusValidServer.Core.Services;

namespace IcarusValidServer.Core.Managers
{
    public static class GameManager
    {
        public static volatile Dictionary<IScsServiceClient, GameServerData> GameServers =
            new Dictionary<IScsServiceClient, GameServerData>();

        private static readonly object ServersLock = new object();

        public static void Init()
        {
            IScsServiceApplication gsservice = ScsServiceBuilder.CreateService(
                new ScsTcpEndPoint(CIniLoader.ReadValue("NETWORK", "GAMESERVER_IP"),
                    Convert.ToInt32(CIniLoader.ReadValue("NETWORK", "GAMESERVER_BRIDGE_PORT"))));

            gsservice.ClientDisconnected += gsservice_ClientDisconnected;
            gsservice.AddService<IGameServerBridge, GameBridge>(new GameBridge());
            gsservice.Start();

            Log.Info("[{0}] Started at {1}:{2}", "GAME MANAGER",
                CIniLoader.ReadValue("NETWORK", "GAMESERVER_IP"),
                Convert.ToInt32(CIniLoader.ReadValue("NETWORK", "GAMESERVER_BRIDGE_PORT")));
        }

        private static void gsservice_ClientDisconnected(object sender, ServiceClientEventArgs e)
        {
            lock (ServersLock)
            {
                if (GameServers.ContainsKey(e.Client))
                {
                    Log.Info("GameServer: {0} Disconnected!", GameServers[e.Client].Name);
                    GameServers.Remove(e.Client); //If Gs disconnected, remove
                }
            }
        }

        public class GameBridge : ScsService, IGameServerBridge
        {
            public void RegisterGameServer(GameServerData info)
            {
                if (info.Securitykey != CIniLoader.ReadValue("SERVER_INFO", "SECURITY_KEY"))
                {
                    Log.Info("Wrong security key!\nConnection info:{0} {1}:{2}\n", info.Name, info.Ip, info.Port);
                    return;
                }
                var server = GameServers.FirstOrDefault(sr => sr.Value.Id == info.Id).Key;

                lock (ServersLock)
                {
                    if (server != null)
                        GameServers.Remove(server);

                    GameServers.Add(CurrentClient, info);

                    Log.Info("GAME SERVER: {0} CONNECTED!", info.Name);
                }
            }

            public AccountData SetAccountData(int charId)
            {
                return AccountService.GetAccountDataByCharId(charId);
            }

            public void SaveCharacter(AccountData accData, PlayerData playerData)
            {
                CharacterService.SaveCharacter(accData, playerData);
            }

            public void SetTotalPlayersOnline(int serverId, int total)
            {
                GameServers.Values.First(s => s.Id == serverId).PlayersOnline = total;
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