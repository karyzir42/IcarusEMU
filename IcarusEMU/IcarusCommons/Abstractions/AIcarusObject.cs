// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using IcarusCommons.Abstractions.Creature;
using IcarusCommons.Utils;

namespace IcarusCommons.Abstractions
{
    public abstract class AIcarusObject
    {
        public abstract int UID { get; set; }

        public int NextId;
        private readonly object _lock = new object();
        public static Dictionary<int, ACreature> UidFactory = new Dictionary<int, ACreature>();

        protected AIcarusObject()
        {
            UID = GenerateId((ACreature) this);
        }

        public int GenerateId(ACreature creature)
        {
            var i = ++NextId;
            lock (_lock)
            {
                if (UidFactory.ContainsKey(i))
                    return GenerateId(creature);

                UidFactory.Add(i, creature);
                return i;
            }
        }

        public virtual void Release(ACreature creature)
        {
            lock (_lock)
            {
                if (UidFactory.ContainsValue(creature))
                {
                    UidFactory.Remove(UidFactory.First(s => s.Value == creature).Key);
                    Log.Info("UID Released:{0}", creature.GetType());
                    return;
                }
                Log.Info("ERRER! {0}", creature.GetType());
            }
        }
    }
}