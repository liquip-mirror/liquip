using Cosmos.Debug.Kernel;
using Cosmos.HAL;
using Debugger = Cosmos.Debug.Kernel.Debugger;

namespace Zarlo.Cosmos.FileSystems;

public class Global
{
    public static readonly Debugger mFileSystemDebugger = DebuggerFactory.CreateDebugger("System");
}