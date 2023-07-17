using Spectre.Console.Cli;
using CosmosManager;

var app = new CommandApp();
app.Configure(config =>
{
    config.AddCommand<InitCommand>("init");
    config.AddCommand<InstallCommand>("install");
});


app.Run(args);
