// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using IcarusCommons.Models.Player;
using IcarusCommons.Structures.Chat;
using IcarusCommons.Structures.Inventory;
using IcarusCommons.Utils;
using IcarusGameServer.Managers;
using IcarusGameServer.Network;
using IcarusGameServer.Network.Packets.Send;

namespace IcarusGameServer.Services
{
    public static class GameService
    {
        public static volatile Dictionary<Connection, Player> PlayersOnline = new Dictionary<Connection, Player>();

        static GameService()
        {
            TaskProcessor.AddTask(() =>
            {
                foreach (var player in PlayersOnline)
                {
                    GameManager.GameServiceClient.ServiceProxy.SaveCharacter(player.Key.AccountData,
                        player.Value.PlayerData);

                    new SM_CHAT_MESSAGE("System bot",
                        string.Format("Players online: {0}", PlayersOnline.Count), ChatChannelsEnum.Bullhorn).Send(
                            player.Key);
                }
            }, 150);
        }

        public static void SetAccountDataProcess(Connection connection, int characterId)
        {
            connection.AccountData = GameManager.GameServiceClient.ServiceProxy.SetAccountData(characterId);
            connection.ActivePlayer =
                new Player(connection.AccountData.Players.FirstOrDefault(s => s.PlayerId == characterId))
                {
                    Connection = connection
                };
            if (connection.ActivePlayer.PlayerData.Storage == null)
                connection.ActivePlayer.PlayerData.Storage = new PlayerStorage();

            new SM_UNK1().Send(connection);
            new SM_CONNECT().Send(connection);

            PlayersOnline.Add(connection, connection.ActivePlayer);
        }

        public static void SetArea(Connection connection)
        {
            var currPlayer = connection.ActivePlayer;

            InstanceService.EnterInGame(ref currPlayer);
            ItemManager.InitilizeInventory(ref currPlayer.PlayerData.Storage);

            connection.ActivePlayer = currPlayer;

            new SM_SETAREA(connection.ActivePlayer).Send(connection);

            new SM_CHAT_MESSAGE("Necroz Systems", "Welcome to Necroz Icarus Online Free server", ChatChannelsEnum.SystemAdvert)
                .Send(connection);
        }

        public static void LogOut(Connection connection)
        {
            PlayersOnline.Remove(connection);
            InstanceService.LogOut(connection.ActivePlayer);
            Log.Info("Disconnected:{0}:{1}", connection.AccountData.Login, connection.ActivePlayer.PlayerData.Name);
        }
    }
}