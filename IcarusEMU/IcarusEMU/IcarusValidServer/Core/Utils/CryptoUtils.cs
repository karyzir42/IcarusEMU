// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System;
using System.Security.Cryptography;
using System.Text;
using IcarusCommons.Models.Account;
using IcarusCommons.Models.Bridge;
using IcarusCommons.Utils;
using IcarusValidServer.Core.Managers;

namespace IcarusValidServer.Core.Utils
{
    public class CryptoUtils
    {
        private const string Salt = @"8vd%h>Jxb6nVTDiU5wc2K<XK!W8>2{q]=.kL,№8A-ZE_Xma!>Q6'izQq_b%ji5Gu";

        public static string GenerateByAccount(AccountData account)
        {
            string token = CalculateMd5Hash(account.Login + Salt + account.Password + new Random().Next(200000));

            account.LoginToken = new TokenData(token);

            LauncherManager.RegisterToken(account);
            Log.Debug("SESSION CREATED:{0}", token);

            return token;
        }

        public static string CalculateMd5Hash(string input)
        {
            using (var md = MD5.Create())
                return md.ComputeHash(Encoding.Default.GetBytes(input)).ToHex();
        }

        public static int GenerateSessionHash()
        {
            return new Random().Next(1000000, 9999999);
        }
    }
}