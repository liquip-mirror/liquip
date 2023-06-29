namespace Zarlo.Cosmos.Memory;

public static unsafe class BufferUtils
{
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
