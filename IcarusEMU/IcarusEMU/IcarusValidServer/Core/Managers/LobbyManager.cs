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
using IcarusValidServer.Core.DAO.MongoDatabase;
using IcarusValidServer.Core.Services;

namespace IcarusValidServer.Core.Managers
{
    public static class LobbyManager
    {
        public static volatile Dictionary<IScsServiceClient, LobbyServerData> LobbyServers =
            new Dictionary<IScsServiceClient, LobbyServerData>();

        private static readonly object ServersLock = new object();

        public static void Init()
        {
            IScsServiceApplication gsservice = ScsServiceBuilder.CreateService(
                new ScsTcpEndPoint(CIniLoader.ReadValue("NETWORK", "LOBBYSERVER_IP"),
                    Convert.ToInt32(CIniLoader.ReadValue("NETWORK", "LOBBYSERVER_BRIDGE_PORT"))));

            gsservice.ClientDisconnected += gsservice_ClientDisconnected;

            gsservice.AddService<ILobbyServerBridge, GameServerBridge>(new GameServerBridge());
            gsservice.Start();

            Log.Info("[{0}] Started at {1}:{2}", "LOBBY MANAGER",
                CIniLoader.ReadValue("NETWORK", "LOBBYSERVER_IP"),
                Convert.ToInt32(CIniLoader.ReadValue("NETWORK", "LOBBYSERVER_BRIDGE_PORT")));
        }

        public static List<LobbyServerData> GetLobbyServers()
        {
            lock (ServersLock)
                return LobbyServers.Values.ToList();
        }

        private static void gsservice_ClientDisconnected(object sender, ServiceClientEventArgs e)
        {
            lock (ServersLock)
            {
                if (LobbyServers.ContainsKey(e.Client))
                {
                    Log.Info("LobbyServer: {0} Disconnected!", LobbyServers[e.Client].Name);
                    LobbyServers.Remove(e.Client); //If Gs disconnected, remove
                }
            }
        }

        internal class GameServerBridge : ScsService, ILobbyServerBridge
        {
            public void RegisterLobbyServer(LobbyServerData info)
            {
                if (info.Securitykey != CIniLoader.ReadValue("SERVER_INFO", "SECURITY_KEY"))
                {
                    Log.Info("Wrong security key!\nConnection info:{0} {1}:{2}\n", info.Name, info.Ip, info.Port);
                    return;
                }
                var server = LobbyServers.FirstOrDefault(sr => sr.Value.Id == info.Id).Key;

                lock (ServersLock)
                {
                    if (server != null)
                        LobbyServers.Remove(server);

                    LobbyServers.Add(CurrentClient, info);

                    Log.Info("LOBBY SERVER: {0} CONNECTED!", info.Name);
                }
            }

            public List<PlayerData> GetCharacterList(int accId)
            {
                return CharacterService.GetCharacterList(accId);
            }

            public AccountData GetAccountData(int accId)
            {
                return AccountService.GetAccountData(accId);
            }

            public bool IsUsedName(string name)
            {
                return CharacterRepository.IsUsedName(name);
            }

            public void CreateCharacter(AccountData data, PlayerData plData)
            {
                CharacterService.CreateCharacter(data, plData);
            }

            public void DeleteCharacter(AccountData data, int charId)
            {
                CharacterService.DeleteCharacter(data, charId);
            }

            public void UpdateAccountData(AccountData data)
            {
                AccountService.UpdateAccount(data);
            }
        }

        [ScsService]
        public interface ILobbyServerBridge
        {
            void RegisterLobbyServer(LobbyServerData info);
            List<PlayerData> GetCharacterList(int accId);
            AccountData GetAccountData(int accId);
            bool IsUsedName(string name);
            void CreateCharacter(AccountData data, PlayerData plData);
            void DeleteCharacter(AccountData data, int charId);
            void UpdateAccountData(AccountData data);
        }
    }
}