// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.Collections.Generic;

namespace BMP
{
    public class WALL
    {
        /// <remarks />
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class Data
        {
            /// <remarks />
            [System.Xml.Serialization.XmlElementAttribute("Wall")]
            public List<DataWall> Wall { get; set; }
        }

        /// <remarks />
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class DataWall
        {
            /// <remarks />
            public DataWallProperty Property { get; set; }

            /// <remarks />
            [System.Xml.Serialization.XmlArrayItemAttribute("vertex", IsNullable = false)]
            public List<DataWallVertex> VertexList { get; set; }

            /// <remarks />
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string name { get; set; }
        }

        /// <remarks />
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class DataWallProperty
        {
            /// <remarks />
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte SpaceType { get; set; }

            /// <remarks />
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Height { get; set; }

            /// <remarks />
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Closed { get; set; }

            /// <remarks />
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Enable_Walk { get; set; }

            /// <remarks />
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Enable_Attack { get; set; }

            /// <remarks />
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte CCW { get; set; }

            /// <remarks />
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte CW { get; set; }
        }

        /// <remarks />
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class DataWallVertex
        {
            /// <remarks />
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string pos { get; set; }
        }
    }
}