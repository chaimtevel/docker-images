using DbUp;
using DbUp.Helpers;
using DockerDbup.Options;

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
            var disableVars = Options.GetDisableVariables();
            var disableTransaction = Options.GetDisableDbTransaction();
            var searchScriptRecursively = Options.GetSearchRecursively();
            var timeout = Options.GetTimeout();

            if (connectionString == null)
            {
                throw new ArgumentNullException(nameof(connectionString), Messages.RequireConnectionString);
            }

            Console.WriteLine($"Starting Migrations on scripts at {path} for db engine {dbEngine}");

            var journaledRunnerScriptOptions = new DbUp.ScriptProviders.FileSystemScriptOptions
            {
                Filter = s => !s.Contains("idempotent", StringComparison.OrdinalIgnoreCase),
                IncludeSubDirectories = searchScriptRecursively
            };

            // with journal
            var journaledRunnerBuilder = Mappings.GetDbBuilder(dbEngine, DeployChanges.To, connectionString)
                .WithScriptsFromFileSystem(path, journaledRunnerScriptOptions)
                .LogToConsole()
                .LogScriptOutput();

            if (disableVars)
            {
                journaledRunnerBuilder = journaledRunnerBuilder.WithVariablesDisabled();
            }

            if(!disableTransaction)
            {
                journaledRunnerBuilder = journaledRunnerBuilder.WithTransaction();
            }

            if(timeout.HasValue)
            {
                journaledRunnerBuilder = journaledRunnerBuilder.WithExecutionTimeout(timeout.Value);
            }

            var journaledRunner = journaledRunnerBuilder.Build();

            var journaledRunnerResult = journaledRunner.PerformUpgrade();
            PrintResult(journaledRunnerResult, "Successfully executed scripts:");

            if (!journaledRunnerResult.Successful)
            {
                throw journaledRunnerResult.Error;
            }

            // idempotent
            Console.WriteLine();

            Console.WriteLine($"Starting idempotent Migrations on scripts at {path} for db engine {dbEngine}");

            var idempotentRunnerScriptOptions = new DbUp.ScriptProviders.FileSystemScriptOptions
            {
                Filter = s => s.Contains("idempotent", StringComparison.OrdinalIgnoreCase),
                IncludeSubDirectories = searchScriptRecursively
            };

            var idempotentRunnerBuilder = Mappings.GetDbBuilder(dbEngine, DeployChanges.To, connectionString)
                .WithScriptsFromFileSystem(path, idempotentRunnerScriptOptions)
                .JournalTo(new NullJournal())
                .LogToConsole()
                .LogScriptOutput();

            if (disableVars)
            {
                idempotentRunnerBuilder = idempotentRunnerBuilder.WithVariablesDisabled();
            }

            if (!disableTransaction)
            {
                idempotentRunnerBuilder = idempotentRunnerBuilder.WithTransaction();
            }

            var idempotentRunner = idempotentRunnerBuilder.Build();

            var idempotentRunnerResult = idempotentRunner.PerformUpgrade();
            PrintResult(idempotentRunnerResult, "Successfully executed idempotent scripts:");

            if (!idempotentRunnerResult.Successful)
            {
                throw idempotentRunnerResult.Error;
            }
        }

        private static void PrintResult(DbUp.Engine.DatabaseUpgradeResult result, string msg)
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
