namespace Liquip.WASM.VM;

public class State
{
    public Function function;
    public int ip;
    public int labelPtr;
    public Value[] locals;
    public Label[] lStack;
    public Memory memory;
    public Inst[] program;
    public int vStackPtr;
}
