using Spectre.Console.Cli;
using CosmosManager;

var app = new CommandApp();
app.Configure(config => { config.AddCommand<InitCommand>("init"); });


app.Run(args);
