using System;

namespace Liquip.WASM;

public class Memory
{
    private const int PAGE_CAP = 1024;
    private const int PAGE_SIZE = 65536;
    public byte[] Buffer;
    public int MinPages, MaxPages, CurrentPages;

    public Memory(int minPages, int maxPages)
    {
        if (minPages > PAGE_CAP)
        {
            throw new Exception("Out of memory!");
        }

        MinPages = minPages;
        MaxPages = maxPages;
        CurrentPages = MinPages;

        Buffer = new byte[CurrentPages * PAGE_SIZE];
        Array.Clear(Buffer, 0, Buffer.Length);
    }

    public bool CompatibleWith(Memory m)
    {
        return MinPages == m.MinPages && MaxPages == m.MaxPages;
    }

    public override string ToString()
    {
        return "<memory min: " + MinPages + ", max: " + MaxPages + ", cur: " + CurrentPages + ">";
    }

    public void Set(ulong offset, byte b)
    {
        if (offset < (ulong)Buffer.Length)
        {
            Buffer[offset] = b;
        }
        else
        {
            throw new TrapException("out of bounds memory access");
        }
    }

    public byte[] GetBytes(ulong offset, ulong bytes)
    {
        if (offset + bytes <= (ulong)Buffer.Length)
        {
            var buffer = new byte[bytes];
            Array.Copy(Buffer, (int)offset, buffer, 0, (int)bytes);

            return buffer;
        }

        throw new TrapException("out of bounds memory access");
    }

    public void SetBytes(ulong offset, byte[] bytes)
    {
        if (offset + (ulong)bytes.Length <= (ulong)Buffer.Length)
        {
            Array.Copy(bytes, 0, Buffer, (int)offset, bytes.Length);
        }
        else
        {
            throw new TrapException("out of bounds memory access");
        }
    }

    public float GetF32(ulong offset)
    {
        return BitConverter.ToSingle(GetBytes(offset, 4), 0);
    }

    public double GetF64(ulong offset)
    {
        return BitConverter.ToDouble(GetBytes(offset, 8), 0);
    }

    public void SetI16(ulong offset, ushort value)
    {
        if (offset + 1 < (ulong)Buffer.Length)
        {
            Buffer[offset + 1] = (byte)((value & 0xFF00) >> 8);
            Buffer[offset + 0] = (byte)(value & 0xFF);
        }
        else
        {
            throw new TrapException("out of bounds memory access");
        }
    }

    public void SetI32(ulong offset, uint value)
    {
        if (offset + 3 < (ulong)Buffer.Length)
        {
            Buffer[offset + 3] = (byte)((value & 0xFF000000) >> 24);
            Buffer[offset + 2] = (byte)((value & 0xFF0000) >> 16);
            Buffer[offset + 1] = (byte)((value & 0xFF00) >> 8);
            Buffer[offset + 0] = (byte)(value & 0xFF);
        }
        else
        {
            throw new TrapException("out of bounds memory access");
        }
    }

    public void SetI64(ulong offset, ulong value)
    {
        if (offset + 7 < (ulong)Buffer.Length)
        {
            Buffer[offset + 7] = (byte)((value & 0xFF00000000000000) >> 56);
            Buffer[offset + 6] = (byte)((value & 0xFF000000000000) >> 48);
            Buffer[offset + 5] = (byte)((value & 0xFF0000000000) >> 40);
            Buffer[offset + 4] = (byte)((value & 0xFF00000000) >> 32);
            Buffer[offset + 3] = (byte)((value & 0xFF000000) >> 24);
            Buffer[offset + 2] = (byte)((value & 0xFF0000) >> 16);
            Buffer[offset + 1] = (byte)((value & 0xFF00) >> 8);
            Buffer[offset + 0] = (byte)(value & 0xFF);
        }
        else
        {
            throw new TrapException("out of bounds memory access");
        }
    }

    public uint GetI32(ulong offset)
    {
        if (offset + 3 < (ulong)Buffer.Length)
        {
            return Buffer[offset] |
                   ((uint)Buffer[offset + 1] << 8) |
                   ((uint)Buffer[offset + 2] << 16) |
                   ((uint)Buffer[offset + 3] << 24);
        }

        throw new TrapException("out of bounds memory access");
    }

