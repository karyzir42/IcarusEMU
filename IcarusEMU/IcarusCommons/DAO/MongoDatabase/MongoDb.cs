// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.
/*
 * Author:Maxes727
 */
using System;
using IcarusCommons.Utils;
using MongoDB.Driver;

namespace IcarusCommons.DAO.MongoDatabase
{
    public class MongoDb
    {
        private static MongoClient _client;
        protected static object GetLock = new object();

        public static void Initialize()
        {
            try
            {
                _client = new MongoClient("mongodb://localhost/?safe=true");

                var server = _client.GetServer();
                server.GetDatabase("IcarusWorld");

                Log.Info("Mongo database connection:{0} is alive", server.Instance.Address);
            }
            catch (Exception e)
            {
                Log.ErrorException("Hyuston! We have some problem!", e);
            }
        }

        protected static MongoServer GetConnection()
        {
            return _client.GetServer();
        }

        protected static MongoDB.Driver.MongoDatabase GetDatabase(string name)
        {
            lock (GetLock)
                return GetConnection().GetDatabase(name);
        }
    }
}