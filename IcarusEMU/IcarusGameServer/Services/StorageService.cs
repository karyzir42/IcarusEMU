// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using IcarusCommons.Models.Player;
using IcarusCommons.Structures.Inventory;
using IcarusGameServer.Managers;

namespace IcarusGameServer.Services
{
    public static class StorageService
    {
        public static volatile int MAX_ITEM_IN_INVENTORY = 40;

        public static void AddItem(int itemId, int slot, int count, int isEquip, Player player)
        {
            if (player.PlayerData.Storage == null)
                player.PlayerData.Storage = new PlayerStorage();

            if(player.PlayerData.Storage.Storage.Count == 40)
                return;

            var storageData = player.PlayerData.Storage;

            if (!storageData.Storage.Any())
                storageData.LastUid = 1;
            else if (storageData.Storage.Any())
                storageData.LastUid = storageData.Storage.Last().UId;

            var generator = new PlayerUidGenerator();
            generator.SetLastId(storageData.LastUid);

            var newItem = new Item
            {
                Count = count,
                ItemId = itemId,
                Slot = (short) slot,
                PlayerId = player.Id,
                SId = (short) generator.GenerateId(),
                IsEquip = (byte) isEquip,
                UId = 0
            };
            ItemManager.SetUid(ref newItem);

            storageData.Storage.Add(newItem);
        }

        public static void AddItem(uint itemId, int slot, int count, int isEquip, Player player)
        {
            var storageData = player.PlayerData.Storage;

            if (!storageData.Storage.Any())
                storageData.LastUid = 1;
            else if (storageData.Storage.Any())
                storageData.LastUid = storageData.Storage.Last().UId;

            var generator = new PlayerUidGenerator();
            generator.SetLastId(storageData.LastUid);

            var newItem = new Item
            {
                Count = count,
                ItemId = (int) itemId,
                Slot = (short) slot,
                PlayerId = player.Id,
                SId = (short) generator.GenerateId(),
                IsEquip = (byte) isEquip
            };
            storageData.Storage.Add(newItem);
        }
    }

    internal class PlayerUidGenerator
    {
        public int NextId;
        private readonly object _lock = new object();
        public readonly List<int> Released = new List<int>();

        public void SetLastId(int id)
        {
            lock (_lock)
                NextId = id >= 0 ? id : 0;
        }

        public int GenerateId()
        {
            var i = ++NextId;
            lock (_lock)
            {
                if (Released.Contains(i))
                    return GenerateId();

                Released.Add(i);
                return i;
            }
        }

        public bool IsEmpty(object id)
        {
            var i = (int) id;
            if (i == 0)
                return true;
            if (!Released.Contains(i))
                Released.Add(i);
            return false;
        }
    }
}