using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.Json;
using Fluid;
using Spectre.Console;
using Spectre.Console.Cli;

namespace CosmosManager;

public class StatsCommand : Command<StatsCommand.Settings>
{
    public override int Execute([NotNull] CommandContext context, Settings settings)
    {
        if (settings.Name == null)
        {
            settings.OutputPath ??= Directory.GetCurrentDirectory();
        }

        settings.Name ??= new DirectoryInfo(Directory.GetCurrentDirectory()).Name;
        settings.OutputPath ??= Path.Join(Directory.GetCurrentDirectory(), settings.Name);
        Paths.SetRoot(settings.OutputPath);



        var table = new Table();
        table.HideHeaders();
        table.AddColumn("");
        table.AddColumn("");

        table.AddRow("Project Name", settings.Name);
        table.AddRow("Project Path", settings.OutputPath);

        table.AddRow("Project is set up", Utils.StringBool(Directory.Exists(Paths.CosmosManagerPath)));
        table.AddRow("Yasm", Utils.StringBool(Utils.IsInPath("yasm")));


        AnsiConsole.Write(table);

        return 0;
    }

    public class Settings : CommandSettings
    {
        [CommandArgument(0, "[name]")] public string? Name { get; set; }

        [CommandOption("-o|--out")]
        [DefaultValue(null)]
        public string? OutputPath { get; set; }


    }
}
