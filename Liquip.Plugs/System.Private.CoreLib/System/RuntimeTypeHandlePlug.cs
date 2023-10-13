using System;
using IL2CPU.API.Attribs;

namespace Liquip.Plugs.System.Private.CoreLib.System;

[Plug(Target = typeof(RuntimeTypeHandle))]
public class RuntimeTypeHandlePlug
{

    [PlugMethod(Signature = "System_Int32__System_RuntimeTypeHandle_GetNumVirtuals_System_RuntimeType_")]
    public static UInt32 GetNumVirtuals(object aRuntimeType)
    {
        throw new NotImplementedException();
    }

    [PlugMethod(Signature = "System_RuntimeMethodHandleInternal__System_RuntimeTypeHandle_GetMethodAt_System_RuntimeType__System_Int32_")]
    public static object GetMethodAt(object aRuntimeType, int index)
    {
        throw new NotImplementedException();
    }

}
