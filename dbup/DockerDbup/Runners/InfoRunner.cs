using DbUp;
using DbUp.Engine;
using DockerDbup.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace DockerDbup.Runners
{
    public class InfoRunner : IRunner
    {
        public InfoRunner(InfoOptions options)
        {
            Options = options;
        }

        public InfoOptions Options { get; }

        public void Run()
        {
            string connectionString = Options.GetConnectionString();
            string path = Options.GetScriptsPath();
            var dbEngine = Options.GetDbEngine();

            if (connectionString == null)
            {
                throw new ArgumentNullException(nameof(connectionString), Messages.RequireConnectionString);
            }

            var showExcutedScriptsBuilder = Mappings.GetDbBuilder(dbEngine, DeployChanges.To, connectionString)
                .WithScriptsFromFileSystem(path)
                .LogToConsole()
                .LogScriptOutput()
                .Build();

            foreach(string script in showExcutedScriptsBuilder.GetExecutedScripts())
            {
                Console.WriteLine($"Executed: {script}");
            }

            foreach (SqlScript script in showExcutedScriptsBuilder.GetScriptsToExecute())
            {
                Console.WriteLine($"Pending: {script.Name}");
            }

        }
    }
}
