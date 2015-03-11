// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using IcarusCommons.Models.Player;
using MongoDB.Bson.Serialization;
using MongoDB.Driver.Builders;

namespace IcarusValidServer.Core.DAO.MongoDatabase
{
    internal class CharacterRepository : ARepository
    {
        public static void Init()
        {
            Initialize();

            BsonClassMap.RegisterClassMap<PlayerData>(cm => cm.AutoMap());
        }

        /// <summary>
        ///     Get player data by character name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static PlayerData GetPlayerByName(string name)
        {
            var db = GetDatabase("IcarusWorld");
            var collection = db.GetCollection<PlayerData>("Characters");
            var query = Query<PlayerData>.EQ(p => p.Name, name);
            return collection.FindOneAs<PlayerData>(query);
        }

        /// <summary>
        ///     Save character data
        /// </summary>
        /// <param name="data"></param>
        public static void InsertPlayer(PlayerData data)
        {
            var db = GetDatabase("IcarusWorld");
            var collection = db.GetCollection<PlayerData>("Characters");

            collection.Insert(data);
        }

        /// <summary>
        ///     Get character data by account id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static PlayerData GetPlayerById(int id)
        {
            var db = GetDatabase("IcarusWorld");
            var collection = db.GetCollection<PlayerData>("Characters");
            return collection.FindOneByIdAs<PlayerData>(id);
        }

        /// <summary>
        ///     Get PlayerData`s by account
        /// </summary>
        /// <param name="accId"></param>
        /// <returns></returns>
        public static IList<PlayerData> GetPlayersByAccount(int accId)
        {
            var db = GetDatabase("IcarusWorld");
            var collection = db.GetCollection<PlayerData>("Characters");
            var query = Query<PlayerData>.EQ(p => p.AccountId, accId);
            return collection.FindAs<PlayerData>(query).ToList();
        }

        /// <summary>
        ///     Update player data`s
        /// </summary>
        /// <param name="data"></param>
        public static void UpdatePlayer(PlayerData data)
        {
            var db = GetDatabase("IcarusWorld");
            var collection = db.GetCollection<PlayerData>("Characters");
            var query = Query<PlayerData>.EQ(p => p.PlayerId, data.PlayerId);
            var update = Update<PlayerData>.Replace(data);

            collection.Update(query, update);
        }

        /// <summary>
        ///     Remove player data
        /// </summary>
        /// <param name="data"></param>
        public static void DeletePlayer(PlayerData data)
        {
            var db = GetDatabase("IcarusWorld");
            var collection = db.GetCollection<PlayerData>("Characters");
            var query = Query<PlayerData>.EQ(p => p.PlayerId, data.PlayerId);

            collection.Remove(query);
        }

        /// <summary>
        ///     Get last character id
        /// </summary>
        /// <returns></returns>
        public static int GetLastId()
        {
            var db = GetDatabase("IcarusWorld");
            var collection = db.GetCollection<PlayerData>("Characters");
            if (!collection.FindAll().Any())
                return 0;

            return collection.FindAll().Max(p => p.PlayerId);
        }

        public static bool IsUsedName(string name)
        {
            var db = GetDatabase("IcarusWorld");
            var collection = db.GetCollection<PlayerData>("Characters");
            var query = Query<PlayerData>.EQ(p => p.Name, name);
            if (collection.FindOneAs<PlayerData>(query) != null)
                return true;

            return false;
        }
    }
}