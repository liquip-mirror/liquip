using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Zarlo.CosmosManager;

public class InitCommand: Command<InitCommand.Settings>
{
    public class Settings: CommandSettings
    { 

        [CommandArgument(0, "[name]")]
        public string? Name { get; set; }

        [CommandOption("-o|--out")]
        [DefaultValue(null)]
        public string? OutputPath { get; set; }

        [CommandOption("-m|--make")]
        [DefaultValue(false)]
        public bool MakelFile { get; set; }

        [CommandOption("--project|-p")]
        [DefaultValue(false)]
        public bool Projects { get; set; }

        [CommandOption("--install|-i")]
        [DefaultValue(false)]
        public bool Install { get; set; }
    }


    public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
    {
        if (settings.Name == null)
        {
            settings.OutputPath ??= Directory.GetCurrentDirectory();
        }
        settings.Name ??= new DirectoryInfo(Directory.GetCurrentDirectory()).Name;
        settings.OutputPath ??= Path.Join(Directory.GetCurrentDirectory(), settings.Name);

        AnsiConsole.MarkupLine("Project Name:[bold]{0}[/]", settings.Name);
        AnsiConsole.MarkupLine("Project Path:[bold]{0}[/]", settings.OutputPath);

        Directory.CreateDirectory(Paths.CosmosManagerPath);
        

        return 0;
    }


}
