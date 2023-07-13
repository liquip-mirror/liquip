namespace Liquip.Asm.Assembler.x86;

public class x86OpCodes : IOpCodes
{
    private readonly x86Assembler _assembler;

    public x86OpCodes(x86Assembler assembler)
    {
        _assembler = assembler;
    }

    public IBaseAssembler GetAssembler()
    {
        return _assembler;
    }
}
