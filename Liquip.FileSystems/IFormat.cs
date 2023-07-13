using Cosmos.HAL.BlockDevice;
using Cosmos.System.FileSystem;

namespace Liquip.FileSystems;

public interface IFormat
{
    public void Format(ManagedPartition partition);
    public void Format(Partition partition);
}
