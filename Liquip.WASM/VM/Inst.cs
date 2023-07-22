namespace Liquip.WASM.VM;


public struct Inst
{

    public Instruction.Instruction i;

    public uint pointer;

    public uint opCode;

    public uint i32;

    public ulong i64;

    public float f32;

    public double f64;

    public int pos;
    public ulong pos64;
    public int[] table;
    public Value value;
    public Value[] values;
    public int a, b, c;
    public Inst[] optimalProgram;
}
