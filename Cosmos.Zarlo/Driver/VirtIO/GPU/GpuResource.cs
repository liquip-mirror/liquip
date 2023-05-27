using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Cosmos.Zarlo.Driver.VirtIO.GPU.Struct;

namespace Cosmos.Zarlo.Driver.VirtIO.GPU;



public class Resource2d
{

    public GpuFormats Format { get; init; }

    public int Id { get; init; }

    public int Width { get; init; }
    public int Height { get; init; }

    public Pointer Buffer { get; internal set; }

}