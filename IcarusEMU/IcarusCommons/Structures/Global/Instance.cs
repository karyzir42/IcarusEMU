// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using IcarusCommons.Models.Creature;
using IcarusCommons.Models.Player;
using MongoDB.Driver.Linq;

namespace IcarusCommons.Structures.Global
{
    public class Instance
    {
        public int InstanceId;
        public string MapName = "area_f01_p_0001";

        public volatile List<Player> PlayersInInstance = new List<Player>();
        public volatile List<Mob> MobsInInstance = new List<Mob>();

        protected readonly object MobsLock = new object();
        protected readonly object PlayersLock = new object();

        public virtual void PlayerMove(Player player, Position position)
        {
            PlayersInInstance.First(s => s == player).Position = position;
        }

        #region Collection utils

        public void AddMob(Mob mob)
        {
            lock (MobsLock)
                MobsInInstance.Add(mob);
        }

        public void AddMobs(Mob[] mobs)
        {
            lock (MobsLock)
                MobsInInstance.AddRange(mobs);
        }

        public void AddPlayer(Player player)
        {
            lock (PlayersLock)
                PlayersInInstance.Add(player);
        }

        public void RemoveMob(Mob mob)
        {
            lock (MobsLock)
                if (MobsInInstance.Contains(mob))
                    MobsInInstance.Remove(mob);
        }

        public void RemoveMobs(Mob[] mobs)
        {
            lock (MobsLock)
                if (MobsInInstance.ContainsAll(mobs))
                    MobsInInstance.RemoveAll(s => s.UID == mobs.First(u => u.UID == s.UID).UID);
        }

        #endregion
    }
}