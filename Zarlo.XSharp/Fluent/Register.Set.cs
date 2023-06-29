using XSharp;

namespace Zarlo.XSharp.Fluent;

public static partial class RegisterEx
{
    /// <summary>
    /// </summary>
    /// <param name="me"></param>
    /// <param name="destination"></param>
    /// <param name="sourcePlugArgument"></param>
    /// <returns></returns>
    public static FluentXSharpX86 SetPointer(
        this FluentXSharpX86 me,
        XSRegisters.Register destination,
        PlugArgument sourcePlugArgument,
        XSRegisters.RegisterSize? size = null
    )
    {
        XS.Set(destination, PlugArgument.Register, sourceDisplacement: sourcePlugArgument.Offset,
            sourceIsIndirect: true, size: size);
        return me;
    }

    public static FluentXSharpX86 SetPointer(
        this FluentXSharpX86 me,
        PlugArgument destinationPlugArgument,
        XSRegisters.Register source,
        XSRegisters.RegisterSize? size = null
    )
    {
        XS.Set(
            PlugArgument.Register,
            source,
            destinationDisplacement: destinationPlugArgument.Offset,
            destinationIsIndirect: true,
            size: size
        );
        return me;
    }

    /// <summary>
    /// </summary>
    /// <param name="me"></param>
    /// <param name="destination"></param>
    /// <param name="plugArgument"></param>
    /// <returns></returns>
    public static FluentXSharpX86 Set(
        this FluentXSharpX86 me,
        XSRegisters.Register destination,
        PlugArgument plugArgument
    )
    {
        XS.Set(destination, PlugArgument.Register, sourceDisplacement: plugArgument.Offset);
        return me;
    }

    /// <summary>
    /// </summary>
    /// <param name="me"></param>
    /// <param name="destination"></param>
    /// <param name="value"></param>
    /// <param name="destinationIsIndirect"></param>
    /// <param name="destinationDisplacement"></param>
    /// <param name="sourceIsIndirect"></param>
    /// <param name="sourceDisplacement"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public static FluentXSharpX86 Set(
        this FluentXSharpX86 me,
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

    /// <summary>
    /// </summary>
    /// <param name="me"></param>
    /// <param name="destination"></param>
    /// <param name="source"></param>
    /// <param name="destinationIsIndirect"></param>
    /// <param name="destinationDisplacement"></param>
    /// <param name="sourceIsIndirect"></param>
    /// <param name="sourceDisplacement"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public static FluentXSharpX86 Set(
        this FluentXSharpX86 me,
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

    /// <summary>
    /// </summary>
    /// <param name="me"></param>
    /// <param name="destination"></param>
    /// <param name="value"></param>
    /// <param name="destinationIsIndirect"></param>
    /// <param name="destinationDisplacement"></param>
    /// <param name="sourceIsIndirect"></param>
    /// <param name="sourceDisplacement"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public static FluentXSharpX86 Set(
        this FluentXSharpX86 me,
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

    /// <summary>
    /// </summary>
    /// <param name="me"></param>
    /// <param name="destination"></param>
    /// <param name="source"></param>
    /// <param name="destinationIsIndirect"></param>
    /// <param name="destinationDisplacement"></param>
    /// <param name="sourceIsIndirect"></param>
    /// <param name="sourceDisplacement"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public static FluentXSharpX86 Set(
        this FluentXSharpX86 me,
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

    /// <summary>
    /// </summary>
    /// <param name="me"></param>
    /// <param name="destination"></param>
    /// <param name="sourceLabel"></param>
    /// <param name="destinationIsIndirect"></param>
    /// <param name="destinationDisplacement"></param>
    /// <param name="sourceIsIndirect"></param>
    /// <param name="sourceDisplacement"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public static FluentXSharpX86 Set(
        this FluentXSharpX86 me,
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

    /// <summary>
    /// </summary>
    /// <param name="me"></param>
    /// <param name="destination"></param>
    /// <param name="source"></param>
    /// <param name="destinationIsIndirect"></param>
    /// <param name="destinationDisplacement"></param>
    /// <param name="sourceIsIndirect"></param>
    /// <param name="sourceDisplacement"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public static FluentXSharpX86 Set(
        this FluentXSharpX86 me,
        XSRegisters.Register destination,
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

    /// <summary>
    /// </summary>
    /// <param name="me"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static FluentXSharpX86 Increment(
        this FluentXSharpX86 me,
        XSRegisters.Register value)
    {
        XS.Increment(value);
        return me;
    }

    /// <summary>
    /// </summary>
    /// <param name="me"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static FluentXSharpX86 Decrement(
        this FluentXSharpX86 me,
        XSRegisters.Register value)
    {
        XS.Decrement(value);
        return me;
    }
}
