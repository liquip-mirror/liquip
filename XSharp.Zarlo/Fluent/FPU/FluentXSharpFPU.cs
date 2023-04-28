namespace XSharp.Zarlo.Fluent.FPU;

public class FluentXSharpFPU : FluentXSharpX86
{
    public FluentXSharpFPU FloatCompareAndSet(XSRegisters.RegisterFPU register)
    {
        XS.FPU.FloatCompareAndSet(register);
        return this;
    }

    public FluentXSharpFPU FloatStoreAndPop(
        XSRegisters.Register32 register,
        bool isIndirect = false,
        int? displacement = null,
        XSRegisters.RegisterSize? size = null)
    {
        XS.FPU.FloatStoreAndPop(register, isIndirect, displacement, size);
        return this;
    }

    public FluentXSharpFPU FloatStoreAndPop(XSRegisters.RegisterFPU register)
    {
        XS.FPU.FloatStoreAndPop(register);
        return this;
    }

    public FluentXSharpFPU FloatLoad(
        PlugArgument plugArgument,
        bool destinationIsIndirect = false,
        XSRegisters.RegisterSize? size = null)
    => FloatLoad(XSRegisters.EBP, destinationIsIndirect, plugArgument.Offset, size);
    
    public FluentXSharpFPU FloatLoad(
        XSRegisters.Register32 register,
        bool destinationIsIndirect = false,
        int? displacement = null,
        XSRegisters.RegisterSize? size = null)
    {
        XS.FPU.FloatLoad(register, destinationIsIndirect, displacement, size);
        return this;
    }

    public FluentXSharpFPU FloatAbs()
    {
        XS.FPU.FloatAbs();
        return this;
    }

    public FluentXSharpFPU FloatInit()
    {
        XS.FPU.FloatInit();
        return this;
    }

    public FluentXSharpFPU FloatNegate()
    {
        XS.FPU.FloatNegate();
        return this;
    }

    public FluentXSharpFPU FloatRound()
    {
        XS.FPU.FloatRound();
        return this;
    }

    public FluentXSharpFPU FloatCosine()
    {
        XS.FPU.FloatCosine();
        return this;
    }

    public FluentXSharpFPU FloatSine()
    {
        XS.FPU.FloatSine();
        return this;
    }

    public FluentXSharpFPU FloatTan()
    {
        XS.FPU.FloatTan();
        return this;
    }

    public FluentXSharpFPU FloatPop()
    {
        XS.FPU.FloatPop();
        return this;
    }

    public FluentXSharpFPU FloatAdd(
        XSRegisters.Register32 destination,
        bool isIndirect = false,
        XSRegisters.RegisterSize? size = null)
    {
        XS.FPU.FloatAdd(destination, isIndirect, size);
        return this;
    }

    public FluentXSharpFPU IntLoad(
        PlugArgument plugArgument,
        bool isIndirect = false,
        XSRegisters.RegisterSize? size = null) 
        => IntLoad(XSRegisters.EBP, isIndirect, displacement: plugArgument.Offset, size);
    
    public FluentXSharpFPU IntLoad(
        XSRegisters.Register32 destination,
        bool isIndirect = false,
        int? displacement = null,
        XSRegisters.RegisterSize? size = null)
    {
        XS.FPU.IntLoad(destination, isIndirect, displacement, size);
        return this;
    }
    
    
    public FluentXSharpFPU IntStoreWithTruncate(
        XSRegisters.Register32 destination,
        bool isIndirect = false,
        XSRegisters.RegisterSize? size = null)
    {
        XS.FPU.IntStoreWithTruncate(destination, isIndirect, size);
        return this;
    }
}