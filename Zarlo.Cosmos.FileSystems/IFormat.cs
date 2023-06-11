using Cosmos.HAL.BlockDevice;
using Cosmos.System.FileSystem;

namespace Zarlo.Cosmos.FileSystems;

public interface IFormat
{

    public void Format(ManagedPartition partition);
    public void Format(Partition partition);

}
