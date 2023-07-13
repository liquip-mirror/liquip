using XSharp.Assembler.x86;
using static XSharp.XSRegisters;

namespace Liquip.XSharp;

public class RegisterYMM : RegisterXMM
{
    public RegisterYMM(string name, RegistersEnum regEnum) : base(name, regEnum)
    {
    }
}

public static partial class Registers
{
}
