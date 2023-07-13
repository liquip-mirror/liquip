using System.Reflection;
using Liquip.Driver.VirtIO.GPU.Struct;

namespace Liquip.Driver.VirtIO.GPU;

public class Resource2d
{
    public GpuFormats Format { get; init; }

    public int Id { get; init; }

    public int Width { get; init; }
    public int Height { get; init; }

    public Pointer Buffer { get; internal set; }
}
