namespace Zarlo.Asm.Assembler.OpCodes;

public class LabelOpCode : IBaseOpCode
{
    private readonly string _label;

    public LabelOpCode(string label)
    {
        _label = label;
    }

    public void Emit(IBaseAssembler assembler)
    {
        assembler.AddLabel(_label, assembler.GetPC());
    }

    public uint Size()
    {
        return 0;
    }
}

public static class LabelOpCodeEx
{
    public static IOpCodes AddLabel(this IOpCodes asm, string label)
    {
        asm.GetAssembler().AddOpCode(new LabelOpCode(label));
        return asm;
    }
}
