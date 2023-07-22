
namespace Liquip.WASM;

public class Global
{
    public uint Index;
    private readonly bool _mutable;
    public string Name;
    public byte Type;
    private Value _value;


    public Global(byte type, bool mutable, Value value, uint index)
    {
        Type = type;
        _mutable = mutable;
        this._value = value;
        Index = index;
        Name = "$global" + Index;
    }

    public Value GetValue()
    {
        return _value;
    }

    public void Set(Value value, bool force = false)
    {
        if (!_mutable && !force)
        {
            throw new Exception("Global not mutable");
        }

        if (value.type != this._value.type)
        {
            throw new TrapException("indirect call type mismatch");
        }

        this._value = value;
    }

    public void SetName(string name)
    {
        Name += "(" + name + ")";
    }
}
