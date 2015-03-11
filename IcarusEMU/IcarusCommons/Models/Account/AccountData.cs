// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System;
using System.Collections.Generic;
using IcarusCommons.Models.Bridge;
using IcarusCommons.Models.Player;
using ProtoBuf;

namespace IcarusCommons.Models.Account
{
    [Serializable]
    [ProtoContract]
    public class AccountData
    {
        [ProtoMember(1)] public int Id;
        [ProtoMember(2)] public string Login;
        [ProtoMember(3)] public string Password;
        [ProtoMember(4)] public string FamilyName;
        [ProtoMember(5)] public int AccessLevel;
        [ProtoMember(6)] public byte[] PinCode = new byte[4];
        public int Money;
        public TokenData LoginToken;

        public IList<PlayerData> Players { get; set; }
    }
}