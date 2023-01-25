using Cosmos.Zarlo.FileSystems.NTFS.Model;
using Cosmos.Zarlo.FileSystems.NTFS.Model.Attributes;

namespace Cosmos.Zarlo.FileSystems.NTFS.IO
{
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
}