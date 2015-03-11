// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System;

namespace IcarusCommons.Models.Bridge
{
    [Serializable]
    public class LobbyServerData
    {
        public int Id;
        public string Name;
        public string Ip;
        public int Port;
        public int MaxPlayers;
        public int PlayersOnline;
        public int Status;
        public string Securitykey;
    }
}