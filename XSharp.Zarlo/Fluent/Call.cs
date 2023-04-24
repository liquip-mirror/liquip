
using XSharp;
using XSharp.Assembler;
using XSharp.Assembler.x86;
using static XSharp.XSRegisters;


namespace XSharp.Zarlo.Fluent;

public static class CallEx
{
    public static FluentXSharpX86 Call(this FluentXSharpX86 me, Label label) =>
        me.Call(label.ToString());

    public static FluentXSharpX86 Call(this FluentXSharpX86 me, string target)
    {
        XS.Call(target);
        return me;
    }

    public static FluentXSharpX86 Call(this FluentXSharpX86 me, XSRegisters.Register32 register)
    {
        XS.Call(register);
        return me;
    }
    
}