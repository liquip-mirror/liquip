using System.Runtime.InteropServices;

namespace Zarlo.Cosmos.Driver.VirtIO.GPU.Struct;


[StructLayout(LayoutKind.Auto)]
public struct GpuCursorPos { 
    public uint ScanoutId;
    public uint X;
    public uint Y;
    public int padding;
}

[StructLayout(LayoutKind.Auto)]
public struct GpuUpdateCursor
{
    public GPUCtrlHdr Header;
    public GpuCursorPos Pos;
    public int ResourceId;
    public int hot_x;
    public int hot_y;
    public int padding;
}