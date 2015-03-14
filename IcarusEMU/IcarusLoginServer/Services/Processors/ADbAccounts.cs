using IcarusCommons.DAO.MySql;
using IcarusCommons.Models.Account;

namespace IcarusLoginServer.Services.Processors
{
    public abstract class ADbAccounts : ADbScript
    {
        public abstract AccountData GetAccountInfo(string masterAccount, string token);
    }
}
