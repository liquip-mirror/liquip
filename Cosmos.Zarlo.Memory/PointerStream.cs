using System.Runtime.InteropServices;

namespace Cosmos.Zarlo.Memory;

public class PointerStream: Stream
{
    public Pointer Pointer { get; init; }
    public unsafe uint* Ptr => Pointer.Ptr;

    public override bool CanRead { get; } = true;
    public override bool CanSeek { get; } = true;
    public override bool CanWrite { get; } = true;
    public override long Length => Pointer.Size;
    public override long Position { get; set; }

    public PointerStream(Pointer ptr)
    {
        Pointer = ptr;
    }

    public override void Flush()
    {
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        switch (origin)
        {
            case SeekOrigin.Begin:
                Position = offset;
                break;
            case SeekOrigin.Current:
                Position += offset;
                break;
            case SeekOrigin.End:
                Position = Length - offset;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(origin), origin, null);
        }

        if (Position < 0 || Position > Length) throw new IndexOutOfRangeException();

        return Position;
    }

    public override void SetLength(long value)
    {
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        Pointer.CopyTo(buffer, (uint)offset, (uint)Position, (uint)count);
        return count;
    }
    
    public override void Write(byte[] buffer, int offset, int count)
    {
        BufferUtils.MemoryCopy(new Pointer(buffer), Pointer, (uint)Position, (uint)(count - offset));
    }
    
}