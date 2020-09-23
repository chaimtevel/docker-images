using CommandLine;

namespace DockerDbup.Options
{
    [Verb("migrate", HelpText = "This will run your migration scripts")]
    public class MigrateOptions : ISharedOptions
    {
        public string ScriptsPath { get; set; }
        public string ConnectionString { get; set; }
        public DbEngines? DbEngine { get; set; }
        [Option('d', "disable-vars", HelpText = "Specify whether vairbale substitution should be skipped. The default is false.")]
        public bool? DisableVariables { get; set; }
    }
}
