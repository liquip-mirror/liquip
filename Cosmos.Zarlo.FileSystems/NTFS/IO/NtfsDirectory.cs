using System.Collections.Generic;
using Cosmos.Zarlo.FileSystems.NTFS.Model;
using Cosmos.Zarlo.FileSystems.NTFS.Model.Attributes;
using Cosmos.Zarlo.FileSystems.NTFS.Model.Enums;

namespace Cosmos.Zarlo.FileSystems.NTFS.IO
{
    public class NtfsDirectory : NtfsFileEntry
    {
        private const string DirlistAttribName = "$I30";

        internal NtfsDirectory(Ntfs ntfs, FileRecord record, AttributeFileName fileName)
            : base(ntfs, record, fileName)
        {
        }

        private void LoadIndex(List<IndexEntry> input, List<NtfsFileEntry> output)
        {
            foreach (var e in input)
            {
                var fileId = e.FileRefence.FileId;
                if (fileId <= 11) continue;
                var exists = false;
                foreach (var o in output)
                    if (o.MFTRecord.FileReference.FileId == e.FileRefence.FileId)
                    {
                        exists = true;
                        break;
                    }

                if (!exists)
                    output.Add(CreateEntry(ntfs, e.FileRefence.FileId));
            }
        }

        public List<NtfsFileEntry> ListFiles()
        {
            var result = new List<NtfsFileEntry>();
            var largeIndex = false;

            foreach (var att in MFTRecord.Attributes)
            {
                if (att is AttributeIndexRoot indexRoot && att.AttributeName == DirlistAttribName)
                {
                    LoadIndex(indexRoot.Entries, result);
                    largeIndex = (indexRoot.IndexFlags & MFTIndexRootFlags.LargeIndex) != 0;
                }
            }

            if (!largeIndex) return result;

            foreach (var att in MFTRecord.Attributes)
            {
                if (att is AttributeIndexAllocation alloc && att.AttributeName == DirlistAttribName)
                {
                    ntfs.ParseNonResidentAttribute(alloc);
                    LoadIndex(alloc.Entries, result);
                }
            }

            return result;
        }
    }
}