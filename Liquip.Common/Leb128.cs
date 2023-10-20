using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Liquip.Memory;

namespace Liquip.Common;

/// <summary>
///
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public readonly struct Leb128
{
    /// <summary>
    /// denotes if its signed
    /// </summary>
    public readonly bool IsSigned;

    /// <summary>
    /// the raw data
    /// </summary>
    public readonly byte[] Raw;

    public Leb128(Span<byte> data, bool isSigned = false)
    {
        IsSigned = isSigned;
        var index = 0;
        byte b;
        do
        {
            b = data[index++];
        } while ((b & 0x80) != 0);
        Raw = new byte[index];
        for (int i = 0; i < index; i++)
        {
            Raw[i] = data[i];
        }
    }

    public Leb128(ref Pointer ptr, int offset = 0, bool isSigned = false): this(ref ptr, out _, offset, isSigned)
    {

    }
    public Leb128(ref Pointer ptr, out int size, int offset = 0, bool isSigned = false)
    {
        IsSigned = isSigned;
        var index = offset;
        byte b;
        do
        {
            unsafe
            {
                b = (byte)(byte*)ptr.Ptr[index++];
            }
        } while ((b & 0x80) != 0);
        Raw = new byte[index];
        for (var i = 0; i < index; i++)
        {
            unsafe
            {
                Raw[i] = (byte)(byte*)ptr.Ptr[i];
            }
        }

        size = Raw.Length;
    }


    /// <summary>
    /// gets a ulong
    /// </summary>
    /// <returns></returns>
    public ulong GetLong()
    {
        ulong result = 0;
        byte shift = 0;
        byte b;
        var index = 0;
        do
        {
            b = Raw[index++];
            result |= ((ulong)0x7F & b) << shift;
            shift += 7;

        } while ((b & 0x80) != 0);

        return result;
    }

    /// <summary>
    /// get a long
    /// </summary>
    /// <returns></returns>
    public long GetSignedLong()
    {
        ulong result = 0;
        byte shift = 0;
        byte b;
        var index = 0;
        do
        {
            b = Raw[index++];
            result |= ((ulong)0x7F & b) << shift;
            shift += 7;

        } while ((b & 0x80) != 0);

        /* sign bit of byte is second high order bit (0x40) */
        if ((b & 0x40) != 0)
        {
            result |= ~(ulong)0 << shift;
        }

        return (long)result;
    }

    /// <summary>
    /// make a Leb128 from a ulong
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Leb128 From(ulong value) {
        var data = new List<byte>();
        var more = true;

        while (more) {
            var chunk = (byte)(value & 0x7fUL); // extract a 7-bit chunk
            value >>= 7;

            more = value != 0;
            if (more) { chunk |= 0x80; } // set msb marker that more bytes are coming

            data.Add(chunk);
        };

        return new Leb128(data.ToArray(), false);
    }

    /// <summary>
    /// make a Leb128 from a long
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Leb128 From(long value)
    {
        var data = new List<byte>();
        var more = true;

        while (more) {
            var chunk = (byte)(value & 0x7fL); // extract a 7-bit chunk
            value >>= 7;

            var signBitSet = (chunk & 0x40) != 0; // sign bit is the msb of a 7-bit byte, so 0x40
            more = !((value == 0 && !signBitSet) || (value == -1 && signBitSet));
            if (more) { chunk |= 0x80; } // set msb marker that more bytes are coming

            data.Add(chunk);
        };

        return new Leb128(data.ToArray(), true);
    }


}
