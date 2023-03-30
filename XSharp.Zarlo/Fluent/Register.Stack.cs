namespace XSharp.Zarlo.Fluent;

public static partial class RegisterEx
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="me"></param>
    /// <param name="destinationValue"></param>
    /// <param name="isIndirect"></param>
    /// <param name="displacement"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public static FluentXSharp Push(
        this FluentXSharp me,
        uint destinationValue,
        bool isIndirect = false,
        int? displacement = null,
        XSRegisters.RegisterSize size = XSRegisters.RegisterSize.Int32)
    {
        XS.Push(destinationValue, isIndirect, displacement, size);
        return me;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="me"></param>
    /// <param name="register"></param>
    /// <param name="isIndirect"></param>
    /// <param name="displacement"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public static FluentXSharp Push(
        this FluentXSharp me,
        XSRegisters.Register register,
        bool isIndirect = false,
        int? displacement = null,
        XSRegisters.RegisterSize size = XSRegisters.RegisterSize.Int32)
    {
        XS.Push(register, isIndirect, displacement, size);
        return me;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="me"></param>
    /// <param name="label"></param>
    /// <param name="isIndirect"></param>
    /// <param name="displacement"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public static FluentXSharp Push(
        this FluentXSharp me,
        string label,
        bool isIndirect = false,
        int? displacement = null,
        XSRegisters.RegisterSize size = XSRegisters.RegisterSize.Int32)
    {
        XS.Push(label, isIndirect, displacement, size);
        return me;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="me"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static FluentXSharp Pop(
        this FluentXSharp me,
        XSRegisters.Register value
    )
    {
        XS.Pop(value);
        return me;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="me"></param>
    /// <returns></returns>
    public static FluentXSharp Pushfd(this FluentXSharp me)
    {
        me.Pushfd();
        return me;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="me"></param>
    /// <returns></returns>
    public static FluentXSharp Popfd(this FluentXSharp me)
    {
        me.Popfd();
        return me;
    }
    
    public static FluentXSharp PopAllRegisters(this FluentXSharp me)
    {
        XS.PopAllRegisters();
        return me;
    }

    public static FluentXSharp PushAllRegisters(this FluentXSharp me)
    {
        XS.PushAllRegisters();
        return me;
    }
    
}