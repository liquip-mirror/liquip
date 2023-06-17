namespace Zarlo.Asm.Assembler.OpCodes;

public class DataOpCode : IBaseOpCode
{

    private readonly byte[] _content;

    public DataOpCode(byte[] content)
    { 
        _content = content;
    }

    public DataOpCode(uint size)
    { 
        _content = new byte[size];
    }

    public void Emit(IBaseAssembler assembler)
    {
        assembler.Emit(assembler.GetPC(), _content);
        assembler.AppendPC(Size());
    }

    public uint Size() => (uint)_content.Length;
}

public static class DataOpCodeEx
{
    public static IOpCodes AddData(this IOpCodes asm, byte[] content)
    {
        asm.GetAssembler().AddOpCode(new DataOpCode(content));
        return asm;
    }

    public static IOpCodes AddData(this IOpCodes asm, uint size)
    {
        asm.GetAssembler().AddOpCode(new DataOpCode(size));
        return asm;
    }

    public static IOpCodes AddData(this IOpCodes asm, byte[] content, string label)
    {
        return asm
        .AddLabel(label)
        .AddData(content);
    }

    public static IOpCodes AddData(this IOpCodes asm, uint size, string label)
    {
        return asm
        .AddLabel(label)
        .AddData(size);
    }

}
