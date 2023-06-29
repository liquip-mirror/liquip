using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.Listing;

namespace Zarlo.Cosmos.FileSystems.Cosmos;

public class DiskDeviceFileSystem : FileSystem
{
    private readonly Disk _disk;


    public DiskDeviceFileSystem(Disk disk, string aRootPath) : base(null, aRootPath, 0)
    {
        _disk = disk;
    }

    public override long AvailableFreeSpace => 0;

    public override long TotalFreeSpace => 0;

    public override string Type => "BlockFS";

    public override string Label
    {
        get => _disk.GetHashCode().ToString();
        set { }
    }

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
