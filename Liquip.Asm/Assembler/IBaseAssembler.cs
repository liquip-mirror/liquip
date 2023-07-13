using Liquip.Memory;

namespace Liquip.Asm.Assembler;

public interface IBaseAssembler : IDisposable
{
    void Build(ref Pointer ptr, uint offset);
    Span<byte> Build();

    void Emit(uint offset, Span<byte> data);

    void AppendPC(uint offset);
    void SetPC(uint address);
    uint GetPC();

    void AddLabel(string name, uint offset);
    void UseLabel(string name, uint offset);

    IBaseOpCode[] GetOpCodes();
    uint GetOpCodeCount();
    void RemoveOpCode(uint index);
    void RemoveOpCode(uint index, uint count);

    void AddOpCode(IBaseOpCode opCode);
    void AddOpCode(IBaseOpCode opCode, uint index);

    void AddOpCodes(IBaseOpCode[] opCodes);
    void AddOpCodes(IBaseOpCode[] opCodes, uint index);
}
