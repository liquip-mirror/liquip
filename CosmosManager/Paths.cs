using System.Reflection;

namespace CosmosManager;

public class Paths
{
    private static string? _Root;

    public static string Root
    {
        get
        {
            if (_Root == null)
            {
                SetRoot();
            }

            return _Root!;
        }
    }

    public static string RootPath => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

    public static string ContentPath => Path.Join(RootPath, "content");
    public static string CosmosContentPath => Path.Join(ContentPath, "cosmos");
    public static string CosmosManagerPath => Path.Join(Root, "CosmosManager");
    public static string CosmosManagerPatchesPath => Path.Join(CosmosManagerPath, "Patches");

    public static string CosmosSrcPath => Path.Join(CosmosManagerPath, "src");
    public static string CosmosBinPath => Path.Join(CosmosManagerPath, "bin");

    public static void SetRoot(string? path = null)
    {
        path ??= Directory.GetCurrentDirectory();
        _Root = path;
    }
}
