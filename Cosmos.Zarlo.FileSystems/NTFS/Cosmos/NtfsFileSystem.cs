using System;
using System.Collections.Generic;
using Cosmos.HAL.BlockDevice;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.Listing;
using Cosmos.Zarlo.FileSystems.NTFS.IO;
using Cosmos.Zarlo.Logger;
using Cosmos.Zarlo.Logger.Interfaces;

namespace Cosmos.Zarlo.FileSystems.NTFS.Cosmos
{
    public class NtfsFileSystem : FileSystem
    {
        private readonly Partition _device;
        private readonly string _rootPath;
        private readonly long _size;

        private Ntfs _ntfs;

        private readonly ILogger _Logger;

        public NtfsFileSystem(Partition aDevice, string aRootPath, long aSize) : base(aDevice, aRootPath, aSize)
        {
            _Logger = Log.GetLogger("NtfsFileSystem");
            _device = aDevice;
            _rootPath = aRootPath;
            _size = aSize;
            Initialize();
        }

        private void Initialize()
        {
            _Logger.Info("[NTFSDRV2] Initializing NTFS file system on drive " + _rootPath);
            _ntfs = Ntfs.Create(new BlockDeviceStream(_device, _size));
        }

        public override void DisplayFileSystemInfo()
        {
        }

        public override List<DirectoryEntry> GetDirectoryListing(DirectoryEntry baseDirectory)
        {
            if (!(baseDirectory is NtfsDirectoryEntry ntfsEntry)) throw new Exception("ntfs: invalid dirlist request");
            if (!(ntfsEntry.NtfsEntry is NtfsDirectory ntfsDir)) throw new Exception("ntfs: dirlist: not a directory");
            var result = new List<DirectoryEntry>();
            foreach (var f in ntfsDir.ListFiles())
            {
                result.Add(new NtfsDirectoryEntry(this, null, baseDirectory.mFullPath + "\\" + f.Name, f.Name,
                    0,
                    f is NtfsFile ? DirectoryEntryTypeEnum.File : DirectoryEntryTypeEnum.Directory, f));
            }

            return result;
        }

        public override DirectoryEntry GetRootDirectory()
        {
            return new NtfsDirectoryEntry(this, null, "\\", _rootPath, _size, DirectoryEntryTypeEnum.Directory,
                _ntfs.GetRootDirectory());
        }

        public override DirectoryEntry CreateDirectory(DirectoryEntry aParentDirectory, string aNewDirectory)
        {
            return null;
        }

        public override DirectoryEntry CreateFile(DirectoryEntry aParentDirectory, string aNewFile)
        {
            return null;
        }

        public override void DeleteDirectory(DirectoryEntry aPath)
        {
        }

        public override void DeleteFile(DirectoryEntry aPath)
        {
        }

        public override void Format(string aDriveFormat, bool aQuick)
        {
        }

        public override long AvailableFreeSpace { get; }
        public override long TotalFreeSpace => _size;
        public override string Type => "NTFS";
        public override string Label { get; set; }
    }
}