using CommandLine;

namespace DockerDbup.Options
{
    public interface ISharedOptions
    {
        [Option('c', "connection-string", HelpText = "specify the connection string to use for migrations")]
        string ConnectionString { get; }

        [Option('p', "scripts-path", HelpText = "override the default location for migrations of /dbup/sql")]
        string ScriptsPath { get; }

        [Option('e', "db-engine", HelpText = "Specify the db engine to use. The default value is SqlServer")]
        DbEngines? DbEngine { get; }
    }
}
