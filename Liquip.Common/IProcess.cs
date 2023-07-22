namespace Liquip.Common;

public interface IProcess
{
    public void SetWorkingDir(string wd);
    public void Environment(ProcessEnvironment env);
    public int Main(string[] args);
}
