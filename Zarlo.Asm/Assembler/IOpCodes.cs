namespace Zarlo.Asm.Assembler;

public interface IOpCodes: IFluentInterface
{
    IBaseAssembler GetAssembler();
}
