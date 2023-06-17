namespace Zarlo.Asm.Assembler;

public interface IBaseOpCode
{

    uint Size();
    void Emit(IBaseAssembler assembler);

}
