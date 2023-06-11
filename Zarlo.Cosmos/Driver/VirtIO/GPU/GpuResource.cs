using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Zarlo.Cosmos.Driver.VirtIO.GPU.Struct;

namespace Zarlo.Cosmos.Driver.VirtIO.GPU;



public class Resource2d
{

    public GpuFormats Format { get; init; }

    public int Id { get; init; }

    public int Width { get; init; }
    public int Height { get; init; }

    public Pointer Buffer { get; internal set; }

}