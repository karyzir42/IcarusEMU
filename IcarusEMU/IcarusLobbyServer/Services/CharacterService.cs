// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using IcarusCommons.Configurations;
using IcarusCommons.Models.Account;
using IcarusCommons.Models.Player;
using IcarusCommons.Structures.Global;
using IcarusLobbyServer.Managers;
using IcarusLobbyServer.Network;
using IcarusLobbyServer.Network.Packets.Send;

namespace IcarusLobbyServer.Services
{
    public static class CharacterService
    {
        public static volatile int MaxPlayersOnAccount = int.Parse(CIniLoader.ReadValue("LOGIC", "MAX_PLAYERS_ON_ACCOUNT"));
        public static volatile bool UsePinCode = bool.Parse(CIniLoader.ReadValue("LOGIC", "USE_PIN"));

        public static void GetBaseInfoProceed(Connection connection, int accId)
        {
            connection.AccountData = LobbyManager.LobbyServiceClient.ServiceProxy.GetAccountData(accId);
            connection.AccountData.Players = LobbyManager.LobbyServiceClient.ServiceProxy.GetCharacterList(accId);

            new SM_CONNECT().Send(connection);
        }

        public static void CharacterListProcess(Connection connection)
        {
            new SM_CHARACTERLIST((List<PlayerData>) connection.AccountData.Players).Send(connection);
        }

        public static void CharacterCreationProcess(Connection connection, PlayerData player)
        {
            player.AccountId = connection.AccountData.Id;
            player.Abilities = new CharacterAbilities
            {
                AbilityPoints = 0,
                AbilityPointsMax = 0,
                Accuracy = 5,
                CharacterSpeed = 5,
                Agility = 5,
                CurrentHp = 60,
                MaxHp = 120,
                CurrentMp = 60,
                MaxMp = 120,
                DualBlades = 5,
                MagicalDefence = 15,
                PhysicalDefence = 20,
                ShieldWithThreePupirishki = 5,
                Evasion = 15,
                PhysicalCrit = 30,
                PhysicalCritTwo = 30,
                PhysicalCritRate = 15,
                Power = 15,
                Intelligence = 15,
                Fitness = 15,
                Mentality = 15,
                TimeRestorePoints = 15,
                Blade = 5,
            };
            player.Position = new Position((float) 759.58215, (float) 898.30786, (float) 32.00925, 35);

            LobbyManager.LobbyServiceClient.ServiceProxy.CreateCharacter(connection.AccountData, player);

            var toCreation =
                LobbyManager.LobbyServiceClient.ServiceProxy.GetCharacterList(connection.AccountData.Id).Last();

            new SM_CREATECHARACTER(toCreation).Send(connection);
        }

        public static void CharacterDeletionProcess(Connection connection, int charId)
        {
            LobbyManager.LobbyServiceClient.ServiceProxy.DeleteCharacter(connection.AccountData, charId);

            if (
                connection.AccountData.Players.Contains(
                    connection.AccountData.Players.FirstOrDefault(s => s.PlayerId == charId)))
                connection.AccountData.Players.Remove(
                    connection.AccountData.Players.FirstOrDefault(s => s.PlayerId == charId));

            new SM_DELETECHARACTER(connection, charId).Send(connection);
        }

        public static void StartGameProceed(Connection connection, int charId)
        {
            new SM_STARTGAME(charId).Send(connection);
        }

        public static void UpdateAccount(AccountData data)
        {
            LobbyManager.LobbyServiceClient.ServiceProxy.UpdateAccountData(data);
        }
    }
}