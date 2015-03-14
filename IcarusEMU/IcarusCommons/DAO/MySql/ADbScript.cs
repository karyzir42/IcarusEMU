using System;
using System.Data.Common;
using System.Data.Odbc;
using NLog;

namespace IcarusCommons.DAO.MySql
{
    public abstract class ADbScript
    {
        protected static readonly Logger Log = LogManager.GetCurrentClassLogger();

        protected delegate void DbResultCallback(DbDataReader reader);

        protected OdbcConnection AuthConnection
        {
            get
            {
                return Global.LoginDatabaseService.AuthConnection;
            }
        }

        protected OdbcConnection GameConnection
        {
            get
            {
                return Global.GameDatabaseService.GameConnection;
            }
        }

        protected void ExequteAuthQuerry(string querry, DbResultCallback readerCallback = null,
            params OdbcParameter[] parameters)
        {
            try
            {
                using (var cmd = new OdbcCommand(querry, AuthConnection))
                {
                    if (parameters != null)
                        foreach (var odbcParameter in parameters)
                            cmd.Parameters.Add(odbcParameter);

                    cmd.Prepare();

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (readerCallback != null)
                            readerCallback(reader);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error("Error occured while execute db querry,\n{0}", e);
            }
        }

        protected void ExequteGameQuerry(string querry, params OdbcParameter[] parameters)
        {
            ExequteGameQuerry(querry, null, parameters);
        }

        protected void ExequteGameQuerry(string querry, DbResultCallback readerCallback = null,
            params OdbcParameter[] parameters)
        {
            try
            {
                using (var cmd = new OdbcCommand(querry, GameConnection))
                {
                    if (parameters != null)
                        foreach (var odbcParameter in parameters)
                            cmd.Parameters.Add(odbcParameter);

                    cmd.Prepare();

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (readerCallback != null)
                            readerCallback(reader);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error("Error occured while execute db querry,\n{0}", e);
            }
        }
    }
}
