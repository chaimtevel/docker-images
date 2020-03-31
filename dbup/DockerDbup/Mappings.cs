using DbUp.Builder;
using DockerDbup.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace DockerDbup
{
    using DbEngineGetter = Func<string, SupportedDatabases, UpgradeEngineBuilder>;

    public static class Mappings
    {
        private static readonly IReadOnlyDictionary<DbEngines, DbEngineGetter> _dbEngineMappings = new Dictionary<DbEngines, DbEngineGetter>
        {
            { DbEngines.SqlServer, (cs, sd) => sd.SqlDatabase(cs) },
            { DbEngines.PostgreSql, (cs, sd) => sd.PostgresqlDatabase(cs) },
            { DbEngines.MySql, (cs, sd) => sd.MySqlDatabase(cs) }
        };

        public static UpgradeEngineBuilder GetDbBuilder(DbEngines dbEngine, SupportedDatabases db, string connectionString) =>
            _dbEngineMappings[dbEngine]?.Invoke(connectionString, db);
    }
}
