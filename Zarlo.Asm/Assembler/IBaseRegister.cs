namespace Zarlo.Asm.Assembler;

public interface IBaseRegister
{
    /// <summary>
    ///     the mane of the register
    /// </summary>
    /// <returns></returns>
    string Name();

    /// <summary>
    ///     get the size of the register in bits
    /// </summary>
    /// <returns></returns>
    uint Size();

    /// <summary>
    ///     get registers that take up the same space in the cpu
    /// </summary>
    /// <returns></returns>
    IBaseRegister[] SameSpace();
}
