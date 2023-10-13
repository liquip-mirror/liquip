using System;
using System.IO;
using Cosmos.HAL.BlockDevice;

namespace Liquip.FileSystems;

public partial class BlockDeviceStream
{
    private readonly BlockDevice _blockDevice;

    public BlockDeviceStream(BlockDevice blockDevice)
    {
        _blockDevice = blockDevice;

        Length = (long)(blockDevice.BlockCount * blockDevice.BlockSize);
        BlockSize = (long)_blockDevice.BlockSize;
    }

    public BlockDeviceStream(BlockDevice blockDevice, long length)
    {
        _blockDevice = blockDevice;

        Length = length;
        BlockSize = (long)_blockDevice.BlockSize;
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        var startBlock = GetOffsetBlock(offset);
        var blockCount = GetOffsetBlock(count) + 1;
        var temp = _blockDevice.NewBlockArray((uint)blockCount);
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
        var newBuffer = _blockDevice.NewBlockArray((uint)blockCount);

        if (blockCount == 1)
        {
            ReadBlock(newBuffer, (int)startBlock, 1);
        }
        else
        {
            var temp = _blockDevice.NewBlockArray(1);
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
