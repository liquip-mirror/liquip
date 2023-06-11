using IL2CPU.API.Attribs;

namespace Zarlo.Cosmos.Plugs.System;


[Plug(Target = typeof(Delegate))]
public static unsafe class DelegateImpl
{ 
    public static int GetHashCode(
        Delegate aThis, 
        [FieldAccess(Name = "System.IntPtr System.Delegate._methodPtr")] ref IntPtr aAddress
        )
    {
        return (int)aAddress.ToPointer();
    }
}