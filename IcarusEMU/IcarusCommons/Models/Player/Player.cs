// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.Collections.Generic;
using IcarusCommons.Abstractions.Creature;
using IcarusCommons.Models.Battle;
using IcarusCommons.Network.Interface;
using IcarusCommons.Structures.Global;

namespace IcarusCommons.Models.Player
{
    public class Player : ACreature
    {
        public override int UID { get; set; }
        public int Id;

        public PlayerData PlayerData { get; set; }
        public Position Position;
        public BattleModel BattleModel { get; set; }
        public List<Skill> Skills = new List<Skill>();
        public IConnection Connection;

        public Player(PlayerData playerData)
        {
            PlayerData = playerData;
            Id = PlayerData.PlayerId;
        }
    }
}