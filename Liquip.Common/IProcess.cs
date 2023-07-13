namespace Liquip.Common;

public interface IProcess
{
    public void SetWorkingDir(string wd);
    public void Env(object env);
    public int Main(string[] args);
}
