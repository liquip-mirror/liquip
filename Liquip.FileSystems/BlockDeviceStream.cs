using System;
using System.IO;
using Cosmos.HAL.BlockDevice;

namespace Liquip.FileSystems;

public partial  class BlockDeviceStream : Stream
{
    private long position;


    public long BlockSize { get; init; }

    public override bool CanRead => true;
    public override bool CanSeek => true;
    public override bool CanWrite => true;
    public override long Length { get; }

    public override long Position
    {
        get => position;
        set
        {
            if (value < 0)
            {
                throw new IndexOutOfRangeException("");
            }

            if (value > Length)
            {
                throw new IndexOutOfRangeException("longer then th block device size");
            }

            position = value;
        }
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
                Position = Length - 1 - offset;
                break;
            default:
                throw new ArgumentException(null, nameof(origin));
        }

        return Position;
    }


}
