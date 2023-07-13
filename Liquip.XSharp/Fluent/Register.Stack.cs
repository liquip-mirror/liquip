using XSharp;

namespace Liquip.XSharp.Fluent;

public static partial class RegisterEx
{
    /// <summary>
    /// </summary>
    /// <param name="me"></param>
    /// <param name="destinationValue"></param>
    /// <param name="isIndirect"></param>
    /// <param name="displacement"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public static FluentXSharpX86 Push(
        this FluentXSharpX86 me,
        uint destinationValue,
        bool isIndirect = false,
        int? displacement = null,
        XSRegisters.RegisterSize size = XSRegisters.RegisterSize.Int32)
    {
        XS.Push(destinationValue, isIndirect, displacement, size);
        return me;
    }

    /// <summary>
    /// </summary>
    /// <param name="me"></param>
    /// <param name="register"></param>
    /// <param name="isIndirect"></param>
    /// <param name="displacement"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public static FluentXSharpX86 Push(
        this FluentXSharpX86 me,
        XSRegisters.Register register,
        bool isIndirect = false,
        int? displacement = null,
        XSRegisters.RegisterSize size = XSRegisters.RegisterSize.Int32)
    {
        XS.Push(register, isIndirect, displacement, size);
        return me;
    }

    /// <summary>
    /// </summary>
    /// <param name="me"></param>
    /// <param name="label"></param>
    /// <param name="isIndirect"></param>
    /// <param name="displacement"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public static FluentXSharpX86 Push(
        this FluentXSharpX86 me,
        string label,
        bool isIndirect = false,
        int? displacement = null,
        XSRegisters.RegisterSize size = XSRegisters.RegisterSize.Int32)
    {
        XS.Push(label, isIndirect, displacement, size);
        return me;
    }

    /// <summary>
    /// </summary>
    /// <param name="me"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static FluentXSharpX86 Pop(
        this FluentXSharpX86 me,
        XSRegisters.Register value
    )
    {
        XS.Pop(value);
        return me;
    }

    /// <summary>
    /// </summary>
    /// <param name="me"></param>
    /// <returns></returns>
    public static FluentXSharpX86 Pushfd(this FluentXSharpX86 me)
    {
        me.Pushfd();
        return me;
    }

    /// <summary>
    /// </summary>
    /// <param name="me"></param>
    /// <returns></returns>
    public static FluentXSharpX86 Popfd(this FluentXSharpX86 me)
    {
        me.Popfd();
        return me;
    }

    public static FluentXSharpX86 PopAllRegisters(this FluentXSharpX86 me)
    {
        XS.PopAllRegisters();
        return me;
    }

    public static FluentXSharpX86 PushAllRegisters(this FluentXSharpX86 me)
    {
        XS.PushAllRegisters();
        return me;
    }
}
