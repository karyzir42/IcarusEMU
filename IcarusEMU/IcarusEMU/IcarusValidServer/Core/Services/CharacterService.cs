// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using IcarusCommons.Models.Account;
using IcarusCommons.Models.Player;
using IcarusCommons.Models.Storage;
using IcarusCommons.Structures.Inventory;
using IcarusCommons.Utils;
using IcarusValidServer.Core.DAO.MongoDatabase;

namespace IcarusValidServer.Core.Services
{
    public static class CharacterService
    {
        public static List<PlayerData> GetCharacterList(int accId)
        {
            return (List<PlayerData>) (CharacterRepository.GetPlayersByAccount(accId) ?? new List<PlayerData>());
        }

        public static void CreateCharacter(AccountData data, PlayerData plData)
        {
            Log.Info("TRYING CREATE CHARACTER:{0}", plData.Name);

            plData.Level = 1;
            plData.Storage = new PlayerStorage();

            plData.Storage.Storage.Add(new Item
            {
                Count = 1,
                IsEquip = 1,
                ItemId = 0x00010064,
                Slot = (short) StorageEnum.Armor,
                PlayerId = plData.PlayerId,
                SId = 0,
                UId = 1
            });
            plData.Storage.Storage.Add(new Item
            {
                Count = 1,
                IsEquip = 1,
                ItemId = 0x0001006d,
                Slot = (short) StorageEnum.Foot,
                PlayerId = plData.PlayerId,
                SId = 1,
                UId = 2
            });


            var playerData = CharacterRepository.GetPlayerByName(plData.Name);
            if (playerData == null)
                CharacterRepository.InsertPlayer(plData);
            else
                Log.Info("CREATING CHARACTER IS ERRED! {0} Already added!", plData.Name);
        }

        public static void DeleteCharacter(AccountData data, int charId)
        {
            CharacterRepository.DeletePlayer(data.Players.First(s => s.PlayerId == charId));
        }

        public static void SaveCharacter(AccountData accData, PlayerData playerData)
        {
            CharacterRepository.UpdatePlayer(playerData);
        }
    }
}