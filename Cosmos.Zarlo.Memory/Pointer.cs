using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cosmos.Core;
using Cosmos.Core.Memory;

namespace Cosmos.Zarlo.Memory;

public struct Pointer : IDisposable
{
    /// <summary>
    /// defaults as <see cref="Null"/>
    /// </summary>
    public static readonly Pointer Default = Null;

    /// <summary>
    /// the Null pointer
    /// </summary>
    public static unsafe Pointer Null => new Pointer(null, 0);

    private readonly bool _autoCleanUp;

    /// <summary>
    /// get a pointer to of an object with the given size
    /// </summary>
    /// <param name="size">size in bytes</param>
    /// <param name="autoCleanUp"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe Pointer New(uint size, bool autoCleanUp)
    {
        return new Pointer(NativeMemory.Alloc(size), size);
    }

    /// <summary>
    /// get a pointer to of an object with the given size
    /// </summary>
    /// <param name="ptr"></param>
    /// <param name="size">size in bytes</param>
    /// <param name="autoCleanUp"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe Pointer MakeFrom<T>(T data, bool autoCleanUp) where T : struct
    {
        var p = new Pointer(GCImplementation.GetPointer(data), (uint)sizeof(T), autoCleanUp);
        return p;
    }
    
    /// <summary>
    /// get a pointer to of an object with the given size
    /// </summary>
    /// <param name="ptr"></param>
    /// <param name="size">size in bytes</param>
    /// <param name="autoCleanUp"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe Pointer MakeFrom(uint* ptr, uint size, bool autoCleanUp = true) 
    {
        var p = new Pointer(ptr, size, autoCleanUp);
        return p;
    }

    /// <summary>
    /// get a pointer to of an object with the given size
    /// </summary>
    /// <param name="ptr"></param>
    /// <param name="size">size in bytes</param>
    /// <param name="autoCleanUp"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe Pointer MakeFrom(uint ptr, uint size, bool autoCleanUp = true) 
    {
        var p = new Pointer((uint*)ptr, size, autoCleanUp);
        return p;
    }

    /// <summary>
    /// get a pointer to of an object with the given size
    /// </summary>
    /// <param name="ptr"></param>
    /// <param name="size">size in bytes</param>
    /// <param name="autoCleanUp"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe Pointer MakeFrom(byte[] data, bool autoCleanUp = true) 
    {
        var p = new Pointer(data, autoCleanUp);
        return p;
    }

    /// <summary>
    /// get a pointer to of an object with the given size
    /// auto cleans up
    /// </summary>
    /// <param name="size">size in bytes</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe Pointer New(uint size) => New(size, true);

    private unsafe Pointer(byte[] buffer, bool autoCleanUp = true)
    {
        fixed (byte* ptr = buffer)
            Ptr = (uint*)ptr;
        GCImplementation.IncRootCount((ushort*)Ptr);
        _autoCleanUp = autoCleanUp;
        Size = (uint)buffer.Length;
        
    }
    
    private unsafe Pointer(void* ptr, uint size, bool autoCleanUp = true) : this((uint*)ptr, size, autoCleanUp)
    {
    }

    private unsafe Pointer(uint* ptr, uint size, bool autoCleanUp = true)
    {
        _autoCleanUp = autoCleanUp;
        Ptr = ptr;
        GCImplementation.IncRootCount((ushort*)Ptr);
        Size = size;
    }

    public unsafe uint* Ptr { get; private set; }
    public uint Size { get; private set; }

    /// <summary>
    /// Resize the given pointer
    /// </summary>
    /// <param name="size"></param>
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe void Resize(uint size)
    {
        if (Equals(this, Default)) throw new Exception("you cant resize null");
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
    => Buffer.MemoryCopy((Ptr + sourceIndex), (destination + destinationIndex), destinationSize, Size);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void CopyTo(
        Pointer destination,
        uint destinationIndex,
        uint sourceIndex,
        uint size
    )
    => BufferUtils.MemoryCopy( this, destination, destinationIndex, sourceIndex, size);

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

    public static unsafe explicit operator byte*(Pointer ptr) => (byte*)ptr.Ptr;
    public static unsafe explicit operator uint*(Pointer ptr) => ptr.Ptr;
    public static unsafe explicit operator ushort*(Pointer ptr) => (ushort*)ptr.Ptr;
    public static unsafe explicit operator ulong*(Pointer ptr) => (ulong*)ptr.Ptr;

    public void Free()
    {
        unsafe
        {
            NativeMemory.Free(Ptr);
        }
    }

    public void Dispose()
    {
        if (_autoCleanUp)
        {
            Free();
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

            if (obj.GetType() != typeof(Pointer)) return false;
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
}