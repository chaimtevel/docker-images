using CommandLine;

namespace DockerDbup.Options;

[Verb("info", HelpText = "Provides info ")]
public class InfoOptions : ISharedOptions
{
    public string ScriptsPath { get; set; }
    public string ConnectionString { get; set; }
    public DbEngines? DbEngine { get; set; }
}
