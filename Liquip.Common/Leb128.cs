using System;
using System.Runtime.InteropServices;
using Liquip.Memory;

namespace Liquip.Common;

/// <summary>
///
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct Leb128
{
    public bool IsSigned;
    public byte[] Raw;

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

    public Leb128(ref Pointer ptr, bool isSigned = false)
    {
        IsSigned = isSigned;
        var index = 0;
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

}
