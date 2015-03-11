// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System;
using System.ComponentModel;
using System.Xml.Serialization;
using ProtoBuf;

namespace IcarusCommons.Structures.Global
{
    [Serializable]
    [ProtoContract]
    public class Position
    {
        [ProtoMember(1), XmlAttribute] public float X;

        [ProtoMember(2), XmlAttribute] public float Y;

        [ProtoMember(3), XmlAttribute] public float Z;

        [ProtoMember(4), XmlAttribute] public int ZoneId;

        [ProtoMember(5), XmlAttribute, DefaultValue(0)] public int InstanceId;

        [ProtoMember(6), XmlAttribute] public short Heading;

        public int MovementType;
        public int MovementStatus;

        public Position(float x = 0, float y = 0, float z = 0, short heading = 0, int zoneId = 0)
        {
            X = x;
            Y = y;
            Z = z;
            Heading = heading;
            ZoneId = zoneId; // Need or not, need analyse maps..
        }

        public override string ToString()
        {
            return string.Format("[Position] X:{0}, Y:{1}, Z:{2}, MapId:{3}, InstanceId:{4}, Heading:{5}.", X, Y, Z,
                ZoneId, InstanceId, Heading);
        }
    }
}