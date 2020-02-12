using CommandLine;
using DockerDbup.Options;
using DockerDbup.Runners;
using System;

namespace DockerDbup
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                Parser parser = new Parser(c =>
                {
                    c.CaseInsensitiveEnumValues = true;
                    c.CaseSensitive = false;
                    c.EnableDashDash = Parser.Default.Settings.EnableDashDash;
                    c.HelpWriter = Parser.Default.Settings.HelpWriter;
                });
                
                // I'm unsure if its a bug but verbs aren't validated unless there are two of them
                var result = parser.ParseArguments<MigrateOptions, InfoOptions>(args)
                        .WithParsed<MigrateOptions>(o => new MigrationRunner(o).Run())
                        .WithParsed<InfoOptions>(o => new InfoRunner(o).Run());

                return 0;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Exception: {e.Message}");
                return -1;
            }

        }

    }
}
