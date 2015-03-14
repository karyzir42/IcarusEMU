using System.Collections.Generic;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using IcarusCommons.DAO.MySql;
using IcarusCommons.Utils;

namespace IcarusLoginServer.Services
{
    public static class DatabaseService
    {
        private static readonly List<ADbScript> Scripts = new List<ADbScript>();

        public static void Initialize()
        {
            var scripts = Directory.GetFiles("Scripts/Database/MySQL");

            foreach (var scr in scripts.Select(script => ScriptsCompiler.Compile<ADbScript>(script)))
                Scripts.Add(scr);

            Log.Debug("Database scripts compiled ({0})", scripts.Length);

            _authConnection.ConnectionString = "Driver={MySQL ODBC 5.3 UNICODE Driver};Server=localhost;Database=icarus_login;User=root;Password=;AUTO_RECONNECT=1;FOUND_ROWS=1;NO_PROMPT=1;";
            _authConnection.Open();

            Log.Debug("Successfully connected to auth database");
        }

        private static readonly OdbcConnection _authConnection = new OdbcConnection();

        public static OdbcConnection AuthConnection
        {
            get
            {
                if (_authConnection != null)
                    return _authConnection;

                Log.Error("Auth connection is not opened");

                return new OdbcConnection();
            }
        }

        public static T Get<T>() where T : ADbScript
        {
            return Scripts.OfType<T>().FirstOrDefault();
        }
    }
}
