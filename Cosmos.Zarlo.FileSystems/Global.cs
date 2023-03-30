using Cosmos.Debug.Kernel;
using Cosmos.HAL;
using Debugger = Cosmos.Debug.Kernel.Debugger;

namespace Cosmos.Zarlo.FileSystems;

public class Global
{
    public static readonly Debugger mFileSystemDebugger = DebuggerFactory.CreateDebugger("System", "FileSystemCustom");
}