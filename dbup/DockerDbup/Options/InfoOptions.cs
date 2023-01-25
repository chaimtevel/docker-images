using CommandLine;

namespace DockerDbup.Options;

[Verb("info", HelpText = "Provides info ")]
public class InfoOptions : ISharedOptions
{
    public string ScriptsPath { get; init; }
    public string ConnectionString { get; init; }
    public DbEngines? DbEngine { get; init; }
}
