// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using IcarusCommons.Models.Player;
using IcarusCommons.Structures.Global;
using IcarusCommons.Utils;
using IcarusGameServer.Network.Packets.Send;

namespace IcarusGameServer.Services
{
    public static class InstanceService
    {
        private static Thread InstanceLoop { get; set; }
        public static readonly Dictionary<int, List<Instance>> Instances = new Dictionary<int, List<Instance>>();

        static InstanceService()
        {
            InstanceLoop = new Thread(() =>
            {
                while (true)
                {
                    foreach (var instance in Instances.Values)
                        foreach (var member in instance)
                            foreach (var player in member.PlayersInInstance)
                                foreach (var mob in member.MobsInInstance)
                                {
                                    var mobPosition = mob.Position;
                                    var playerPosition = player.Position;

                                    var distance = (playerPosition.X - mobPosition.X)*(playerPosition.X - mobPosition.X) +
                                                   (playerPosition.Y - mobPosition.Y)*(playerPosition.Y - mobPosition.Y) +
                                                   (playerPosition.Z - mobPosition.Z)*(playerPosition.Z - mobPosition.Z);

                                    var math = Math.Sqrt(distance);

                                    if (math <= 60 && !mob.PlayerhasVisibleMy[player.Id])
                                    {
                                        //Spawn action
                                        mob.PlayerhasVisibleMy[player.Id] = true;
                                        new SM_GAMEOBJECT_SPAWN(mob).Send(player.Connection);

                                        Log.Info("MOB HAS BEEN SPAWNED:{0}", mob.Id);
                                    }
                                    else if (math >= 60 && mob.PlayerhasVisibleMy[player.Id])
                                    {
                                        //Despawn action TODO find despawn packet
                                        mob.PlayerhasVisibleMy[player.Id] = false;
                                        Log.Info("MOB HAS BEEN DESPAWNED:{0}", mob.Id);
                                    }
                                }
                    Thread.Sleep(1);
                }
            });
            InstanceLoop.Start();
        }

        public static void InitilizeInstance()
        {
            Instances.Add(1, new List<Instance>());
        }

        public static void EnterInGame(ref Player player)
        {
            if (player.Position == null)
                player.Position = player.PlayerData.Position;

            if (!Instances[1].Any())
                Instances[1].Add(new Instance {InstanceId = 1});

            Instances[1][0].PlayersInInstance.Add(player);

            player.Instance = Instances[1][0];
        }

        public static void LogOut(Player player)
        {
            Instances[1][0].PlayersInInstance.Remove(player); //TODO!
        }
    }
}