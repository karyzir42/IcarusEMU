// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.
/*
 * Author:Maxes727
 */
using System.Collections.Generic;
using MongoDB.Bson.Serialization;

namespace IcarusCommons.DAO.MongoDatabase
{
    public class MongoIdFactory : IIdGenerator
    {
        private static int _nextId;
        private static readonly List<int> Released = new List<int>();

        public static void SetLastId(int id)
        {
            _nextId = id >= 0 ? id : 0;
        }

        public object GenerateId(object container, object document)
        {
            var i = ++_nextId;
            if (Released.Contains(i))
                return GenerateId(container, document);
            Released.Add(i);
            return i;
        }

        public bool IsEmpty(object id)
        {
            var i = (int) id;
            if (i == 0)
                return true;
            if (!Released.Contains(i))
                Released.Add(i);
            return false;
        }
    }
}