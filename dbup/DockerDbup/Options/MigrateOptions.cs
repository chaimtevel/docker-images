using CommandLine;

namespace DockerDbup.Options
{
    [Verb("migrate", HelpText = "This will run your migration scripts")]
    public class MigrateOptions : ISharedOptions
    {
        public string ScriptsPath { get; set; }
        public string ConnectionString { get; set; }
        public DbEngines? DbEngine { get; set; }

        [Option('d', "disable-vars", HelpText = "Specify whether variable substitution should be skipped. The default is false.")]
        public bool? DisableVariables { get; set; }

        [Option('t', "disable-db-transaction", HelpText = "Specify whether migrations should be wrapped in a db transaction. The default is true.")]
        public bool? DisableTransaction { get; set; }

        [Option('r', "recurse", HelpText = "Specify whether the runner should search for sql scripts in directories recursively")]
        public bool? SearchScriptsRecursively { get; set; }
    }
}
