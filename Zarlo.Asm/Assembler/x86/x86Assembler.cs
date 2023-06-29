using Zarlo.Cosmos.Memory;

namespace Zarlo.Asm.Assembler.x86;

public sealed class x86Assembler : IBaseAssembler
{
    private uint BaseOffset;

    private readonly List<IBaseOpCode> Codes = new();

    private readonly Dictionary<string, uint> Label = new();

    private readonly Dictionary<string, List<uint>> LabelUse = new();

    private uint PC;

    private Pointer? ptr;

    public x86OpCodes OpCodes => new(this);

    public void AddLabel(string name, uint offset)
    {
        Label.Add(name, offset);
    }

    public void AddOpCode(IBaseOpCode opCode)
    {
        Codes.Add(opCode);
    }

    public void AddOpCode(IBaseOpCode opCode, uint index)
    {
        Codes.Insert((int)index, opCode);
    }

    public void AddOpCodes(IBaseOpCode[] opCodes)
    {
        Codes.AddRange(opCodes);
    }

    public void AddOpCodes(IBaseOpCode[] opCodes, uint index)
    {
        Codes.InsertRange((int)index, opCodes);
    }

    public void AppendPC(uint offset)
    {
        PC += offset;
    }

    public void Build(ref Pointer ptr, uint offset)
    {
        this.ptr = ptr;
        // get needed size
        BaseOffset = offset;
        uint bufferSize = 0;
        foreach (var item in Codes)
        {
            bufferSize += item.Size();
        }

        if (bufferSize + offset > ptr.Size)
        {
            throw new OutOfMemoryException("the givven buffer is to small");
        }

        foreach (var item in Codes)
        {
            item.Emit(this);
        }

        foreach (var item in LabelUse)
        {
            var value = Label[item.Key];
            foreach (var address in item.Value)
            {
                ptr[offset + address] = value;
            }
        }

        this.ptr = null;
    }

    public Span<byte> Build()
    {
        uint bufferSize = 0;
        foreach (var item in Codes)
        {
            bufferSize += item.Size();
        }

        var ptr = Pointer.New(bufferSize);

        Build(ref ptr, 0);

        return ptr.ToSpan();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        throw new NotImplementedException();
    }

    public void Emit(uint offset, Span<byte> data)
    {
    }

    public uint GetOpCodeCount()
    {
        throw new NotImplementedException();
    }

    public IBaseOpCode[] GetOpCodes()
    {
        throw new NotImplementedException();
    }

    public uint GetPC()
    {
        return PC;
    }

    public void RemoveOpCode(uint index)
    {
        Codes.RemoveAt((int)index);
    }

    public void RemoveOpCode(uint index, uint count)
    {
        Codes.RemoveRange((int)index, (int)count);
    }

    public void SetPC(uint address)
    {
        PC = address;
    }

    public void UseLabel(string name, uint offset)
    {
        if (!LabelUse.ContainsKey(name))
        {
            LabelUse.Add(name, new List<uint>());
        }

        LabelUse[name].Add(offset);
    }
}
