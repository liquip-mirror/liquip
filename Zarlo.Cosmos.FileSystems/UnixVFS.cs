using System.Security;
using System.Text;
using Cosmos.HAL.BlockDevice;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.FAT;
using Cosmos.System.FileSystem.ISO9660;
using Cosmos.System.FileSystem.Listing;
using Cosmos.System.FileSystem.VFS;

namespace Zarlo.Cosmos.FileSystems;

public class UnixVFS : VFSBase
{
    private class MountPoint
    {
        public string RootPath { get; set; }
        public FileSystem FileSystem { get; set; }
    }

    private List<MountPoint> MountPoints = new List<MountPoint>();

    /// <summary>
    /// List of disks.
    /// </summary>
    public List<Disk> Disks { get; } = new List<Disk>();

    (string Path, FileSystem FS)? GetFileSystemFromPath(string path)
    {
        var filtered = new List<MountPoint>();
        foreach (var item in MountPoints)
        {
            if (path.StartsWith(item.RootPath)) filtered.Add(item);
        }

        switch (filtered.Count)
        {
            case 0:
                return null;
            case 1:
                return (filtered[0].RootPath, filtered[0].FileSystem);
        }

        int longest = 0;
        MountPoint output = null;
        foreach (var item in filtered)
        {
            if (longest < item.RootPath.Length)
            {
                longest = item.RootPath.Length;
                output = item;
            }
        }

        return (output.RootPath, output.FileSystem);
    }

    public override void Initialize(bool aShowInfo)
    {
        
    }

    public override DirectoryEntry CreateFile(string aPath)
    {
        throw new NotImplementedException();
    }

    public override DirectoryEntry CreateDirectory(string aPath)
    {
        throw new NotImplementedException();
    }

    public override bool DeleteFile(DirectoryEntry aPath)
    {
        throw new NotImplementedException();
    }

    public override bool DeleteDirectory(DirectoryEntry aPath)
    {
        throw new NotImplementedException();
    }

    public override DirectoryEntry GetDirectory(string aPath)
    {
        throw new NotImplementedException();
    }

    public override DirectoryEntry GetFile(string aPath)
    {
        throw new NotImplementedException();
    }

    public override List<DirectoryEntry> GetDirectoryListing(string aPath)
    {
        throw new NotImplementedException();
    }

    public override List<DirectoryEntry> GetDirectoryListing(DirectoryEntry aEntry)
    {
        throw new NotImplementedException();
    }

    public override DirectoryEntry GetVolume(string aVolume)
    {
        throw new NotImplementedException();
    }

    public override List<DirectoryEntry> GetVolumes()
    {
        throw new NotImplementedException();
    }

    public override FileAttributes GetFileAttributes(string aPath)
    {
        throw new NotImplementedException();
    }

    public override void SetFileAttributes(string aPath, FileAttributes fileAttributes)
    {
        throw new NotImplementedException();
    }

    public override bool IsValidDriveId(string driveId)
    {
        throw new NotImplementedException();
    }

    public override long GetTotalSize(string aDriveId)
    {
        throw new NotImplementedException();
    }

    public override long GetAvailableFreeSpace(string aDriveId)
    {
        throw new NotImplementedException();
    }

    public override long GetTotalFreeSpace(string aDriveId)
    {
        throw new NotImplementedException();
    }

    public override string GetFileSystemType(string aDriveId)
    {
        throw new NotImplementedException();
    }

    public override string GetFileSystemLabel(string aDriveId)
    {
        throw new NotImplementedException();
    }

    public override void SetFileSystemLabel(string aDriveId, string aLabel)
    {
        throw new NotImplementedException();
    }

    public override string GetNextFilesystemLetter() => "";

    public override List<Disk> GetDisks() => Disks;
}