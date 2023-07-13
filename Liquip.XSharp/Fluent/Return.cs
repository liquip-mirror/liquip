using XSharp;

namespace Liquip.XSharp.Fluent;

public static class ReturnEx
{
    /// <summary>
    /// </summary>
    /// <param name="me"></param>
    /// <returns></returns>
    public static FluentXSharpX86 Return(this FluentXSharpX86 me)
    {
        XS.Return();
        return me;
    }

    /// <summary>
    /// </summary>
    /// <param name="me"></param>
    /// <param name="aReturnSize"></param>
    /// <returns></returns>
    public static FluentXSharpX86 Return(this FluentXSharpX86 me, uint aReturnSize)
    {
        XS.Return(aReturnSize);
        return me;
    }

    /// <summary>
    /// </summary>
    /// <param name="me"></param>
    /// <returns></returns>
    public static FluentXSharpX86 InterruptReturn(this FluentXSharpX86 me)
    {
        XS.InterruptReturn();
        return me;
    }
}
