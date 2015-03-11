// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.Collections.Generic;

/// <remarks />
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class AREA_LIST
{
    /// <remarks />
    [System.Xml.Serialization.XmlElementAttribute("AREA")]
    public List<AREA_LISTAREA> AREA { get; set; }
}

/// <remarks />
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class AREA_LISTAREA
{
    /// <remarks />
    public AREA_LISTAREAAREA_GEOM AREA_GEOM { get; set; }

    /// <remarks />
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string id { get; set; }

    /// <remarks />
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string desc { get; set; }

    /// <remarks />
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string DestAreaId { get; set; }

    /// <remarks />
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public bool NoRiding { get; set; }

    /// <remarks />
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public bool NoAirRiding { get; set; }

    /// <remarks />
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public bool NoSummon { get; set; }

    /// <remarks />
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public bool spawn { get; set; }

    /// <remarks />
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string skill { get; set; }

    /// <remarks />
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string space_type { get; set; }

    /// <remarks />
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public ushort enter_angle { get; set; }

    /// <remarks />
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string DestMapId { get; set; }

    /// <remarks />
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public bool EventChannel { get; set; }

    /// <remarks />
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string EventQuestID { get; set; }
}

/// <remarks />
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class AREA_LISTAREAAREA_GEOM
{
    //private List<List<AREA_LISTAREAAREA_GEOMAREA_TRIANGLEPOS>> aREA_TRIANGLEField;

    /// <remarks />
    [System.Xml.Serialization.XmlArrayItemAttribute("POS", IsNullable = false)]
    public List<AREA_LISTAREAAREA_GEOMPOS> AREA_LINE { get; set; }

    //[System.Xml.Serialization.XmlArrayItemAttribute("POS", typeof(AREA_LISTAREAAREA_GEOMAREA_TRIANGLEPOS), IsNullable=false)]
//public List<List<AREA_LISTAREAAREA_GEOMAREA_TRIANGLEPOS>> AREA_TRIANGLE
//{
//    get {
//        return this.aREA_TRIANGLEField;
//    }
//    set {
//        this.aREA_TRIANGLEField = value;
//    }
//}
    /// <remarks />
    /// <remarks />
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte height { get; set; }

    /// <remarks />
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public bool check_z { get; set; }
}

/// <remarks />
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class AREA_LISTAREAAREA_GEOMPOS
{
    /// <remarks />
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal x { get; set; }

    /// <remarks />
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal y { get; set; }

    /// <remarks />
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal z { get; set; }
}

/// <remarks />
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class AREA_LISTAREAAREA_GEOMAREA_TRIANGLEPOS
{
    /// <remarks />
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal x { get; set; }

    /// <remarks />
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal y { get; set; }

    /// <remarks />
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal z { get; set; }
}