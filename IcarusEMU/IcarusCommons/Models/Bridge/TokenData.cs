﻿// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System;
using IcarusCommons.Utils;

namespace IcarusCommons.Models.Bridge
{
    [Serializable]
    public class TokenData
    {
        public readonly string Key;

        public readonly int TokenCreationTime = Extensions.RoundedUtc;

        public TokenData(string key)
        {
            Key = key;
        }
    }
}