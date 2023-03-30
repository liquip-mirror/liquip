namespace XSharp.Zarlo.Fluent;

public static class MathDivideEx
{
    
        public static FluentXSharp Add(
        this FluentXSharp me, 
        XSRegisters.Register register, 
        uint valueToAdd)
    {
        XS.Add(register, valueToAdd);
        return me;
    }

    public static FluentXSharp Add(
        this FluentXSharp me,
        XSRegisters.Register register,
        uint valueToAdd,
        bool destinationIsIndirect = false)
    {
        XS.Add(register, valueToAdd, destinationIsIndirect);
        return me;
    }
    
    public static FluentXSharp Add(
        this FluentXSharp me,
        XSRegisters.Register register,
        XSRegisters.Register valueToAdd,
        bool destinationIsIndirect = false)
    {
        XS.Add(register, valueToAdd, destinationIsIndirect);
        return me;
    }
    
    public static FluentXSharp AddWithCarry(
        this FluentXSharp me,
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

    public static FluentXSharp AddWithCarry(
        this FluentXSharp me,
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

    public static FluentXSharp AddWithCarry(
        this FluentXSharp me,
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

    public static FluentXSharp AddWithCarry(
        this FluentXSharp me,
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

    public static FluentXSharp AddWithCarry(
        this FluentXSharp me,
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

    public static FluentXSharp AddWithCarry(
        this FluentXSharp me,
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
    
    
    public static FluentXSharp Divide(
        this FluentXSharp me,
        uint destinationValue,
        bool isIndirect = false,
        int? displacement = null,
        XSRegisters.RegisterSize size = XSRegisters.RegisterSize.Int32)
    {
        XS.Divide(destinationValue, isIndirect, displacement, size);
        return me;
    }

    public static void Divide(
        this FluentXSharp me,
        XSRegisters.Register register,
        bool isIndirect = false,
        int? displacement = null,
        XSRegisters.RegisterSize? size = null)
    {
        XS.Divide(register, isIndirect, displacement, size);
    }

    public static FluentXSharp Divide(
        this FluentXSharp me,
        Label label,
        bool isIndirect = false,
        int? displacement = null,
        XSRegisters.RegisterSize size = XSRegisters.RegisterSize.Int32) =>
        me.Divide(label.ToString(), isIndirect, displacement, size);
    
    public static FluentXSharp Divide(
        this FluentXSharp me,
        string label,
        bool isIndirect = false,
        int? displacement = null,
        XSRegisters.RegisterSize size = XSRegisters.RegisterSize.Int32)
    {
        XS.Divide(label, isIndirect, displacement, size);
        return me;
    }

    public static FluentXSharp IntegerDivide(
        this FluentXSharp me,
        uint destinationValue,
        bool isIndirect = false,
        int? displacement = null,
        XSRegisters.RegisterSize size = XSRegisters.RegisterSize.Int32)
    {
        XS.IntegerDivide(destinationValue, isIndirect, displacement, size);
        return me;
    }

    public static FluentXSharp IntegerDivide(
        this FluentXSharp me,
        XSRegisters.Register register,
        bool isIndirect = false,
        int? displacement = null,
        XSRegisters.RegisterSize? size = null)
    {
        XS.IntegerDivide(register, isIndirect, displacement, size);
        return me;
    }

    public static FluentXSharp IntegerDivide(
        this FluentXSharp me,
        string label,
        bool isIndirect = false,
        int? displacement = null,
        XSRegisters.RegisterSize size = XSRegisters.RegisterSize.Int32)
    {
        XS.IntegerDivide(label, isIndirect, displacement, size);
        return me;
    }
    
    public static FluentXSharp IntegerDivide(
        this FluentXSharp me,
        Label label,
        bool isIndirect = false,
        int? displacement = null,
        XSRegisters.RegisterSize size = XSRegisters.RegisterSize.Int32)
        => me.IntegerDivide(label.ToString(), isIndirect, displacement, size);
        
    
}