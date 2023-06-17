namespace Zarlo.Asm.Assembler.x86.OpCodes;

public class AddOpCode : IBaseOpCode
{
    private readonly x86Register? _destinationRegister;
    private readonly byte? _toAdd8;
    private readonly short? _toAdd16;
    private readonly int? _toAdd32;

    public AddOpCode(
        x86Register? destinationRegister = null,
        byte? toAdd8 = null,
        Int16? toAdd16 = null,
        Int32? toAdd32 = null
        )
    {
        _destinationRegister = destinationRegister;
        _toAdd8 = toAdd8;
        _toAdd16 = toAdd16;
        _toAdd32 = toAdd32;
    }

    public void Emit(IBaseAssembler assembler)
    {
        throw new NotImplementedException();
    }

    public uint Size()
    {
        throw new NotImplementedException();
    }
}

public static class AddOpCodeEx
{
    public static x86OpCodes Add(this x86OpCodes asm)
    {
        asm.GetAssembler().AddOpCode(new AddOpCode());
        return asm;
    }
}
