using Cosmos.Zarlo.Memory;

namespace Cosmos.Zarlo.Driver.VirtIO;


public struct VirtQDescriptor 
{

    public const byte Phys = 0;
    public const byte Len = 8;
    public const byte Flags = 12;
    public const byte NextDescIdx = 14;

    public static VirtQDescriptor FromStruct<T>(T data, DescFlags flag) where T: struct
    {
        return new VirtQDescriptor(Pointer.MakeFrom(data, false), flag);
    }

    public unsafe VirtQDescriptor(Pointer ptr, DescFlags flag) {
        addr = (UInt64)ptr.Ptr;
        len = ptr.Size;
        flags = flag;
    }

    /* Address (guest-physical). */
    public UInt64 addr;
    /* Length. */
    public UInt32 len;

    /* The flags as indicated above. */
    public DescFlags flags;


    public Pointer GetPointer()
    {
        unsafe
        {
            return Pointer.MakeFrom((uint*)addr, len, false);
        }
    }

}
