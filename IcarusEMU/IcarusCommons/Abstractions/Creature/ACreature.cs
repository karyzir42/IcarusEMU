// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

namespace IcarusCommons.Abstractions.Creature
{
    public abstract class ACreature : AIcarusObject
    {
        public Structures.Global.Instance Instance { get; set; }
    }
}