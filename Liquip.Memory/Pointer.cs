using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cosmos.Core;

namespace Liquip.Memory;

[StructLayout(LayoutKind.Auto)]
public struct Pointer
{
    /// <summary>
    ///     defaults as <see cref="Null" />
    /// </summary>
    public static readonly Pointer Default = Null;

    /// <summary>
    ///     the Null pointer
    /// </summary>
    public static unsafe Pointer Null => new(null, 0);

    /// <summary>
    ///     get a pointer to of an object with the given size
    /// </summary>
    /// <param name="size">size in bytes</param>
    /// <param name="autoCleanUp"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe Pointer New(uint size)
    {
        return new Pointer(NativeMemory.Alloc(size), size);
    }


    /// <summary>
    ///     get a pointer to of an object with the given size
    /// </summary>
    /// <param name="ptr"></param>
    /// <param name="size">size in bytes</param>
    /// <param name="autoCleanUp"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe Pointer MakeFrom(uint* ptr, uint size)
    {
        var p = new Pointer(ptr, size);
        return p;
    }

    /// <summary>
    ///     get a pointer to of an object with the given size
    /// </summary>
    /// <param name="ptr"></param>
    /// <param name="size">size in bytes</param>
    /// <param name="autoCleanUp"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe Pointer MakeFrom(Address ptr, uint size)
    {
        var p = new Pointer((uint*)(uint)ptr, size);
        return p;
    }

    /// <summary>
    ///     get a pointer to of an object with the given size
    /// </summary>
    /// <param name="ptr"></param>
    /// <param name="size">size in bytes</param>
    /// <param name="autoCleanUp"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pointer MakeFrom(byte[] data)
    {
        var p = new Pointer(data);
        return p;
    }

    private unsafe Pointer(byte[] buffer)
    {
        fixed (byte* ptr = buffer)
        {
            Ptr = (uint*)ptr;
        }

        GCImplementation.IncRootCount((ushort*)Ptr);
        Size = (uint)buffer.Length;
    }

    private unsafe Pointer(void* ptr, uint size) : this((uint*)ptr, size)
    {
    }

    private unsafe Pointer(uint* ptr, uint size)
    {
        Ptr = ptr;
        GCImplementation.IncRootCount((ushort*)Ptr);
        Size = size;
    }

    public unsafe uint* Ptr { get; private set; }
    public uint Size { get; }

    /// <summary>
    ///     Resize the given pointer
    /// </summary>
    /// <param name="size"></param>
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe void Resize(uint size)
    {
        if (Equals(this, Default))
        {
            throw new Exception("you cant resize null");
        }

        Ptr = (uint*)NativeMemory.Realloc(Ptr, size);
        GCImplementation.IncRootCount((ushort*)Ptr);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe void CopyTo(
        byte[] buffer,
        uint destinationIndex,
        uint sourceIndex,
        uint destinationSize
    )
    {
        var temp = new byte[destinationSize];
        Marshal.Copy(new IntPtr(Ptr), temp, (int)sourceIndex, (int)destinationSize);
        Array.Copy(temp, 0, buffer, (int)destinationIndex, destinationSize);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe void CopyTo(
        uint* destination,
        uint destinationIndex,
        uint sourceIndex,
        uint destinationSize
    )
    {
        Buffer.MemoryCopy(Ptr + sourceIndex, destination + destinationIndex, destinationSize, Size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void CopyTo(
        Pointer destination,
        uint destinationIndex,
        uint sourceIndex,
        uint size
    )
    {
        BufferUtils.MemoryCopy(this, destination, destinationIndex, sourceIndex, size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Fill(byte value)
    {
        unsafe
        {
            MemoryOperations.Fill(Ptr, value, (int)Size);
        }
    }

    public uint GetAddress()
    {
        unsafe
        {
            return (uint)Ptr;
        }
    }

    public uint this[uint index]
    {
        get
        {
            unsafe
            {
                return Ptr[index];
            }
        }
        set
        {
            unsafe
            {
                Ptr[index] = value;
            }
        }
    }

    public static unsafe explicit operator byte*(Pointer ptr)
    {
        return (byte*)ptr.Ptr;
    }

    public static unsafe explicit operator uint*(Pointer ptr)
    {
        return ptr.Ptr;
    }

    public static unsafe explicit operator ushort*(Pointer ptr)
    {
        return (ushort*)ptr.Ptr;
    }

    public static unsafe explicit operator ulong*(Pointer ptr)
    {
        return (ulong*)ptr.Ptr;
    }

    public void Free()
    {
        unsafe
        {
            NativeMemory.Free(Ptr);
        }
    }


    public override bool Equals(object? obj)
    {
        unsafe
        {
            switch (obj)
            {
                case null when Ptr == Default.Ptr:
                    return true;
                case null:
                    return false;
            }

            if (obj.GetType() != typeof(Pointer))
            {
                return false;
            }

            var o = (Pointer)obj;
            return o.Ptr == Ptr && o.Size == Size;
        }
    }

    public override int GetHashCode()
    {
        unsafe
        {
            return (int)Ptr;
        }
    }

    public Span<byte> ToSpan()
    {
        unsafe
        {
            return new Span<byte>(Ptr, (int)Size);
        }
    }
}