using Cosmos.HAL.BlockDevice;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.Listing;

namespace Cosmos.Zarlo.FileSystems.Cosmos.BlockDevice;

public class BlockDeviceFileSystem : FileSystem
{
    private readonly Disk _disk;
    private readonly long _availableFreeSpace;

    public BlockDeviceFileSystem(Disk disk, string aRootPath) : base(null, aRootPath, 0)
    {
        _disk = disk;
        
        long total = 0;
        foreach(var item in _disk.Partitions)
        {
            total += item.MountedFS.Size;
        }
        _availableFreeSpace = total;
    }

    public override long AvailableFreeSpace => _availableFreeSpace;

    public override long TotalFreeSpace => throw new NotImplementedException();

    public override string Type => "BlockFS";

    public override string Label { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public override DirectoryEntry CreateDirectory(DirectoryEntry aParentDirectory, string aNewDirectory)
    {
        throw new NotSupportedException();
    }

    public override DirectoryEntry CreateFile(DirectoryEntry aParentDirectory, string aNewFile)
    {
        throw new NotSupportedException();
    }

    public override void DeleteDirectory(DirectoryEntry aPath)
    {
        throw new NotSupportedException();
    }

    public override void DeleteFile(DirectoryEntry aPath)
    {
        throw new NotSupportedException();
    }

    public override void DisplayFileSystemInfo()
    {
        throw new NotImplementedException();
    }

    public override void Format(string aDriveFormat, bool aQuick)
    {
        throw new NotSupportedException();
    }

    public override List<DirectoryEntry> GetDirectoryListing(DirectoryEntry baseDirectory)
    {
        throw new NotImplementedException();
    }

    public override DirectoryEntry GetRootDirectory()
    {
        throw new NotImplementedException();
    }
}
