// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System;

namespace IcarusCommons.Structures.Inventory
{
    [Serializable]
    public class Item
    {
        public int UId;
        public int PlayerId;
        public int ItemId;
        public int Count;
        public byte IsEquip;
        public short SId;
        public short Slot;
    }
}