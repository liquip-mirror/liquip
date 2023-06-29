using System.Collections.Generic;
using System.Text;
using Zarlo.Cosmos.Memory;

namespace Zarlo.Cosmos.ELF;

public class ArgumentWriter
{
    private uint _offset = 50;
    private readonly Pointer _stack;

    internal ArgumentWriter(Pointer stack)
    {
        _stack = stack;
    }

    public void Push(byte value)
    {
        unsafe
        {
            _stack.Ptr[_offset] = value;
            _offset += sizeof(byte);
        }
    }

    public void Push(uint value)
    {
        unsafe
        {
            _stack.Ptr[_offset] = value;
            _offset += sizeof(uint);
        }
    }

    public void Push(string str)
    {
        unsafe
        {
            var output = new List<byte>();
            output.AddRange(Encoding.UTF8.GetBytes(str));
            output.Add(0x00);
            fixed (byte* ptr = output.ToArray())
            {
                Push((uint)ptr);
            }
        }
    }
}
