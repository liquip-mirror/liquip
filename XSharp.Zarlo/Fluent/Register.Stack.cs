namespace XSharp.Zarlo.Fluent;

public static partial class RegisterEx
{

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

    public static FluentXSharp Pop(
        this FluentXSharp me,
        XSRegisters.Register value
    )
    {
        XS.Pop(value);
        return me;
    }

    public static FluentXSharp Pushfd(this FluentXSharp me)
    {
        me.Pushfd();
        return me;
    }

    public static FluentXSharp Popfd(this FluentXSharp me)
    {
        me.Popfd();
        return me;
    }

}