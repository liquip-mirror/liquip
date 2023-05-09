// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable FieldCanBeMadeReadOnly.Global

using System.Runtime.InteropServices;

namespace Cosmos.Zarlo.Driver.VirtIO.GPU;



[StructLayout(LayoutKind.Auto)]
public struct GPUCtrlHdr
{
    public GpuCmd Type;
    public uint Flags;
    public ulong FenceId;
    public uint CtxId;
    public byte RingIdx;
    public byte Padding0;
    public byte Padding1;
    public byte Padding2;

}
