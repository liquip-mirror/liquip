using Liquip.Memory;

namespace Liquip.Asm.Assembler.x86.OpCodes;

public class AddOpCode : IBaseOpCode
{
    private readonly Address? _destinationMemory;
    private readonly x86IndirectRegister? _destinationIndirectRegister;
    private readonly x86Register? _destinationRegister;
    
    private readonly short? _toAdd16;
    private readonly int? _toAdd32;
    private readonly byte? _toAdd8;
    private readonly x86IndirectRegister? _toIndirectRegister;

    public AddOpCode(
        x86Register? destinationRegister = null,
        Address? destinationMemory = null,
        x86IndirectRegister? destinationIndirectRegister = null,
        byte? toAdd8 = null,
        short? toAdd16 = null,
        int? toAdd32 = null,
        x86IndirectRegister? toIndirectRegister = null
    )
    {
        _destinationRegister = destinationRegister;
        _destinationMemory = destinationMemory;
        _destinationIndirectRegister = destinationIndirectRegister;
        _toAdd8 = toAdd8;
        _toAdd16 = toAdd16;
        _toAdd32 = toAdd32;
        _toIndirectRegister = toIndirectRegister;
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
