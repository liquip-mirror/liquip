using Cosmos.Debug.Kernel;

namespace Liquip.FileSystems;

public class Global
{
    public static readonly Debugger mFileSystemDebugger = DebuggerFactory.CreateDebugger("System");
}
