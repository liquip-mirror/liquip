namespace XSharp.Zarlo.Fluent;

public static partial class RegisterEx
{
    public static FluentXSharp Set(
        this FluentXSharp me,
        XSRegisters.Register destination,
        uint value,
        bool destinationIsIndirect = false,
        int? destinationDisplacement = null,
        bool sourceIsIndirect = false,
        int? sourceDisplacement = null,
        XSRegisters.RegisterSize? size = null)
    {
        XS.Set(
            destination,
            value,
            destinationIsIndirect,
            destinationDisplacement,
            sourceIsIndirect,
            sourceDisplacement,
            size
        );
        return me;
    }

    public static FluentXSharp Set(
        this FluentXSharp me,
        string destination,
        XSRegisters.Register source,
        bool destinationIsIndirect = false,
        int? destinationDisplacement = null,
        bool sourceIsIndirect = false,
        int? sourceDisplacement = null,
        XSRegisters.RegisterSize? size = null)
    {
        XS.Set(destination, source, destinationIsIndirect, destinationDisplacement, sourceIsIndirect,
            sourceDisplacement, size);
        return me;
    }

    public static FluentXSharp Set(
        this FluentXSharp me,
        string destination,
        uint value,
        bool destinationIsIndirect = false,
        int? destinationDisplacement = null,
        bool sourceIsIndirect = false,
        int? sourceDisplacement = null,
        XSRegisters.RegisterSize size = XSRegisters.RegisterSize.Int32)
    {
        XS.Set(destination, value, destinationIsIndirect, destinationDisplacement, sourceIsIndirect, 
            sourceDisplacement, size);
        return me;
    }

    public static FluentXSharp Set(
        this FluentXSharp me,
        string destination,
        string source,
        bool destinationIsIndirect = false,
        int? destinationDisplacement = null,
        bool sourceIsIndirect = false,
        int? sourceDisplacement = null,
        XSRegisters.RegisterSize size = XSRegisters.RegisterSize.Int32)
    {
        XS.Set(destination, source, destinationIsIndirect, destinationDisplacement, sourceIsIndirect,
            sourceDisplacement, size);
        return me;
    }

    public static FluentXSharp Set(
        this FluentXSharp me,
        XSRegisters.Register destination,
        string sourceLabel,
        bool destinationIsIndirect = false,
        int? destinationDisplacement = null,
        bool sourceIsIndirect = false,
        int? sourceDisplacement = null,
        XSRegisters.RegisterSize? size = null)
    {
        XS.Set(destination, sourceLabel, destinationIsIndirect, destinationDisplacement, sourceIsIndirect,
            sourceDisplacement, size);
        return me;
    }

    public static FluentXSharp Increment(
        this FluentXSharp me,
        XSRegisters.Register value)
    {
        XS.Increment(value);
        return me;
    }

    public static FluentXSharp Decrement(
        this FluentXSharp me,
        XSRegisters.Register value)
    {
        XS.Decrement(value);
        return me;
    }
}