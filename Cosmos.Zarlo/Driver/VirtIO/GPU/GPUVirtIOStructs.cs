// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable FieldCanBeMadeReadOnly.Global

using System.Runtime.InteropServices;

namespace Cosmos.Zarlo.Driver.VirtIO.GPU;

[StructLayout(LayoutKind.Auto)]
public struct GPUConfig
{
    public uint EventsRead;
    public uint EventsClear;
    public uint NumScanOuts;
    public uint Reserved;
}

[StructLayout(LayoutKind.Auto)]
public struct GPUCtrlHdr
{
    public uint Type;
    public uint Flags;
    public uint FenceId;
    public uint CtxId;
    public uint Padding;
}

[StructLayout(LayoutKind.Auto)]
public struct GPURect
{
    public uint X;
    public uint Y;
    public uint Width;
    public uint Height;
}

[StructLayout(LayoutKind.Auto)]
public struct DisplayOne
{
    public GPURect R;
    public uint Enabled;
    public uint Flags;
}

[StructLayout(LayoutKind.Auto)]
public struct RespDisplayInfo
{
    public GPUCtrlHdr HDR;
    public DisplayOne[] PModes;
}