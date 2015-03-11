// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System;
using IcarusCommons.DAO.MongoDatabase;
using IcarusCommons.Structures.Global;
using IcarusCommons.Structures.Inventory;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace IcarusCommons.Models.Player
{
    [Serializable]
    public class PlayerData
    {
        [BsonId(IdGenerator = typeof (MongoIdFactory))] public int PlayerId;
        public int AccountId;
        public int AccessType;
        public ClassEnum ClassType;
        public int Level;
        public long Exp;
        public string Name;
        public Position Position;
        public CHAR_STYLE Style;
        public CharacterAbilities Abilities;
        public PlayerStorage Storage;
        public SexEnum Sex;
    }

    [Serializable]
    public class CHAR_STYLE
    {
        [ProtoMember(1)] public int unk0;
        [ProtoMember(2)] public int unk1;
        [ProtoMember(3)] public int eye;
        [ProtoMember(4)] public int unk2;
        [ProtoMember(5)] public int eyebrows;
        [ProtoMember(6)] public int unk3;
        [ProtoMember(7)] public int iris;
        [ProtoMember(8)] public int unk4;
        [ProtoMember(9)] public int unk5;
        [ProtoMember(10)] public int tatoo;
        [ProtoMember(11)] public int unk6;
        [ProtoMember(12)] public int unk7;
        [ProtoMember(13)] public int unk81;
        [ProtoMember(14)] public int unk8;
        [ProtoMember(15)] public int unk9;
        [ProtoMember(16)] public int hair;
        [ProtoMember(17)] public int unk10;
        [ProtoMember(18)] public int unk11;
        [ProtoMember(19)] public int unk12;
        [ProtoMember(20)] public int unk13;
        [ProtoMember(21)] public int unk14;
        [ProtoMember(22)] public int color_lips;
        [ProtoMember(23)] public int color_eyeb;
        [ProtoMember(24)] public int color_iris;
        [ProtoMember(25)] public int color_eyebrows;
        [ProtoMember(26)] public int color_eye;
        [ProtoMember(27)] public int unk15;
        [ProtoMember(28)] public int unk16;
        [ProtoMember(29)] public int unk17;
        [ProtoMember(30)] public int unk18;
        [ProtoMember(31)] public int unk19;
        [ProtoMember(32)] public int unk20;
        [ProtoMember(33)] public int unk21;
        [ProtoMember(34)] public int unk22;
        [ProtoMember(35)] public int unk23;
        [ProtoMember(36)] public int unk24;
        [ProtoMember(37)] public int unk25;
        [ProtoMember(38)] public int unk26;
        [ProtoMember(39)] public int unk27;
        [ProtoMember(40)] public int unk28;
        [ProtoMember(41)] public int unk29;
        [ProtoMember(42)] public int unk30;
        [ProtoMember(43)] public int unk31;
        [ProtoMember(44)] public int unk32;
        [ProtoMember(45)] public int unk33;
        [ProtoMember(46)] public int unk34;
        [ProtoMember(47)] public int unk35;
        [ProtoMember(48)] public int unk36;
    }

    [Serializable]
    public class CharacterAbilities
    {
        public float PhysicalDefence;
        public float MagicalDefence;
        public float Evasion;
        public float Accuracy;
        public float PhysicalCritRate;
        public float Power;
        public float Agility;
        public float Intelligence;
        public float Fitness;
        public float Mentality;
        public float MaxHp;
        public float MaxMp;
        public float AbilityPointsMax; //for new level
        public float AbilityPoints; //for new level
        public float DualBlades; //for rework O_o dual blades hah
        public float Blade;
        public float PhysicalCrit;
        public float PhysicalCritTwo;
        public float ShieldWithThreePupirishki;
        public float CurrentHp;
        public float CurrentMp;
        public float CharacterSpeed;
        public float TimeRestorePoints;
    }
}