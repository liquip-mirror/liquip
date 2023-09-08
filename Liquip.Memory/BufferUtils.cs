using System;
using System.Collections.Generic;

namespace Liquip.Memory;

public struct Diff
{
    public uint Offset { get; init; }
    public Pointer Pointer { get; init; }

    public Diff(uint offset, Pointer pointer)
    {
        Offset = offset;
        this.Pointer = pointer;
    }

}

public static unsafe class BufferUtils
{

    public static List<Diff> GetDiffs(Pointer a, Pointer b)
    {
        var minSize = Math.Min(a.Size, b.Size);

        uint offset = 0;

        bool found = false;

        List<Diff> diffs = new List<Diff>();

        for (uint i = 0; i < minSize; i++)
        {

            if (a[i] == b[i])
            {

                if (!found)
                {
                    offset = i;
                    found = true;
                }

            }
            else
            {
                if (found)
                {
                    found = false;
                    diffs.Add(new Diff(offset, Pointer.MakeFrom(new Address(b.GetAddress() + offset), offset - i)));
                }
            }

        }

        return diffs;
    }

    public static void MemoryCopy(Pointer source, Pointer destination)
    {
        Buffer.MemoryCopy(source.Ptr, destination.Ptr, destination.Size, source.Size);
    }

    public static void MemoryCopy(
        Pointer source,
        Pointer destination,
        uint destinationIndex
    )
    {
        MemoryCopy(source, destination, destinationIndex, source.Size);
    }

    public static void MemoryCopy(
        Pointer source,
        Pointer destination,
        uint destinationIndex,
        uint size
    )
    {
        Buffer.MemoryCopy(
            source.Ptr,
            destination.Ptr + destinationIndex,
            source.Size,
            size
        );
    }

    public static void MemoryCopy(
        Pointer source,
        Pointer destination,
        uint destinationIndex,
        uint sourceIndex,
        uint size
    )
    {
        Buffer.MemoryCopy(
            source.Ptr + sourceIndex,
            destination.Ptr + destinationIndex,
            source.Size,
            size
        );
    }
}
