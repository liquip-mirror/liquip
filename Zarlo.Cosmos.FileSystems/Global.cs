using Cosmos.Debug.Kernel;

namespace Zarlo.Cosmos.FileSystems;

public class Global
{
    public static readonly Debugger mFileSystemDebugger = DebuggerFactory.CreateDebugger("System");
}
