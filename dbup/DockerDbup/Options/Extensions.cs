using System;

namespace DockerDbup.Options
{
    public static class Extensions
    {
        public static string GetConnectionString(this ISharedOptions options)
            => options?.ConnectionString ?? Environment.GetEnvironmentVariable("CONNECTION_STRING");

        public static string GetScriptsPath(this ISharedOptions options)
            => options?.ScriptsPath ?? Environment.GetEnvironmentVariable("SCRIPTS_PATH") ?? "/dbup/sql";

        public static DbEngines GetDbEngine(this ISharedOptions options)
        {
            if(!(options?.DbEngine).HasValue)
            {
                string env = Environment.GetEnvironmentVariable("DB_ENGINE");
                Enum.TryParse(env, true, out DbEngines dbEngine);
                return dbEngine;
            }

            return options.DbEngine.Value;
        }

        public static bool GetDisableVariables(this MigrateOptions options)
        {
            if(!options.DisableVariables.HasValue)
            {
                string disableVars = Environment.GetEnvironmentVariable("DISABLE_VARS");
                if(bool.TryParse(disableVars, out bool result))
                {
                    return result;
                }
            }
            else
            {
                return options.DisableVariables.Value;
            }

            return false;
        }

        public static bool GetDisableDbTransaction(this MigrateOptions options)
        {
            if (!options.DisableTransaction.HasValue)
            {
                string disableTransaction = Environment.GetEnvironmentVariable("DISABLE_DB_TRANSACTION");
                if (bool.TryParse(disableTransaction, out bool result))
                {
                    return result;
                }
            }
            else
            {
                return options.DisableTransaction.Value;
            }

            return false;
        }

    }
}
