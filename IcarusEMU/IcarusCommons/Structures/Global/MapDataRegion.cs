// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System;
using System.Collections.Generic;
using System.Xml.Schema;
using System.Xml.Serialization;
using ProtoBuf;

namespace IcarusCommons.Structures.Global
{
    [Serializable, ProtoContract]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "blocks", Namespace = "", IsNullable = false)]
    public class MapDataRegion
    {
        [ProtoMember(1), XmlElement("block", Form = XmlSchemaForm.Unqualified)] public List<MapRegion> MapRegions;
    }

    [Serializable, ProtoContract, XmlType(AnonymousType = true)]
    public class MapRegion
    {
        [XmlAttribute("id")] [ProtoMember(1)] public string Id;
        [XmlAttribute("name")] [ProtoMember(2)] [XmlIgnore] public string Name;
        [XmlAttribute("locality_output_id")] [ProtoMember(3)] public string Locality_output_id;
        [XmlAttribute("map_id")] [ProtoMember(4)] public string MapId;
        [XmlAttribute("resurrection_location_id")] [ProtoMember(5)] public string Resurrection_location_id;
        [XmlAttribute("local_images")] [ProtoMember(6)] public int Local_images;
        [XmlAttribute("war_fog")] [ProtoMember(7)] public string War_fog;
        [XmlAttribute("npc_portal_register")] [ProtoMember(8)] public string Npc_portal_register;
        [XmlAttribute("servo_register")] [ProtoMember(9)] public int Servo_register;
        [XmlAttribute("npc_icons")] [ProtoMember(10)] public int Npc_icons;
        [XmlAttribute("npcutscene_will_triggerc_icons")] [ProtoMember(11)] public int Сutscene_will_trigger;
        [XmlAttribute("regional_radar_images")] [ProtoMember(12)] public string Regional_radar_images;
        [XmlAttribute("max_height_region")] [ProtoMember(13)] public int Max_height_region;
        [XmlAttribute("region_height_min")] [ProtoMember(14)] public int Region_height_min;
    }
}