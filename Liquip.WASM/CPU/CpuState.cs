namespace Liquip.WASM.CPU;

/// <summary>
///
/// </summary>
public class CpuState
{
    /// <summary>
    /// value stack
    /// </summary>
    public WasmValue[] ValueStack;

    /// <summary>
    /// pointer the current item in Value stack
    /// </summary>
    public uint ValueStackPtr;

    /// <summary>
    /// call stack
    /// </summary>
    public uint[] CallStack;

    /// <summary>
    /// pointer the current item in Call stack
    /// </summary>
    public uint CallStackPtr;

    /// <summary>
    /// the current InstructionPointer
    /// </summary>
    public uint InstructionPointer;

    /// <summary>
    /// Cache for use in the CPU
    /// </summary>
    public WasmValue CacheA;
    /// <summary>
    /// Cache for use in the CPU
    /// </summary>
    public WasmValue CacheB;
    /// <summary>
    /// Cache for use in the CPU
    /// </summary>
    public WasmValue CacheC;
    /// <summary>
    /// Cache for use in the CPU
    /// </summary>
    public WasmValue CacheD;

}


