using XSharp;

namespace Zarlo.XSharp.Fluent;

public static class MathDivideEx
{
    public static FluentXSharpX86 Add(
        this FluentXSharpX86 me,
        XSRegisters.Register register,
        uint valueToAdd)
    {
        XS.Add(register, valueToAdd);
        return me;
    }

    public static FluentXSharpX86 Add(
        this FluentXSharpX86 me,
        XSRegisters.Register register,
        uint valueToAdd,
        bool destinationIsIndirect = false)
    {
        XS.Add(register, valueToAdd, destinationIsIndirect);
        return me;
    }

    public static FluentXSharpX86 Add(
        this FluentXSharpX86 me,
        XSRegisters.Register register,
        XSRegisters.Register valueToAdd,
        bool destinationIsIndirect = false)
    {
        XS.Add(register, valueToAdd, destinationIsIndirect);
        return me;
    }

    public static FluentXSharpX86 AddWithCarry(
        this FluentXSharpX86 me,
        string destination,
        XSRegisters.Register source,
        bool destinationIsIndirect = false,
        int? destinationDisplacement = null,
        bool sourceIsIndirect = false,
        int? sourceDisplacement = null,
        XSRegisters.RegisterSize? size = null)
    {
        XS.AddWithCarry(destination, source, destinationIsIndirect, destinationDisplacement, sourceIsIndirect,
            sourceDisplacement, size);
        return me;
    }

    public static FluentXSharpX86 AddWithCarry(
        this FluentXSharpX86 me,
        string destination,
        uint value,
        bool destinationIsIndirect = false,
        int? destinationDisplacement = null,
        bool sourceIsIndirect = false,
        int? sourceDisplacement = null,
        XSRegisters.RegisterSize size = XSRegisters.RegisterSize.Int32)
    {
        XS.AddWithCarry(destination, value, destinationIsIndirect, destinationDisplacement, sourceIsIndirect,
            sourceDisplacement, size);
        return me;
    }

    public static FluentXSharpX86 AddWithCarry(
        this FluentXSharpX86 me,
        string destination,
        string source,
        bool destinationIsIndirect = false,
        int? destinationDisplacement = null,
        bool sourceIsIndirect = false,
        int? sourceDisplacement = null,
        XSRegisters.RegisterSize size = XSRegisters.RegisterSize.Int32)
    {
        XS.AddWithCarry(destination, source, destinationIsIndirect, destinationDisplacement, sourceIsIndirect,
            sourceDisplacement, size);
        return me;
    }

    public static FluentXSharpX86 AddWithCarry(
        this FluentXSharpX86 me,
        XSRegisters.Register destination,
        string sourceLabel,
        bool destinationIsIndirect = false,
        int? destinationDisplacement = null,
        bool sourceIsIndirect = false,
        int? sourceDisplacement = null,
        XSRegisters.RegisterSize? size = null)
    {
        XS.AddWithCarry(destination, sourceLabel, destinationIsIndirect, destinationDisplacement, sourceIsIndirect,
            sourceDisplacement, size);
        return me;
    }

    public static FluentXSharpX86 AddWithCarry(
        this FluentXSharpX86 me,
        XSRegisters.Register destination,
        uint value,
        bool destinationIsIndirect = false,
        int? destinationDisplacement = null,
        bool sourceIsIndirect = false,
        int? sourceDisplacement = null,
        XSRegisters.RegisterSize? size = null)
    {
        XS.AddWithCarry(destination, value, destinationIsIndirect, destinationDisplacement, sourceIsIndirect,
            sourceDisplacement, size);
        return me;
    }

    public static FluentXSharpX86 AddWithCarry(
        this FluentXSharpX86 me,
        XSRegisters.Register destination,
        XSRegisters.Register source,
        bool destinationIsIndirect = false,
        int? destinationDisplacement = null,
        bool sourceIsIndirect = false,
        int? sourceDisplacement = null)
    {
        XS.AddWithCarry(destination, source, destinationIsIndirect, destinationDisplacement, sourceIsIndirect,
            sourceDisplacement);
        return me;
    }


    public static FluentXSharpX86 Divide(
        this FluentXSharpX86 me,
        uint destinationValue,
        bool isIndirect = false,
        int? displacement = null,
        XSRegisters.RegisterSize size = XSRegisters.RegisterSize.Int32)
    {
        XS.Divide(destinationValue, isIndirect, displacement, size);
        return me;
    }

    public static void Divide(
        this FluentXSharpX86 me,
        XSRegisters.Register register,
        bool isIndirect = false,
        int? displacement = null,
        XSRegisters.RegisterSize? size = null)
    {
        XS.Divide(register, isIndirect, displacement, size);
    }

    public static FluentXSharpX86 Divide(
        this FluentXSharpX86 me,
        Label label,
        bool isIndirect = false,
        int? displacement = null,
        XSRegisters.RegisterSize size = XSRegisters.RegisterSize.Int32)
    {
        return me.Divide(label.ToString(), isIndirect, displacement, size);
    }

    public static FluentXSharpX86 Divide(
        this FluentXSharpX86 me,
        string label,
        bool isIndirect = false,
        int? displacement = null,
        XSRegisters.RegisterSize size = XSRegisters.RegisterSize.Int32)
    {
        XS.Divide(label, isIndirect, displacement, size);
        return me;
    }

    public static FluentXSharpX86 IntegerDivide(
        this FluentXSharpX86 me,
        uint destinationValue,
        bool isIndirect = false,
        int? displacement = null,
        XSRegisters.RegisterSize size = XSRegisters.RegisterSize.Int32)
    {
        XS.IntegerDivide(destinationValue, isIndirect, displacement, size);
        return me;
    }

