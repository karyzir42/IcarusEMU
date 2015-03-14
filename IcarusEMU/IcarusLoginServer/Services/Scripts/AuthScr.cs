using System;
using System.Data.Odbc;
using IcarusCommons.Models.Account;
using IcarusLoginServer.Services.Processors;

namespace IcarusLoginServer.Services.Scripts
{
    class MySQL_Accounts : ADbAccounts
    {
        private const string QSelectAccount = "SELECT * FROM `accounts` where `accounts`.`name` = ? AND `accounts`.`token` = ?";

        public override AccountData GetAccountInfo(string masterAccount, string token)
        {
            AccountData result = null;

            ExequteAuthQuerry(QSelectAccount, reader =>
            {
                if (!reader.HasRows)
                    return;

                var id = (int)reader["id"];
                var name = (string)reader["name"];
                var tok = (string)reader["token"];
                var time = (DateTime)reader["tokenExpireTime"];
                var accessLevel = (int)reader["accessLevel"];
                var pinCode = (string)reader["pinCode"];

                result = new AccountData(id, name, tok);//TODO! 
            },
                new OdbcParameter("?ID1", OdbcType.VarChar, 50) { Value = masterAccount },
                new OdbcParameter("?ID2", OdbcType.VarChar, 50) { Value = token }
                );

            return result;
        }
    }
}
