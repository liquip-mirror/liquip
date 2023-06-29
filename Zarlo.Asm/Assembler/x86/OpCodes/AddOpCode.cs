namespace Zarlo.Asm.Assembler.x86.OpCodes;

public class AddOpCode : IBaseOpCode
{
    private readonly int? _destinationMemory;
    private readonly x86Register? _destinationRegister;
    private readonly short? _toAdd16;
    private readonly int? _toAdd32;
    private readonly byte? _toAdd8;

    public AddOpCode(
        x86Register? destinationRegister = null,
        int? destinationMemory = null,
        byte? toAdd8 = null,
        short? toAdd16 = null,
        int? toAdd32 = null
    )
    {
        _destinationRegister = destinationRegister;
        _destinationMemory = destinationMemory;
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
