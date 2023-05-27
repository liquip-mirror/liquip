namespace Cosmos.Zarlo.Driver.VirtIO.GPU.Struct;


public struct GpuResourceCreate2d
{
    public GPUCtrlHdr Header;
    public int ResourceId;
    public GpuFormats Format;
    public int Width;
    public int Height;
}

public struct GpuResourceUnref
{ 
    public GPUCtrlHdr Header;
    public int ResourceId;
    public int Padding;
}

public struct ResourceAttachBacking
{
    public GPUCtrlHdr Header;

    int ResourceId;
    int nr_entries;

}