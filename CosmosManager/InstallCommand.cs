using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Text.Json;
using Fluid;
using Spectre.Console;
using Spectre.Console.Cli;

namespace CosmosManager;

public class InstallCommand : Command<InstallCommand.Settings>
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

        AnsiConsole.MarkupLine("Project Name:[bold]{0}[/]", settings.Name);
        AnsiConsole.MarkupLine("Project Path:[bold]{0}[/]", settings.OutputPath);

        var configFile = JsonSerializer.Deserialize<ConfigFile>(File.ReadAllText(Path.Join(Paths.CosmosManagerPath, "config.json")));

        PullRepo("Common", configFile.Common);
        PullRepo("XSharp", configFile.XSharp);
        PullRepo("IL2CPU", configFile.IL2CPU);
        PullRepo("Cosmos", configFile.Cosmos);

        ApplyPatch("Common");
        ApplyPatch("XSharp");
        ApplyPatch("IL2CPU");
        ApplyPatch("Cosmos");

        Utils.RunShellCommand("make", "-C", Path.Join(Paths.CosmosSrcPath, "Cosmos"), "build", $@"DESTDIR={Paths.CosmosBinPath}");
        Utils.RunShellCommand("make", "-C", Path.Join(Paths.CosmosSrcPath, "Cosmos"), "publish", $@"DESTDIR={Paths.CosmosBinPath}");
        Utils.RunShellCommand("make", "-C", Path.Join(Paths.CosmosSrcPath, "Cosmos"), "install", $@"DESTDIR={Paths.CosmosBinPath}");

        return 0;
    }

    void ApplyPatch(string project)
    {
        var project_patch_path = Path.Join(Paths.CosmosManagerPatchesPath, project);
        var project_path = Path.Join(Paths.CosmosSrcPath, project);
        if (Directory.Exists(project_patch_path))
        {
            foreach (var patch in Directory.GetFiles(project_patch_path))
            {
                Utils.RunShellCommand("git", "-C", project_path, "apply", Path.Join(project_patch_path, patch));
            }

            var os = Environment.OSVersion.Platform.ToString();

            if (Directory.Exists(project_patch_path + os))
            {
                foreach (var patch in Directory.GetFiles(project_patch_path + os))
                {
                    Utils.RunShellCommand("git", "-C", project_path, "apply", Path.Join(project_patch_path, os, patch));
                }
            }
            else
            {
                AnsiConsole.MarkupLine("no patches for platform {0} found", project);
            }

        }
        else
        {
            AnsiConsole.MarkupLine("no patches for {0} found", project);
        }
    }

    void PullRepo(string Name, GitRepo repo)
    {
        var installPath = Path.Join(Paths.CosmosSrcPath, Name);
        if(!Directory.Exists(installPath))
        {
            Utils.RunShellCommand("git", "clone", repo.Uri, installPath);
        }

        if (!string.IsNullOrWhiteSpace(repo.Branch))
        {
            Utils.RunShellCommand("git", "-C", installPath, "checkout", repo.Branch);
        }

        if (!string.IsNullOrWhiteSpace(repo.Commit))
        {
            Utils.RunShellCommand("git", "-C", installPath, "checkout", repo.Commit);
        }

        if (!string.IsNullOrWhiteSpace(repo.Tag))
        {
            Utils.RunShellCommand("git", "-C", installPath, "checkout", repo.Tag);
        }

        Utils.RunShellCommand("git", "-C", installPath, "pull");

    }

    public class Settings : CommandSettings
    {
        [CommandArgument(0, "[name]")] public string? Name { get; set; }

        [CommandOption("-o|--out")]
        [DefaultValue(null)]
        public string? OutputPath { get; set; }


    }
}
