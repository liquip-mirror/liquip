using System.Runtime.InteropServices;
using Cosmos.Core;

namespace Cosmos.Zarlo.Memory;

public struct Pointer: IDisposable
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
    public static unsafe Pointer New(uint size, bool autoCleanUp)
    {
        return new Pointer(NativeMemory.Alloc(size), size);
    }

    /// <summary>
    /// get a pointer to of an object with the given size
    /// auto cleans up
    /// </summary>
    /// <param name="size">size in bytes</param>
    /// <returns></returns>
    public static unsafe Pointer New(uint size) => New(size, true);
    
    public unsafe Pointer(void* ptr, uint size, bool autoCleanUp = true) : this((uint*)ptr, size, autoCleanUp)
    {

    }
    
    public unsafe Pointer(uint* ptr, uint size, bool autoCleanUp = true)
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
    public unsafe void Resize(uint size)
    {
        if (Equals(this, Default)) throw new Exception("you cant resize null");
        Ptr = (uint*)NativeMemory.Realloc(Ptr, size);
        GCImplementation.IncRootCount((ushort*)Ptr);
    }

    public static unsafe explicit operator uint*(Pointer ptr) => ptr.Ptr;

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