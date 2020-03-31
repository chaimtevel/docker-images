using DbUp;
using DbUp.Builder;
using DbUp.Helpers;
using DockerDbup.Options;
using System;
using System.Collections.Generic;

namespace DockerDbup.Runners
{
    public class MigrationRunner : IRunner
    {
        public MigrationRunner(MigrateOptions options)
        {
            Options = options;
        }

        public MigrateOptions Options { get; }

        public void Run()
        {
            string connectionString = Options.GetConnectionString();
            string path = Options.GetScriptsPath();
            var dbEngine = Options.GetDbEngine();

            if (connectionString == null)
            {
                throw new ArgumentNullException(nameof(connectionString), Messages.RequireConnectionString);
            }

            Console.WriteLine($"Starting Migrations on scripts at {path} for db engine {dbEngine}");

            var journaledRunnerBuilder = Mappings.GetDbBuilder(dbEngine, DeployChanges.To, connectionString)
                .WithTransaction()
                .WithScriptsFromFileSystem(path, s => !s.Contains("idempotent", StringComparison.OrdinalIgnoreCase))
                .LogToConsole()
                .LogScriptOutput()
                .Build();

            var journaledRunnerResult = journaledRunnerBuilder.PerformUpgrade();
            PrintResult(journaledRunnerResult, "Successfully executed scripts:");

            if (!journaledRunnerResult.Successful)
            {
                throw journaledRunnerResult.Error;
            }

            var idempotentRunnerBuilder = Mappings.GetDbBuilder(dbEngine, DeployChanges.To, connectionString)
                .WithScriptsFromFileSystem(path, s => s.Contains("idempotent", StringComparison.OrdinalIgnoreCase))
                .JournalTo(new NullJournal())
                .LogToConsole()
                .LogScriptOutput()
                .Build();

            var idempotentRunnerResult = idempotentRunnerBuilder.PerformUpgrade();
            PrintResult(idempotentRunnerResult, "Successfully executed idempotent scripts:");

            if (!idempotentRunnerResult.Successful)
            {
                throw idempotentRunnerResult.Error;
            }

        }

        private void PrintResult(DbUp.Engine.DatabaseUpgradeResult result, string msg)
        {
            if (result.Successful)
            {
                Console.WriteLine(msg);

                foreach (var script in result.Scripts)
                {
                    Console.WriteLine(script.Name);
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error.Message);
                Console.ResetColor();
            }
        }

    }
}
