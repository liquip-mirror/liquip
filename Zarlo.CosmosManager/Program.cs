using Spectre.Console.Cli;
using Zarlo.CosmosManager;

var app = new CommandApp();
app.Configure(config => { config.AddCommand<InitCommand>("init"); });


app.Run(args);
