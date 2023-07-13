using Liquip.FileSystems.NTFS.Model;
using Liquip.FileSystems.NTFS.Model.Attributes;

namespace Liquip.FileSystems.NTFS.IO;

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