    public static FluentXSharpX86 IntegerDivide(
        this FluentXSharpX86 me,
        XSRegisters.Register register,
        bool isIndirect = false,
        int? displacement = null,
        XSRegisters.RegisterSize? size = null)
    {
        XS.IntegerDivide(register, isIndirect, displacement, size);
        return me;
    }

    public static FluentXSharpX86 IntegerDivide(
        this FluentXSharpX86 me,
        string label,
        bool isIndirect = false,
        int? displacement = null,
        XSRegisters.RegisterSize size = XSRegisters.RegisterSize.Int32)
    {
        XS.IntegerDivide(label, isIndirect, displacement, size);
        return me;
    }

    public static FluentXSharpX86 IntegerDivide(
        this FluentXSharpX86 me,
        Label label,
        bool isIndirect = false,
        int? displacement = null,
        XSRegisters.RegisterSize size = XSRegisters.RegisterSize.Int32)
    {
        return me.IntegerDivide(label.ToString(), isIndirect, displacement, size);
    }


    public static FluentXSharpX86 ShiftLeft(this FluentXSharpX86 me, XSRegisters.Register destination, byte bitCount)
    {
        XS.ShiftLeft(destination, bitCount);
        return me;
    }

    public static FluentXSharpX86 ShiftLeft(this FluentXSharpX86 me, XSRegisters.Register destination,
        XSRegisters.Register8 bitCount, bool destinationIsIndirect = false,
        XSRegisters.RegisterSize size = XSRegisters.RegisterSize.Int32)
    {
        XS.ShiftLeft(destination, bitCount, destinationIsIndirect, size);
        return me;
    }


    public static FluentXSharpX86 Or(
        this FluentXSharpX86 me,
        string destination,
        XSRegisters.Register source,
        bool destinationIsIndirect = false,
        int? destinationDisplacement = null,
        bool sourceIsIndirect = false,
        int? sourceDisplacement = null,
        XSRegisters.RegisterSize? size = null)
    {
        XS.Or(destination, source, destinationIsIndirect, destinationDisplacement, sourceIsIndirect, sourceDisplacement,
            size);
        return me;
    }

    public static FluentXSharpX86 Or(
        this FluentXSharpX86 me,
        PlugArgument destination,
        uint value,
        bool sourceIsIndirect = false,
        int? sourceDisplacement = null,
        XSRegisters.RegisterSize size = XSRegisters.RegisterSize.Int32)
    {
        XS.Or(PlugArgument.Register, value, true, destination.Offset, sourceIsIndirect, sourceDisplacement, size);
        return me;
    }

    public static FluentXSharpX86 Or(
        this FluentXSharpX86 me,
        string destination,
        uint value,
        bool destinationIsIndirect = false,
        int? destinationDisplacement = null,
        bool sourceIsIndirect = false,
        int? sourceDisplacement = null,
        XSRegisters.RegisterSize size = XSRegisters.RegisterSize.Int32)
    {
        XS.Or(destination, value, destinationIsIndirect, destinationDisplacement, sourceIsIndirect, sourceDisplacement,
            size);
        return me;
    }

    public static FluentXSharpX86 Or(
        this FluentXSharpX86 me,
        string destination,
        string source,
        bool destinationIsIndirect = false,
        int? destinationDisplacement = null,
        bool sourceIsIndirect = false,
        int? sourceDisplacement = null,
        XSRegisters.RegisterSize size = XSRegisters.RegisterSize.Int32)
    {
        XS.Or(destination, source, destinationIsIndirect, destinationDisplacement, sourceIsIndirect, sourceDisplacement,
            size);
        return me;
    }

    public static FluentXSharpX86 Or(
        this FluentXSharpX86 me,
        XSRegisters.Register destination,
        Label sourceLabel,
        bool destinationIsIndirect = false,
        int? destinationDisplacement = null,
        bool sourceIsIndirect = false,
        int? sourceDisplacement = null,
        XSRegisters.RegisterSize? size = null)
    {
        return me.Or(destination, sourceLabel.ToString(), destinationIsIndirect, destinationDisplacement,
            sourceIsIndirect, sourceDisplacement, size);
    }


    public static FluentXSharpX86 Or(
        this FluentXSharpX86 me,
        XSRegisters.Register destination,
        string sourceLabel,
        bool destinationIsIndirect = false,
        int? destinationDisplacement = null,
        bool sourceIsIndirect = false,
        int? sourceDisplacement = null,
        XSRegisters.RegisterSize? size = null)
    {
        XS.Or(destination, sourceLabel, destinationIsIndirect, destinationDisplacement, sourceIsIndirect,
            sourceDisplacement, size);
        return me;
    }

    public static FluentXSharpX86 Or(
        this FluentXSharpX86 me,
        XSRegisters.Register destination,
        uint value,
        bool destinationIsIndirect = false,
        int? destinationDisplacement = null,
        bool sourceIsIndirect = false,
        int? sourceDisplacement = null,
        XSRegisters.RegisterSize? size = null)
    {
        XS.Or(destination, value, destinationIsIndirect, destinationDisplacement, sourceIsIndirect, sourceDisplacement,
            size);
        return me;
    }

    public static FluentXSharpX86 Or(
        this FluentXSharpX86 me,
        XSRegisters.Register destination,
        XSRegisters.Register source,
        bool destinationIsIndirect = false,
        int? destinationDisplacement = null,
        bool sourceIsIndirect = false,
        int? sourceDisplacement = null)
    {
        XS.Or(destination, source, destinationIsIndirect, destinationDisplacement, sourceIsIndirect,
            sourceDisplacement);
        return me;
    }
}
