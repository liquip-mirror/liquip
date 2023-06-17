namespace Zarlo.Asm.Assembler.x86;

public class x86Register : IBaseRegister
{
    private readonly string _name;
    private readonly uint _size;
    private readonly string[] _sameSpace;
    private readonly byte _code;
    private readonly bool _is4bits;
    private IBaseRegister[]? _sameSpaceData;

    public x86Register(string name, uint size, byte code): this(name, size, code, false)
    { }
    public x86Register(string name, uint size, byte code, params string[] sameSpace): this(name, size, code, false, sameSpace)
    { }

    public x86Register(string name, uint size, byte code, bool is4bits)
    {
        _name = name;
        _size = size;
        _sameSpaceData = Array.Empty<IBaseRegister>();
        _sameSpace = Array.Empty<string>();
        _code = code;
        _is4bits = is4bits;
        x86Registers.Register(this);
    }

    public x86Register(string name, uint size, byte code, bool is4bits, params string[] sameSpace)
    {
        _name = name;
        _size = size;
        _sameSpace = sameSpace;
        _sameSpaceData = null;
        _code = code;
        _is4bits = is4bits;
        x86Registers.Register(this);
    }

    public bool Is4bits() => _is4bits;
    public byte Code() => _code;
    public byte Code(byte shift) =>  unchecked((byte)(_code >> shift));

    public string Name() => _name;

    public IBaseRegister[] SameSpace()
    {
        if (_sameSpaceData == null)
        {
            _sameSpaceData = new IBaseRegister[_sameSpace.Length];
            for (int i = 0; i < _sameSpace.Length; i++)
            {
                _sameSpaceData[i] = x86Registers.Get(_sameSpace[i]);
            }
        }

        return _sameSpaceData!;
    }

    public uint Size() => _size;
}