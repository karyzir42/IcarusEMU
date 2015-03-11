// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System;
using IcarusCommons.Models.Account;
using IcarusCommons.Utils;
using IcarusValidServer.Core.DAO.MongoDatabase;
using IcarusValidServer.Core.Utils;

namespace IcarusValidServer.Core.Services
{
    public class AccountService
    {
        static AccountService()
        {
            TaskProcessor.AddTask(GC.Collect, 600); //30 min
        }

        public static string CheckAuth(string login, string password)
        {
            var cachedAccount =
                AccountRepository.GetAccountData(login, CryptoUtils.CalculateMd5Hash(password));

            if (cachedAccount.Login != null)
                return CryptoUtils.GenerateByAccount(cachedAccount);

#if DEBUG // Под отладкой пусть будет авторега.
            var data = new AccountData {Id = 1, Login = login, Password = CryptoUtils.CalculateMd5Hash(password)};

            AccountRepository.SaveAccount(data);

            Log.Debug("REGISTERED:{0}", login);

            return CryptoUtils.GenerateByAccount(data);
#endif
        }

        public static AccountData GetAccountData(int accId)
        {
            return AccountRepository.GetAccountData(accId);
        }

        public static AccountData GetAccountDataByCharId(int charId)
        {
            var accData = AccountRepository.GetAccountByPlayerId(charId);
            accData.Players = CharacterRepository.GetPlayersByAccount(accData.Id);

            return accData;
        }

        public static void UpdateAccount(AccountData account)
        {
            AccountRepository.UpdateAccount(account);
        }
    }
}