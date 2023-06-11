using System;
using System.IO;
using Cosmos.Core.Memory;
using Cosmos.HAL.BlockDevice;

namespace Zarlo.Cosmos.FileSystems;

public class BlockDeviceStream : Stream
{
    private readonly BlockDevice _blockDevice;

    private long BlockSize => (long)_blockDevice.BlockSize;

    public override bool CanRead => true;
    public override bool CanSeek => true;
    public override bool CanWrite => true;
    public override long Length { get; }
    long position = 0;

    public override long Position
    {
        get => position;
        set
        {
            if (value < 0) throw new IndexOutOfRangeException("");
            if (value > Length) throw new IndexOutOfRangeException("longer then th block device size");
            position = value;
        }
    }

    public BlockDeviceStream(BlockDevice blockDevice)
    {
        this._blockDevice = blockDevice;

        Length = (long)(blockDevice.BlockCount * blockDevice.BlockSize);
    }

    public BlockDeviceStream(BlockDevice blockDevice, long length)
    {
        this._blockDevice = blockDevice;

        Length = length;
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        var startBlock = GetOffsetBlock(offset);
        var blockCount = GetOffsetBlock(count) + 1;
        byte[] temp = _blockDevice.NewBlockArray((uint)blockCount);
        _blockDevice.ReadBlock((ulong)startBlock, (ulong)blockCount, ref temp);
        var start = (uint)(_blockDevice.BlockSize * (ulong)startBlock) - offset;
        Array.Copy(temp, start, buffer, 0, count);
        return count;
    }

    public int ReadBlock(byte[] buffer, int offset, int count)
    {
        _blockDevice.ReadBlock((ulong)offset, (ulong)count, ref buffer);
        return buffer.Length;
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

    private long GetOffsetBlock(long offset)
    {
        var index = (double)offset / BlockSize;
        return (long)Math.Floor(index);
    }

    public void WriteBlock(byte[] buffer, int offset, int count)
    {
        _blockDevice.WriteBlock((ulong)offset, (ulong)count, ref buffer);
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        var startBlock = GetOffsetBlock(offset);
        var blockCount = GetOffsetBlock(count) + 1;
        byte[] newBuffer = _blockDevice.NewBlockArray((uint)blockCount);

        if (blockCount == 1)
        {
            ReadBlock(newBuffer, (int)startBlock, 1);
        }
        else
        {
            byte[] temp = _blockDevice.NewBlockArray(1);
            ReadBlock(temp, (int)startBlock, 1);
            Array.Copy(temp, newBuffer, temp.Length);
            ReadBlock(temp, (int)startBlock, 1);
            Array.Copy(temp, 0, newBuffer, (blockCount - 1) * (int)_blockDevice.BlockSize, temp.Length);
        }

        var start = (uint)(_blockDevice.BlockSize * (ulong)startBlock) - offset;
        Array.Copy(buffer, start, newBuffer, 0, count);
        WriteBlock(newBuffer, (int)startBlock, (int)blockCount);
    }

    public override void Flush()
    {
    }

    public override void SetLength(long value)
    {
        throw new Exception($@"Unable to SetLength on {nameof(BlockDeviceStream)}");
    }
}