    public ulong GetI64(ulong offset)
    {
        if (offset + 7 < (ulong)Buffer.Length)
        {
            return Buffer[offset] |
                   ((ulong)Buffer[offset + 1] << 8) |
                   ((ulong)Buffer[offset + 2] << 16) |
                   ((ulong)Buffer[offset + 3] << 24) |
                   ((ulong)Buffer[offset + 4] << 32) |
                   ((ulong)Buffer[offset + 5] << 40) |
                   ((ulong)Buffer[offset + 6] << 48) |
                   ((ulong)Buffer[offset + 7] << 56);
        }

        throw new TrapException("out of bounds memory access");
    }

    public uint GetI3216s(ulong offset)
    {
        if (offset + 1 < (ulong)Buffer.Length)
        {
            return (uint)(short)(Buffer[offset] |
                                 (Buffer[offset + 1] << 8));
        }

        throw new TrapException("out of bounds memory access");
    }

    public uint GetI3216u(ulong offset)
    {
        if (offset + 1 < (ulong)Buffer.Length)
        {
            return (uint)(Buffer[offset] |
                          (Buffer[offset + 1] << 8));
        }

        throw new TrapException("out of bounds memory access");
    }

    public uint GetI328s(ulong offset)
    {
        if (offset < (ulong)Buffer.Length)
        {
            return (uint)(sbyte)Buffer[offset];
        }

        throw new TrapException("out of bounds memory access");
    }

    public uint GetI328u(ulong offset)
    {
        if (offset < (ulong)Buffer.Length)
        {
            return Buffer[offset];
        }

        throw new TrapException("out of bounds memory access");
    }

    public ulong GetI6416s(ulong offset)
    {
        if (offset + 1 < (ulong)Buffer.Length)
        {
            return (ulong)(short)(Buffer[offset] |
                                  (Buffer[offset + 1] << 8));
        }

        throw new TrapException("out of bounds memory access");
    }

    public ulong GetI6416u(ulong offset)
    {
        if (offset + 1 < (ulong)Buffer.Length)
        {
            return (ulong)(Buffer[offset] |
                           (Buffer[offset + 1] << 8));
        }

        throw new TrapException("out of bounds memory access");
    }

    public ulong GetI6432s(ulong offset)
    {
        if (offset + 3 < (ulong)Buffer.Length)
        {
            return (ulong)(int)(Buffer[offset] |
                                ((uint)Buffer[offset + 1] << 8) |
                                ((uint)Buffer[offset + 2] << 16) |
                                ((uint)Buffer[offset + 3] << 24));
        }

        throw new TrapException("out of bounds memory access");
    }

    public ulong GetI6432u(ulong offset)
    {
        if (offset + 3 < (ulong)Buffer.Length)
        {
            return Buffer[offset] |
                   ((uint)Buffer[offset + 1] << 8) |
                   ((uint)Buffer[offset + 2] << 16) |
                   ((uint)Buffer[offset + 3] << 24);
        }

        throw new TrapException("out of bounds memory access");
    }

    public ulong GetI648s(ulong offset)
    {
        if (offset < (ulong)Buffer.Length)
        {
            return (ulong)(sbyte)Buffer[offset];
        }

        throw new TrapException("out of bounds memory access");
    }

    public ulong GetI648u(ulong offset)
    {
        if (offset < (ulong)Buffer.Length)
        {
            return Buffer[offset];
        }

        throw new TrapException("out of bounds memory access");
    }

    public uint Grow(uint size)
    {
        if ((MaxPages != 0 && CurrentPages + size > MaxPages) || CurrentPages + size > PAGE_CAP)
        {
            return 0xFFFFFFFF;
        }

        if (CurrentPages + size < CurrentPages)
        {
            return 0xFFFFFFFF;
        }

        if (CurrentPages + size == CurrentPages)
        {
            return (uint)CurrentPages;
        }

        Array.Resize(ref Buffer, (int)(size + CurrentPages) * 65536);
        Array.Clear(Buffer, CurrentPages * 65536, 65536 * (int)size);

        CurrentPages += (int)size;
        return (uint)(CurrentPages - size);
    }
}
