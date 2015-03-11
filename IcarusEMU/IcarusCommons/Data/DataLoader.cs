// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using IcarusCommons.Structures.Global;
using IcarusCommons.Utils;
using ProtoBuf;

namespace IcarusCommons.Data
{
    public class DataLoader
    {
        public static string DataPath = "./data/static_data/";
        public static string CachePath = "./data/cached/";

        public static MapDataRegion MapDatas = new MapDataRegion();

        private static readonly List<Loader> Loaders = new List<Loader>
        {
            LoadMapDatas
        };

        private delegate int Loader();

        public static void LoadAll(string datapath = "./data/static_data/")
        {
            DataPath = datapath;

            while (!Parallel.For(0, Loaders.Count, delegate(int i)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                var readed = Loaders[i].Invoke();
                stopwatch.Stop();

                Log.Info(string.Format("Data: {0,-26} {1,7} values in {2}s"
                    , Loaders[i].Method.Name
                    , readed
                    , (stopwatch.ElapsedMilliseconds/1000.0).ToString("0.00")));
            }).IsCompleted)
            {
            }

            Thread.Sleep(1000);
        }

        public static int LoadMapDatas()
        {
            MapDatas.MapRegions = Deserialize<MapDataRegion>("2.xml").MapRegions;


            return MapDatas.MapRegions.Count;
        }

        private static TXml Deserialize<TXml>(string fileName)
        {
            var cd = AppDomain.CurrentDomain.BaseDirectory;
            var trgtPath = Path.Combine(cd, CachePath);

            if (!Directory.Exists(trgtPath))
                Directory.CreateDirectory(trgtPath);

            var pt = Path.Combine(trgtPath, fileName.Replace(".xml", ".cached"));

            if (fileName.Contains("\\"))
            {
                var nfl = fileName.Substring(fileName.IndexOf("\\", StringComparison.Ordinal) + 1);
                pt = Path.Combine(trgtPath, nfl.Replace(".xml", ".cached"));
            }

            TXml t;

            string newFileHash = null;

            if (File.Exists(pt))
            {
                using (var sr = new BinaryReader(File.OpenRead(pt)))
                {
                    var hash = sr.ReadString();

                    using (var md = MD5.Create())
                    using (var xmlStream = File.OpenRead(Path.Combine(DataPath, fileName)))
                    {
                        var xmlHash = md.ComputeHash(xmlStream).ToHex();
                        xmlStream.Position = 0;
                        if (xmlHash != hash)
                        {
                            t = (TXml) new XmlSerializer(typeof (TXml)).Deserialize(xmlStream);

                            newFileHash = xmlHash;
                        }
                        else
                            t = Serializer.Deserialize<TXml>(sr.BaseStream);
                    }
                }
            }
            else
            {
                using (var md = MD5.Create())
                using (var xmlStream = File.OpenRead(Path.Combine(DataPath, fileName)))
                {
                    t = (TXml) new XmlSerializer(typeof (TXml)).Deserialize(xmlStream);
                    newFileHash = md.ComputeHash(xmlStream).ToHex();
                }
            }

            if (newFileHash != null)
                using (var fs = new BinaryWriter(File.Create(pt)))
                {
                    fs.Write(newFileHash);
                    Serializer.Serialize(fs.BaseStream, t);
                }

            return t;
        }
    }
}