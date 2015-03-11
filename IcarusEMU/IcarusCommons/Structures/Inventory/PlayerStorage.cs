// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System;
using System.Collections.Generic;

namespace IcarusCommons.Structures.Inventory
{
    [Serializable]
    public class PlayerStorage
    {
        public int LastUid;
        public IList<Item> Storage = new List<Item>(40);
    }
}