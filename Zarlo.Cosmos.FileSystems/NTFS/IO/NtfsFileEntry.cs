using Zarlo.Cosmos.FileSystems.NTFS.Model;
using Zarlo.Cosmos.FileSystems.NTFS.Model.Attributes;
using Zarlo.Cosmos.FileSystems.NTFS.Model.Enums;
using Zarlo.Cosmos.FileSystems.NTFS.Utility;

namespace Zarlo.Cosmos.FileSystems.NTFS.IO;

public abstract class NtfsFileEntry
{
    private AttributeStandardInformation _standardInformation;

    internal AttributeFileName FileName;
    protected Ntfs ntfs;

    protected NtfsFileEntry(Ntfs ntfs, FileRecord record, AttributeFileName fileName)
    {
        this.ntfs = ntfs;
        MFTRecord = record;
        FileName = fileName;
        Init();
    }

    public FileRecord MFTRecord { get; }

    public DateTime TimeCreation =>
        _standardInformation == null ? DateTime.MinValue : _standardInformation.TimeCreated;

    public DateTime TimeModified =>
        _standardInformation == null ? DateTime.MinValue : _standardInformation.TimeModified;

    public DateTime TimeAccessed =>
        _standardInformation == null ? DateTime.MinValue : _standardInformation.TimeAccessed;

    public DateTime TimeMftModified =>
        _standardInformation == null ? DateTime.MinValue : _standardInformation.TimeMftModified;

    public string Name => FileName.FileName;

    public NtfsDirectory Parent => CreateEntry(FileName.ParentDirectory.FileId) as NtfsDirectory;

    private void Init()
    {
        foreach (var att in MFTRecord.Attributes)
        {
            if (att is AttributeStandardInformation info)
            {
                _standardInformation = info;
            }
        }
    }

    internal NtfsFileEntry CreateEntry(uint fileId, AttributeFileName fileName = null)
    {
        return CreateEntry(ntfs, fileId, fileName);
    }

    internal static NtfsFileEntry CreateEntry(Ntfs ntfs, uint fileId, AttributeFileName fileName = null)
    {
        var record = ntfs.ReadMftRecord(fileId);
        if (fileName == null)
        {
            fileName = NtfsUtils.GetPreferredDisplayName(record);
        }

        if ((record.Flags & FileEntryFlags.Directory) != 0)
        {
            return new NtfsDirectory(ntfs, record, fileName);
        }

        return new NtfsFile(ntfs, record, fileName);
    }
}
