using Liquip;

using Liquip.Memory;
using Liquip.Collections;

namespace Liquip.Threading.Core.Context;

public struct IpcMessageContext
{ 
    public uint From { get; set; }
    public Pointer Data { get; set; }
}

public class IpcContext
{

    public uint? Tid { get; set; }
    public uint? Pid { get; set; }

    public ContextList<IpcMessageContext> Messages { get; set; }

}
