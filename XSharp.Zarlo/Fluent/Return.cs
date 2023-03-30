namespace XSharp.Zarlo.Fluent;

public static class ReturnEx
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="me"></param>
    /// <returns></returns>
    public static FluentXSharp Return(this FluentXSharp me)
    {
        XS.Return();
        return me;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="me"></param>
    /// <param name="aReturnSize"></param>
    /// <returns></returns>
    public static FluentXSharp Return(this FluentXSharp me, uint aReturnSize)
    {
        XS.Return(aReturnSize);
        return me;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="me"></param>
    /// <returns></returns>
    public static FluentXSharp InterruptReturn(this FluentXSharp me)
    {
        XS.InterruptReturn();
        return me;
    }
}