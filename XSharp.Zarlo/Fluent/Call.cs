
using XSharp;
using XSharp.Assembler;
using XSharp.Assembler.x86;
using static XSharp.XSRegisters;


namespace XSharp.Zarlo.Fluent;

public static class CallEx
{
    public static FluentXSharp Call(this FluentXSharp me, Label label) =>
        me.Call(label.ToString());

    public static FluentXSharp Call(this FluentXSharp me, string target)
    {
        XS.Call(target);
        return me;
    }

    public static FluentXSharp Call(this FluentXSharp me, XSRegisters.Register32 register)
    {
        XS.Call(register);
        return me;
    }
    
}