using System;

namespace DockerDbup.Options
{
    public static class Extensions
    {
        public static string GetConnectionString(this ISharedOptions options)
            => options?.ConnectionString ?? Environment.GetEnvironmentVariable("CONNECTION_STRING");

        public static string GetScriptsPath(this ISharedOptions options)
            => options?.ScriptsPath ?? Environment.GetEnvironmentVariable("SCRIPTS_PATH") ?? "/dbup/sql";

    }
}
