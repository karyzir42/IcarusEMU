// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.Collections.Generic;
using IcarusCommons.Structures.Inventory;

namespace IcarusGameServer.Managers
{
    public static class ItemManager
    {
        private static int NextId;
        private static readonly object _lock = new object();
        public static List<int> Released = new List<int>();

        static ItemManager()
        {
            SetLastId(0);
        }

        public static void InitilizeInventory(ref PlayerStorage storage)
        {
            foreach (var stor in storage.Storage)
                stor.UId = GenerateId();
        }

        private static void SetLastId(int id)
        {
            lock (_lock)
                NextId = id >= 0 ? id : 0;
        }

        private static int GenerateId()
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

        public static void SetUid(ref Item item)
        {
            item.UId = GenerateId();
        }
    }
}