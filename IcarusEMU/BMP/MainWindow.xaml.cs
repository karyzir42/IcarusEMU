// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace BMP
{
    /// <summary>
    ///     Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        protected AREA_LIST Areas;

        public MainWindow()
        {
            InitializeComponent();

            var p = new Pen(Color.Indigo, 2.0F);
            var t = new Pen(Color.Chartreuse, 2.0F);

            List<Point> Points = new List<Point>();

            string fileName = "area_map_f05";
            string fileNameWall = "wall_f05_evil_desert";

            string xmlAres = File.ReadAllText(string.Format("{0}.xml", fileName));
            string xmlWall = File.ReadAllText(string.Format("{0}.xml", fileNameWall));

            var ares = xmlAres.ParseXML<AREA_LIST>();
            var walls = xmlWall.ParseXML<WALL.Data>();

            using (var bmp = new Bitmap(4000, 4000))
            {
                foreach (var testtt in ares.AREA)
                {
                    Points.AddRange(from coords in testtt.AREA_GEOM.AREA_LINE
                        let X = Convert.ToInt32(coords.x)
                        let Y = Convert.ToInt32(coords.y)
                        select new Point(X/2, Y/2));
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.DrawPolygon(p, Points.ToArray());
                    }
                    Points.Clear();
                }
                foreach (var wallData in walls.Wall)
                {
                    foreach (
                        string[] coordArray in
                            wallData.VertexList.Select(coords => coords.pos)
                                .Select(coordinations => coordinations.Split(',')))
                    {
                        int dX;
                        int dY;
                        try
                        {
                            dX = Convert.ToInt32(coordArray[0].Remove(coordArray[0].IndexOf('.')));
                        }
                        catch
                        {
                            dX = Convert.ToInt32(coordArray[0]);
                        }
                        try
                        {
                            dY = Convert.ToInt32(coordArray[1].Remove(coordArray[1].IndexOf('.')));
                        }
                        catch
                        {
                            dY = Convert.ToInt32(coordArray[1]);
                        }
                        Points.Add(new Point(dX/2, dY/2));
                    }
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.DrawPolygon(t, Points.ToArray());
                    }
                    Points.Clear();
                }

                bmp.Save(string.Format("{0}.bmp", fileName));
                Close();
            }
        }
    }

    internal static class ParseHelpers
    {
        public static Stream ToStream(this string @this)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(@this);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static T ParseXML<T>(this string @this) where T : class
        {
            var reader = XmlReader.Create(@this.Trim().ToStream(), new XmlReaderSettings
            {
                ConformanceLevel = ConformanceLevel.Auto,
                CheckCharacters = false,
            });
            return new XmlSerializer(typeof (T)).Deserialize(reader) as T;
        }
    }
}