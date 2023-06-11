
using XSharp;
using XSharp.Assembler;
using XSharp.Assembler.x86;
using static XSharp.XSRegisters;


namespace Zarlo.XSharp.Fluent;

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
    
    public static FluentXSharpX86 Loop(this FluentXSharpX86 me, ConditionalTestEnum test, Action<FluentXSharpX86> content)
    {
        me.Label(out Label o);
        content(me);
        me.Jump(o, test);
        return me;
    }

    public static FluentXSharpX86 Loop(this FluentXSharpX86 me, ConditionalTestEnum test, out Label o, Action<FluentXSharpX86> content)
    {
        me.Label(out o);
        content(me);
        me.Jump(o, test);
        return me;
    }

}