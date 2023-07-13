using Cosmos.Core;
using Zarlo.Cosmos.Memory;

namespace Zarlo.Cosmos.Syscall;

public static partial class SysCallManager
{

    public static void Initialize()
    {
    }

    public static Address Handel(ref SysCallContext context)
    {

        return new Address(GCImplementation.GetSafePointer(context));
    }

}
