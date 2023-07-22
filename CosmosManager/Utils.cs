using System.Diagnostics;
using Spectre.Console;

namespace CosmosManager;

public class Utils
{
    public static void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
    {
        // Get information about the source directory
        var dir = new DirectoryInfo(sourceDir);

        // Check if the source directory exists
        if (!dir.Exists)
        {
            throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");
        }

        // Cache directories before we start copying
        var dirs = dir.GetDirectories();

        // Create the destination directory
        Directory.CreateDirectory(destinationDir);

        // Get the files in the source directory and copy to the destination directory
        foreach (var file in dir.GetFiles())
        {
            var targetFilePath = Path.Combine(destinationDir, file.Name);
            file.CopyTo(targetFilePath);
        }

        // If recursive and copying subdirectories, recursively call this method
        if (!recursive)
        {
            return;
        }

        foreach (var subDir in dirs)
        {
            var newDestinationDir = Path.Combine(destinationDir, subDir.Name);
            CopyDirectory(subDir.FullName, newDestinationDir, true);
        }
    }


    public static void RunShellCommand(string command) => RunShellCommand(command, "");

    public static void RunShellCommand(string command, params string[] args)
    {
        AnsiConsole.MarkupLine("Running command {0} {1}", command, string.Join(' ', args));
        var process = new Process()
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = command,
                Arguments = string.Join(' ', args),
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            }
        };
        process.OutputDataReceived += (sender, args) => AnsiConsole.WriteLine(args.Data ?? "");
        process.Start();
        process.BeginOutputReadLine();
        process.WaitForExit();

    }

    public static string StringBool(bool boolean)
    {
        return boolean ? "[lime]Yes[/]" : "[maroon]No[/]";
    }


    public static bool IsInPath(string fileName)
    {
        if (File.Exists(fileName))
            return true;

        var values = Environment.GetEnvironmentVariable("PATH");
        return values.Split(Path.PathSeparator)
            .Select(path => Path.Combine(path, fileName))
            .Any(fullPath => File.Exists(fullPath));
    }

}
