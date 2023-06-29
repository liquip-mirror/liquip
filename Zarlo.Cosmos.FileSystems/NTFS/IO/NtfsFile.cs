using Zarlo.Cosmos.FileSystems.NTFS.Model;
using Zarlo.Cosmos.FileSystems.NTFS.Model.Attributes;

namespace Zarlo.Cosmos.FileSystems.NTFS.IO;

public class NtfsFile : NtfsFileEntry
{
    internal NtfsFile(Ntfs ntfs, FileRecord record, AttributeFileName fileName)
        : base(ntfs, record, fileName)
    {
    }

    public override string ToString()
    {
        return FileName.FileName;
    }
}
