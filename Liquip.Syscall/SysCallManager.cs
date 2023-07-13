using Cosmos.Core;
using Liquip.Memory;

namespace Liquip.Syscall;

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
