// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.Linq;
using IcarusCommons.Models.Account;
using MongoDB.Bson.Serialization;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace IcarusValidServer.Core.DAO.MongoDatabase
{
    internal class AccountRepository : ARepository
    {
        public static void Init()
        {
            Initialize();
            BsonClassMap.RegisterClassMap<AccountData>(cm => cm.AutoMap());
        }

        public static AccountData GetAccountByPlayerId(int playerId)
        {
            var player = CharacterRepository.GetPlayerById(playerId);
            return GetAccountData(player.AccountId);
        }

        public static AccountData GetAccountData(string login, string password)
        {
            var db = GetDatabase("IcarusWorld");
            var collection = db.GetCollection<AccountData>("Accounts");

            var query = collection.AsQueryable().Where(e => e.Login == login && e.Password == password);

            foreach (var accountData in query)
                return accountData;

            return new AccountData();
        }

        public static AccountData GetAccountData(int accId)
        {
            var db = GetDatabase("IcarusWorld");
            var collection = db.GetCollection<AccountData>("Accounts");
            var query = Query<AccountData>.EQ(a => a.Id, accId);

            return collection.FindOneAs<AccountData>(query) ?? new AccountData();
        }

        public static void SaveAccount(AccountData accountData)
        {
            var db = GetDatabase("IcarusWorld");
            var collection = db.GetCollection<AccountData>("Accounts");

            collection.Insert(accountData);
        }

        public static void UpdateAccount(AccountData accountData)
        {
            var db = GetDatabase("IcarusWorld");
            var collection = db.GetCollection<AccountData>("Accounts");
            var query = Query<AccountData>.EQ(p => p.Id, accountData.Id);
            var update = Update<AccountData>.Replace(accountData);

            collection.Update(query, update);
        }
    }
}