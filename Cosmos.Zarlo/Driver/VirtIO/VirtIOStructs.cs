// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable FieldCanBeMadeReadOnly.Global

using System.Runtime.InteropServices;
using Cosmos.Zarlo.Memory;

namespace Cosmos.Zarlo.Driver.VirtIO;

[StructLayout(LayoutKind.Auto)]
public struct QueueBuffer
{
    public ulong Address;
    public uint Length;
    public ushort Flags;
    public ushort Next;
}

[StructLayout(LayoutKind.Auto)]
public struct VirtioAvailable
{
    public ushort Flags;
    public ushort Index;
    public ushort[] Rings;
}

[StructLayout(LayoutKind.Auto)]
public struct VirtioUsedItem
{
    public ushort Index;
    public ushort Length;
}

[StructLayout(LayoutKind.Auto)]
public struct VirtioUsed
{
    public ushort Flags;
    public ushort Index;
    public VirtioUsedItem[] Rings;
}

[StructLayout(LayoutKind.Explicit)]
public unsafe struct VirtQueue
{
    [FieldOffset(0)] public ushort QueueSize;
    [FieldOffset(1)] public QueueBuffer* QueueBuffer;
    [FieldOffset(1)] public ulong BaseAddress;
    [FieldOffset(2)] public VirtioAvailable* Available;
    [FieldOffset(3)] public VirtioUsed* Used;
    [FieldOffset(4)] public ushort LastUsedIndex;
    [FieldOffset(5)] public ushort LastAvailableIndex;
    [FieldOffset(6)] public byte* Buffer;
    [FieldOffset(7)] public uint ChunkSize;
    [FieldOffset(8)] public ushort NextBuff;
    [FieldOffset(8)] public ulong Lock;
}

[StructLayout(LayoutKind.Auto)]
public struct VirtioDeviceInfo
{
    public uint DeviceAddress;
    public ushort IoBase;
    public ulong MemoryAddress;
    public ushort irq;
    public ulong MacAddress;
    public VirtQueue[] Queues;
};

[StructLayout(LayoutKind.Auto)]
public struct AvailableBuffer
{
    public uint Index;
    public ulong Address;
    public uint Length;
}

[StructLayout(LayoutKind.Auto)]
public unsafe struct BufferInfo
{
    public Pointer Buffer;
    public ulong Size;
    public byte Flags;
    public bool Copy;
}
