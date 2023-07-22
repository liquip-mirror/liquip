using System;

namespace Liquip.WASM;

public class Table
{
    public readonly uint MinSize;
    public readonly uint MaxSize;
    public readonly uint CurrentSize;
    private readonly uint[] _table;
    public byte Type = 0x70;

    public Table(byte type, uint minSize, uint maxSize)
    {
        Type = type;
        MinSize = minSize;
        MaxSize = maxSize;
        CurrentSize = MinSize;

        _table = new uint[CurrentSize];
    }

    public bool CompatibleWith(Table t)
    {
        return MinSize == t.MinSize && MaxSize == t.MaxSize && Type == t.Type;
    }

    public override string ToString()
    {
        return "<table type: 0x" + Type.ToString("X") + ", min: " + MinSize + ", max: " + MaxSize + ", cur: " +
               CurrentSize + ">";
    }

    public void Set(uint offset, uint funcidz)
    {
        if (offset >= _table.Length)
        {
            throw new Exception("Invalid table offset");
        }

        _table[offset] = funcidz;
    }

    public uint Get(uint offset)
    {
        if (offset >= _table.Length)
        {
            throw new TrapException("undefined element");
        }

        return _table[offset];
    }
}
