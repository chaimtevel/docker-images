using CommandLine;

namespace DockerDbup.Options
{
    [Verb("migrate", HelpText = "This will run your migration scripts")]
    public class MigrateOptions : ISharedOptions
    {
        public string ScriptsPath { get; init; }
        public string ConnectionString { get; init; }
        public DbEngines? DbEngine { get; init; }

        [Option('d', "disable-vars", HelpText = "Specify whether variable substitution should be skipped. The default is false.")]
        public bool? DisableVariables { get; init; }

        [Option('t', "disable-db-transaction", HelpText = "Specify whether migrations should be wrapped in a db transaction. The default is true.")]
        public bool? DisableTransaction { get; init; }

        [Option('r', "recurse", HelpText = "Specify whether the runner should search for sql scripts in directories recursively")]
        public bool? SearchScriptsRecursively { get; init; }

        [Option('o', "timeout", HelpText = "Specify the number of seconds to timeout the migration")]
        public int? TimeoutSeconds { get; init; }
    }
}
