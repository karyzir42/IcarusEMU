// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.IO;
using ProtoBuf;

namespace IcarusCommons.Utils
{
    public static class ProtocolBuffersProcessor
    {
        public static T DeserializeFile<T>(string filePath)
        {
            if (!File.Exists(filePath)) return default(T);
            using (var fs = File.OpenRead(filePath))
                return Serializer.Deserialize<T>(fs);
        }

        public static void SerializeFile(object data, string filePath)
        {
            using (var fs = File.Create(filePath))
                Serializer.Serialize(fs, data);
        }
    }
}