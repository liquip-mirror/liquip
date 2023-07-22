using System;
using System.Text;
using Liquip.WASM.VM;

namespace Liquip.WASM;

public class Parser
{
    private readonly byte[] bytes;
    private uint index;
    public BaseModule BaseModule;

    public Parser(byte[] bytes, BaseModule baseModule)
    {
        this.bytes = bytes;
        index = 0;
        BaseModule = baseModule;
    }

    public uint GetPointer()
    {
        return index;
    }

    public void SetPointer(uint index)
    {
        this.index = index;
    }

    public bool Done()
    {
        return index >= bytes.Length;
    }

    public byte GetByte()
    {
        return bytes[index++];
    }

    public byte[] GetBytes(int offset, int length)
    {
        var output = new byte[length];
        Buffer.BlockCopy(bytes, offset, output, 0, length);
        return output;
    }

    public byte PeekByte()
    {
        return bytes[index];
    }

    public Inst[] GetExpr(bool debug = false)
    {
        return Instruction.Instruction.Consume(this, debug);
    }

    public uint GetIndex()
    {
        return GetUInt32();
    }

    public uint GetUInt32()
    {
        uint result = 0;
        byte shift = 0;
        while (true)
        {
            var b = GetByte();
            result |= (uint)(b & 0x7F) << shift;
            if ((b & 0x80) == 0)
            {
                break;
            }

            shift += 7;
        }

        return result;
    }

    public int GetInt32()
    {
        return (int)GetSignedLEB128(32);
    }

    public long GetInt64()
    {
        return GetSignedLEB128(64);
    }

    public ulong GetUInt64()
    {
        ulong result = 0;
        byte shift = 0;
        while (true)
        {
            var b = GetByte();
            result |= (ulong)(b & 0x7F) << shift;
            if ((b & 0x80) == 0)
            {
                break;
            }

            shift += 7;
        }

        return result;
    }

    public float GetF32()
    {
        var result = BitConverter.ToSingle(bytes, (int)index);
        index += 4;
        return result;
    }

    public double GetF64()
    {
        var result = BitConverter.ToDouble(bytes, (int)index);
        index += 8;
        return result;
    }

    public Table GetTableType()
    {
        var elemType = GetElemType();
        uint min, max;
        GetLimits(out min, out max);

        return new Table(elemType, min, max);
    }

    public Memory GetMemType()
    {
        uint min = 0, max = 0;
        GetLimits(out min, out max);

        return new Memory((int)min, (int)max);
    }

    public bool GetLimits(out uint min, out uint max)
    {
        if (GetBoolean())
        {
            min = GetUInt32();
            max = GetUInt32();
            return true;
        }

        min = GetUInt32();
        max = 0;
        return false;
    }

    public byte GetElemType()
    {
        var type = GetByte();

        switch (type)
        {
            case 0x70: //funcref
                break;
            default:
                throw new Exception("Invalid element type: 0x" + type.ToString("X"));
        }

        return type;
    }

    public bool GetBoolean()
    {
        var b = GetByte();

        switch (b)
        {
            case 0x00:
                return false;
            case 0x01:
                return true;
            default:
                throw new Exception("Invalid boolean value: 0x" + b.ToString("X"));
        }
    }

    public void GetGlobalType(out byte type, out bool mutable)
    {
        type = GetValType();
        mutable = GetBoolean();
    }

    public byte GetBlockType()
    {
        if (bytes[index] == 0x40)
        {
            index++;
            return 0x40;
        }

        return GetValType();
    }

    public byte GetValType()
    {
        var valType = GetByte();

        switch (valType)
        {
            case 0x7F:
            case 0x7E:
            case 0x7D:
            case 0x7C:
                break;
            default:
                throw new Exception("Invalid value type: 0x" + valType.ToString("X"));
        }

        return valType;
    }

    public string GetName()
    {
        var length = GetUInt32();
        var sub = new byte[length];
        Array.Copy(bytes, index, sub, 0, length);
        var result = Encoding.UTF8.GetString(sub);
        index += length;

        return result;
    }

    public uint GetVersion()
    {
        index += 4;
        return BitConverter.ToUInt32(bytes, 4);
    }

    public void Skip(uint size)
    {
        index += size;
    }

    private long GetSignedLEB128(byte size)
    {
        ulong result = 0;
        byte shift = 0;
        byte b;
        do
        {
            b = bytes[index++];
            result |= ((ulong)0x7F & b) << shift;
            shift += 7;
        } while ((b & 0x80) != 0);

        /* sign bit of byte is second high order bit (0x40) */
        if (shift < size && (b & 0x40) != 0)
            /* sign extend */
        {
            result |= ~(ulong)0 << shift;
        }

        return (long)result;
    }
}
