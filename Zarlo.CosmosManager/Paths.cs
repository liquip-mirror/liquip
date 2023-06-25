namespace Zarlo.CosmosManager;

public class Paths
{
    public static void SetRoot(string? path)
    { 
        path ??= Directory.GetCurrentDirectory();
        Root = path;
    }


    public static string Root { get; private set; }

    public static string CosmosManagerPath => Path.Join(Root, "CosmosManager");
    public static string CosmosManagerPatchesPath => Path.Join(CosmosManagerPath, "Patches");

    public static string CosmosSrcPath => Path.Join(CosmosManagerPath, "src");
    public static string CosmosBinPath => Path.Join(CosmosManagerPath, "bin");


}
