using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Fluid;
using Spectre.Console;
using Spectre.Console.Cli;

namespace CosmosManager;

public class InitCommand : Command<InitCommand.Settings>
{
    public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
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

        Utils.CopyDirectory(Paths.CosmosContentPath, Paths.CosmosManagerPath, true);

        var defaultConfig = new ConfigFile()
        {
            Cosmos = new GitRepo()
            {
                Uri = "https://github.com/zarlo/Cosmos.git",
                Branch = "packages"
            },
            Common = new GitRepo()
            {
                Uri = "https://github.com/CosmosOS/Common.git",
                Branch = "master"
            },
            IL2CPU = new GitRepo()
            {
                Uri = "https://github.com/CosmosOS/Il2CPU.git",
                Branch = "master"
            },
            XSharp = new GitRepo()
            {
                Uri = "https://github.com/CosmosOS/XSharp.git",
                Branch = "master"
            }
        };
        var options = new JsonSerializerOptions()
        {
            WriteIndented = true
        };
        File.WriteAllText(Path.Join(Paths.CosmosManagerPath, "config.json"), JsonSerializer.Serialize(defaultConfig, options));

        if (settings.MakeFile)
        {
            File.Copy(Path.Join(Paths.ContentPath, "root", "Makefile"), Path.Join(Paths.Root, "Makefile"));
        }

        File.Copy(Path.Join(Paths.ContentPath, "root", "Directory.Build.props"), Path.Join(Paths.Root, "Directory.Build.props"));
        File.Copy(Path.Join(Paths.ContentPath, "root", "Directory.Build.targets"), Path.Join(Paths.Root, "Directory.Build.targets"));
        File.Copy(Path.Join(Paths.ContentPath, "root", "nuget.config"), Path.Join(Paths.Root, "nuget.config"));


        if (settings.Projects)
        {

            var project_name = settings.Name;
            var project_name_plug = settings.Name + "_Plugs";
            var project_name_system = settings.Name + ".System";
            var project_name_hal = settings.Name + ".HAL";
            var project_name_core = settings.Name + ".Core";

            var version = "0.1.0-localbuild";

            MakeProject(new TemplateData()
            {
                ProjectName = project_name,
                ProjectReference = new List<string>()
                {
                    project_name_system
                },
                PackageReference = new List<PackageReference>()
                {
                    new PackageReference()
                    {
                        Name = "Cosmos.System2",
                        Version = version
                    },
                    new PackageReference()
                    {
                        Name = "Cosmos.Build",
                        Version = version
                    },
                    new PackageReference()
                    {
                        Name = "Cosmos.Plugs",
                        Version = version
                    },
                    new PackageReference()
                    {
                        Name = "Cosmos.Debug.Kernel",
                        Version = version
                    }
                }
            },
                "kernel.xml"
            );

            MakeProject(new TemplateData()
            {
                ProjectName = project_name_plug,
                ProjectReference = new List<string>()
                {
                    project_name,
                    project_name_system,
                    project_name_hal,
                    project_name_core
                },
                PackageReference = new List<PackageReference>()
                {
                    new PackageReference()
                    {
                        Name = "XSharp",
                        Version = version
                    }
                }
            },
                "project.xml"
            );

            MakeProject(new TemplateData()
            {
                ProjectName = project_name_system,
                ProjectReference = new List<string>()
                {
                    project_name_core
                },
                PackageReference = new List<PackageReference>()
                {
                    new PackageReference()
                    {
                        Name = "Cosmos.HAL2",
                        Version = version
                    },
                    new PackageReference()
                    {
                        Name = "Cosmos.System2",
                        Version = version
                    }
                }
            },
                "project.xml"
            );

            MakeProject(new TemplateData()
            {
                ProjectName = project_name_hal,
                ProjectReference = new List<string>()
                {
                    project_name_core
                },
                PackageReference = new List<PackageReference>()
                {
                    new PackageReference()
                    {
                        Name = "Cosmos.Core",
                        Version = version
                    },
                    new PackageReference()
                    {
                        Name = "Cosmos.System2",
                        Version = version
                    }
                }
            },
                "project.xml"
            );

            MakeProject(new TemplateData()
            {
                ProjectName = project_name_core,
                ProjectReference = new List<string>()
                {
                },
                PackageReference = new List<PackageReference>()
                {
                    new PackageReference()
                    {
                        Name = "Cosmos.Core",
                        Version = version
                    }
                }
            },
            "project.xml"
            );

            Utils.RunShellCommand("dotnet", "new", "sln", "-n", project_name, "-o", settings.OutputPath);

            var sln_name = Path.Join(settings.OutputPath, project_name + ".sln");

            foreach (var projectName in new string[] { project_name, project_name_system, project_name_hal, project_name_core, project_name_plug })
            {
                Utils.RunShellCommand("dotnet", "sln", sln_name, "add", Path.Join(settings.OutputPath, projectName, projectName + ".csproj"));

            }
            File.WriteAllText(Path.Join(Paths.Root, project_name, "Kernel.cs"), RenderTemplate(new
            {
                ProjectName = project_name
            },
            "Kernel.cs"
            ));

        }

        return 0;
    }

    void MakeProject(TemplateData data, string fileName)
    {
        Directory.CreateDirectory(Path.Join(Paths.Root, data.ProjectName));
        File.WriteAllText(Path.Join(Paths.Root, data.ProjectName, data.ProjectName + ".csproj"), RenderTemplate(data, fileName));
    }

    public class TemplateData
    {
        public string ProjectName { get; set; }
        public List<PackageReference> PackageReference { get; set; }
        public List<string> ProjectReference { get; set; }
    }

    public class PackageReference
    {
        public string Name { get; set; }
        public string Version { get; set; }
    }

    string RenderTemplate(object model, string fileName)
    {
        // var options = new JsonSerializerOptions()
        // {
        //     WriteIndented = true
        // };
        // Console.WriteLine("{0}", JsonSerializer.Serialize(model, options));
        var options = new TemplateOptions();
        options.MemberAccessStrategy.Register<PackageReference>();
        var parser = new FluidParser();
        var context = new TemplateContext(model, options);
        return parser
            .Parse(
                File.ReadAllText(
                    Path.Join(Paths.ContentPath, "root", fileName)
                    )
                )
            .Render(context);
    }

    public class Settings : CommandSettings
    {
        [CommandArgument(0, "[name]")] public string? Name { get; set; }

        [CommandOption("-o|--out")]
        [DefaultValue(null)]
        public string? OutputPath { get; set; }

        [CommandOption("-m|--make")]
        [DefaultValue(false)]
        public bool MakeFile { get; set; }

        [CommandOption("--project|-p")]
        [DefaultValue(false)]
        public bool Projects { get; set; }

    }
}
