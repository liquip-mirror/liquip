namespace Liquip.Asm.Assembler;

public interface IOpCodes : IFluentInterface
{
    IBaseAssembler GetAssembler();
}
