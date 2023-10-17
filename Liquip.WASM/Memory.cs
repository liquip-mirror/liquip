using System;
using Liquip.Memory;

namespace Liquip.WASM;

public class WasmMemory
{
    private const int PAGE_CAP = 1024;
    private const int PAGE_SIZE = 65536;
    public Pointer Ptr;
    public int MinPages, MaxPages, CurrentPages;

    public WasmMemory(int minPages, int maxPages)
    {
        if (minPages > PAGE_CAP)
        {
            throw new Exception("Out of memory!");
        }

        MinPages = minPages;
        MaxPages = maxPages;
        CurrentPages = MinPages;

        Ptr = Pointer.New(PAGE_SIZE * CurrentPages);
    }

    public bool CompatibleWith(WasmMemory m)
    {
        return MinPages == m.MinPages && MaxPages == m.MaxPages;
    }

    public override string ToString()
    {
        return "<memory min: " + MinPages + ", max: " + MaxPages + ", cur: " + CurrentPages + ">";
    }

    public void Set(ulong offset, byte b)
    {
        if (offset < Ptr.Size)
        {
            Ptr[(uint)offset] = b;
        }
        else
        {
            throw new TrapException("out of bounds memory access");
        }
    }

    public byte[] GetBytes(ulong offset, ulong bytes)
    {
        if (offset + bytes <= Ptr.Size)
        {
            var buffer = new byte[bytes];
            Ptr.CopyTo(buffer, 0, 0, (uint)bytes);
            return buffer;
        }

        throw new TrapException("out of bounds memory access");
    }

    public void SetBytes(ulong offset, byte[] bytes)
    {
        if (offset + (ulong)bytes.Length <= Ptr.Size)
        {
            Pointer.MakeFrom(bytes).CopyTo(Ptr, (uint)offset, (uint)0, (uint)bytes.Length);
        }
        else
        {
            throw new TrapException("out of bounds memory access");
        }
    }

    public uint Grow(uint pages)
    {
        if ((MaxPages != 0 && CurrentPages + pages > MaxPages) || CurrentPages + pages > PAGE_CAP)
        {
            return 0xFFFFFFFF;
        }

        if (CurrentPages + pages < CurrentPages)
        {
            return 0xFFFFFFFF;
        }

        if (CurrentPages + pages == CurrentPages)
        {
            return (uint)CurrentPages;
        }

        Ptr.Resize((CurrentPages + pages) * 65536);

        CurrentPages += (int)pages;
        return (uint)(CurrentPages);
    }
}
