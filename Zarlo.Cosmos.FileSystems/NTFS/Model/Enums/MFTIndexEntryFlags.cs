namespace Zarlo.Cosmos.FileSystems.NTFS.Model.Enums;

[Flags]
public enum MFTIndexEntryFlags : byte
{
    SubnodeEntry = 0x01,
    LastEntry = 0x02
}
