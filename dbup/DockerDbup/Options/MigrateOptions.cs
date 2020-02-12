using CommandLine;

namespace DockerDbup.Options
{
    [Verb("migrate", HelpText = "This will run your migration scripts")]
    public class MigrateOptions : ISharedOptions
    {
        public string ScriptsPath { get; set; }
        public string ConnectionString { get; set; }
        public DbEngines DbEngine { get; set; }
    }
}
