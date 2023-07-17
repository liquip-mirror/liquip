namespace CosmosManager;

public class ConfigFile
{

    public GitRepo Cosmos { get; set; }
    public GitRepo Common { get; set; }
    public GitRepo IL2CPU { get; set; }
    public GitRepo XSharp { get; set; }

}

public class GitRepo
{
    public string Uri { get; set; }
    public string? Tag { get; set; }
    public string? Commit { get; set; }
    public string? Branch { get; set; }
}
